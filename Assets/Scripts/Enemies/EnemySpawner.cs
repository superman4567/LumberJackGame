using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [System.Serializable]
        public struct EnemySpawnProps
        {
            public Enemy EnemyPrefab;
            public int SpawnProbability;
        }
        [SerializeField] private EnemySpawnProps[] enemies;
        [SerializeField] private Transform spawnPointsContainer;
        [SerializeField] private Transform parentTransform;
        [SerializeField] private float spawnDuration = 2.0f;
        // @TODO: THIS IS FOR DEBUG ONLY
        public bool _startToSpawn;

        private List<Transform> _spawnPoints = new();
        private float _timeBetweenSpawns = 2.5f;
        private float _spawnTimer;
        private bool _limitReached;
        private int _spawnedEnemies;
        private int _totalProbability;

        private void Start()
        {
            foreach (Transform spawnPoint in spawnPointsContainer)
            {
                _spawnPoints.Add(spawnPoint);
            }

            foreach (var enemy in enemies)
            {
                _totalProbability += enemy.SpawnProbability;
            }
        }

        private void Update()
        {
            if (_spawnedEnemies > 0) return;
            if (RoundManager.Instance.orcsSpawnedInCurrentRound < RoundManager.Instance.OrcsSpawnedThisRound())
            {
                _spawnTimer += Time.deltaTime;

                if (_spawnTimer >= _timeBetweenSpawns)
                {
                    SpawnEnemy();
                    _spawnTimer = 0.0f;
                }
            }
        }

        private void SpawnEnemy()
        {
            var randomSpawnPoint = Random.Range(0, _spawnPoints.Count);
            var enemy = Instantiate(ChooseRandomEnemy(), _spawnPoints[randomSpawnPoint].position, _spawnPoints[randomSpawnPoint].rotation, parentTransform);
            _spawnedEnemies++;
            enemy.EnemyMovement.SetUpdateFrameNumber(_spawnedEnemies);
        }

        private Enemy ChooseRandomEnemy()
        {
            var random = Random.Range(0, _totalProbability + 1);
            var currentProb = 0;
            foreach (var e in enemies)
            {
                currentProb += e.SpawnProbability;
                if (currentProb >= random)
                {
                    return e.EnemyPrefab;
                }
            }
            return enemies[0].EnemyPrefab;
        }
    }
}