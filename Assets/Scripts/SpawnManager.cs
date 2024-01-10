using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerUpPrefab;
    public float spawnRange = 8;

    public int waveNumber;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        waveNumber = 1;
        SpawnEnemyWave(waveNumber);
    }

    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    void SpawnEnemyWave(int enemySpawnNumber)
    {
        for (int i = 0; i < enemySpawnNumber; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomIndex], GenerateRandomPostition(), enemyPrefab[randomIndex].transform.rotation);
        }
        int randomIndexPowerUp = Random.Range(0, powerUpPrefab.Length);
        Instantiate(powerUpPrefab[randomIndexPowerUp], GenerateRandomPostition(), powerUpPrefab[randomIndexPowerUp].transform.rotation);
    }

    Vector3 GenerateRandomPostition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
