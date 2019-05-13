using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public abstract class DragSystem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	protected bool isDrag { get; private set; }

	protected virtual void Start ()
	{
		DragActive (false);
	}

	protected virtual void Drag (Vector3 point)
	{
	}

	protected virtual void StartDrag (Vector3 point)
	{
		DragActive (true);
	}
	protected virtual void EndDrag ()
	{
		DragActive (false);
	}
	protected virtual void DragActive (bool active)
	{
		isDrag = active;
	}

	public virtual void OnBeginDrag (PointerEventData data)
	{
		StartDrag (data.position);
	}
	public virtual void OnDrag (PointerEventData data)
	{
		Drag (data.position);
	}
	public virtual void OnEndDrag (PointerEventData data)
	{
		EndDrag ();
	}

	public virtual Vector3 GetInput ()
	{
		return Vector3.zero;
	}
}
