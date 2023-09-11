using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemiesList = new List<GameObject>();
    public TextMeshProUGUI scoreText;
    public Button restartButton;
    public TextMeshProUGUI winText;
    public List<GameObject> livesList;

    float[] xPos ={ -1.6f , 0, 1.6f };
    public int score = 0;
    public bool isGameOver = false;

    

    private void Awake()
    {
        EnemyController.speed = 1f;
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    
    void Update()
    {
        scoreText.text = score.ToString();
        if (score == 100)
        {
            GameOver();
            winText.gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            int randomIndexForX = Random.Range(0, 3);
            int randomIndexForEnemy = Random.Range(0, 2);
            GameObject randomEnemy = enemiesList[randomIndexForEnemy];
            float waitTime = Random.Range(1f, 5f);
            yield return new WaitForSeconds(waitTime);
            Instantiate(randomEnemy,  new Vector2(xPos[randomIndexForX],5.6f), Quaternion.identity);                
        }
    }


    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        isGameOver = true;
        Time.timeScale = 0;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

}
