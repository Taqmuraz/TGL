using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	public static class Extentions
	{
		public static void SetMoveVelocity (this Rigidbody body, Vector3 velocity)
		{
			body.velocity = new Vector3 (velocity.x, body.velocity.y, velocity.z);
		}
		public static int DefaultMask ()
		{
			return LayerMask.GetMask ("Default");
		}
		public static AvatarIKGoal Negative (this AvatarIKGoal origin)
		{
			switch (origin) {
			default :
				return AvatarIKGoal.LeftFoot;
			case AvatarIKGoal.LeftFoot:
				return AvatarIKGoal.RightFoot;
			case AvatarIKGoal.LeftHand:
				return AvatarIKGoal.RightHand;
			case AvatarIKGoal.RightHand:
				return AvatarIKGoal.LeftHand;
			}
		}
	}
}