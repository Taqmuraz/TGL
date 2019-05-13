using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	public class PlayerLocomotionController : LocomotionController
	{
		Transform mainCamera;

		public static PlayerLocomotionController playerController { get; private set; }

		protected override void Start ()
		{
			mainCamera = Camera.main.transform;
			playerController = this;
			base.Start ();
			TestAddWeapon ();
		}

		protected void TestAddWeapon ()
		{
			GameObject prefab = Resources.Load<GameObject> ("Prefabs/Weapon/Test");
			Weapon w = Instantiate (prefab).GetComponent<Weapon> ();
			locomotionBehaviour.SetHoldable (w);
		}

		protected override void UpdateWeapon (Weapon weapon)
		{
			weapon.SetAimDirection(mainCamera.forward);
		}

		protected Vector3 FromCamera (Vector3 origin)
		{
			Vector3 f = mainCamera.forward;
			Vector3 r = mainCamera.right;
			f = Vector3.ProjectOnPlane (f, Vector3.up).normalized;
			r = Vector3.ProjectOnPlane (r, Vector3.up).normalized;
			return f * origin.z + r * origin.x;
		}

		protected virtual Vector3 GetMoveInput ()
		{
			Vector3 mobile = MobileInput.GetMoveInput ();
			mobile = mobile.magnitude < float.Epsilon ? new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical")) : mobile;
			return mobile;
		}

		protected override void Update ()
		{
			if (Input.GetButtonDown("Jump")) {
				Jump ();
			}

			Debug.Log (locomotionBehaviour.onGround.ToString());

			Vector3 move = GetMoveInput ();
			move = move.magnitude > 1 ? move.normalized : move;
			move = Input.GetKey (KeyCode.LeftShift) ? move * 2f : move;

			Vector3 dir = Vector3.ProjectOnPlane (FromCamera (move), Vector3.up);

			MoveAt (dir);

			base.Update ();

			if (Input.GetKeyDown(KeyCode.RightShift)) {
				InitBody (true);
			}
			if (Input.GetKeyDown(KeyCode.RightControl)) {
				InitBody (false);
			}
		}
		protected virtual void OnCollisionEnter (Collision other)
		{
			if (other.relativeVelocity.magnitude > 10f) {
				//InitBody (true);
			}
		}
	}
}