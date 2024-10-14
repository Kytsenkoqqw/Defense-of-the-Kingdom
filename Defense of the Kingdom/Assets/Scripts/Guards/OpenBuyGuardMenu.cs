using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OpenBuyGuardMenu : MonoBehaviour
{
   private void OnEnable()
   {
      transform.DOScale(new Vector3(1, 1, 0), 0.5f);
   }

   private void OnDisable()
   {
      transform.DOScale(new Vector3(0, 0, 0), 0.5f);
   }
}
