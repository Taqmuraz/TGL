using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
	[SerializeField] Text text;

	protected virtual void Awake ()
	{
		DontDestroyOnLoad (gameObject);
	}

	protected virtual void Update ()
	{
		text.text = Time.time.ToString();
	}
}

