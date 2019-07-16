﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSpawnPointToList(GameObject newSpawn)
    {
        if(spawnPoints.Count > 0)
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                if(spawnPoints[i].GetComponent<SpawnPoint>().playerID != newSpawn.GetComponent<SpawnPoint>().playerID)
                {
                    spawnPoints.Add(newSpawn);
                }
            }
        }
        else
        {
            spawnPoints.Add(newSpawn);
        }
        
    }

    public void AddPlayerToList(GameObject newPlayer)
    {
        bool containPlayer = false;

        if(players.Count > 0)
        {
           for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetComponent<PlayerMovement>().playerID == newPlayer.GetComponent<PlayerMovement>().playerID)
                {
                    containPlayer = true;
                    break;
                }
                else
                {
                    containPlayer = false;
                }
            }

           if(!containPlayer)
            {
                players.Add(newPlayer);
            }
            
        }
        else
        {
            players.Add(newPlayer);
            //players[0].GetComponent<PlayerMovement>().playerID = 1;
        }
    }

    public void SpawnPlayers()
    {
        Debug.Log("Spawn Players");
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < spawnPoints.Count; j++)
            {
                if (players[i].GetComponent<PlayerMovement>().playerID == spawnPoints[j].GetComponent<SpawnPoint>().playerID)
                {
                    Instantiate(players[i], new Vector3(spawnPoints[j].transform.position.x, spawnPoints[j].transform.position.y, spawnPoints[j].transform.position.z),Quaternion.identity);
                }
               /* else
                {
                   if(players[i].GetComponent<PlayerMovement>().playerID == spawnPoints[j + 1].GetComponent<SpawnPoint>().playerID)
                    {
                        Instantiate(players[i], spawnPoints[j].transform);
                    }
                   else if(players[i+1].GetComponent<PlayerMovement>().playerID == spawnPoints[j].GetComponent<SpawnPoint>().playerID)
                    {
                        Instantiate(players[i], spawnPoints[j].transform);
                    }
                   else
                    {
                        Debug.Log($"Can't find correct spawnpoint for {players[i].GetComponent<PlayerMovement>().playerID}");
                    }
                }*/
                
            }
            
        }
    }
}
