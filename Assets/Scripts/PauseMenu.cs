using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;

    [SerializeField] private GameObject pausePanel;

    public AudioSource _musicAudioSource;
    public AudioSource _sfxAduioSource;
    

    private void Start()
    {
        CurrentButton(GameObject.Find("0"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void CurrentButton(GameObject g)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(g);
    }

    public void Pause()
    {
        if (!GetComponent<GameManager>().isGameOver)
        {
            isPaused = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            CurrentButton(GameObject.Find("Resume"));
        }
    }

    public void Resume()
    {
        if (!GetComponent<GameManager>().isGameOver)
        {
            isPaused = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            GetComponent<GameManager>().NullButton();
        }
    }

    public void ResetGame()
    {
        Resume();
        GetComponent<GameManager>().gameOverPanel.SetActive(false);
        GetComponent<GameManager>().ResetGame();
        CurrentButton(GameObject.Find("0"));
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void MusicToggle(bool b)
    {
        if (b)
        {
            _musicAudioSource.Play();
        }
        else
        {
            _musicAudioSource.Pause();
        }
    }

    public void SFXToggle(bool b)
    {
        if (b)
        {
            _sfxAduioSource.mute = false;
        }
        else
        {
            _sfxAduioSource.mute = true;
        }
    }
    public void NextGame()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        int newScene = scene += 1;
        if(newScene < 5)
        {
            SceneManager.LoadScene(newScene);
        }
    }

    public void PreviousGame()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        int newScene = scene -= 1;
        if (newScene > 0)
        {
            SceneManager.LoadScene(newScene);
        }
    }
}
