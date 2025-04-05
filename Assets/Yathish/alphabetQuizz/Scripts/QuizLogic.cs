using UnityEngine;
using UnityEngine.UI;
using Oculus.Interaction;
using UnityEngine.Events;
using System;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public enum Options
    {
        Option1,
        Option2,
        Option3
    }

    [System.Serializable]
    public class Question
    {
        public Sprite questionImage;
        public Material[] Options = new Material[3];
        public Options correctOption;
    }

    [SerializeField] private Question[] questions;
    private int currentQuestionIndex = 0;

    [SerializeField] private GameObject OptionReference1;
    [SerializeField] private GameObject OptionReference2;
    [SerializeField] private GameObject OptionReference3;

    [SerializeField] private GameObject OptionImage1;
    [SerializeField] private GameObject OptionImage2;
    [SerializeField] private GameObject OptionImage3;

    [SerializeField] private Image QuestionImageReference;

    void Start()
    {
        SetupPokeEvent();
        DisplayQuestion(0);
    }

    void SetupPokeEvent()
    {
        OptionReference1.GetComponent<PointableUnityEventWrapper>().WhenSelect.AddListener(pointerEvent => OnSelectEvent(pointerEvent, Options.Option1));
        OptionReference2.GetComponent<PointableUnityEventWrapper>().WhenSelect.AddListener(pointerEvent => OnSelectEvent(pointerEvent, Options.Option2));
        OptionReference3.GetComponent<PointableUnityEventWrapper>().WhenSelect.AddListener(pointerEvent => OnSelectEvent(pointerEvent, Options.Option3));
    }

    void OnSelectEvent(PointerEvent pointerEvent, Options option)
    {
        //Debug.Log("Clicked");
        if (CheckAnswer(option, questions[currentQuestionIndex]))
        {
            Debug.Log("Correct Answer!");

            if (currentQuestionIndex >= questions.Length - 1) return;

            currentQuestionIndex++;
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            Debug.Log("Wrong Answer! Try again.");
        }
    }

    void DisplayQuestion(int index)
    {
        QuestionImageReference.sprite = questions[index].questionImage;

        OptionImage1.GetComponent<MeshRenderer>().material = questions[index].Options[0];
        OptionImage2.GetComponent<MeshRenderer>().material = questions[index].Options[1];
        OptionImage3.GetComponent<MeshRenderer>().material = questions[index].Options[2];
    }

    public bool CheckAnswer(Options option, Question question)
    {
        return option == question.correctOption;
    }
}
