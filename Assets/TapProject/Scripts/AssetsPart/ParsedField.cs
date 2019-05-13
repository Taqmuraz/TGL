using UnityEngine;
using System.Collections;

public class ParsedField
{
	public readonly string name;
	public readonly string value;

	public ParsedField (string name, string value)
	{
		this.name = name;
		this.value = value;
	}
	public static implicit operator float (ParsedField field)
	{
		return float.Parse (field.value);
	}
	public static implicit operator int (ParsedField field)
	{
		return int.Parse (field.value);
	}
}
