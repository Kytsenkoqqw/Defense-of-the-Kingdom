using System;
using UnityEngine;
using UnityEngine.UI;

namespace Kalkatos.DottedArrow
{
	public class ActivateOnMousePress : MonoBehaviour
	{
		
		[SerializeField] private Transform origin;
		[SerializeField] private Arrow arrow;
		[SerializeField] private SelectPawnAction _selectPawnAction;
		[SerializeField] private PawnRepairBuilding _pawnRepairBuilding;
		

		private void OnEnable()
		{
			_pawnRepairBuilding.StartPawnMove.AddListener(OffArrow);
		}

		public void OnArrowCursor()
		{
			arrow.SetupAndActivate(origin);
			_selectPawnAction.ClosedPawnPanel();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(1))
			{
				OffArrow();
			}
		}

		private void OnDisable()
		{
			_pawnRepairBuilding.StartPawnMove.RemoveListener(OffArrow);
		}

		private void OffArrow()
		{
			Debug.Log("OffArrow");
			arrow.Deactivate();
		}
	}
}
