using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : BaseBar
{
    private const float Accuracy = 0.001f;
    
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;
    
    private IEnumerator FillBar(float targetValue)
    {
        while (Math.Abs(_healthBar.fillAmount - targetValue) > Accuracy)
        {
            _healthBar.fillAmount = Mathf.MoveTowards(_healthBar.fillAmount, targetValue, _speed * Time.deltaTime);
            yield return null;
        }
    }

    protected override void OnHealthChanged()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(FillBar(_health.RelativeHealth));
    }
}
