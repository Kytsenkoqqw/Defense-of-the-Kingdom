using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public abstract class Currensy<T> : MonoBehaviour
{
    public abstract event Action<T> OnValueChangedEvent; 
    
    public T value;

    public abstract void AddCurrency(T amount);
    public abstract void SpendCurrency(T amount);
}
