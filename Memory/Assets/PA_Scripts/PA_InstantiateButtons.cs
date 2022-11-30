using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_InstantiateButtons : MonoBehaviour
{
    public Transform Background;

    [SerializeField] private GameObject button;

    [SerializeField] private int instanceNumber;

    // Using the awake function it instanciates a number of buttons determined by a given number, gives
    // each one of them a number for a name and sets their parent to a panel with a grid layout component
    private void Awake()
    {
        for (int i = 0; i < instanceNumber; i++)
        {
            GameObject g = Instantiate(button);
            g.name = i.ToString();
            g.transform.SetParent(Background, false);
        }
    }
}
