using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] Collider playerCollider;
    [SerializeField] LayerMask ghostLayerMask;
    [SerializeField] int rayCastDistance = 100;

    private GameObject lastHitGameObject;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        CheckForGhosts();
    }

    /// <summary>
    /// Uses a raycast from the main camera to spot ghosts, if a ghoost is hit
    /// stores it in lastHitGameObject so we can have ghost perform a behavior
    /// as needed 
    /// </summary>
    private void CheckForGhosts()
    {
        Vector3 viewport = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = cam.ViewportPointToRay(viewport);
        RaycastHit hit;

        ray.origin = cam.transform.position;

        if (Physics.Raycast(ray, out hit, rayCastDistance, ghostLayerMask))
        {
            lastHitGameObject = hit.transform.gameObject;
            Debug.Log(lastHitGameObject);

            //Debug.DrawLine(cam.transform.position, hit.transform.position, Color.red);
        }
      
    }

    /// <summary>
    /// handles triggers such as player walking into a specific part of the level
    /// that triggers a scream, etc 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
    }
}

