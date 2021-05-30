using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseTime : MonoBehaviour
{
   public void StopCameraTime()
    {
        Time.timeScale = 0f;
    }

    public void StartCameraTime()
    {
        Time.timeScale = 1f;
    }

}
