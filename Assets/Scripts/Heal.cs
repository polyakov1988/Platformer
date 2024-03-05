using UnityEngine;

public class Heal : TakebleItem
{
    [SerializeField] private float _healValue;

    public float GetHealValue => _healValue;
}
