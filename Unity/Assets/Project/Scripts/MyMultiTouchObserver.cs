namespace SagoTouchAssignment.MultiTouch {

	using SagoTouch;
	using System.Collections.Generic;
	using UnityEngine;
	using Touch = SagoTouch.Touch;

	public class MyMultiTouchObserver : MonoBehaviour, ISingleTouchObserver {


		#region Fields

		[System.NonSerialized]
		private Camera m_Camera;

		[System.NonSerialized]
		private Renderer m_Renderer;

		[System.NonSerialized]
		private List<Touch> m_Touches;

		[System.NonSerialized]
		private Transform m_Transform;

		#endregion


		#region Properties

		public Camera Camera {
			get { return m_Camera = m_Camera ?? CameraUtils.FindRootCamera(this.Transform); }
		}

		public Renderer Renderer {
			get { return m_Renderer = m_Renderer ?? GetComponent<Renderer>(); }
		}

		public List<Touch> Touches {
			get { return m_Touches = m_Touches ?? new List<Touch>(); }
		}

		public Transform Transform {
			get { return m_Transform = m_Transform ?? GetComponent<Transform>(); }
		}

		#endregion


		#region Methods

		private bool HitTest(Touch touch) {
			var bounds = this.Renderer.bounds;
			bounds.extents += Vector3.forward;
			return bounds.Contains(CameraUtils.TouchToWorldPoint(touch, this.Transform, this.Camera));
		}

		private void OnEnable() {
			if (TouchDispatcher.Instance) {
				TouchDispatcher.Instance.Add(this, 0, true);
			}
		}

		private void OnDisable() {
			if (TouchDispatcher.Instance) {
				TouchDispatcher.Instance.Remove(this);
			}
		}

		#endregion


		#region ISingleTouchObserver

		public bool OnTouchBegan(Touch touch) {
			if (HitTest(touch)) {
				this.Touches.Add(touch);
				Debug.Log($"+++ MyMultiTouchObserver::OnTouchBegan:this.Touches.Count<{this.Touches.Count}> +++");
				return true;
			}
			return false;
		}

		public void OnTouchMoved(Touch touch) {
			// ...
		}

		public void OnTouchEnded(Touch touch) {
			this.Touches.Remove(touch);
			Debug.Log($"--- MyMultiTouchObserver::OnTouchEnded:this.Touches.Count<{this.Touches.Count}> ---");
		}

		public void OnTouchCancelled(Touch touch) {
			Debug.Log("MyMultiTouchObserver::OnTouchCancelled");
			OnTouchEnded(touch);
		}

		#endregion


	}
}