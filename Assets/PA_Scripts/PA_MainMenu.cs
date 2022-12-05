using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PA_MainMenu : MonoBehaviour
{
    public GameObject levelSelectorPanel, optionsPanel, mainMenuPanle;
    public GameObject mainButton, optionsButton, levelsButton, mainButton2;

    private Animator _animator;

    private bool canSwitch = true;

    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    private PA_SoundEffects soundEffectsScript;

    private int music;
    private int sfx;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        soundEffectsScript = sfxAudioSource.GetComponent<PA_SoundEffects>();

        // Starts a functions at the start of the game
        StartSFX();
        StartMusic();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainButton);
    }

    private void Update()
    {
        // Lets the player navigate throw the UI using only the keyboard or a controller
        if (levelSelectorPanel.activeInHierarchy && !mainMenuPanle.activeInHierarchy && canSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                LevelsToMain();
            }
        }
        if (optionsPanel.activeInHierarchy && !mainMenuPanle.activeInHierarchy && canSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                OptionsToMain();
            }
        }
    }

    // Coroutine that makes sure that you cant interact with hte buttons within a certain amount of time so it may not brake a series of animations
    IEnumerator Delay(GameObject menu, GameObject button)
    {
        canSwitch = false;
        EventSystem.current.SetSelectedGameObject(null);
        button.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(2.5f);
        menu.SetActive(false);
        button.GetComponent<Button>().interactable = true;
        EventSystem.current.SetSelectedGameObject(button);
        canSwitch = true;
    }

    // Functon of a button that goes back to the main menu and plays an animation
    public void LevelsToMain()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        levelSelectorPanel.SetActive(true);
        mainMenuPanle.SetActive(true);
        _animator.Play("LevelsToMain");
        StartCoroutine(Delay(levelSelectorPanel, mainButton));
    }

    // Functon of a button that goes to the level selector panel and plays an animation
    public void LevelSelector()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        mainMenuPanle.SetActive(true);
        levelSelectorPanel.SetActive(true);
        _animator.Play("MainMenuUp");
        StartCoroutine(Delay(mainMenuPanle, levelsButton));
    }

    // Functon of a button that goes back to the main menu panel and plays an animation
    public void OptionsToMain()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        optionsPanel.SetActive(true);
        mainMenuPanle.SetActive(true);
        _animator.Play("OptionsToMain");
        StartCoroutine(Delay(optionsPanel, mainButton2));
    }

    // Functon of a button that goes to the options panel and plays an animation
    public void Options()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        mainMenuPanle.SetActive(true);
        optionsPanel.SetActive(true);
        _animator.Play("MainMenuDown");
        StartCoroutine(Delay(mainMenuPanle, optionsButton));
    }

    // Functon of a button that exits the game
    public void Exit()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        Application.Quit();
    }

    // Functon of a button that loads a scene
    public void Level1()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        LoadScene(1);
    }

    // Functon of a button that loads a scene
    public void Level2()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        LoadScene(2);
    }

    // Functon of a button that loads a scene
    public void Level3()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        LoadScene(3);
    }

    // Functon of a button that loads a scene
    public void Level4()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        LoadScene(4);
    }

    // Functon of a button that loads a scene determined by a give index number
    void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
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
        soundEffectsScript.ClickSound2(sfxAudioSource);
        if (b)
        {
            music = 1;
            PlayerPrefs.SetInt("music", music);
            musicAudioSource.mute = false;
        }
        else
        {
            music = 0;
            PlayerPrefs.SetInt("music", music);
            musicAudioSource.mute = true;
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
        soundEffectsScript.ClickSound2(sfxAudioSource);
        if (b)
        {
            sfx = 1;
            PlayerPrefs.SetInt("sfx", sfx);
            sfxAudioSource.mute = false;
        }
        else
        {
            sfx = 0;
            PlayerPrefs.SetInt("sfx", sfx);
            sfxAudioSource.mute = true;
        }
    }
}
