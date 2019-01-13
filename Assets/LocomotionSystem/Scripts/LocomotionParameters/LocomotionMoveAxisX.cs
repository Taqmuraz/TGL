using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	
	public class LocomotionMoveAxisX : LocomotionMoveAxis
	{
		
		public LocomotionMoveAxisX (Rigidbody _body, float _moveSpeed) : base (_body, _moveSpeed, "MoveX")
		{
			
		}
		public override object GetValue ()
		{
			return controller.destinationAngularVelocity / moveSpeed;
		}
	}
	
}