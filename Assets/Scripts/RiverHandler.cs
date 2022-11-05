using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverHandler : MonoBehaviour
{

    private SpawnAndRespawn spawnManager;
    private PlayerMovement playerMovement;
    private bool isTouchingRiver;
    GameObject player;
    

    private void Start()
    {
        player = GameObject.Find("Player");
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnAndRespawn>();
        playerMovement = player.GetComponent<PlayerMovement>(); 
    }

    /// <summary>
    /// Checks if the object that collided with the river is the player. Destroys the player. Respawns the player.
    /// </summary>
    /// <param name="other"></param>
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Destroy(other.gameObject);
    //        spawnManager.RespawnPlayer();
    //    }
    //}
}
