using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using System;

public class ReadBO : MonoBehaviour
{
    GameObject[] allStands;
    int curStand = 0;
    string picsPath;
    int curFrame = 1;
    string txtPath;
    int curBook = 1;

    public GameObject framePrefab;
    public GameObject bookPrefab;

    void Start()
    {
        allStands =  GameObject.FindGameObjectsWithTag("stand");
        
        txtPath = Application.streamingAssetsPath + "/BOFiles/txt/";
        picsPath = Application.streamingAssetsPath + "/BOFiles/pics/";

        makeBooks();
        curStand = 0;
        makeFrames();

    }

    public void makeFrames()
    {
        foreach (string path in Directory.GetFiles(picsPath))
        {
            if (!path.Contains(".meta"))
            {
                createFrame(allStands[curStand], IMG2Sprite.LoadNewSprite(path));
                curStand++;
            }
        }
    }

    public void makeBooks()
    {
        foreach (string path in Directory.GetFiles(txtPath))
        {
            if (!path.Contains(".meta"))
            {
                createBook(allStands[curStand], File.ReadAllText(path));
                curStand++;
            }
        }
    }

    public void createFrame(GameObject stand, Sprite image)
    {
        GameObject bo = Instantiate(framePrefab);
        bo.name = "Frame" + curFrame;
        curFrame++;
        bo.transform.SetParent(stand.transform);
        bo.transform.localPosition = new Vector3(-6.9208f, 0.7308f, 6.9789f);

        bo.transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite = image;
        bo.transform.GetChild(1).GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite = image;
    }

    public void createBook(GameObject stand,  string text)
    {
        GameObject bo = Instantiate(bookPrefab);
        bo.name = "Book" + curBook;
        curBook++;
        bo.transform.SetParent(stand.transform);
        bo.transform.localPosition = new Vector3(-6.9208f, 0.7308f, 7.108f);

        bo.transform.GetChild(0).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = text;
        bo.transform.GetChild(1).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

}