using System;
using UnityEngine;

[RequireComponent(typeof(Hero))]
public class Hero : MonoBehaviour
{
    private Wallet _wallet;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Coin>())
        {
            other.gameObject.GetComponent<Coin>().Take();
            
            _wallet.AddCoin();
        }
    }

}
