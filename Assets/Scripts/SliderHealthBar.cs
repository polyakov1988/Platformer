using UnityEngine;
using UnityEngine.UI;

public class SliderHealthBar : BaseBar
{
    [SerializeField] private Slider _slider;

    protected override void OnHealthChanged()
    {
        _slider.value = _health.RelativeHealth;
    }
}