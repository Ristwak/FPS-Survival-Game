using System.Collections;
using System.Collections.Generic;
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

    void Awake()
    {
        makeInstance();
    }

    void Start()
    {
        initial_Cannibal_Count = cannibal_enemy_Count;
        initial_Boar_Count = boar_enemy_Count;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    void makeInstance()
    {
        if(instance == null)
            instance = this;
        

    }

    void SpawnEnemies()
    {
        SpawnCannivals();
        SpawnBoars();
    }

    void SpawnCannivals()
    {
        int index = 0;

        for (int i = 0; i < cannibal_enemy_Count; i++)
        {
            Instantiate(canibal_prefab, cannibal_Spawnpoints[index].position, Quaternion.identity);

            index ++;
        }

        cannibal_enemy_Count = 0;
    }

    void SpawnBoars()
    {
        int index = 0;

        for (int i = 0; i < boar_enemy_Count; i++)
        {
            Instantiate(boar_prefab, boar_Spawnpoints[index].position, Quaternion.identity);

            index ++;
        }

        boar_enemy_Count = 0;
    }
    
    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(wait_before_spawn_enemyTime);

        SpawnCannivals();
        SpawnBoars();

        StartCoroutine("CheckToSpawnEnemies");
    }

    public void EnemyDied(bool cannibal)
    {
        if(cannibal)
        {
            cannibal_enemy_Count++;

            if(cannibal_enemy_Count > initial_Cannibal_Count)
            {
                cannibal_enemy_Count = initial_Cannibal_Count;
            }
        }
        else
        {
            boar_enemy_Count++;

            if(boar_enemy_Count > initial_Boar_Count)
            {
                boar_enemy_Count = initial_Boar_Count;
            }
        }
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}
