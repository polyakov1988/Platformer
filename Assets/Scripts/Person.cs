using System;
using UnityEngine;

public abstract class Person : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _damage;
    
    protected float _maxHealth;

    public event Action IsDead;

    public float GetDamage => _damage;

    private void Start()
    {
        _maxHealth = _health;
    }

    public void TakeDamage(float damageValue)
    {
        _health -= damageValue;
        
        if (_health <= 0)
        {
            _health = 0;
            
            IsDead?.Invoke();
        }
    }
}