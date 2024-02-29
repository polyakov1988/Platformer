using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Coin : MonoBehaviour
{
    public event Action IsTaken;

    public void Take()
    {
        IsTaken?.Invoke();
    }
}
