using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PA_PauseMenu : MonoBehaviour
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
        // Makes it posible for the player to be able to pause the game using only the keyboard or a controller
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

    // Function that sets the current selected gameObject by the one given
    public void CurrentButton(GameObject g)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(g);
    }

    // Function of a button that pauses the game if th game isn't over
    public void Pause()
    {
        if (!GetComponent<PA_GameManager>().isGameOver)
        {
            GetComponent<PA_SoundEffects>().ClickSound2(_sfxAduioSource);
            isPaused = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            CurrentButton(GameObject.Find("Resume"));
        }
    }


    // Function of a button that resumes the game if th game isn't over
    public void Resume()
    {
        if (!GetComponent<PA_GameManager>().isGameOver)
        {
            GetComponent<PA_SoundEffects>().ClickSound2(_sfxAduioSource);
            isPaused = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            GetComponent<PA_GameManager>().NullButton();
        }
    }

    // Function of a button that resets the active scene
    public void ResetGame()
    {
        GetComponent<PA_SoundEffects>().ClickSound2(_sfxAduioSource);
        Resume();
        GetComponent<PA_GameManager>().gameOverPanel.SetActive(false);
        GetComponent<PA_GameManager>().ResetGame();
        CurrentButton(GameObject.Find("0"));
    }

    // Function of a button that loads the main menu scene
    public void MainMenu()
    {
        GetComponent<PA_SoundEffects>().ClickSound2(_sfxAduioSource);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    // Functon sets the saved value of an int to determinates whether a toggle should be on or off 
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

    // Functon of a toggle that sets an audioSource mute or unmute determined by the toggle value and saves a key of whether it's muted or unmuted using an int
    public void Music(bool b)
    {
        GetComponent<PA_SoundEffects>().ClickSound2(_sfxAduioSource);
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

    // Functon sets the saved value of an int to determinates whether a toggle should be on or off 
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
            sfxToggle.isOn = false;
        }
    }

    // Functon of a toggle that sets an audioSource mute or unmute determined by the toggle value and saves a key of whether it's muted or unmuted using an int
    public void SFX(bool b)
    {
        GetComponent<PA_SoundEffects>().ClickSound2(_sfxAduioSource);
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

    // Function of a button that loads the next scene depending of the build setting index
    public void NextGame()
    {
        GetComponent<PA_SoundEffects>().ClickSound2(_sfxAduioSource);
        int scene = SceneManager.GetActiveScene().buildIndex;
        int newScene = scene += 1;
        if(newScene < 5)
        {
            SceneManager.LoadScene(newScene);
        }
    }

    // Function of a button that loads the previous scene depending of the build setting index
    public void PreviousGame()
    {
        GetComponent<PA_SoundEffects>().ClickSound2(_sfxAduioSource);
        int scene = SceneManager.GetActiveScene().buildIndex;
        int newScene = scene -= 1;
        if (newScene > 0)
        {
            SceneManager.LoadScene(newScene);
        }
    }
}
