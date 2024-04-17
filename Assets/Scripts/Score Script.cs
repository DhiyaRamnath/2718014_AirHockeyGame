using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    //grouping the related constants together 
    public enum Score

    {
        AiScore, PlayerScore
    }

    public Text AiScoreTxt, PlayerScoreTxt;
    public UIManager uiManager;

    public int MaxScore;

    //this region groups both the ai and players scores
    //gets the value of the score, if it is equal to the maximum amount(5), it will depict the end game restart canvas.
    #region Scores
    private int aiScore, playerScore;

    private int AiScore
    {

        get { return aiScore; }
        set
        {
            aiScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(true);
        }

    }


    private int PlayerScore
    {

        get { return playerScore; }
        set
        {
            playerScore = value;
            if (value == MaxScore)
                uiManager.ShowRestartCanvas(false);
        }

    }
    #endregion


    //this code converts the int score into a strong that can be depicted in the text of the score canvas

    public void Increment(Score whichScore)
    {
        if (whichScore == Score.AiScore)
        {
            AiScoreTxt.text = (++AiScore).ToString();
        }
        else
        {
            PlayerScoreTxt.text = (++PlayerScore).ToString();
        }
    }

    //when the game ends, the score will reset to 0
    public void ResetScore()
    {

        AiScore = PlayerScore = 0;
        AiScoreTxt.text = PlayerScoreTxt.text = "0";

    }
    // Start is called before the first frame update
    
}


