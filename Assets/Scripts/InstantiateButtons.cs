using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateButtons : MonoBehaviour
{
    [SerializeField] private Transform Background;

    [SerializeField] private GameObject button;

    [SerializeField] private int instanceNumber;


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
