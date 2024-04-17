using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //creating the fields

    //if player has just clicked on screen
    bool wasJustClicked = true;

    //Object can be moved
    bool canMove;

    //Keep 2 floats together (x & y bc its 2d), tells us how big the sprite is
    Vector2 PlayerSize;
    Vector2 startingPosition;

    Rigidbody2D rb;
    Collider2D PlayerCollider;
    // store the transform of the boundary gameobject
    public Transform BoundaryHolder;

    Boundary PlayerBoundary;


    // Start is called before the first frame update
    void Start()

    {
        // get extents of player 1 puck from center to edge 
        PlayerSize = gameObject.GetComponent<SpriteRenderer>().bounds.extents;

        //assigning rigid body to player paddle
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;
        PlayerCollider = GetComponent<Collider2D>();  


        PlayerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.x, 
                                        BoundaryHolder.GetChild(1).position.x,
                                            BoundaryHolder.GetChild(2).position.y,
                                                BoundaryHolder.GetChild(3).position.y);

    
    }

    // Update is called once per frame
    void Update()
    {
        //check if left mouse button is pressed - assigned 0
        if (Input.GetMouseButton(0))
        {

        //Gets coordinates of mouse position within the game world screen - convert screen coordinates to game world's mouse coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //only allows the gameobject to move once the left mouse button has been clicked on top of object and dragged
            if (wasJustClicked)
            {
                wasJustClicked = false;

        //checks if we are in the left, right, bottom and top half of the player 1 puck, we can move
                if (PlayerCollider.OverlapPoint(mousePos))
                {
                    canMove = true;
                }

        //if clicked anywhere outside the puck, we cant move the puck
                else
                {
                    canMove = false;
                }
            }

            if (canMove)
            {
           
                //restrict movement beyond boundaries
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, PlayerBoundary.Up, PlayerBoundary.Down),
                                                        Mathf.Clamp(mousePos.y, PlayerBoundary.Left, PlayerBoundary.Right));

                //gives the physics the velocity the player puck will move at
                rb.MovePosition(clampedMousePos);
            }
        }
        else
        {
            wasJustClicked = true;
        }
    }

    //position of player paddle will be reset 
    public void ResetPosition()
    {
        rb.position = startingPosition;
    }

}
