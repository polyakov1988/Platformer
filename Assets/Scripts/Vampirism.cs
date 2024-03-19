using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private BloodEffect _bloodEffect;
    [SerializeField] private float _suckedHealth;

    private Enemy _enemy;
    private Hero _hero;
    
    public bool IsActive { get; private set; }

    private void Awake()
    {
        _hero = gameObject.GetComponentInParent<Hero>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_enemy != null)
        {
            return;
        }
        
        if (other.TryGetComponent(out Enemy enemy) && enemy.IsDead() == false)
        {
            _enemy = enemy;
            _bloodEffect.SetVictim(enemy);
            _bloodEffect.gameObject.SetActive(true);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_enemy == null || (_enemy.IsDead()))
        {
            _bloodEffect.gameObject.SetActive(false);
            _enemy = null;
            
            return;
        }
        
        float health = _suckedHealth * Time.deltaTime;
            
        _enemy.TakeDamage(health);
        _hero.Heal(health);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy) && enemy == _enemy)
        {
            if (_bloodEffect.enabled)
            {
                _bloodEffect.gameObject.SetActive(false);
            }
            
            _enemy = null;
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
