using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] Collider playerCollider;
    [SerializeField] LayerMask ghostLayerMask;
    [SerializeField] int rayCastDistance = 100;

    private GameObject lastGhostHit;
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
        RaycastHit ghostHit;

        ray.origin = cam.transform.position;

        if (Physics.Raycast(ray, out ghostHit, rayCastDistance, ghostLayerMask))
        {
            lastGhostHit = ghostHit.transform.gameObject;
            var triggerToPerform = lastGhostHit.GetComponent<ITriggerHandler>();

            if (triggerToPerform != null)
            {
                triggerToPerform.PerformBehavior();
            }

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
        var triggerToPerform = other.GetComponent<ITriggerHandler>();
        if (triggerToPerform != null)
        {
            triggerToPerform.PerformBehavior();
        }
    }
}

