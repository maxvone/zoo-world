# Zoo World (Unity)

## Overview

Key concepts:
- State-based game initialization and loop (bootstrap/load/play) using a `GameStateMachine`
- Core services in a central DI container (`AllServices`)
- Asynchronous asset loading/instantiate via Addressables and `UniTask`
- Object pooling for animals and prefab lifecycle efficiency
- Movement strategy pattern for animal behavior

## Project Layout

- `Assets/CodeBase/Infrastructure`
  - `GameBootstrapper` - MonoBehaviour entry point (Awake and `DontDestroyOnLoad`)
  - `Game` - holds `GameStateMachine`
  - `GameStateMachine` - state transition manager
  - `States` - `BootstrapState`, `LoadLevelState`, `GameLoopState`
  - `Factory` - `GameFactory`, `AnimalsPool`, `IGameFactory`
  - `Services` - runtime services and service registration

- `Assets/CodeBase/AssetManagement`
  - `IAssetProvider`, `AssetProvider` - Addressables wrapper + cache
  - `AssetAddress` - address string constants

- `Assets/CodeBase/Animals`
  - `AnimalBase` - shared behavior, collision/feeding/death
  - `AnimalType` (Prey/Predator)
  - `MovementStrategies` - `IMovementStrategy`, `LinearMovementStrategy`, `JumpMovementStrategy`

- `Assets/CodeBase/UI`
  - `IUiFactory`, `UiFactory`, `Hud` - UI root and HUD creation and updates

- `Assets/CodeBase/Services`
  - Scene loading service, spawn service, bounds helper, death resolution

- `Assets/Plugins/UniTask` (third-party runtime package) - async task and reactive patterns.

## Runtime Flow

1. `GameBootstrapper.Awake()` constructs `Game` and enters `BootstrapState`, showing loading UI.
2. `BootstrapState`: registers all services with `AllServices` and configures provider/scene/asset/UI/game factories.
3. Transition to `LoadLevelState`.
4. `LoadLevelState`:
   - asynchronously loads the level scene via `SceneLoaderService`
   - warms up asset cache in `GameFactory` (level + animal prefabs)
   - creates UI root and HUD via `UiFactory`
   - creates level object and turns off loader UI
   - enters `GameLoopState`
5. `GameLoopState`: starts animal spawn loop using `AnimalsSpawnerService`.
6. Animals are created with `GameFactory.CreateAnimal<T>()` via `AnimalsPool`.
   - `AnimalBase.Construct` receives services (bounds/death/pools) and movement strategy wiring.
7. Animal movement behavior runs in `FixedUpdate` with chosen `IMovementStrategy`; collisions call `DeathResolverService`.
8. UI is updated via `Hud` counters.

## Main Design Patterns

### 1) State Machine Pattern
- `IGameStateMachine` manages state transitions (`Enter<...>()`)
- Uses interface hierarchy: `IState`, `IPayloadedState<T>`, `IExitableState`.
- Each state encapsulates init/cleanup and transition rules.

### 3) Factory Pattern
- `GameFactory` creates levels and animals.
- `UiFactory` creates UI root and HUD.
- `AnimalsPool` builds object pools on prefab registration.

### 4) Object Pool Pattern
- `AnimalsPool` uses `UnityEngine.Pool.ObjectPool<AnimalBase>`.
- Reuses animal objects
- `AnimalBase.Die()` returns to pool.

### 5) Strategy Pattern
- `IMovementStrategy` interface with concrete behaviors `LinearMovementStrategy`, `JumpMovementStrategy`.

### 6) Dependency Injection
- Services are wired through constructors (e.g., `LoadLevelState`, `AnimalsSpawnerService`, `DeathResolverService`).

### 8) Component-based Unity Model
- `AnimalBase`, `Hud`, movement strategy components keep behavior modifiable per prefab.

## Key Classes/Interfaces

- `GameStateMachine` (State manager)
- `BootstrapState`, `LoadLevelState`, `GameLoopState`
- `AllServices` (service registry)
- `GameFactory`, `AnimalsPool` (Factory + Object Pool)
- `AssetProvider` (Addressables caching)
- `AnimalBase` (base entity, collision/death/feeding)
- `IMovementStrategy`, `LinearMovementStrategy`, `JumpMovementStrategy`
- `DeathResolverService`, `BoundsReturnService`, `AnimalsSpawnerService`
- `SceneLoaderService`, `UiFactory`, `Hud`

## Next improvements (suggestions)

- Replace `AllServices` with explicit dependency injection container if scale grows.
- Add error handling/retry for Addressables operations.
- Add configuration file for bounds, spawn frequency, and scene names.
- Add cancellation support for the infinite spawn loop.
