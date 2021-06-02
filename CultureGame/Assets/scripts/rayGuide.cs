using UnityEngine;
using TMPro;

public class rayGuide : MonoBehaviour
{
    public static bool isPop = false;

    public TMP_InputField input;
    
    public GameObject click;
    public GameObject hover;
    //public GameObject soundEffect;
    public GameObject canvas;
    
    public Dialogue intro_dialogue;
    public Dialogue input_dialogue;

    [SerializeField]
    public Dialogue[] Convos;

    public static int dialogue_num;

    private void Start()
    {
        dialogue_num = -1;
        input.gameObject.SetActive(false);
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
                if (objectHit.CompareTag("Button_npc"))
                {

                    if (Input.GetButtonDown("Fire1") && !isPop)
                    {
                        //Debug.Log(objectHit.name);
                        //soundEffect.GetComponent<AudioSource>().Play();
                        canvas.SetActive(true);
                        if (dialogue_num == -1)
                        {
                            TriggerDialogue(-1);
                        }
                        else if (dialogue_num == -2)
                        {
                            TriggerDialogue(-2);
                            input.gameObject.SetActive(true);
                            input.text = "";
                            input.ActivateInputField();

                        }


                        isPop = true;
                    }
                    else if (Input.GetButtonDown("Fire1"))
                    {
                        input.gameObject.SetActive(false);
                        if (input.text != "" && dialogue_num == -2)
                        {
                            if (int.Parse(input.text) <= 0 || int.Parse(input.text) > Convos.Length)
                            {
                                input.text = "";
                                FindObjectOfType<DialogueManager>().DisplayNextSentence();
                            }
                            else if (dialogue_num < 0)
                            {
                                dialogue_num = int.Parse(input.text) - 1;
                                TriggerDialogue(int.Parse(input.text) - 1);
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
        if(Index == -1)
            FindObjectOfType<DialogueManager>().StartDialogue(intro_dialogue, true);
        else if (Index == -2)
            FindObjectOfType<DialogueManager>().StartDialogue(input_dialogue, true);
        else 
            FindObjectOfType<DialogueManager>().StartDialogue(ReadConvos.AllConvos[Index], false);
    }


}
