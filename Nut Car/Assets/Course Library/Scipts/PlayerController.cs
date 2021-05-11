using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 50.0f;
    [SerializeField] private float horsePower;
    private float horizontalInput;
    private float verticalInput;
    private float speed;
    private float rpm;

    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

        if (IsOnGround())
        {
            // transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput); // Adds the force to the "local" coordinate system of the player NOT the "global" coordinate system.
            // playerRb.AddForce(Vector3.up * turnSpeed * horizontalInput);

            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 2.237f); // velocity (speed) is by default measured in meters/second. To get it in KM/H multiply the magnitude by 3.6f. And to get it in Miles/Hour multiply the magnitude by 2.237f.
            speedometerText.SetText("Speed: " + speed + " mph");

            rpm = (speed % 30) * 40; // "%" symbol is a special kind of operator in programming called the modulus operator or the remainder operator. It gives us the remainder value; for exmaple 30 % 30 = 0. 30 % 31 = 1.
            rpmText.SetText("RPM: " + rpm);
        } 

    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels) // With for "for-loop" you could set the end condition of when the "for-loop" would stop.
        {                                          // Whereas with "Lists" you could use the "foreach" LOOP. In the parentheses you need to tell it what we're trying to find in our list.
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        if (wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
