using UnityEngine;

public class AIScript : MonoBehaviour
{
    //instatiated some fields to be edited for the AI paddle to have to same functionality of the player paddle
    public float MaxMovementSpeed;
    private Rigidbody2D rb;

    public Rigidbody2D Puck;

    public Transform PlayerBoundaryHolder;
    private Boundary playerBoundary;

    public Transform AiBoundaryHolder;
    private Boundary AiBoundary;

    public Transform PuckBoundaryHolder;
    private Boundary PuckBoundary;

    private Vector2 TargetPosition;

    private bool isFirstTimeInOpponentsHalf = true;

    //decrease precision of the ai so it feels more humanlike to player
    private float offsetYFromTarget;
    private Vector2 startingPosition;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;

        //associating the boundaries for the player and puck to the child class's x or y position
        playerBoundary = new Boundary(PlayerBoundaryHolder.GetChild(0).position.y,
                                      PlayerBoundaryHolder.GetChild(1).position.y,
                                          PlayerBoundaryHolder.GetChild(2).position.x,
                                              PlayerBoundaryHolder.GetChild(3).position.x);

        playerBoundary = new Boundary(PuckBoundaryHolder.GetChild(0).position.y,
                                      PuckBoundaryHolder.GetChild(1).position.y,
                                          PuckBoundaryHolder.GetChild(2).position.x,
                                              PuckBoundaryHolder.GetChild(3).position.x);

       
    }

    private void FixedUpdate()
    {
        //stop the AI from moving after a goal is scored
        if (!PuckScript.WasGoal)
        {
            float movementSpeed;

           

                //If puck is in lower region of boundary, these constraints are applied
                if (Puck.position.x > PuckBoundary.Right)
                {
                    if (isFirstTimeInOpponentsHalf)
                    {
                        //the offset amount ranges from these values
                        isFirstTimeInOpponentsHalf = false;
                        offsetYFromTarget = Random.Range(-1f, 1f);
                    }
                    //max move speed multiplied by a range to juggle the speed of the ai paddle
                    movementSpeed = MaxMovementSpeed * Random.Range(0.1f, 0.3f);

                    TargetPosition = new Vector2(Mathf.Clamp(Puck.position.x + offsetYFromTarget, playerBoundary.Left, playerBoundary.Right), startingPosition.y);

                }

                else
                {
                
                    isFirstTimeInOpponentsHalf = true;

                    movementSpeed = Random.Range(MaxMovementSpeed * 0.4f, MaxMovementSpeed);
                    TargetPosition = new Vector2(Mathf.Clamp(Puck.position.x, playerBoundary.Left, playerBoundary.Right),
                                        Mathf.Clamp(Puck.position.y, playerBoundary.Down, playerBoundary.Up));

                   
                }

                rb.MovePosition(Vector2.MoveTowards(rb.position, TargetPosition, movementSpeed * Time.fixedDeltaTime));
            }

           
        
        
    }
    //reset the position of the puck to starting position, center screen
    public void ResetPosition()
    {
        rb.position = startingPosition;
    }
}
   
