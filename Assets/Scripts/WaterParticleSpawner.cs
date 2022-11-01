using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject waterParticlePrefab;
    public Vector3 offset; 
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0f, 0f, Random.Range(-16f, 16f));
        StartCoroutine(WaterSpawner());
    }

    IEnumerator WaterSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.15f);
            Instantiate(waterParticlePrefab, transform.parent.position + offset, Quaternion.identity);
            offset = new Vector3(0f, 0f, Random.Range(-16f, 16f));
        }
    }
}
