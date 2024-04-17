using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    public ScoreScript ScoreScriptInstance;

    //using a getter and setter to increment the scores for AI/Player
    public static bool WasGoal { get; private set; }
    private Rigidbody2D rb;

    public float MaxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WasGoal = false;

    }

    //this code increments either the player score/ai score once the trigger 
    //for either AI goal or Player Goal is triggered on collision.
    //StartCoroutine extends the wait time for the puck and paddles to reset to 
    //brace the player before playing again.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal)
        {
            if (other.tag == "AiGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                WasGoal = true;
                StartCoroutine(ResetPuck(false));
            }

            else if (other.tag == "PlayerGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                WasGoal = true;
                StartCoroutine(ResetPuck(true));
            }
        }
    }
        //this code stops the code from counting more scores after ai/player scored
        //waits 1 second in real time to reset the puck to the center

        private IEnumerator ResetPuck(bool didAiScore)
        {
            yield return new WaitForSecondsRealtime(1);
            WasGoal = false;
            rb.velocity = rb.position = new Vector2(0, 0);

        if (didAiScore)
            rb.position = new Vector2(1, 0);
        else
            rb.position = new Vector2(-1, 0);
        }

    //this code recenters the puck
    public void CenterPuck()
    { 
    
    rb.position = new Vector2 (0, 0);
    }

    //control highest speed puck travels at 
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
    }

    }

