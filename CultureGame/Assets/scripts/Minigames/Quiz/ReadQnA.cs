using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ReadQnA : MonoBehaviour
{
    
    public static List<QnA> AllQuestions = new List<QnA>();
    public static int allAns = 0;

    void Start()
    {
        string path = Application.streamingAssetsPath + "/txtFiles/QnA" + ".txt";

        if (!File.Exists(path))
        {
            Debug.LogError("Not found QnA file");
        }

        List<string> content = File.ReadAllLines(path).ToList();

        for (int i = 0; i < content.Count; i+=5)
        {
            allAns++;
            if (content[i].Length != 0)
            {
                QnA qna = new QnA(content[i+0].Substring(2));
                qna.Answers[0] = content[i + 1].Substring(2);
                qna.Answers[1] = content[i + 2].Substring(2);
                qna.Answers[2] = content[i + 3].Substring(2);
                qna.Answers[3] = content[i + 4].Substring(2);

                if (content[i + 1][0] == '+')
                    qna.CorrectAnswer = 0;
                else if (content[i + 2][0] == '+')
                    qna.CorrectAnswer = 1;
                else if (content[i + 3][0] == '+')
                    qna.CorrectAnswer = 2;
                else if (content[i + 4][0] == '+')
                    qna.CorrectAnswer = 3;
                else
                    Debug.LogError("Not correct QnA file format!");

                int num = Random.Range(0, 4);

                qna.SwapAnswers(qna.CorrectAnswer, num);
                qna.CorrectAnswer = num;

                AllQuestions.Add(qna);

            }
        }


    }

}
