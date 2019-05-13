using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class MobileInput : MonoBehaviour
{
	private static MoveJoystick moveJoystick;
	private static CameraInput cameraInput;

	protected void Start ()
	{
		moveJoystick = FindObjectOfType<MoveJoystick> ();
		cameraInput = FindObjectOfType<CameraInput> ();
	}

	public static Vector3 GetMoveInput ()
	{
		if (!moveJoystick)
		{
			return Vector3.zero;
		}
		Vector3 input = moveJoystick.GetInput ();
		input.z = input.y;
		input.y = 0f;
		return input;
	}
	public static Vector3 GetCameraInput ()
	{
		if (!cameraInput)
		{
			return Vector3.zero;
		}
		Vector3 delta = cameraInput.GetInput ();
		float x = delta.x;
		delta.x = -delta.y;
		delta.y = x;
		return delta;
	}
}

