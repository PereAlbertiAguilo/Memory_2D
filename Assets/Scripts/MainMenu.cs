using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelectorPanel, optionsPanel, mainMenuPanle;
    public GameObject mainButton, optionsButton, levelsButton, mainButton2;

    private Animator _animator;

    private bool canSwitch = true;

    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    private SoundEffects soundEffectsScript;

    private int music;
    private int sfx;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        soundEffectsScript = sfxAudioSource.GetComponent<SoundEffects>();

        StartSFX();
        StartMusic();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainButton);
    }

    private void Update()
    {
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

    public void LevelsToMain()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        levelSelectorPanel.SetActive(true);
        mainMenuPanle.SetActive(true);
        _animator.Play("LevelsToMain");
        StartCoroutine(Delay(levelSelectorPanel, mainButton));
    }
    public void LevelSelector()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        mainMenuPanle.SetActive(true);
        levelSelectorPanel.SetActive(true);
        _animator.Play("MainMenuUp");
        StartCoroutine(Delay(mainMenuPanle, levelsButton));
    }

    public void OptionsToMain()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        optionsPanel.SetActive(true);
        mainMenuPanle.SetActive(true);
        _animator.Play("OptionsToMain");
        StartCoroutine(Delay(optionsPanel, mainButton2));
    }
    public void Options()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        mainMenuPanle.SetActive(true);
        optionsPanel.SetActive(true);
        _animator.Play("MainMenuDown");
        StartCoroutine(Delay(mainMenuPanle, optionsButton));
    }
    public void Exit()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        Application.Quit();
    }
    public void Level1()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        LoadScene(1);
    }
    public void Level2()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        LoadScene(2);
    }
    public void Level3()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        LoadScene(3);
    }
    public void Level4()
    {
        soundEffectsScript.ClickSound2(sfxAudioSource);
        LoadScene(4);
    }

    void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
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
