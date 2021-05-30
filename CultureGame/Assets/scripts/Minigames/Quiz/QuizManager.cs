using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class QuizManager : MonoBehaviour
{
    int correct_button;

    public TextMeshProUGUI timer_text;

    public int Timer;
    int currCountdownValue;

    public TextMeshProUGUI score_text;
    int score_int = 0;
    int correctAns = 0;

    public Queue<QnA> questions;
    public TextMeshProUGUI question_text;

    public TextMeshProUGUI answer1;
    public TextMeshProUGUI answer2;
    public TextMeshProUGUI answer3;
    public TextMeshProUGUI answer4;

    public GameObject answers;
    public GameObject result;

    private void Start()
    {
        timer_text.text = "";
        score_text.text = "";
        question_text.text = "PRESS START PLAY";
        questions = new Queue<QnA>();
        questions.Clear();
        result.SetActive(false);
        answers.SetActive(false);
        timer_text.text = currCountdownValue.ToString();
        currCountdownValue = Timer;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        foreach (QnA qna in ReadQnA.AllQuestions)
        {
            questions.Enqueue(qna);
        }
        DisplayNextQuestion();
    }

    public void DisplayNextQuestion()
    {
        if (questions.Count == 0)
        {
            EndGame();
            return;
        }

        QnA tmp = questions.Dequeue();
        question_text.text = tmp.Question;
        score_text.text = score_int.ToString();
        SetAnswers(tmp);

        

        StopAllCoroutines();
        StartCoroutine(StartCountdown());
    }


    public void SetAnswers(QnA tmp)
    {
        answer1.text = tmp.Answers[0];
        answer2.text = tmp.Answers[1];
        answer3.text = tmp.Answers[2];
        answer4.text = tmp.Answers[3];
        correct_button = tmp.CorrectAnswer;
    }

    IEnumerator StartCountdown()
    {

        while (currCountdownValue > -1)
        {
            timer_text.text = currCountdownValue.ToString();
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        if (currCountdownValue <= 0)
        {
            currCountdownValue = Timer;
            DisplayNextQuestion();
        }
    }

    public void isPressed(GameObject button)
    {

        //Debug.Log(button.name[7] + "   " + correct_button.ToString());
        if (button.name[7].ToString().Equals(correct_button.ToString()))
        {
            //Debug.Log("Correct");
            score_int += 10;
            correctAns++;
        }
        else
        {
            //Debug.Log("Wrong");
            score_int -= 5;
        }
    }

    void EndGame()
    {
        miniGame.score += score_int;
        score_text.text = score_int.ToString();
        question_text.text = "Your Score is:";
        answers.SetActive(false);
        result.SetActive(true);
        result.GetComponentInChildren<TextMeshProUGUI>().text = correctAns + "  /  " + ReadQnA.allAns;
    }
}
