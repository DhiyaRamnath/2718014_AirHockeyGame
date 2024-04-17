using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{

//creating headers for these game objects.These are going to be used in the UI Manager
    [Header("Canvas")]
    public GameObject Canvas;
    public GameObject CanvasRestart;

    [Header("Canva Restart")]
    public GameObject WinTxt;
    public GameObject LoseTxt;

    [Header("Other")]
    public ScoreScript scoreScript;
    public PuckScript puckScript;
    public PlayerMovement playerMovement;
    public AIScript aiScript;
    public Countdown countdown;

    //this code will decipher what text to appear at the end of the game
    //If the AI won, the lose text will appear to notify the player that 
    //they have lost


    public void ShowRestartCanvas (bool DidAiWin)
    {
        Time.timeScale = 0;
        Canvas.SetActive(false);
        CanvasRestart.SetActive(true);

        if (DidAiWin)
        {

            WinTxt.SetActive(false);    
            LoseTxt.SetActive(true);

        }
        else
        {
            WinTxt.SetActive(true);
            LoseTxt.SetActive(false);

        }


    }

    //this code will replay the game, setting the restart screen to false,
    //setting the score to appear again but starting from 0 and to reset all
    //the pucks and paddles to the starting position.
    public void RestartGame ()
    {

        Time.timeScale = 1;
        Canvas.SetActive(true);
        CanvasRestart.SetActive(false);

        scoreScript.ResetScore();
        puckScript.CenterPuck();
        aiScript.ResetPosition();
        playerMovement.ResetPosition();
        countdown.BeginCountdown(); 
    }
}