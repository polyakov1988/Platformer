using UnityEngine;

[RequireComponent(typeof(Hero))]
public class Hero : Person, IHealeable
{
    private Wallet _wallet;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Coin coin))
        {
            coin.Take();
            
            _wallet.AddCoin();
        }
        
        if (other.gameObject.TryGetComponent(out Heal heal))
        {
            heal.Take();
            
            Heal(heal.GetHealValue);
        }
    }

    public void Heal(float healValue)
    {
        _health += healValue;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }
}
