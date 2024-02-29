using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<CoinSpawnPoint> _spawnPoints;
    [SerializeField] private Coin _coinPrefab;

    private Coin _coin;
    private WaitForSeconds _waitForActivate;
    
    private void OnEnable()
    {
        _coin.IsTaken += RespawnCoin;
    }

    private void OnDisable()
    {
        _coin.IsTaken -= RespawnCoin;
    }
    
    private void Awake()
    {
        _waitForActivate = new(5);
        
        _coin = Instantiate(_coinPrefab);
        
        _coin.transform.position = GetRandomSpawnPoint().transform.position;
    }

    private void RespawnCoin()
    {
        StartCoroutine(RespawnCoinWithTimeout());
    }
    
    private IEnumerator RespawnCoinWithTimeout()
    {
        _coin.gameObject.SetActive(false);

        yield return _waitForActivate;
        
        _coin.transform.position = GetRandomSpawnPoint().transform.position;
        _coin.gameObject.SetActive(true);
    }

    private CoinSpawnPoint GetRandomSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
    }

    
    
    
}
