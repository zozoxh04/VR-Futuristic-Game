using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class QuizQuestion
    {
        [TextArea(3,10)]
        public string code;           // Full code snippet
        public string blankWord;      // Word to be replaced with ____
        public string[] answers = new string[3];  // Must be exactly 3 answers
        public int correctAnswer;     // 0, 1, or 2
        
        // New fields to support distributed questions
        public Transform questionLocation;  // Where this question should appear in the scene
        public TextMeshPro codeBoardText;   // Specific text display for this question
        public AnswerButton[] specificAnswerButtons;  // Buttons specific to this question
        public bool isAnswered = false;  // Track if this question has been answered
    }

    public List<QuizQuestion> questions = new List<QuizQuestion>();
    private int currentQuestion = 0;

    void Start()
    {
        InitializeQuestions();
    }

    void InitializeQuestions()
    {
        // Deactivate all question locations initially
        foreach (var question in questions)
        {
            // Disable all question locations and their associated UI elements
            if (question.questionLocation != null)
            {
                question.questionLocation.gameObject.SetActive(false);
            }
            
            // Disable specific answer buttons
            foreach (var button in question.specificAnswerButtons)
            {
                button.gameObject.SetActive(false);
            }
            
            if (question.codeBoardText != null)
            {
                question.codeBoardText.gameObject.SetActive(false);
            }
        }

        // If we have questions, activate the first question's location
        if (questions.Count > 0)
        {
            ActivateQuestion(0);
        }
    }

    void ActivateQuestion(int questionIndex)
    {
        if (questionIndex < 0 || questionIndex >= questions.Count) return;

        var currentQuizQuestion = questions[questionIndex];

        // Activate the specific question location
        if (currentQuizQuestion.questionLocation != null)
        {
            currentQuizQuestion.questionLocation.gameObject.SetActive(true);
        }

        // Show code with blank if not already answered
        if (currentQuizQuestion.codeBoardText != null)
        {
            currentQuizQuestion.codeBoardText.gameObject.SetActive(true);
            
            if (!currentQuizQuestion.isAnswered)
            {
                currentQuizQuestion.codeBoardText.text = currentQuizQuestion.code.Replace(
                    currentQuizQuestion.blankWord, "____");
            }
            else
            {
                // If already answered, show full code
                currentQuizQuestion.codeBoardText.text = currentQuizQuestion.code;
            }
        }

        // Activate and set up specific answer buttons
        for (int i = 0; i < currentQuizQuestion.specificAnswerButtons.Length; i++)
        {
            var button = currentQuizQuestion.specificAnswerButtons[i];
            button.gameObject.SetActive(true);
            button.textDisplay.text = currentQuizQuestion.answers[i];
            
            // Reset material if not already answered
            if (!currentQuizQuestion.isAnswered)
            {
                button.GetComponent<Renderer>().material = button.normalMaterial;
            }
            else
            {
                // If already answered, turn all buttons yellow
                button.GetComponent<Renderer>().material = button.correctMaterial;
            }
            
            // Assign a reference to this specific quiz question
            button.AssignQuizManager(this, questionIndex, i);
        }
    }

    public void CheckAnswer(int questionIndex, int buttonIndex)
    {
        if (questionIndex < 0 || questionIndex >= questions.Count) return;

        var currentQuizQuestion = questions[questionIndex];
        
        // If already answered, do nothing
        if (currentQuizQuestion.isAnswered) return;

        bool isCorrect = buttonIndex == currentQuizQuestion.correctAnswer;

        // Change button color
        var answerButton = currentQuizQuestion.specificAnswerButtons[buttonIndex];
        answerButton.GetComponent<Renderer>().material = 
            isCorrect ? answerButton.correctMaterial : answerButton.wrongMaterial;

        if (isCorrect)
        {
            // Show complete code
            if (currentQuizQuestion.codeBoardText != null)
            {
                currentQuizQuestion.codeBoardText.text = currentQuizQuestion.code;
            }

            // Mark as answered
            currentQuizQuestion.isAnswered = true;

            // Turn all buttons yellow after 2 seconds
            Invoke("TurnAllButtonsYellow", 2f);

            // Move to next question after delay
            Invoke("MoveToNextQuestion", 2f);
        }
    }

    void TurnAllButtonsYellow()
    {
        if (currentQuestion < questions.Count)
        {
            var currentQuizQuestion = questions[currentQuestion];
            foreach (var button in currentQuizQuestion.specificAnswerButtons)
            {
                button.GetComponent<Renderer>().material = button.correctMaterial;
            }
        }
    }

    void MoveToNextQuestion()
    {
        // Move to next question
        currentQuestion++;
        
        // Activate next question if available
        if (currentQuestion < questions.Count)
        {
            ActivateQuestion(currentQuestion);
        }
    }
}
