using TMPro;
using UnityEngine;

namespace CodeBase.UI.Services.Overlays
{
	public class Hud : MonoBehaviour
	{
		[SerializeField] private TMP_Text _deadPreysCounter;
		[SerializeField] private TMP_Text _deadPredatorsCounter;

        public void UpdateDeadPredatorsCounter(int deadPredatorsCounter) =>
			_deadPredatorsCounter.text = deadPredatorsCounter.ToString();

        public void UpdateDeadPreysCounter(int deadPreysCounter) =>
			_deadPreysCounter.text = deadPreysCounter.ToString();
    }
}