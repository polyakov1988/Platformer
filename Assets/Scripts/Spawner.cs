using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : TakebleItem
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private T _prefab;
    [SerializeField] private int _timeout;

    private T _item;
    private WaitForSeconds _waitForActivate;
    
    private void OnEnable()
    {
        _item.IsTaken += RespawnItem;
    }

    private void OnDisable()
    {
        _item.IsTaken -= RespawnItem;
    }
    
    private void Awake()
    {
        _waitForActivate = new(_timeout);
        
        _item = Instantiate(_prefab);
        
        _item.transform.position = GetRandomSpawnPoint().transform.position;
    }

    private void RespawnItem()
    {
        StartCoroutine(RespawnCoinWithTimeout());
    }
    
    private IEnumerator RespawnCoinWithTimeout()
    {
        _item.gameObject.SetActive(false);

        yield return _waitForActivate;
        
        _item.transform.position = GetRandomSpawnPoint().transform.position;
        _item.gameObject.SetActive(true);
    }

    private SpawnPoint GetRandomSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
    }
}