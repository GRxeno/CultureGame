using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class changeScenes : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadPreviousLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public static void LoadSpecificLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    IEnumerator LoadLevel(int levelIndex)
    {

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

    }
}
