using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	public interface IHoldable
	{
		Orientation GetRightHand (IHolder holder);
		Orientation GetLeftHand (IHolder holder);
		void OnAdd (IHolder holder);
		void OnRemove (IHolder holder);
		float GetPosSin(IHolder holder);
	}
}