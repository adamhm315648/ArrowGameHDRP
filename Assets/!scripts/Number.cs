using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Number : MonoBehaviour
{

    public int index;
    public int value;
    public int correctValue;
    public TMP_Text text;

    public GenerateGame generateGame;

    // Start is called before the first frame update
    void Start()
    {
        generateGame = FindObjectOfType<GenerateGame>();
    }

    public void SetCorrectNumber()
    {
        value = correctValue;
        Debug.Log("Set correct number");        
    }
}
