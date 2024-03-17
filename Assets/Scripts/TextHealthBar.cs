using TMPro;
using UnityEngine;

public class TextHealthBar : BaseBar
{
    [SerializeField] private TMP_Text _healthBar;

    protected override void OnHealthChanged()
    {
        _healthBar.text = $"{_health.CurrentHealth} / {_health.MaxHealth}";
    }
}
