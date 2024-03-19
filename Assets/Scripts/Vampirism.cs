using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private BloodEffect _bloodEffect;
    [SerializeField] private float _suckedHealth;
    
    public bool IsActive { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy) && enemy.GetComponent<Health>().CurrentHealth != 0)
        {
            _bloodEffect.SetVictim(enemy);
            _bloodEffect.gameObject.SetActive(true);
            _bloodEffect.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy) && enemy.GetComponent<Health>().CurrentHealth != 0)
        {
            float health = _suckedHealth * Time.deltaTime;
            
            enemy.TakeDamage(health);
            gameObject.GetComponentInParent<Hero>().Heal(health);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _bloodEffect.Stop();
            _bloodEffect.gameObject.SetActive(false);
        }
    }
    
    public void Activate()
    {
        IsActive = true;
    }
    
    public void Deactivate()
    {
        IsActive = false;
    }
}
