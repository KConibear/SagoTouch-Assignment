namespace SagoTouchAssignment.TouchArea {

	using SagoTouch;
	using UnityEngine;
	using UnityEngine.UI;
	using Touch = SagoTouch.Touch;

	public class MyTouchArea : MonoBehaviour {


		#region Fields

		[System.NonSerialized]
		private TouchArea m_TouchArea;

		[System.NonSerialized]
		private TouchAreaObserver m_TouchAreaObserver;

		#endregion

		#region  SerializeFields

		[SerializeField]
		private Toggle m_TouchAreaToggle = null;

		#endregion


		#region Properties

		public TouchArea TouchArea {
			get { return m_TouchArea = m_TouchArea ?? GetComponent<TouchArea>(); }
		}

		public TouchAreaObserver TouchAreaObserver {
			get { return m_TouchAreaObserver = m_TouchAreaObserver ?? GetComponent<TouchAreaObserver>(); }
		}

		#endregion


		#region Methods

		public void ToggleTouchArea() {
			this.TouchArea.enabled = this.m_TouchAreaToggle.isOn;
			Debug.Log($"MyTouchArea::ToggleTouchArea:this.TouchArea.enabled<{this.TouchArea.enabled}>");
		}

		private void OnEnable() {
			this.TouchAreaObserver.TouchUpDelegate = OnTouchUp;
		}

		private void OnDisable() {
			this.TouchAreaObserver.TouchUpDelegate = null;
		}

		public void OnTouchUp(TouchArea touchArea, Touch touch) {
			Debug.Log("Touch Up", this);
		}

		private void OnReactTouchDown(TouchArea touchArea, Touch touch) {
			Debug.Log("Touch Down", this);
		}

		#endregion

	}

}