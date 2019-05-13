using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	public struct Orientation
	{
		public Vector3 position;
		public Quaternion rotation;

		public Orientation (Vector3 position, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}
		public Orientation (Transform transform)
		{
			this.position = transform.position;
			this.rotation = transform.rotation;
		}

		public static Orientation Slerp (Orientation a, Orientation b, float t)
		{
			Vector3 v = Vector3.Slerp (a.position, b.position, t);
			Quaternion q = Quaternion.Slerp (a.rotation, b.rotation, t);
			return new Orientation (v, q);
		}
	}
	
}