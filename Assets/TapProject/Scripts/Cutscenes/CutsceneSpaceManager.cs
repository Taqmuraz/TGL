using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CutsceneSpaceManager : MonoBehaviour
{
	[SerializeField] string sceneToLoad = "None";

	protected virtual void Start ()
	{
		CutscenesManager.LoadScene (sceneToLoad);
	}
}
