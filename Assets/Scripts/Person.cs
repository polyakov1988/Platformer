using UnityEngine;

public abstract class Person : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _damage;
    
    protected Health _health;

    public float GetDamage => _damage;

    protected void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void TakeDamage(float damageValue)
    {
        _health.TakeDamage(damageValue);
    }
}