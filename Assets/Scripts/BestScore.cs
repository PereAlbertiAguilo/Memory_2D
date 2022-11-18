using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BestScore : MonoBehaviour
{
    private int bestScore1, bestScore2, bestScore3, bestScore4;

    private void Start()
    {
        print(""+ PlayerPrefs.GetInt("BestScore1") + PlayerPrefs.GetInt("BestScore2") + PlayerPrefs.GetInt("BestScore3") + PlayerPrefs.GetInt("BestScore4"));

        if (PlayerPrefs.HasKey("BestScore1"))
        {
            bestScore1 = PlayerPrefs.GetInt("BestScore1");
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {bestScore1}";
            }
        }
        if (PlayerPrefs.HasKey("BestScore2"))
        {
            bestScore1 = PlayerPrefs.GetInt("BestScore2");
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {bestScore2}";
            }
        }
        if (PlayerPrefs.HasKey("BestScore3"))
        {
            bestScore1 = PlayerPrefs.GetInt("BestScore3");
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {bestScore3}";
            }
        }
        if (PlayerPrefs.HasKey("BestScore4"))
        {
            bestScore1 = PlayerPrefs.GetInt("BestScore4");
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {bestScore4}";
            }
        }
    }

    private void Update()
    {
        if (GetComponent<GameManager>().isGameOver && GetComponent<GameManager>().victory)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {PlayerPrefs.GetInt("BestScore1")}";

                if (GetComponent<GameManager>().guessCounter < bestScore1 || bestScore1 == 0 || bestScore1 == 1 || bestScore1 == 2)
                {
                    bestScore1 = GetComponent<GameManager>().guessCounter;
                    PlayerPrefs.SetInt("BestScore1", bestScore1);
                }

                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {PlayerPrefs.GetInt("BestScore1")}";
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {PlayerPrefs.GetInt("BestScore2")}";

                if (GetComponent<GameManager>().guessCounter < bestScore2 || bestScore2 == 0 || bestScore2 == 1 || bestScore2 == 2)
                {
                    bestScore2 = GetComponent<GameManager>().guessCounter;
                    PlayerPrefs.SetInt("BestScore2", bestScore2);
                }

                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {PlayerPrefs.GetInt("BestScore2")}";
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {PlayerPrefs.GetInt("BestScore3")}";

                if (GetComponent<GameManager>().guessCounter < bestScore3 || bestScore3 == 0 || bestScore3 == 1 || bestScore3 == 2)
                {
                    bestScore3 = GetComponent<GameManager>().guessCounter;
                    PlayerPrefs.SetInt("BestScore3", bestScore3);
                }

                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {PlayerPrefs.GetInt("BestScore3")}";
            }
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {PlayerPrefs.GetInt("BestScore4")}";

                if (GetComponent<GameManager>().guessCounter < bestScore4 || bestScore4 == 0 || bestScore4 == 1 || bestScore4 == 2)
                {
                    bestScore4 = GetComponent<GameManager>().guessCounter;
                    PlayerPrefs.SetInt("BestScore4", bestScore4);
                }

                GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {PlayerPrefs.GetInt("BestScore4")}";
            }
            /*
            if (GetComponent<GameManager>().victory)
            {
                




                
                BestScoreManager(bestScore1, 1, "BestScore1");
                BestScoreManager(bestScore2, 2, "BestScore2");
                BestScoreManager(bestScore3, 3, "BestScore3");
                BestScoreManager(bestScore4, 4, "BestScore4");
                
                print("" + bestScore1 + bestScore2 + bestScore3 + bestScore4);
                
            }
            */
        }
    }

    /*
    void BestScoreManager(int i, int scene, string key)
    {
        if (SceneManager.GetActiveScene().buildIndex == scene)
        {
            if (GetComponent<GameManager>().guessCounter <= i || i == 0)
            {
                i = GetComponent<GameManager>().guessCounter;
                PlayerPrefs.SetInt(key, i);
            }

            GetComponent<GameManager>().bestScoreText.text = $"Best attempts: {PlayerPrefs.GetInt(key)}";
        }
    }
    */
}
