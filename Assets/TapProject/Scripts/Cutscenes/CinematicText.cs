using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class CinematicText : NullBool
{
	public const string CinematicTextPrefabPath = "Prefabs/Cutscenes/CinematicText";
	public const string CinematicTextAssetPath = "Texts/CinematicText";

	public readonly string text;
	public readonly float lifeTime;
	public readonly float startTime;

	public CinematicText (string text, float startTime, float lifeTime)
	{
		this.text = text;
		this.startTime = startTime;
		this.lifeTime = lifeTime;
	}

	public static CinematicText ParseFromAsset (TextAsset asset)
	{
		ParsedObject obj = AssetParser.ParseObject (asset);

		string name = obj.name;
		float lifeTime = obj.GetFieldByName ("lifeTime");
		float startTime = obj.GetFieldByName ("startTime");

		return new CinematicText (name, startTime, lifeTime);
	}

	public void ShowText ()
	{
		GameObject prefab = Resources.Load<GameObject> (CinematicTextPrefabPath);
		GameObject textObj = GameObject.Instantiate (prefab);

		textObj.GetComponentInChildren<Text> ().text = text;

		GameObject.Destroy (textObj, lifeTime);
	}
}
