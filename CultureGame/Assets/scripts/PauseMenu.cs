using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    //public GameObject panelUi;
    //public GameObject pauseMenuUi;
    //public GameObject pauseMenuOptions;
    //public GameObject crosshair;

    public List<GameObject> ListOfObjectsToHideWhenPaused = new List<GameObject>();
    public List<GameObject> ListOfObjectsToShowWhenPaused = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                this.GetComponent<TrelloUI>().TakeScreenshot();
                //TrelloUI.TakeScreenshot();
                Pause();
            }
        }
    }

    public void Resume()
    {
        foreach(GameObject obj in ListOfObjectsToHideWhenPaused)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in ListOfObjectsToShowWhenPaused)
        {
            obj.SetActive(false);
        }
        //panelUi.SetActive(false);
        //pauseMenuUi.SetActive(false);
        //pauseMenuOptions.SetActive(false);
        //crosshair.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPaused = false;
    }

    void Pause()
    {
        foreach (GameObject obj in ListOfObjectsToHideWhenPaused)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in ListOfObjectsToShowWhenPaused)
        {
            obj.SetActive(true);
        }
        //panelUi.SetActive(true);
        //pauseMenuUi.SetActive(true);
        //crosshair.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}


