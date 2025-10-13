using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class AnswerButton : MonoBehaviour, IInteractable
{
    public TextMeshPro textDisplay;    // Drag the button's TextMeshPro here
    public Material normalMaterial;    // Drag your default material here
    public Material correctMaterial;   // Drag your green material here
    public Material wrongMaterial;     // Drag your red material here

    private QuizManager quizManager;
    private int questionIndex;
    private int buttonIndex;

    public void AssignQuizManager(QuizManager manager, int qIndex, int bIndex)
    {
        quizManager = manager;
        questionIndex = qIndex;
        buttonIndex = bIndex;
    }

    public void Interact()
    {
        if (quizManager != null)
        {
            quizManager.CheckAnswer(questionIndex, buttonIndex);
        }
    }

    public void OnHoverEnter() { }
    public void OnHoverExit() { }
}
