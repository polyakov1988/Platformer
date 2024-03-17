using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _healthBar;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        if (Camera.main == null)
        {
            return;
        }
        
        _healthBar.gameObject.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _offset);
    }
}
