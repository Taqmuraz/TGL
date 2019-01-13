using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	public class AnimatorBehaviour
	{
		public Animator animator { get; private set; }

		public AnimatorBehaviour (Animator _animator)
		{
			animator = _animator;
		}

		public void SetValue (string name, object value)
		{
			float dampTime = 0.25f;
			float deltaTime = Time.deltaTime;
			if (value is float) {
				animator.SetFloat (name, (float)value, dampTime, deltaTime);
			} else if (value is int) {
				animator.SetInteger (name, (int)value);
			} else if (value is bool) {
				animator.SetBool (name, (bool)value);
			} else {
				Debug.Log ("Parameter type out of resolved types");
			}
		}

		public void PlayState (string name, float transition)
		{
			animator.CrossFade (name, transition);
		}

		public virtual void AnimatorIK ()
		{
		}
		public virtual void AnimatorParametersUpdate ()
		{
		}
		protected void SetIKWeights (float weight)
		{
			animator.SetIKPositionWeight (AvatarIKGoal.LeftFoot, weight);
			animator.SetIKPositionWeight (AvatarIKGoal.RightFoot, weight);
			animator.SetIKPositionWeight (AvatarIKGoal.LeftHand, weight);
			animator.SetIKPositionWeight (AvatarIKGoal.RightHand, weight);

			animator.SetIKRotationWeight (AvatarIKGoal.LeftFoot, weight);
			animator.SetIKRotationWeight (AvatarIKGoal.RightFoot, weight);
			animator.SetIKRotationWeight (AvatarIKGoal.LeftHand, weight);
			animator.SetIKRotationWeight (AvatarIKGoal.RightHand, weight);
		}
	}
}