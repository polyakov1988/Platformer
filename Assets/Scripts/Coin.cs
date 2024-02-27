using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Coin : MonoBehaviour
{
    private readonly WaitForSeconds _waitForActivate = new(5);

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_renderer.enabled && other.GetComponent<Hero>())
        {
            other.GetComponent<Hero>().AddCoin();

            _renderer.enabled = false;
            
            StartCoroutine(Activate());
        }
    }
    
    private IEnumerator Activate() {
        yield return _waitForActivate;
        
        _renderer.enabled = true;
    }
}
