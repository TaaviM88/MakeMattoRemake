using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int playerID = 0;
    private bool spawnplayers = false;
    private bool hasSpawnedPlayers = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AddSpawnPointToList(gameObject);
        if(playerID == 1)
        {
            spawnplayers = true;
        }
    }

    private void LateUpdate()
    {
        if(!hasSpawnedPlayers && spawnplayers)
        {
            GameManager.instance.SpawnPlayers();
            hasSpawnedPlayers = true;
        }
    }
}
