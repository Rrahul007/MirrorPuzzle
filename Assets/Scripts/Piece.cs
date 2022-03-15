using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    /// <summary>
    /// Class Piece contains the functionality of broken piece
    /// </summary>

    [SerializeField]
    private Transform piecePlace;   // Variable for holding the correct targeted position of the broken piece

    private float deltaX, deltaY;   // New position variable as per piece movement

    public bool locked;             // Status variable for locking the piece movement if piece is dropped at correct place

    public AudioSource matchedSound;    // Audiosource to play on successfull drop

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !locked)  // To check if input touch is more than 0, and is not locked for movement
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began: // Executes the block as user touch get started the screen
                    if (UIManager.isMove)   // Check if control is on move or on rotate
                    {
                        if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos)) // Getting touch position if touched on particular piece collider
                        {
                            deltaX = touchPos.x - transform.position.x;
                            deltaY = touchPos.y - transform.position.y;
                        }
                    }
                    else
                    {
                        if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                        {
                            transform.Rotate(0f, 0f, 90f);
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                        transform.position = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);     // Move piece on new position
                    break;

                case TouchPhase.Ended:      // If touch ended leave the piece on that position and check if it is target position
                    //if(this.tag == piecePlace.tag)
                    if (Mathf.Abs(transform.position.x - piecePlace.position.x) <= 0.5f &&
                        Mathf.Abs(transform.position.y - piecePlace.position.y) <= 0.5f && transform.rotation.z == 0f)
                    {
                        transform.position = new Vector2(piecePlace.position.x, piecePlace.position.y);
                        locked = true;
                        gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        UIManager.pieceMatched++;
                        matchedSound.Play();    // Play sound if target position is matched
                    }
                    break;
            }
        }
    }
}