using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isDead = true;
    public int score = 0;
    int bestScore;
    public Text bestScoreText;
    public Text scoreText;
    public GameObject menuManager;
    public GameObject scoreManager;
    public GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    { 
       
    }
    private void Update()
    {
        bestScore = PlayerPrefs.GetInt("HighScore");
        bestScoreText.text = "High Score : " + bestScore.ToString();
        if (Input.GetMouseButtonDown(0))
        {
            GameStart();
        }
        scoreText.text = score.ToString();
    }
    public void GameStart()
    {
        player.GetComponent<Rigidbody>().isKinematic = false;
        menuManager.SetActive(false);
        scoreManager.SetActive(true);
        isDead = false;
    }
    public void GameOver ()
    {
        SaveHighScore();
        isDead = true;
        StartCoroutine(reStart());
    }
    IEnumerator reStart()
    {
        yield return new WaitForSeconds(1.0f);
        scoreManager.SetActive(false);
        menuManager.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void SaveHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore",score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore",bestScore);
        }
    }
}
