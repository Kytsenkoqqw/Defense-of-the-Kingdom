using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenPauseMenu : MonoBehaviour
{
   private void OnEnable()
   {
      transform.DOScale(new Vector3(1, 1, 0), 0.5f).SetUpdate(true);
   }

   private void OnDisable()
   {
      transform.DOScale(new Vector3(0, 0, 0), 0.5f);
   }
}
