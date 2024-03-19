using System;
using UnityEngine;

public class TakebleItem : MonoBehaviour
{
    public event Action IsTaken;

    public void Take()
    {
        IsTaken?.Invoke();
    }
}
