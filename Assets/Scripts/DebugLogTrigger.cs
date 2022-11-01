using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test class to make sure ItriggerHandler interface works
/// </summary>
public class DebugLogTrigger : MonoBehaviour, ITriggerHandler
{
    public void PerformBehavior()
    {
        Debug.Log("AHHHHHHHHHHH");
    }

}
