using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class MoveJoystick : DragSystem
{
	[SerializeField] GameObject stickObj;
	[SerializeField] RectTransform stickRoot;
	[SerializeField] RectTransform stick;
	[SerializeField] float maxDist = 50f;

	protected override void Drag (Vector3 point)
	{
		Vector3 delta = point - stickRoot.position;
		delta = delta.magnitude > maxDist ? delta.normalized * maxDist : delta;
		stick.position = stickRoot.position + delta;
	}

	protected override void StartDrag (Vector3 point)
	{
		stickRoot.position = point;
		stick.localPosition = Vector3.zero;
		base.StartDrag (point);
	}
	protected override void EndDrag ()
	{
		DragActive (false);
	}

	protected override void DragActive (bool active)
	{
		base.DragActive (active);
		stickObj.SetActive (active);
	}

	public override Vector3 GetInput ()
	{
		if (!isDrag)
		{
			return Vector3.zero;
		}
		return (stick.position - stickRoot.position) / maxDist;
	}
}
