using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite buttonSprite;

    public Sprite[] imagesSprites;

    public List<Sprite> piecesImages = new List<Sprite>();

    public List<Button> buttons = new List<Button>();


    private bool firstGuess, secondGuess;

    private int guessCounter;
    private int correctGuessCounter;
    private int totalGuesses;
    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessName, secondGuessName; 

    private void Awake()
    {
        imagesSprites = Resources.LoadAll<Sprite>("Sprites/images");
    }

    private void Start()
    {
        ButtonsList();
        AddFunction();
        AddPieces();
        RandomizePiecesOrder(piecesImages);

        totalGuesses = piecesImages.Count / 2;
    }

    void ButtonsList()
    {
        GameObject[] instances = GameObject.FindGameObjectsWithTag("Piece");

        for (int i = 0; i < instances.Length; i++)
        {
            buttons.Add(instances[i].GetComponent<Button>());
            buttons[i].image.sprite = buttonSprite;
        }
    }

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

    void AddFunction()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => ButtonSelected());
        }
    }

    public void ButtonSelected()
    {
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessName = piecesImages[firstGuessIndex].name;

            buttons[firstGuessIndex].image.sprite = piecesImages[firstGuessIndex];
            buttons[firstGuessIndex].interactable = false;
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessName = piecesImages[secondGuessIndex].name;

            buttons[secondGuessIndex].image.sprite = piecesImages[secondGuessIndex];
            buttons[firstGuessIndex].interactable = true;

            guessCounter++;

            StartCoroutine(CheckIfNamesMatch());
        }
    }

    IEnumerator CheckIfNamesMatch()
    {
        yield return new WaitForSeconds(0.8f);

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

    void IsGameOver()
    {
        correctGuessCounter++;

        if(correctGuessCounter == totalGuesses)
        {
            print("gameover");
        }
    }

}
