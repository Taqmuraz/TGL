using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{

	public class LocomotionMoveAxisZ : LocomotionMoveAxis
	{
		public LocomotionMoveAxisZ (Rigidbody _body, float _moveSpeed) : base (_body, _moveSpeed, "MoveZ")
		{
		}
		public override object GetValue ()
		{
			return Mathf.Clamp(body.velocity.magnitude, 0, float.MaxValue) / moveSpeed;
		}
	}
	
}