using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnAndRespawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnTransform;
    
    public TMP_Text spawnText;

    private float timeToRespawn = 3f;

    /// <summary>
    /// Activates respawn text. Starts respawn coroutine.
    /// </summary>
    public void RespawnPlayer()
    {
        spawnText.text = "You died!\nRespawning in: " + timeToRespawn;
        spawnText.gameObject.SetActive(true);
        StartCoroutine(RespawnTime());   
    }

    /// <summary>
    /// Updates respawn text after every second. Then respawns player.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RespawnTime()
    {
        while (timeToRespawn > 0)
        {
            yield return new WaitForSeconds(1);
            timeToRespawn--;
            spawnText.text = "You died!\nRespawning in: " + timeToRespawn;
        }
        Instantiate(playerPrefab);
    }
}
