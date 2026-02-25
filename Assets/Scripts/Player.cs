using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject gameOverScreen;
    private bool captured;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        captured = false;
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if player is captured to see if they can move
        if (!captured)
        {
        //Handles all of the movement
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(speed,0,0);
        else if  (Input.GetKey(KeyCode.A))
            rb.AddForce(-speed,0,0);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(0,0,speed);
        else if (Input.GetKey(KeyCode.S))
            rb.AddForce(0,0,-speed);
        }

    }

    public void capturePlayer()
    {
        captured = true;
        Debug.Log("Uh oh, you've been captured!");
        gameOverScreen.SetActive(true);
    }
}
