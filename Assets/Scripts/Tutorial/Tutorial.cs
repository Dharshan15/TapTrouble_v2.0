using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI moveText;

    void Start()
    {
        StartCoroutine(ShowRule());

    }

    void Update()
    {
        
    }
    IEnumerator ShowRule()
    {
        yield return new WaitForSeconds(3);
        tutorialText.gameObject.SetActive(true);
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }
}
