using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    public GameObject player;
    public GameObject sound;
    private AudioSource music;
    Vector3 position;
    Vector3 prevPos;
    

     void Start()
    {
        prevPos = player.transform.position;
        music = sound.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {

        StartCoroutine(StartRoutine());


    }

    IEnumerator StartRoutine()
    {
        position = player.transform.position;
        while ((Mathf.Round(position.x) != Mathf.Round(prevPos.x) || Mathf.Round(position.z) != Mathf.Round(prevPos.z)) && music.isPlaying == false)
        {
            prevPos = position;
           

            music.volume = Random.Range(0.5f, 1f);
            music.pitch = Random.Range(0.8f, 1f);
            music.Play();
            yield return null;


        }

        prevPos = position;
    }
} 
