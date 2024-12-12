using System;
using Currensy;
using DG.Tweening;
using Kalkatos.DottedArrow;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class BuyingPawn : MonoBehaviour
    {
        [SerializeField] private Button _buyPawnButton;
        [SerializeField] private GameObject _pawnPrefab;
        [SerializeField] private Transform _pawnSpawnPoint;
        [SerializeField] private int _pricePawn = 3;
        [SerializeField] private  Coins _coins;
        [SerializeField] private Image _pawnRedAlert;

        private bool _yoyTime;
        
        private void OnEnable()
        {
            _buyPawnButton.onClick.AddListener(PawnBuy);
        }

        private void PawnBuy()
        {
            if (_yoyTime)
                return;
         
            
            if (_coins.value < 3)
            {
                _yoyTime = true; // Устанавливаем флаг, что анимация началась
                _pawnRedAlert.DOKill(); // Остановка текущей анимации, если она есть
                _pawnRedAlert.DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() => 
                {
                    _yoyTime = false; // Сбрасываем флаг по завершению анимации
                });
                Debug.Log("не хватает золота");
            }
            else
            {
                _coins.SpendCurrency(_pricePawn); 
                Instantiate(_pawnPrefab, _pawnSpawnPoint.position, Quaternion.identity);
                BlinkEffect blinkEffect = FindObjectOfType<BlinkEffect>();

                Arrow arrow = FindObjectOfType<Arrow>();
                if (arrow != null && blinkEffect != null)
                {
                    arrow.OnArrow += blinkEffect.StartBlinking;
                    arrow.OffArrow += blinkEffect.StopBlinking;
                }
            }
           
        }

        private void OnDisable()
        {
            _buyPawnButton.onClick.RemoveListener(PawnBuy);
        }
    }
}