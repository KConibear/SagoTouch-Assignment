namespace SagoTouchAssignment.TouchFeature {

	using SagoTouch;
	using UnityEngine;
	using Touch = SagoTouch.Touch;

	[RequireComponent(typeof(MeshRenderer))]
	public class MyTouchObserver : MonoBehaviour, ISingleTouchObserver {

		#region Fields

		[System.NonSerialized]
		private Camera m_Camera;

		[System.NonSerialized]
		private Renderer m_Renderer;

		[System.NonSerialized]

		#endregion


		#region Properties

		private Transform m_Transform;

		public Camera Camera {
			get { return m_Camera = m_Camera ?? CameraUtils.FindRootCamera(this.Transform); }
		}

		public Renderer Renderer {
			get { return m_Renderer = m_Renderer ?? GetComponent<Renderer>(); }
		}

		public Transform Transform {
			get { return m_Transform = m_Transform ?? GetComponent<Transform>(); }
		}

		#endregion


		#region ISingleTouchObserver

		/// <summary>
		/// Invoked when a users finger touches the devices screen
		/// </summary>
		/// <param name="touch">The touch object</param>
		/// <returns>True: TouchDispatcher receives events | False: TouchDispatcher DOES NOT receive events</returns>
		public bool OnTouchBegan(Touch touch) {
			bool hasHit = HitTest(touch);
			Debug.Log($"MyTouchObserver::OnTouchBegan:hasHit<{hasHit}>");
			return hasHit;
		}

		public void OnTouchMoved(Touch touch) {
			// ...
		}

		public void OnTouchEnded(Touch touch) {
			Debug.Log("MyTouchObserver::OnTouchEnded");
		}

		public void OnTouchCancelled(Touch touch) {
			Debug.Log("MyTouchObserver::OnTouchCancelled");
		}

		#endregion


		#region Class Methods

		private bool HitTest(Touch touch) {
			var bounds = Renderer.bounds;
			bounds.extents += Vector3.forward;
			return bounds.Contains(CameraUtils.TouchToWorldPoint(touch, this.transform, this.Camera));
		}

		#endregion


		#region MonoBehaviour Methods

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
	}

	#endregion
}