using UnityEngine;
using System.Collections;

namespace LocomotionSystem
{
	public class Weapon : MonoBehaviour, IHoldable
	{
		[SerializeField] float maxAimAngle = 89f;
		[SerializeField] Vector3 localOffset;
		[SerializeField] Vector3 selfOffset;
		[SerializeField] Transform leftHand;
		[SerializeField] Transform rightHand;
		[SerializeField] float weaponDist = 1f;

		public Vector3 aimDirection { get; private set; }
		public IHolder currentHolder { get; private set; }
		protected float posSin { get; private set; }

		Transform trans;

		protected virtual void Start ()
		{
			trans = transform;
		}

		public void SetAimDirection (Vector3 dir)
		{
			aimDirection = dir.normalized;
		}

		public Orientation GetLeftHand (IHolder holder)
		{
			return holder.GetLeftHandDefault ();
		}
		public bool InAimAngle (Vector3 dir)
		{
			return Vector3.Angle (dir, aimDirection) < maxAimAngle;
		}
		public Orientation GetRightHand (IHolder holder)
		{
			Transform hTrans = holder.GetTransform ();

			return new Orientation(hTrans.TransformPoint (localOffset) + trans.TransformDirection(selfOffset) + aimDirection * weaponDist, Quaternion.LookRotation(aimDirection));
		}
		public void OnAdd (IHolder holder)
		{
			currentHolder = holder;
			Start ();
			trans.SetParent (holder.GetRightHandTransform());
			trans.localPosition = Vector3.zero;
		}
		public void OnRemove (IHolder holder)
		{
			currentHolder = null;
			trans.SetParent (null);
		}
		protected virtual void FixedUpdate ()
		{
			UpdateTransform ();
		}
		protected virtual void UpdateTransform ()
		{
			if (currentHolder == null)
			{
				return;
			}
			Vector3 hFwd = currentHolder.GetTransform ().forward;

			float nextSin = 0f;

			if (InAimAngle (hFwd)) {
				Transform parent = trans.parent;
				trans.rotation = Quaternion.LookRotation(parent.up, parent.right);
				nextSin = 1f;
			}

			UpdateSin (nextSin);
		}
		public void UpdateSin (float to)
		{
			posSin = Mathf.Lerp (posSin, to, Time.fixedDeltaTime * 3f);
		}
		public float GetPosSin (IHolder holder)
		{
			return posSin;
		}
	}
}
