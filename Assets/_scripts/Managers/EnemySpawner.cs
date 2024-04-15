using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private string enemyPoolKey = "Enemy";

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private CalculationInfo spawnDelayInfo;
    [SerializeField, Range(0f, 1f)] private float randomness = 0.5f;

    private float timer = 0f;

    private int enemySpawned = 0;

    private bool spawning = false;

    public void Init()
    {
        spawning = true;
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    private void Update()
    {
        if (!spawning) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = CalculateReverseArithmeticProgression(enemySpawned, spawnDelayInfo);
            Debug.Log($"Progression timer value: {timer}");
            timer -= Random.Range(-timer, timer) * randomness;
            Debug.Log($"Randomness timer value: {timer}");
        }
    }

    public void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject spawnedEnemy = ObjectPooling.Instance.SpawnFromPool(enemyPoolKey, 
            spawnPoint.position, Quaternion.identity);
        if(spawnedEnemy.TryGetComponent(out Enemy enemy))
        {
            enemy.Init();
            enemy.OnDie += ScoreManager.Instance.IncreaseCurrentScore;
            EnemyData[] enemyDatas = GameManager.Instance.CoreBalance.EnemyDatas;
            enemy.MoveSpeed = enemyDatas[Random.Range(0, enemyDatas.Length)].MoveSpeed;
        }

        enemySpawned++;
    }

    public float CalculateReverseArithmeticProgression(int lvl, CalculationInfo upgradeCalculation)
    {
        float amount = upgradeCalculation.MinValue + (1f / (lvl + upgradeCalculation.Progression));
        return amount;
    }
}
