using UnityEngine;
using System.Collections;
using System.Linq;

public class ParsedObject
{
	public readonly string name;
	public readonly ParsedField[] fields;

	public ParsedObject (string name, ParsedField[] fields)
	{
		this.name = name;
		this.fields = fields;
	}

	public ParsedField GetFieldByName (string name)
	{
		return fields.FirstOrDefault ((f) => f.name.Equals(name));
	}
}
