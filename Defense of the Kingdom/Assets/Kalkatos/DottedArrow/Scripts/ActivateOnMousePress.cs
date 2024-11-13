using System;
using UnityEngine;

namespace Kalkatos.DottedArrow
{
	public class ActivateOnMousePress : MonoBehaviour
	{
		[SerializeField] private Transform origin;
		[SerializeField] private Arrow arrow;
		[SerializeField] private SelectPawnAction _selectPawnAction;
		[SerializeField] private PawnRepairBuilding _pawnRepairBuilding;
		[SerializeField] private BlinkEffect _blinkEffect;
		

		private void OnEnable()
		{
			_pawnRepairBuilding.StartPawnMove.AddListener(OffArrow);
			_blinkEffect.StartBlink.AddListener(OnArrowCursor);
		}

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

		private void OnDisable()
		{
			_pawnRepairBuilding.StartPawnMove.RemoveListener(OffArrow);
			_blinkEffect.StartBlink.RemoveListener(OnArrowCursor);
		}

		private void OffArrow()
		{
			arrow.Deactivate();
		}
	}
}
