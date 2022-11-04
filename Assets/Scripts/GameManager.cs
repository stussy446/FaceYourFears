using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI elements")]
    private int ghostCount = 0;
    [SerializeField] TMP_Text ghostCountText;

    public static GameManager Instance
    {
        get;
        private set;
    }

    /// <summary>
    /// handles increasing the ghostCount and updating ghostCount text whenever
    /// the count increases
    /// </summary>
    public int GhostCount
    {
        get => ghostCount;
        set
        {
            ghostCount = value;
            ghostCountText.text = $"Ghosts Seen: {ghostCount.ToString()}";
        }
    }

    /// <summary>
    /// Singleton patter to ensure only one GameManager
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



}
