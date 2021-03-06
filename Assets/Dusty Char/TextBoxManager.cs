﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets._2D;


public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;
    public Text theText;

    public TextAsset textfile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;
    //public PlatformerCharacter2D player;  // Need to wait till don gets here to figure out what we did the first time....
    //public PlayerCharacter player;
    //public PlatformerCharacter2D player;
    public Platformer2DUserControl player;

    public bool isActive;
    public bool stopPlayerMovement;
    


    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Platformer2DUserControl>();  // need to change...again....(i think platformer2dusercontrol)

        if (textfile != null)
        {
            textLines = (textfile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        if(isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }

    }

    void Update()
    {
        if (!isActive)
            return;

        theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.K)) 
            currentLine++;

        if (currentLine > endAtLine)
            DisableTextBox();
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;
        if (stopPlayerMovement)
            player.canMove = false;
       
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);   //Once we get to the end of the "lines" we turn off the text block.
        isActive = false;
        player.canMove = true;
    }

    public void ReloadScript(TextAsset theText)
    {
        if(theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));

        }
    }
}
