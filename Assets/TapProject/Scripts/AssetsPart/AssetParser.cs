using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class AssetParser
{
	public const char WordSeparator = ' ';
	public const char LineSeparator = '\n';

	public static ParsedObject ParseObject (TextAsset asset)
	{
		return ParseObject (asset.text);
	}

	public static ParsedObject ParseObject (string text)
	{
		string[] lines = text.Split (LineSeparator);

		string name = lines [0];

		lines = lines.Skip (1).ToArray ();

		ParsedField[] fields = new ParsedField[lines.Length];

		for (int i = 0; i < lines.Length; i++) {
			string[] words = lines[i].Split (WordSeparator);
			fields [i] = new ParsedField (words[0], words[1]);
		}

		return new ParsedObject (name, fields);
	}
}

