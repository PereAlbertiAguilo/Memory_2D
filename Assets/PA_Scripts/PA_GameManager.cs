using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PA_GameManager : MonoBehaviour
{
    [SerializeField] private Sprite buttonSprite;

    public Sprite[] imagesSprites;

    private List<Sprite> piecesImages = new List<Sprite>();

    private List<Button> buttons = new List<Button>();

    public TextMeshProUGUI attempts;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI bestScoreText;

    public GameObject gameOverPanel;
    public Image gameOverImage;

    private bool firstGuess, secondGuess;
    public bool isGameOver;

    public bool victory;
    private bool isTimerOn = true;

    public int guessCounter;
    private int correctGuessCounter;
    private int totalGuesses;
    private int firstGuessIndex, secondGuessIndex;
    

    [SerializeField] private float timeRemaining = 60.0f;
    private float maxTime;

    private string firstGuessName, secondGuessName; 

    private void Start()
    {
        // At the beguining sets the max time to be the time remaining
        maxTime = timeRemaining;

        // Executes all the functions at the start
        ButtonsList();
        AddFunction();
        AddPieces();
        RandomizePiecesOrder(piecesImages);

        // Determines the amaunt of correct guesses the player has to make in order to win
        totalGuesses = piecesImages.Count / 2;

        isGameOver = false;
    }

    private void Update()
    {
        NullButton();

        // Starts a count down timer
        if (isTimerOn)
        {
            timeRemaining -= Time.deltaTime;
            victory = false;
        }

        // If the timer runs out the the game stops and you lose
        if (timeRemaining <= 0.0f)
        {
            timeRemaining = 0;
            victory = false;
            isGameOver = true;
            gameOverPanel.SetActive(true);
            GameOver();
        }

        Timer();

        // Displays a text with the current attempts of the player
        attempts.text = "Attempts : " + guessCounter;
    }

    // Function that makes sure that there is always a button selected
    public void NullButton()
    {
        if (!isGameOver)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                int i = 0;
                Transform bg = GetComponent<PA_InstantiateButtons>().Background;
                while (i >= buttons.Count || !bg.GetChild(i).GetComponent<Button>().interactable)
                {
                    i++;
                }
                EventSystem.current.SetSelectedGameObject(bg.GetChild(i).gameObject);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (Input.anyKeyDown && EventSystem.current.currentSelectedGameObject == null)
            {
                GetComponent<PA_PauseMenu>().CurrentButton(GameObject.Find("Restart2"));
            }
        }
    }

    // Function that converts the timeRemaining into minutes and seconds and display the result in a text
    private void Timer()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = $"{minutes} : {seconds}";
    }

    // Function that resets the game by reloading the current active scene
    public void ResetGame()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
    }

    // Function that finds all the buttons of the scene with a "Piece" tag, assigns a default sprite and puts them one by one into a list
    void ButtonsList()
    {
        GameObject[] instances = GameObject.FindGameObjectsWithTag("Piece");

        for (int i = 0; i < instances.Length; i++)
        {
            buttons.Add(instances[i].GetComponent<Button>());
            buttons[i].image.sprite = buttonSprite;
        }
    }


    // Function that makes sure that there are 2 of the same pieces determined by an sprite array that gets the first half in a list an loops a secon time
    void AddPieces()
    {
        int looper = buttons.Count;
        int index = 0;

        for(int i = 0; i < looper; i++)
        {
            if(index == looper / 2)
            {
                index = 0;
            }
            piecesImages.Add(imagesSprites[index]);

            index++;
        }
    }

    // Function that randomizes the order of a list of sprites
    void RandomizePiecesOrder(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite current = list[i];

            int randomIndex = Random.Range(i, list.Count);

            list[i] = list[randomIndex];
            list[randomIndex] = current;
        }
    }

    // Function that adds a listener to each button of a list of buttons
    void AddFunction()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => ButtonSelected());
        }
    }


    // Function for a button
    public void ButtonSelected()
    {
        // If you click 1 time saves the name of the current selected gameObject and changes the sprite for the same index number as it's name
        if (!firstGuess)
        {
            GetComponent<PA_ParticleEffect>().ParticleInstance(EventSystem.current.currentSelectedGameObject.transform.position);
            GetComponent<PA_SoundEffects>().ClickSound();
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessName = piecesImages[firstGuessIndex].name;

            buttons[firstGuessIndex].image.sprite = piecesImages[firstGuessIndex];
        }
        // If you click a 2nd time and its not the same gameobject as the first guess does the same thing as the first time and adds an attempt
        else if (!secondGuess && EventSystem.current.currentSelectedGameObject != buttons[firstGuessIndex].gameObject)
        {
            GetComponent<PA_ParticleEffect>().ParticleInstance(EventSystem.current.currentSelectedGameObject.transform.position);
            GetComponent<PA_SoundEffects>().ClickSound();
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessName = piecesImages[secondGuessIndex].name;

            buttons[secondGuessIndex].image.sprite = piecesImages[secondGuessIndex];

            guessCounter++;

            // Starts a corutine
            StartCoroutine(CheckIfNamesMatch());
        }
    }

    // Coroutine that determinates if the first guess and the second guess have the same name if it's true
    // disables the buttons and checks if isGameOver else resets their sprites to the default sprite
    IEnumerator CheckIfNamesMatch()
    {
        yield return new WaitForSeconds(0.6f);

        if(firstGuessName == secondGuessName)
        {
            yield return new WaitForSeconds(0.2f);

            buttons[firstGuessIndex].interactable = false;
            buttons[secondGuessIndex].interactable = false;

            buttons[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            buttons[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            IsGameOver();
        }
        else
        {
            yield return new WaitForSeconds(0.2f);

            buttons[firstGuessIndex].image.sprite = buttonSprite;
            buttons[secondGuessIndex].image.sprite = buttonSprite;
        }
        firstGuess = false;
        secondGuess = false;
    }

    // Function that checks if every time you make a guess the total correct guesses are the same as the total guesses you can make in the
    // game and stops the timer, sets that you have won, start the gameOver fnction and determinates the best score of teh current game
    void IsGameOver()
    {
        correctGuessCounter++;

        if(correctGuessCounter == totalGuesses)
        {
            GetComponent<PA_BestScore>().BestScore1(guessCounter, bestScoreText);
            GetComponent<PA_BestScore>().BestScore2(guessCounter, bestScoreText);
            GetComponent<PA_BestScore>().BestScore3(guessCounter, bestScoreText);
            GetComponent<PA_BestScore>().BestScore4(guessCounter, bestScoreText);
            isTimerOn = false;
            victory = true;
            isGameOver = true;
            gameOverPanel.SetActive(true);
            GameOver();
        }
    }


    // Function that executes the gameover, determinates if you have won or lost and
    // loads a gameover panel that changes depending on whether you have won or lost
    void GameOver()
    {
        if (isGameOver)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Restart2"));

            if (victory)
            {
                gameOverImage.sprite = Resources.Load<Sprite>("Sprites/victory");
                currentScore.text = $"You used {guessCounter} attempts and had {Mathf.Round(timeRemaining)} seconds left";
            }
            else
            {
                gameOverImage.sprite = Resources.Load<Sprite>("Sprites/defeat");
                currentScore.text = $"Good luck next time, you can try again as many times as you want";
            }
        }
    }
}
