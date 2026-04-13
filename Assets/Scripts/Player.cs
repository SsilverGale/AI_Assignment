using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameWinScreen;
    [SerializeField] private GameObject goalGem;
    [SerializeField] private Text gemToolTip;
    private bool captured;
    public bool hasGem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        captured = false;
        hasGem = false;
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 movementDirection = UnityEngine.Vector3.zero;
        //Checks if player is captured to see if they can move
        if (!captured)
        {
        //Handles all of the movement 
        if (Input.GetKey(KeyCode.D))
            movementDirection += UnityEngine.Vector3.right;
        if  (Input.GetKey(KeyCode.A))
            movementDirection += UnityEngine.Vector3.left;
        if (Input.GetKey(KeyCode.W))
            movementDirection += UnityEngine.Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            movementDirection += UnityEngine.Vector3.back;

        //Normalizes speed so that going diagonal isn't faster than going forwards
        rb.transform.Translate(movementDirection.normalized*Time.deltaTime*speed);

        //If player is near gem they get a tool tip on how to pick it up
        if(goalGem != null)
            {
                 if (UnityEngine.Vector3.Distance(transform.position, goalGem.transform.position) < 2)
            {
                gemToolTip.enabled=true;

                //Player picks up gem
                if (Input.GetKey(KeyCode.E))
                {
                    Destroy(goalGem); 
                    hasGem = true;
                    gemToolTip.enabled=false; 
                }
            }
            else
            {
                gemToolTip.enabled=false;
            }
            }
       

        }


    }

    public void capturePlayer()
    {
        captured = true;
        Debug.Log("Uh oh, you've been captured!");
        gameOverScreen.SetActive(true);
    }

    //Trigger to check if player has won
        private void OnTriggerEnter(Collider other)
    {
        if (hasGem)
        {
            gameWinScreen.SetActive(true); 
        } 
    }
}
