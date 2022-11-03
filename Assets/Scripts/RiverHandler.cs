using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverHandler : MonoBehaviour
{
    private SpawnAndRespawn spawnManager;

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnAndRespawn>();
    }
    /// <summary>
    /// Checks if the object that collided with the river is the player. Destroys the player. Respawns the player.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            spawnManager.RespawnPlayer();
        }
    }
}
