using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class Cutscene : NullBool
{
	public const int LoadPriority = 15;

	protected static readonly Cutscene[] allCutscenes = 
	{
		new CinematicCutscene ("Cutscene_0")	
	};

	public const string CutscenesPath = "TapProject/Scenes/Cutscenes/";

	public readonly string cutsceneAssetName;

	public Cutscene (string cutsceneAssetName)
	{
		this.cutsceneAssetName = cutsceneAssetName;
		Initialize ();
	}

	public static Cutscene GetByName (string name)
	{
		return allCutscenes.FirstOrDefault ((cs) => cs.cutsceneAssetName.Equals(name));
	}

	protected virtual int GetSceneIndex ()
	{
		return SceneUtility.GetBuildIndexByScenePath (CutscenesPath + cutsceneAssetName);
	}
	public virtual void LoadInGame ()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync (GetSceneIndex());
		operation.priority = LoadPriority;
	}
	protected virtual void Initialize ()
	{
		
	}
	public virtual void Update ()
	{
	}
}