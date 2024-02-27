using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<CoinSpawnPoint> _spawnPoints;
    [SerializeField] private Coin _coinPrefab;
    
    private void Awake()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            Coin coin = Instantiate(_coinPrefab);
            coin.transform.position = spawnPoint.transform.position;
        }
    }
}
