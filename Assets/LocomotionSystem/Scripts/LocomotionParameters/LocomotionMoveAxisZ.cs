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
			if (Vector3.ProjectOnPlane(controller.destinationVelocity, Vector3.up).magnitude < Mathf.Abs(controller.destinationVelocity.y))
			{
				return 0f;
			}
			return Mathf.Clamp(controller.destinationVelocity.magnitude, 0, float.MaxValue) / moveSpeed;
		}
	}
	
}