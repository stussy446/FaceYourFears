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

    public void RespawnPlayer()
    {
        spawnText.text = "You died!\nRespawning in: " + timeToRespawn;
        spawnText.gameObject.SetActive(true);
        StartCoroutine(RespawnTime());   
    }

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
