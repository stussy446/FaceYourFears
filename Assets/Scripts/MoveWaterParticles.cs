using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWaterParticles : MonoBehaviour
{
    [SerializeField] private float waterMoveSpeed = 0.4f;
    [SerializeField] private List<Transform> movePoints;
    [SerializeField] private float rotateSpeed = 2f;
    [SerializeField] private WaterParticleSpawner waterSpawner;
    private Vector3 randomRotation;
    
    private int currentMovePoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        waterSpawner = GameObject.Find("Water Particle Spawner").GetComponent<WaterParticleSpawner>();
        randomRotation = new Vector3(Random.Range(0f, 1f), Random.Range(0f,1f), Random.Range(0f, 1f));
        GameObject allMovePoints = GameObject.Find("Moving Points");
        for (int i = 0; i < allMovePoints.transform.childCount - 1; i++)
        {
            movePoints.Add(allMovePoints.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(randomRotation * rotateSpeed);
        transform.position = Vector3.MoveTowards(transform.position, GetTarget().position, waterMoveSpeed);
    }

    Transform GetTarget()
    {
        Transform target;
        target = movePoints[currentMovePoint];
        if (currentMovePoint < movePoints.Count - 1)
        {
            if (transform.position == target.position)
            {
                currentMovePoint++;
            }
        } else
        {
            Destroy(gameObject);
        }
        return target;
    }
}
