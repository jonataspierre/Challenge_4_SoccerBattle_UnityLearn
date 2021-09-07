﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 12f;
    private float spawnZMin = 15f; // set min spawn Z
    private float spawnZMax = 25f; // set max spawn Z

    public int enemyCount;
    public int waveCount = 1;
    public int speedEnemyIncrease = 50;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {        
        SpawnEnemyPowerupWave(waveCount);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            waveCount++;

            speedEnemyIncrease += 5; 
            
            SpawnEnemyPowerupWave(waveCount);            
        }

    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyPowerupWave(int enemiesToSpawn)
    {
        int powerupToSpawn = enemiesToSpawn - 1; // 

        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        // Spawn number of powerup based on enemy number
        for (int i = 0; i < powerupToSpawn; i++)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        ResetPlayerPosition(); // put player back at start
    }

    //void SpawnPowerupWave(int powerupToSpawn)
    //{
        

    //    // If no powerups remain, spawn a powerup
    //    //if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
    //    for (int i = 0; i < powerupToSpawn; i++)
    //    {
    //        Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
    //    }
    //}

    // Move player back to position in front of own goal
    
    void ResetPlayerPosition ()
    {   
        player.transform.position = new Vector3(0, 1, -7);        
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

}
