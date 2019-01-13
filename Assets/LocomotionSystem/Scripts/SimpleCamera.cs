using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	public class SimpleCamera : MonoBehaviour
	{
		[SerializeField] Transform target;
		[SerializeField] float dist = 3f;
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
			return new Vector3 (-Input.GetAxis ("Mouse Y"), Input.GetAxis ("Mouse X"), 0f);
		}

		protected virtual void OnPreRender ()
		{
			euler += CameraInput ();
			euler.x = Mathf.Clamp (euler.x, -angleMax, angleMax);
			trans.eulerAngles = euler;
			float d = dist;
			Ray ray = new Ray (target.position, -trans.forward);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, d)) {
				d = hit.distance;
			}
			trans.position = target.position - trans.forward * d;
		}
	}
}