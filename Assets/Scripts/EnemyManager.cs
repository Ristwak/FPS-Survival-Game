using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    private GameObject boar_prefab, canibal_prefab;

    public Transform[] cannibal_Spawnpoints, boar_Spawnpoints;

    [SerializeField]
    private int cannibal_enemy_Count, boar_enemy_Count;

    private int initial_Cannibal_Count, initial_Boar_Count;

    public float wait_before_spawn_enemyTime;

    private Coroutine spawnCoroutine;

    void Awake()
    {
        makeInstance();
    }

    void Start()
    {
        initial_Cannibal_Count = cannibal_enemy_Count;
        initial_Boar_Count = boar_enemy_Count;

        SpawnEnemies();

        spawnCoroutine = StartCoroutine(CheckToSpawnEnemies());
    }

    void makeInstance()
    {
        if (instance == null)
            instance = this;
    }

    void SpawnEnemies()
    {
        SpawnCannivals();
        SpawnBoars();
    }

    void SpawnCannivals()
    {
        int spawnCount = Mathf.Min(cannibal_enemy_Count, cannibal_Spawnpoints.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(canibal_prefab, cannibal_Spawnpoints[i].position, Quaternion.identity);
        }

        cannibal_enemy_Count = 0;
    }

    void SpawnBoars()
    {
        int spawnCount = Mathf.Min(boar_enemy_Count, boar_Spawnpoints.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(boar_prefab, boar_Spawnpoints[i].position, Quaternion.identity);
        }

        boar_enemy_Count = 0;
    }

    IEnumerator CheckToSpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(wait_before_spawn_enemyTime);

            SpawnCannivals();
            SpawnBoars();
        }
    }

    public void EnemyDied(bool cannibal)
    {
        if (cannibal)
        {
            cannibal_enemy_Count++;

            if (cannibal_enemy_Count > initial_Cannibal_Count)
            {
                cannibal_enemy_Count = initial_Cannibal_Count;
            }
        }
        else
        {
            boar_enemy_Count++;

            if (boar_enemy_Count > initial_Boar_Count)
            {
                boar_enemy_Count = initial_Boar_Count;
            }
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}