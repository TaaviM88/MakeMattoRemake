using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    private GameObject spawn1, spawn2;

    bool weHaveWinner = false;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        GameObject temp = null;
        GameObject temp2 = null;
        players.Add(temp);
        players.Add(temp2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSpawnPointToList(GameObject newSpawn)
    {

        if(newSpawn.GetComponent<SpawnPoint>().playerID == 1 && spawn2 == null)
        {
            spawn1 = newSpawn;
        }

        if(newSpawn.GetComponent<SpawnPoint>().playerID == 2 && spawn2 == null)
        {
            spawn2 = newSpawn;
        }
    /*    if(spawnPoints.Count > 0)
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
        }*/
    }

    public void AddPlayerToList(GameObject newPlayer, int id)
    {
        if(id==1)
        {
            players[0] = newPlayer;
        }

        if(id==2)
        {
            players[1] = newPlayer;
            
        }
        /*
        GameObject clone = newPlayer;
        //   bool containPlayer = false;
        Debug.Log("new players ID" + newPlayer.GetComponent<Player>().playerID);
        if (players.Count > 0)
        {
           for (int i = 0; i < players.Count; i++)
            {

                if (players[i].GetComponent<Player>().playerID != clone.GetComponent<Player>().playerID)
                {
                    Debug.Log("added player to list" + clone.GetComponent<Player>().playerID + "And I is " + i);
                    players.Add(clone);
                }
                else
                {   Debug.Log("Player ID in [0] is " + players[0].GetComponent<Player>().playerID);
                    Debug.Log("Not added player to list newPlayer ID = " + clone.GetComponent<Player>().playerID + "and I position is " + i);
                }
            }

          /* if(!containPlayer)
            {
                
                
                Debug.Log($"Added player {newPlayer.name} and Playerlist count is {players.Count}");
            }
            
        }
        else
        {
            players.Add(clone);
            Debug.Log("added player to list" + clone.GetComponent<Player>().playerID);
            //players[0].GetComponent<PlayerMovement>().playerID = 1;
        }*/
    }

    public void SpawnPlayers()
    {
        
        for (int i = 0; i < players.Count; i++)
        {

          if(i == 0)
            {
                GameObject clone = Instantiate(players[i], new Vector3(spawn1.transform.position.x, spawn1.transform.position.y, spawn1.transform.position.z), Quaternion.identity);
                clone.GetComponent<Player>().playerID = 1;
            }

          if(i == 1)
            {
                GameObject clone = Instantiate(players[i], new Vector3(spawn2.transform.position.x, spawn2.transform.position.y, spawn2.transform.position.z), Quaternion.identity);
                clone.GetComponent<Player>().playerID = 2;
            }

            /*if(players[i].GetComponent<Player>().playerID == 1)
            {
               GameObject clone = Instantiate(players[i], new Vector3(spawn1.transform.position.x, spawn1.transform.position.y, spawn1.transform.position.z), Quaternion.identity);
                clone.GetComponent<Player>().playerID = 1;
            }

            if (players[i].GetComponent<Player>().playerID == 2)
            {
               GameObject clone = Instantiate(players[i], new Vector3(spawn2.transform.position.x, spawn2.transform.position.y, spawn2.transform.position.z), Quaternion.identity);
                clone.GetComponent<Player>().playerID = 2;
            }*/
            /*for (int j = 0; j < spawnPoints.Count; j++)
            {
                if (players[i].GetComponent<Player>().playerID == spawnPoints[j].GetComponent<SpawnPoint>().playerID)
                {
                    Instantiate(players[i], new Vector3(spawnPoints[j].transform.position.x, spawnPoints[j].transform.position.y, spawnPoints[j].transform.position.z),Quaternion.identity);
                }
                /*else
                {
                   if(players[i].GetComponent<Player>().playerID == spawnPoints[j + 1].GetComponent<SpawnPoint>().playerID)
                    {
                        Instantiate(players[i], spawnPoints[j].transform);
                    }
                   else if(players[i+1].GetComponent<Player>().playerID == spawnPoints[j].GetComponent<SpawnPoint>().playerID)
                    {
                        Instantiate(players[i], spawnPoints[j].transform);
                    }
                   else
                    {
                        Debug.Log($"Can't find correct spawnpoint for {players[i].GetComponent<Player>().playerID}");
                    }
                }
                
            }*/
            
        }
    }

    public void RemovePlayer(GameObject deadPlayer)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<Player>().playerID == deadPlayer.GetComponent<Player>().playerID)
            {
                Debug.Log($"Losers name {players[i].name}");
                players.RemoveAt(i);
                CheckWinner();
                break;
            }
        }
    }

    public void CheckWinner()
    {
        if(!weHaveWinner)
        {
            if (players.Count == 1)
            {
                //DO WINNING STUFF HERE
                Debug.Log($"Winner is Player: {players[0].GetComponent<Player>().playerID}");
                Time.timeScale = 0;
                Debug.Log($"TimeScale is: {Time.timeScale}");
            }

            else if (players.Count < 1)
            {
                //DO STUFF IF SOMEREASON ALL PLAYERS ARE DEAD
                Debug.Log($"There are no players left. Abort the mission guys");
                Time.timeScale = 0;
            }
        }
    }
}
