using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateGame : MonoBehaviour
{
    public Transform placeToSpawn;
    public GameObject numberPrefab;
    public GameObject arrowPrefab;

    public List<GameObject> numbers = new List<GameObject>();
    public List<GameObject> arrows = new List<GameObject>();

    public GameObject solvedText;

    public GameObject nextLevelButton;

    public AudioSource solvedSound;

    public bool solved = true;

    public bool isGenerating = false;

    [Range(1, 5)] public int difficulty = 1;

    public TMP_Text difficultyText;



    public void Start()
    {
        isGenerating = true;
        Generate();
        Debug.Log("End");
        AfterGeneration();
        isGenerating = false;
    }

    public void AfterGeneration()
    {
        isGenerating = true;
        while (CheckIfPuzzleIsSolved())
        {
            // Loop through all child objects of the placeToSpawn transform
            foreach (Transform child in placeToSpawn)
            {
                Destroy(child.gameObject); // Destroy each child GameObject
            }

            // Clear the numbers and arrows lists since all objects are destroyed
            numbers.Clear();
            arrows.Clear();
            Debug.Log(" is generating");
            Generate(); // Generate a new puzzle
        }

        Debug.Log(" is not generating");
        isGenerating = false;
    }

    public void Generate()
    {
        //int rand = UnityEngine.Random.Range(1, 2) * 2;
        //Debug.Log(rand);
        for (int i = 0; i < difficulty; i++)
        {
            GameObject numberObject = Instantiate(numberPrefab, placeToSpawn);
            numberObject.GetComponent<Number>().index = i;
            numbers.Add(numberObject);
            GameObject arrowObject = Instantiate(arrowPrefab, placeToSpawn);
            arrowObject.GetComponent<Arrow>().index = i += 1;
            arrows.Add(arrowObject);
            arrowObject.GetComponent<Arrow>().SetRandomArrowDirection();
        }
        Debug.Log("Generated");

        CalculateInitialNumberValues();
        Debug.Log("End");

    }

    public void CalculateInitialNumberValues()
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            var temp_index = 0;
            temp_index = numbers[i].GetComponent<Number>().index;
            Debug.Log(temp_index);

            for (int j = 0; j < arrows.Count; j++)
            {
                if (arrows[j].GetComponent<Arrow>().index < temp_index)
                {
                    if (arrows[j].GetComponent<Image>().sprite == arrows[j].GetComponent<Arrow>().arrowRight)
                    {
                        numbers[i].GetComponent<Number>().correctValue += 1;
                    }
                }
                else if (arrows[j].GetComponent<Arrow>().index > temp_index)
                {
                    if (arrows[j].GetComponent<Image>().sprite == arrows[j].GetComponent<Arrow>().arrowLeft)
                    {
                        numbers[i].GetComponent<Number>().correctValue += 1;
                    }
                }
            }
            numbers[i].GetComponent<Number>().text.text = numbers[i].GetComponent<Number>().correctValue.ToString();
            CalculateNumberValues();
        }
        Debug.Log("Calculated");
        for (int i = 0; i < arrows.Count; i++)
        {
            arrows[i].GetComponent<Arrow>().SetRandomArrowDirection();
        }
        CalculateNumberValues();

        Debug.Log("End");
    }

    public void CalculateNumberValues()
    {
        solved = true;
        for (int i = 0; i < numbers.Count; i++)
        {
            var temp_index = 0;
            temp_index = numbers[i].GetComponent<Number>().index;
            Debug.Log(temp_index);
            numbers[i].GetComponent<Number>().value = 0;

            for (int j = 0; j < arrows.Count; j++)
            {
                if (arrows[j].GetComponent<Arrow>().index < temp_index)
                {
                    if (arrows[j].GetComponent<Image>().sprite == arrows[j].GetComponent<Arrow>().arrowRight)
                    {
                        numbers[i].GetComponent<Number>().value += 1;
                    }
                }
                else if (arrows[j].GetComponent<Arrow>().index > temp_index)
                {
                    if (arrows[j].GetComponent<Image>().sprite == arrows[j].GetComponent<Arrow>().arrowLeft)
                    {
                        numbers[i].GetComponent<Number>().value += 1;
                    }
                }
            }

            if (numbers[i].GetComponent<Number>().value == numbers[i].GetComponent<Number>().correctValue)
            {
                numbers[i].GetComponent<Image>().color = Color.red;
            }
            else
            {
                solved = false;
                numbers[i].GetComponent<Image>().color = Color.green;
            }
        }
        // After checking all numbers, show or hide the solved text based on the result
        if (solved)
        {
            solvedText.SetActive(true); // Show solved text if everything is correct

            Debug.Log("Puzzle solved!");
            Debug.Log(isGenerating);
            // Play the audio when the puzzle is solved
            if (!isGenerating)
            {
                difficulty += 1;
                solvedSound.Play();
                nextLevelButton.SetActive(true);
            }

        }
        else
        {
            solvedText.SetActive(false); // Hide solved text if not solved
            Debug.Log("Puzzle not solved.");
        }

        Debug.Log("End");

    }

    void Update()
    {
        difficultyText.text = "Difficulty: " + difficulty;
    }

    // Method to check if the puzzle is already solved
    private bool CheckIfPuzzleIsSolved()
    {
        // Assume puzzle is solved initially
        bool isSolved = true;

        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i].GetComponent<Number>().value != numbers[i].GetComponent<Number>().correctValue)
            {
                isSolved = false; // Puzzle is not solved if any number is incorrect
                break;
            }
        }

        return isSolved; // Return true if solved, false otherwise
    }
}
