using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{


    public CanvasGroup canvasGroup;
    public TMP_Text text;

    void DisplayMessage(string message)
    {
        canvasGroup.alpha = 1; // Show the canvas
        text.text = message;   // Update the text
    }

    void OnDisableText()
    {
        canvasGroup.alpha = 0;
    }

    void OnEnable()
    {
        PointToPc.OnDisplayTextEvent += DisplayMessage; // Subscribe to the event
        PointToPc.OnDisableTextEvent += OnDisableText;
    }

    void OnDisable()
    {
        PointToPc.OnDisplayTextEvent -= DisplayMessage; // Unsubscribe from the event
        PointToPc.OnDisableTextEvent -= OnDisableText;
    }
}
