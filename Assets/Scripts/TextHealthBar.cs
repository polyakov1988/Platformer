using TMPro;
using UnityEngine;

public class TextHealthBar : BaseBar
{
    [SerializeField] private TMP_Text _healthBar;

    protected override void OnHealthChanged()
    {
        _healthBar.text = $"{(int) _health.CurrentHealth} / {(int) _health.MaxHealth}";
    }
}
