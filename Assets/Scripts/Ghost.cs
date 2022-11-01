using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, ITriggerHandler
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// this is where a detected ghosts behavior will be implemented, and this
    /// method will be called in TriggerDetector.cs when the raycast detects
    /// a ghost
    /// </summary>
    public void PerformBehavior()
    {
        Debug.Log("Yo dawg im a ghost!");
    }

}
