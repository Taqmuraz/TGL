using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraInput : DragSystem
{
	Vector3 dragInput;
	[SerializeField] float sensetify = 0.25f;

	protected override void Drag (Vector3 delta)
	{
		dragInput = delta;
	}
	public override void OnDrag (PointerEventData data)
	{
		Drag (data.delta);
	}

	public override Vector3 GetInput ()
	{
		if (!isDrag)
		{
			return Vector3.zero;
		}
		Vector3 temp = dragInput * sensetify;
		dragInput = Vector3.zero;
		return temp;
	}
}
