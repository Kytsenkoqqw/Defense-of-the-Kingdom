using System;
using UnityEngine;

namespace Kalkatos.DottedArrow
{
	public class Arrow : MonoBehaviour
    {
		public Transform Origin { get { return origin; } set { origin = value; } }

		public Action OnArrow;
		public Action OffArrow;
		[SerializeField] private float baseHeight;
		[SerializeField] private RectTransform baseRect;
		[SerializeField] private Transform origin;
		[SerializeField] private bool startsActive;
		
		

		private RectTransform myRect;
		private Canvas canvas;
		private Camera mainCamera;
		private bool isActive;

		private void Awake ()
		{
			myRect = (RectTransform)transform;
			canvas = GetComponentInParent<Canvas>();
			mainCamera = Camera.main;
			SetActive(startsActive);
		}

		private void Update ()
		{
			if (!isActive)
				return;
			Setup();
		}

		private void Setup ()
		{
			if (origin == null)
				return;
    
			// Преобразование мировых координат origin в локальные координаты Canvas
			Vector2 originLocalPosition = canvas.transform.InverseTransformPoint(origin.position);
    
			// Установка позиции стрелки относительно локальных координат origin
			myRect.anchoredPosition = originLocalPosition;

			// Преобразование позиции мыши в локальные координаты на Canvas
			Vector2 mouseLocalPosition = canvas.transform.InverseTransformPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition));
    
			// Расчёт направления от origin к мыши
			Vector2 differenceToMouse = mouseLocalPosition - originLocalPosition;
			differenceToMouse.Scale(new Vector2(1f / myRect.localScale.x, 1f / myRect.localScale.y));
    
			// Направление стрелки на указатель мыши
			transform.up = differenceToMouse;

			// Установка длины стрелки в зависимости от расстояния до мыши
			baseRect.anchorMax = new Vector2(baseRect.anchorMax.x, differenceToMouse.magnitude * myRect.localScale.x / baseHeight);
		}

		public void SetActive (bool b)
		{
			isActive = b;
			if (b)
			{
				OnArrow?.Invoke();
				Setup();
			}
			baseRect.gameObject.SetActive(b);
		}

		public void Activate () => SetActive(true);

		public void Deactivate()
		{
			OffArrow?.Invoke();
			SetActive(false);
		}

		public void SetupAndActivate (Transform origin)
		{
			Origin = origin;
			Activate();
		}
	}
}
