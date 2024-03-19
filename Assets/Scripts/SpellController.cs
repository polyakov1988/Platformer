using System.Collections;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;
    
    private WaitForSeconds _waitForActivate;
    
    private void Awake()
    {
        _vampirism.gameObject.SetActive(false);
        _waitForActivate = new(6);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && _vampirism.IsActive == false)
        {
            StartCoroutine(ActivateVampirism());
        }
    }
    
    private IEnumerator ActivateVampirism()
    {
        _vampirism.gameObject.SetActive(true);
        _vampirism.Activate();

        yield return _waitForActivate;

        _vampirism.gameObject.SetActive(false);
        _vampirism.Deactivate();
    }
}
