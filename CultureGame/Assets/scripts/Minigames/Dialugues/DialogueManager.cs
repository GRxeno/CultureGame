using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject canvas;
    public GameObject arrow;

    public string npc_name;

    public Animator canvas_animator;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

  
    public void StartDialogue(Dialogue dialogue, bool generic)
    {

        canvas_animator.SetBool("canPop", false);

        canvas.SetActive(true);
        arrow.SetActive(true);
        nameText.text = npc_name;
        sentences.Clear();

        if (generic) {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }
        if (!generic)
        {
            foreach (string sentence in dialogue.AllSentences)
            {
                sentences.Enqueue(sentence);
            }
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 1)
        {
            // Don't show arrow on last sentence
            arrow.SetActive(false);
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void EndDialogue()
    {
        if(rayGuide.dialogue_num == -1)
            AIscript.canWalk = true;

        canvas_animator.SetBool("canPop", true);

        rayGuide.dialogue_num = -2;
        rayGuide.isPop = false;
        canvas.SetActive(false); 
    }
}
