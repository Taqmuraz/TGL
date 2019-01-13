using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LocomotionSystem
{
	public abstract class LocomotionMoveAxis : LocomotionParameter
	{
		protected Rigidbody body { get; private set; }
		protected LocomotionController controller { get; private set; }
		protected float moveSpeed { get; private set; }

		public LocomotionMoveAxis (Rigidbody _body, float _moveSpeed, string paramName) : base (paramName)
		{
			body = _body;
			moveSpeed = _moveSpeed;
			controller = _body.GetComponent<LocomotionController> ();
		}
	}

	public abstract class LocomotionParameter
	{
		public string name { get; private set; }

		public LocomotionParameter (string _name)
		{
			name = _name;
		}

		public abstract object GetValue ();
	}
	
}