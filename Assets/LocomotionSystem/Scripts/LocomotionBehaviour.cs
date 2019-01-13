using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{

	public class LocomotionBehaviour : AnimatorBehaviour
	{
		protected List<LocomotionParameter> parameters { get; private set; }
		protected Rigidbody rigidbody { get; private set; }
		protected LocomotionController controller { get; private set; }
		protected Transform trans { get; private set; }
		protected float moveSpeed { get; private set; }
		protected float turnSpeed { get; private set; }
		public bool onGround { get; set; }

		Vector3 curLeft;
		Vector3 curRight;
		float moving;
		Vector3 groundNormal;

		Vector3 curLeftLeg;
		Vector3 curRightLeg;

		public LocomotionBehaviour (Animator _animator, Rigidbody body, float _moveSpeed, float _turnSpeed) : base (_animator)
		{
			moveSpeed = _moveSpeed;
			turnSpeed = _turnSpeed;
			rigidbody = body;
			controller = body.GetComponent<LocomotionController> ();
			trans = body.transform;
			parameters = new List<LocomotionParameter> ();
			InitializeParameters ();
		}
		protected virtual void InitializeParameters ()
		{
			parameters.Add (new LocomotionMoveAxisX(rigidbody, turnSpeed));
			parameters.Add (new LocomotionMoveAxisZ(rigidbody, moveSpeed));
			parameters.Add (new LocomotionOnGround (this));
		}
		public override void AnimatorIK ()
		{
			SetIKWeights (1f);
			SetLeg (AvatarIKGoal.LeftFoot);
			SetLeg (AvatarIKGoal.RightFoot);
		}
		protected void SetLeg (AvatarIKGoal goal)
		{
			Vector3 current = animator.GetIKPosition (goal);
			Vector3 currentFlat = Vector3.ProjectOnPlane (current - trans.position, groundNormal);
			Vector3 need = currentFlat + trans.position + groundNormal * trans.InverseTransformPoint (current).y;

			Debug.DrawLine (current, need, Color.magenta);
			Debug.DrawRay (current, groundNormal, Color.cyan);

			animator.SetIKPosition (goal, need);

			Quaternion rotation = Quaternion.FromToRotation (Vector3.up, trans.InverseTransformDirection(groundNormal));
			animator.SetIKRotation (goal, animator.GetIKRotation(goal) * rotation);
		}
		public Vector3 GetGroundNormal ()
		{
			return groundNormal;
		}
		void CheckGroundNormal ()
		{
			Vector3 norm = Vector3.up;
			RaycastHit hit;
			if (Physics.Raycast(trans.position + trans.up, -trans.up, out hit, 2f, Extentions.DefaultMask())) {
				norm = hit.normal;
			}
			groundNormal = Vector3.Slerp(groundNormal, norm, Time.fixedDeltaTime * 4f);
		}
		protected float IsMoving ()
		{
			if (!onGround) {
				return 1f;
			}
			return moving;
		}
		public virtual void FixedUpdate ()
		{
			CheckGroundNormal ();
			moving = Mathf.Lerp(moving, Vector3.ProjectOnPlane (rigidbody.velocity, Vector2.up).magnitude / moveSpeed + controller.destinationAngularVelocity, Time.fixedDeltaTime * 4f);
			onGround = Physics.OverlapSphere (trans.position, 0.5f, Extentions.DefaultMask()).FirstOrDefault ();
		}
		public override void AnimatorParametersUpdate ()
		{
			foreach (LocomotionParameter p in parameters) {
				object value = p.GetValue ();
				SetValue (p.name, value);
			}
		}
	}
	
}