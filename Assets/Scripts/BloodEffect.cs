using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BloodEffect : MonoBehaviour
{
    [SerializeField] private Transform _hero;
    [SerializeField] private ParticleSystem _particleSystem;
    private Enemy _victim; 
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        _particleSystem.Play();
    }
    
    private void Update()
    {
        Vector3 heroPosition = _hero.position;
        Vector3 enemyPosition = transform.position;
        
        float angle = Mathf.Atan2(enemyPosition.y - heroPosition.y , enemyPosition.x -heroPosition.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = _victim.transform.position;
    }
    
    private void OnDisable()
    {
        _particleSystem.Stop();
    }
    
    public void SetVictim(Enemy enemy)
    {
        _victim = enemy;
    }
}