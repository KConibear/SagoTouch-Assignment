namespace SagoTouchAssignment.TouchController {

	using UnityEngine;
	using UnityEngine.UI;
	using SagoTouch;

	public class MyTouchController : MonoBehaviour {

		[SerializeField] private Toggle m_AllTouchToggle = null;
		public void ToggleAllTouches() {
			if (TouchDispatcher.Instance) {
				TouchDispatcher.Instance.enabled = this.m_AllTouchToggle.isOn;
				Debug.Log($"MyTouchController::ToggleAllTouches:TouchDispatcher.Instance.enabled<{TouchDispatcher.Instance.enabled}>");
			}
		}
	}
}