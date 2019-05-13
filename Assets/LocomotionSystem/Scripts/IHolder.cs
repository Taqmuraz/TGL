using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	public interface IHolder
	{
		Transform GetTransform();
		Orientation GetLeftHandDefault();
		Orientation GetRightHandDefault();
		Transform GetLeftHandTransform();
		Transform GetRightHandTransform();
	}
	
}