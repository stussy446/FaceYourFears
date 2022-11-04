using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private Button returnButton;
    [SerializeField] private TMP_Text instructionsText;

    private void Awake()
    {
        HandleButtons(true);
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void OnInstructionsClicked()
    {
        HandleButtons(false);
    }

    public void OnReturnClicked()
    {
        HandleButtons(true);
    }

    private void HandleButtons(bool active)
    {
        playButton.gameObject.SetActive(active);
        instructionsButton.gameObject.SetActive(active);
        quitButton.gameObject.SetActive(active);

        instructionsText.gameObject.SetActive(!active);
        returnButton.gameObject.SetActive(!active);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
