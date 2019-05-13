using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	public class SimpleCamera : MonoBehaviour
	{
		[SerializeField] Transform target;
		[SerializeField] Vector3 offset = new Vector3(0.5f, 0.5f, -1f);
		[SerializeField] float angleMax = 80f;

		Vector3 euler;
		Transform trans;

		void Start ()
		{
			trans = transform;
			euler = trans.eulerAngles;
		}

		protected virtual Vector3 CameraInput ()
		{
			Vector3 input = MobileInput.GetCameraInput ();
			//input = input.magnitude < float.Epsilon ? new Vector3 (-Input.GetAxis ("Mouse Y"), Input.GetAxis ("Mouse X"), 0f) : input;
			return input;
		}

		protected virtual void OnPreRender ()
		{
			euler += CameraInput ();
			euler.x = Mathf.Clamp (euler.x, -angleMax, angleMax);
			trans.eulerAngles = euler;
			float d = offset.magnitude;
			Vector3 dir = trans.TransformDirection (offset).normalized;
			Ray ray = new Ray (target.position, dir);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, d)) {
				d = hit.distance;
			}
			Vector3 delta = dir * d;
			trans.position = target.position + delta;
		}
	}
}