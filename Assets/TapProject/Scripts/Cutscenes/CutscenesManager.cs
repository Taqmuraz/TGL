using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CutscenesManager : MonoBehaviour
{
	private void Awake ()
	{
		DontDestroyOnLoad (gameObject);
	}

	private static Cutscene activeCutscene;

	public static void LoadScene (string name)
	{
		activeCutscene = Cutscene.GetByName (name);
		if (activeCutscene) {
			activeCutscene.LoadInGame ();
		} else {
			Debug.Log ("There are no scene with name \"" + name + '\"');
		}
	}

	private void Update ()
	{
		if (activeCutscene)
		{
			activeCutscene.Update ();
		}
	}
}
