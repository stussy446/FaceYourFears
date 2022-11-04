using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleGoal : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            messageText.text = "Congratulations! You made it out!";
            messageText.gameObject.SetActive(true);
            StartCoroutine(RestartProcess());
        }
    }

    private IEnumerator RestartProcess()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
