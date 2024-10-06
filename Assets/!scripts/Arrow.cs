using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{

    public Sprite arrowLeft, arrowRight;
    public int index;
    public GenerateGame generateGame;

    // Start is called before the first frame update
    void Start()
    {
        generateGame = FindObjectOfType<GenerateGame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecalculateNumberValues()
    {
        if(generateGame.solved) return;
        generateGame.CalculateNumberValues();
    }

    public void SetRandomArrowDirection()
    {
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            GetComponent<Image>().sprite = arrowLeft;
        }
        else
        {
            GetComponent<Image>().sprite = arrowRight;
        }
    }

        public void ChangeArrowDirection()
    {
        if(generateGame.solved) return;
        if(GetComponent<Image>().sprite == arrowRight)
        {
            GetComponent<Image>().sprite = arrowLeft;
        }
        else
        {
            GetComponent<Image>().sprite = arrowRight;
        }
    }
}
