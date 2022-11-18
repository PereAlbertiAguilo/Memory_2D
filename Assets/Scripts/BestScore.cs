using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BestScore : MonoBehaviour
{
    public int bestScore1, bestScore2, bestScore3, bestScore4;

    private void Start()
    {
        if (PlayerPrefs.HasKey("BestScore1"))
        {
            bestScore1 = PlayerPrefs.GetInt("BestScore1");
        }

        if (PlayerPrefs.HasKey("BestScore2"))
        {
            bestScore2 = PlayerPrefs.GetInt("BestScore2");
        }

        if (PlayerPrefs.HasKey("BestScore3"))
        {
            bestScore3 = PlayerPrefs.GetInt("BestScore3");
        }

        if (PlayerPrefs.HasKey("BestScore4"))
        {
            bestScore4 = PlayerPrefs.GetInt("BestScore4");
        }
    }

    public void BestScore1(int i, TextMeshProUGUI t)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (bestScore1 > i || bestScore1 == 0)
            {
                bestScore1 = i;
                PlayerPrefs.SetInt("BestScore1", bestScore1);
                t.text = $"Best attempts: {bestScore1}";
            }
            else
            {
                t.text = $"Best attempts: {bestScore1}";
            }
        }
    }
    public void BestScore2(int i, TextMeshProUGUI t)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (bestScore2 > i || bestScore2 == 0)
            {
                bestScore2 = i;
                PlayerPrefs.SetInt("BestScore2", bestScore2);
                t.text = $"Best attempts: {bestScore2}";
            }
            else
            {
                t.text = $"Best attempts: {bestScore2}";
            }
        }
    }
    public void BestScore3(int i, TextMeshProUGUI t)
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (bestScore3 > i || bestScore3 == 0)
            {
                bestScore3 = i;
                PlayerPrefs.SetInt("BestScore3", bestScore3);
                t.text = $"Best attempts: {bestScore3}";
            }
            else
            {
                t.text = $"Best attempts: {bestScore3}";
            }
        }
    }
    public void BestScore4(int i, TextMeshProUGUI t)
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (bestScore4 > i || bestScore4 == 0)
            {
                bestScore4 = i;
                PlayerPrefs.SetInt("BestScore4", bestScore4);
                t.text = $"Best attempts: {bestScore4}";
            }
            else
            {
                t.text = $"Best attempts: {bestScore4}";
            }
        }
    }
}
