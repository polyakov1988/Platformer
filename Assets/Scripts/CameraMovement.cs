using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Vector3 _minValue;
    [SerializeField] private Vector3 _maxValue;
    
    private readonly Vector3 _offset = new(0f, 0f, -10f);
    
    private void Update()
    {
        Vector3 targetPosition = _target.position + _offset;
        
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, _minValue.x, _maxValue.x),
            Mathf.Clamp(targetPosition.y, _minValue.y, _maxValue.y), 
            -10f);
        
        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, _smoothSpeed);
        
        transform.position = smoothPosition;
    }
}
