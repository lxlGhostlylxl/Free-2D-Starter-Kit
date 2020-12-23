using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public class spawnRoom : MonoBehaviour
{
    public LayerMask room;
    public GameObject[] fillerRooms;
     levelGen levelGen;
    bool hasSpawned;


    private void Awake()
    {
        GameObject go = GameObject.Find("levelGen");
        levelGen = go.GetComponent<levelGen>();
    }

    private void Update()
    {
        if (levelGen.madeExit && !hasSpawned)
        {
            Collider2D roomDection = Physics2D.OverlapCircle(transform.position, 1, room);
            if (!roomDection)
            {
                int rand = Random.Range(0, fillerRooms.Length);
                Instantiate(fillerRooms[rand], transform.position, Quaternion.identity);
            }
            hasSpawned = true;
            
        }
        
    }
}
