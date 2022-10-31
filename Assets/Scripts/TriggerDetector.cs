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
}

