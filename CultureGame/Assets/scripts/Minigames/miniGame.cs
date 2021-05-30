using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniGame : MonoBehaviour
{

    public static int score;

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 8))
            {
                var hitted = hit.transform;
                if (hitted.CompareTag("Button") && this.name.Equals(hitted.name))
                {
                    this.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(255, 215, 0);

                    if (Input.GetButtonDown("Fire1"))
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        if(this.name.Equals("QuizGame"))
                            changeScenes.LoadSpecificLevel("QuizScene");
                    }
                }
            }
            else
            {
                this.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
    }

}
