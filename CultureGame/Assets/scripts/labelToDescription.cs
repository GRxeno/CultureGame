using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class labelToDescription : MonoBehaviour
{
    public string input;

    public GameObject click;
    public GameObject hover;
    public GameObject loli;
    public GameObject canvas;

    private void Start()
    {
        //rayGuide.dialogue_num = -1;
        //input.gameObject.SetActive(false);
        //canvas.SetActive(false);

    }

    void Update()
    {
        
        if (!PauseMenu.GameIsPaused)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
         
            if (Physics.Raycast(ray, out hit, 8.0f, -1, QueryTriggerInteraction.Collide))
            {
                Transform objectHit = hit.transform;

                // Do something with the object that was hit by the raycast.
                if (objectHit.CompareTag("label"))
                {
                    //objectHit.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().color = new Color(255,215,0);

                    if (Input.GetButtonDown("Fire1"))
                    {
                        input = objectHit.GetChild(0).GetChild(0).transform.GetComponent<TextMeshProUGUI>().text;
                        loli.GetComponent<AudioSource>().Play();

                        rayGuide.isPop = true;

                        if (input != "" && rayGuide.dialogue_num == -2)
                        {
                            if (rayGuide.dialogue_num < 0)
                            {
                                rayGuide.dialogue_num = int.Parse(input) - 1;
                                TriggerDialogue(int.Parse(input) - 1);
                            }
                            else
                            {
                                FindObjectOfType<DialogueManager>().DisplayNextSentence();
                            }
                        }
                        else
                        {
                            FindObjectOfType<DialogueManager>().DisplayNextSentence();
                        }
                    }
                }
            }
        }
    }
    public void TriggerDialogue(int Index)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(ReadConvos.AllConvos[Index], false);
    }
}
