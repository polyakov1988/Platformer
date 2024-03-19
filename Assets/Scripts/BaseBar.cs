using UnityEngine;

public abstract class BaseBar : MonoBehaviour
{
    protected Health _health;
    
    private Canvas _canvas;

    protected void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
        _health.IsDead += OnDead;
    }

    protected void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
        _health.IsDead -= OnDead;
    }
    
    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>().rootCanvas;
        _health = _canvas.GetComponentInParent<Health>();
    }

    protected abstract void OnHealthChanged();

    private void OnDead()
    {
        _canvas.gameObject.SetActive(false);
    }
}
