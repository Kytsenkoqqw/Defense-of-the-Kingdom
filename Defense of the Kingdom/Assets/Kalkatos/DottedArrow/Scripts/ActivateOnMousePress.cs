using System;
using UnityEngine;

namespace Kalkatos.DottedArrow
{
	public class ActivateOnMousePress : MonoBehaviour
	{
		[SerializeField] private Transform origin;
		[SerializeField] private Arrow arrow;
		[SerializeField] private SelectPawnAction _selectPawnAction;
		
		public void OnArrowCursor ()
		{
			arrow.SetupAndActivate(origin);
			_selectPawnAction.ClosedPawnPanel();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(1))
			{
				arrow.Deactivate();
			}
		}
	}
}
