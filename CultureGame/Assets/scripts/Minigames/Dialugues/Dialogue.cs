using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue 
{
    public string exhibit_name;
    [TextArea(3, 10)]
    public string[] sentences;

    public List<string> AllSentences;
    
    
    public Dialogue(string name)
    {
        this.exhibit_name = name;
    }
}
