using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CinematicCutscene : Cutscene
{
	protected readonly List<CinematicText> texts = new List<CinematicText> ();

	public CinematicCutscene (string name) : base (name)
	{
		
	}
	protected override void Initialize ()
	{
		base.Initialize ();
		TextAsset[] assets = Resources.LoadAll<TextAsset> (CinematicText.CinematicTextAssetPath + '/' + cutsceneAssetName);

		foreach (var asset in assets) {
			texts.Add (CinematicText.ParseFromAsset(asset));
		}
	}
	public override void Update ()
	{
		base.Update ();
		CheckText ();
	}
	protected void CheckText ()
	{
		CinematicText toShow = texts.FirstOrDefault ((t) => t.startTime < Time.time);
		if (toShow)
		{
			toShow.ShowText ();
			texts.Remove (toShow);
		}
	}
}
