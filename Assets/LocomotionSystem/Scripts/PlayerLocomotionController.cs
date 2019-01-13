using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	public class PlayerLocomotionController : LocomotionController
	{
		Transform mainCamera;

		protected override void Start ()
		{
			mainCamera = Camera.main.transform;
			base.Start ();
		}

		protected override void Update ()
		{
			if (Input.GetButtonDown("Jump")) {
				Jump ();
			}

			Vector3 move = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			move = move.magnitude > 1 ? move.normalized : move;
			move = Input.GetKey (KeyCode.LeftShift) ? move * 2f : move;

			Vector3 dir = Vector3.ProjectOnPlane (mainCamera.TransformDirection (move), Vector3.up);

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