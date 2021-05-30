using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    
    public static bool isZoom = false;
    public static string curOpenName; // the current zoomed iamge


    // Update is called once per frame
    void Update()
    {
        if (!isZoom && !PauseMenu.GameIsPaused)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 8))
            {
                var hitted = hit.transform;
                if (hitted.CompareTag("Button") && this.name.Equals(hitted.name)) 
                {
                    this.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(255,215,0);

                    if (Input.GetButtonDown("Fire1"))
                    {
                        PauseMenu.GameIsPaused = true;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        curOpenName = this.name;
                        Time.timeScale = 0f;
                        this.transform.GetChild(1).gameObject.SetActive(true);
                        isZoom = true;
                    }
                }
            }
            else
            {
                this.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.Escape))
            {
                exitZoom();
            }
        }
    }

    public void exitZoom()
    {
        if (this.name.Equals(curOpenName))
        {
            PauseMenu.GameIsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            this.transform.GetChild(1).gameObject.SetActive(false);
            isZoom = false;
            curOpenName = "";
        }
    }
}
