using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, ITriggerHandler
{
    Material ghostMaterial;
    [SerializeField] private AudioSource ghostAudioSource;

    [Header("Fade Settings")]
    [Tooltip("handles how quickly the ghost fades away")]
    [SerializeField] private float fadeSpeed;


    void Start()
    {
        ghostMaterial = GetComponent<MeshRenderer>().material;
        ghostAudioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// disables collider to avoid unneeded additional raycast hits, grabs
    /// the ghosts material and starts the FadeAway coroutine
    /// </summary>
    public void PerformBehavior()
    {
        Color ghostMatColor = ghostMaterial.color;
        GetComponent<Collider>().enabled = false;  
        ghostAudioSource.Play();

        StartCoroutine(FadeAway(ghostMatColor));
    }

    /// <summary>
    /// Fades the ghost to be invisible at a set speed, ghost object is
    /// destroyed after it is fully invisible
    /// </summary>
    /// <param name="color">color of the ghosts material</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator FadeAway(Color color)
    {
        while (ghostMaterial.color.a > 0)
        {
            float alpha = ghostMaterial.color.a - fadeSpeed;
            ghostMaterial.color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(ghostAudioSource.clip.length);
        Destroy(gameObject);
    }
}
