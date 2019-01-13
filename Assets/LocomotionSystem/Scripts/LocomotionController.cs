using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{

	[RequireComponent(typeof(Rigidbody), typeof(Animator))]
	public class LocomotionController : MonoBehaviour
	{
		const float VectorLerpSpeed = 6f;
		public const string JumpStateName = "JumpUp";
		const float LateJumpTime = 0.5f;
		const float JumpTransitionTime = 0.1f;
		const float LegMinDist = 0.6f;

		//List<Collider> contactsAboutLegs = new List<Collider>();

		Rigidbody body;
		Transform hips;
		Collider coll;
		public Transform trans { get; private set; }
		public LocomotionBehaviour locomotionBehaviour { get; private set; }

		[SerializeField] PhysicMaterial physics;
		[SerializeField] float angularDrag = 0.01f;
		[SerializeField] CollisionDetectionMode collisionMode;
		[SerializeField] float moveSpeed = 1.5f;
		[SerializeField] float turnSpeed = 2f;
		[SerializeField] float jumpVelocity = 5f;

		public Vector3 destinationVelocity { get; private set; }
		public float destinationAngularVelocity { get; private set; }

		protected virtual void Start ()
		{
			body = GetComponent<Rigidbody> ();
			coll = GetComponent<Collider> ();
			trans = transform;
			locomotionBehaviour = new LocomotionBehaviour (GetComponent<Animator> (), body, moveSpeed, turnSpeed);
			hips = locomotionBehaviour.animator.GetBoneTransform (HumanBodyBones.Hips);
			InitBody (false);
		}

		protected virtual void InitBody (bool ragdoll)
		{
			coll.enabled = !ragdoll;
			Collider[] colls = GetComponentsInChildren<Collider> ();
			foreach (var c in colls) {
				c.material = physics;
				Physics.IgnoreCollision (c, coll);
				Rigidbody r = c.GetComponent<Rigidbody> ();
				if (r && r != body) {
					r.angularDrag = angularDrag;
					r.collisionDetectionMode = collisionMode;
					r.isKinematic = !ragdoll;
					r.velocity = body.velocity;
				}
			}
			body.isKinematic = ragdoll;
			if (!ragdoll) {
				trans.position = hips.position;
			}
			locomotionBehaviour.animator.enabled = !ragdoll;
		}

		protected virtual void Update ()
		{
			locomotionBehaviour.AnimatorParametersUpdate ();
		}

		protected virtual void FixedUpdate ()
		{
			locomotionBehaviour.FixedUpdate ();
			//locomotionBehaviour.onGround = contactsAboutLegs.Count > 0;
			if (!OnGround()) {
				destinationVelocity = body.velocity;
				destinationAngularVelocity = 0f;
			}
			Quaternion next = Quaternion.LookRotation (Vector3.ProjectOnPlane (trans.forward, Vector3.up));
			trans.rotation = Quaternion.Slerp (trans.rotation, next, Time.deltaTime * 4f);
			body.SetMoveVelocity (destinationVelocity);
			//body.angularVelocity = Vector3.up * destinationAngularVelocity;// / (1f + (destinationVelocity.magnitude / moveSpeed) * 0.5f);
			trans.eulerAngles += Vector3.up * destinationAngularVelocity;
		}

		public bool OnGround ()
		{
			return locomotionBehaviour.onGround && !IsInvoking("LateJump");
		}

		protected virtual void OnAnimatorIK ()
		{
			locomotionBehaviour.AnimatorIK ();
		}

		public void Jump ()
		{
			if (OnGround()) {
				Invoke ("LateJump", LateJumpTime);
				body.angularDrag = 1f;
				locomotionBehaviour.PlayState (JumpStateName, JumpTransitionTime);
			}
		}
		void LateJump ()
		{
			body.angularDrag = 0.05f;
			body.AddForce (locomotionBehaviour.GetGroundNormal() * jumpVelocity, ForceMode.VelocityChange);
		}

		protected virtual void Move (Vector3 input)
		{
			float t = VectorLerpSpeed * Time.fixedDeltaTime;
			Vector3 dv = Vector3.ProjectOnPlane (trans.forward * input.z * moveSpeed, locomotionBehaviour.GetGroundNormal());
			destinationVelocity = Vector3.Slerp (destinationVelocity, dv, t);
			destinationAngularVelocity = Mathf.Lerp (destinationAngularVelocity, input.x * turnSpeed, t * 0.5f);
			Debug.DrawRay (trans.position, destinationVelocity.normalized, Color.red);
		}
		public virtual void MoveAt (Vector3 direction)
		{
			float angle = direction.magnitude > 0f ? Mathf.DeltaAngle (trans.eulerAngles.y, Quaternion.LookRotation (direction).eulerAngles.y) / 45f : 0f;
			Vector3 dir = trans.InverseTransformDirection (direction);
			float move = dir.z > 0 ? dir.z : 0;
			Move (new Vector3 (angle, 0, move));
		}
		/*protected virtual void OnTriggerStay (Collider coll)
		{
			if (!contactsAboutLegs.Contains(coll) && coll.attachedRigidbody != body) {
				contactsAboutLegs.Add (coll);
			}
		}
		protected virtual void OnTriggerExit (Collider coll)
		{
			if (contactsAboutLegs.Contains(coll)) {
				contactsAboutLegs.Remove (coll);
			}
		}*/
	}
	
}