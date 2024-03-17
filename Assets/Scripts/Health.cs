using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;
    
    private float _maxHealth;

    public float CurrentHealth => _health;
    public float MaxHealth => _maxHealth;
    public float RelativeHealth => _health / _maxHealth;
    
    public event Action HealthChanged;
    
    public event Action IsDead;

    private void Start()
    {
        _maxHealth = _health;
        HealthChanged?.Invoke();
    }

    public void Heal(float healValue)
    {
        if (healValue <= 0)
        {
            return;
        }
        
        _health += healValue;

        ClampHealth();
    }

    public void TakeDamage(float damageValue)
    {
        if (damageValue <= 0)
        {
            return;
        }
        
        _health -= damageValue;
        
        ClampHealth();
        
        if (_health <= 0)
        {
            _health = 0;
            
            IsDead?.Invoke();
        }
    }

    private void ClampHealth()
    {
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        
        HealthChanged?.Invoke();
    }
}
