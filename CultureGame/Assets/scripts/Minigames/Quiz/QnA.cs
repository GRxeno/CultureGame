using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class QnA
{
    public string Question;
    public int CorrectAnswer;

    public string[] Answers = new string[4];

    public QnA(string name)
    {
        this.Question = name;
    }

    public void SwapAnswers(int index1, int index2)
    {
        string tmp = this.Answers[index1];
        this.Answers[index1] = this.Answers[index2];
        this.Answers[index2] = tmp;
    }
}
