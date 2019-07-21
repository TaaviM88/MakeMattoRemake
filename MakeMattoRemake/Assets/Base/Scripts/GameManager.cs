using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
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
                if (players[i].GetComponent<Player>().playerID == newPlayer.GetComponent<Player>().playerID)
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
                if (players[i].GetComponent<Player>().playerID == spawnPoints[j].GetComponent<SpawnPoint>().playerID)
                {
                    Instantiate(players[i], new Vector3(spawnPoints[j].transform.position.x, spawnPoints[j].transform.position.y, spawnPoints[j].transform.position.z),Quaternion.identity);
                }
                else
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
                
            }
            
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
