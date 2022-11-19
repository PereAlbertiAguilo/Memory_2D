using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;

    [SerializeField] private GameObject pausePanel;

    public AudioSource _musicAudioSource;
    public AudioSource _sfxAduioSource;

    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;

    private int sfx;
    private int music;

    private void Start()
    {
        StartSFX();
        StartMusic();

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
            GetComponent<SoundEffects>().ClickSound2(_sfxAduioSource);
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
            GetComponent<SoundEffects>().ClickSound2(_sfxAduioSource);
            isPaused = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            GetComponent<GameManager>().NullButton();
        }
    }

    public void ResetGame()
    {
        GetComponent<SoundEffects>().ClickSound2(_sfxAduioSource);
        Resume();
        GetComponent<GameManager>().gameOverPanel.SetActive(false);
        GetComponent<GameManager>().ResetGame();
        CurrentButton(GameObject.Find("0"));
    }

    public void MainMenu()
    {
        GetComponent<SoundEffects>().ClickSound2(_sfxAduioSource);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void StartMusic()
    {
        music = PlayerPrefs.GetInt("music");
        if (music == 1)
        {
            Music(true);
            musicToggle.isOn = true;
        }
        else
        {
            Music(false);
            musicToggle.isOn = false;
        }
    }

    public void Music(bool b)
    {
        GetComponent<SoundEffects>().ClickSound2(_sfxAduioSource);
        if (b)
        {
            music = 1;
            PlayerPrefs.SetInt("music", music);
            _musicAudioSource.mute = false;
        }
        else
        {
            music = 0;
            PlayerPrefs.SetInt("music", music);
            _musicAudioSource.mute = true;
        }
    }

    void StartSFX()
    {
        sfx = PlayerPrefs.GetInt("sfx");
        if (sfx == 1)
        {
            SFX(true);
            sfxToggle.isOn = true;
        }
        else
        {
            SFX(false);
            sfxToggle.isOn = true;
        }
    }

    public void SFX(bool b)
    {
        GetComponent<SoundEffects>().ClickSound2(_sfxAduioSource);
        if (b)
        {
            sfx = 1;
            PlayerPrefs.SetInt("sfx", sfx);
            _sfxAduioSource.mute = false;
        }
        else
        {
            sfx = 0;
            PlayerPrefs.SetInt("sfx", sfx);
            _sfxAduioSource.mute = true;
        }
    }
    public void NextGame()
    {
        GetComponent<SoundEffects>().ClickSound2(_sfxAduioSource);
        int scene = SceneManager.GetActiveScene().buildIndex;
        int newScene = scene += 1;
        if(newScene < 5)
        {
            SceneManager.LoadScene(newScene);
        }
    }

    public void PreviousGame()
    {
        GetComponent<SoundEffects>().ClickSound2(_sfxAduioSource);
        int scene = SceneManager.GetActiveScene().buildIndex;
        int newScene = scene -= 1;
        if (newScene > 0)
        {
            SceneManager.LoadScene(newScene);
        }
    }
}
