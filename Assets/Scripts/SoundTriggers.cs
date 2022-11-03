using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggers : MonoBehaviour
{
    private AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_AudioSource.isPlaying)
        {
            m_AudioSource.Play();
        }
    }


}
