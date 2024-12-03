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
		[SerializeField] private BlinkEffect _blinkEffect;

		private void OnEnable()
		{
			_pawnRepairBuilding.StartPawnMove.AddListener(OffArrow);
		}

		public void OnArrowCursor()
		{
			arrow.SetupAndActivate(origin);
			_blinkEffect.StartBlinking(); // Включаем мерцание
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
			_blinkEffect.StopBlinking(); // Останавливаем мерцание
			arrow.Deactivate();
		}
	}
}
