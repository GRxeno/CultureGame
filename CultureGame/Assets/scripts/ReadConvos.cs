using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ReadConvos : MonoBehaviour
{

    public static List<Dialogue> AllConvos = new List<Dialogue>();

    List<int> myList = new List<int>();
    

    void Start()
    {
        string path = Application.streamingAssetsPath + "/txtFiles/Convos" + ".txt";

        if (!File.Exists(path))
        {
            Debug.LogError("Not found NPC conversetion file");
        }

        List<string> content = File.ReadAllLines(path).ToList();

        for (int i = 0; i < content.Count; i++)
        {
            if (content[i].Length != 0)
            {
                if (content[i][0].Equals('#'))
                {
                    myList.Add(i);
                }
            }
        }

        int from, to;
        for (int i = 0; i < myList.Count-1; i++)
        {
            from = myList[i];
            to = myList[i + 1];

            Dialogue dialogue = new Dialogue(content[from]);
            List<string> AllSentences = new List<string>();

            for (int j = from+1; j < to; j++)
            {
                if (content[j].Length != 0)
                {
                    //Debug.Log(content[j]);
                    AllSentences.Add(content[j]);
                }
            }

            dialogue.AllSentences = AllSentences;
            AllConvos.Add(dialogue);

        }


    }



}
