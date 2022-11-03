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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            spawnManager.RespawnPlayer();
        }
    }
}
