using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	public class LocomotionOnGround : LocomotionParameter
	{
		LocomotionBehaviour behaviour;

		public LocomotionOnGround (LocomotionBehaviour _behaviour) : base("OnGround")
		{
			behaviour = _behaviour;
		}

		public override object GetValue ()
		{
			for (int i = 0; i < behaviour.animator.layerCount; i++) {
				AnimatorStateInfo info = behaviour.animator.GetCurrentAnimatorStateInfo (i);
				if (info.IsName(LocomotionController.JumpStateName)) {
					return false;
				}
			}
			return behaviour.onGround;
		}
	}
	
}