using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool isMove;      // Variable to set move or rotate control of piece
    public AudioSource winSound;    // Sound variable for winning sound
    public static int pieceMatched; // Number of piece matched
    public GameObject cleanImage;   // Clean image of solved puzzle
    public Text winText;            // Greeting text on winning

    // Start is called before the first frame update
    void Start()        // To initialize the primary values of data members on game start
    {
        cleanImage.SetActive(false);
        winText.enabled = false;
        pieceMatched = 0;
        isMove = true;
    }

    private void Update()
    {
        if(pieceMatched == 9)   // Check if all the pieces are matched and then react according
        {
            cleanImage.SetActive(true);
            winText.enabled = true;
            winSound.Play();
            pieceMatched = 0;
        }
    }

    public void RotatePiece()   // To set rotate control
    {
        isMove = false;
    }

    public void MovePiece()     // To set move control
    {
        isMove = true;
    }

    public void RetryGame()     // Restart the game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()      // Quit the game application
    {
        Application.Quit();
    }
}
