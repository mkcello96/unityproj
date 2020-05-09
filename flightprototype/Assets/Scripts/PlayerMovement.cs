using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Text flightModeText;

    public float speed = 12f;
    public float gravity = -30f;
    public float jumpHeight = 6f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool flightModeEnabled = false;
    public float flightSpeed = 15f;
    public float downwardSoarVelocity = -2f;

    Vector3 velocity;
    bool isGrounded;
    bool isSoaring;

    void Start()
    {
        flightModeText.text = "Flight Mode Off";
        flightModeText.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            flightModeEnabled = !flightModeEnabled;
            if (flightModeEnabled)
            {
                flightModeText.text = "Flight Mode On";
                flightModeText.color = Color.green;
            }
            else
            {
                flightModeText.text = "Flight Mode Off";
                flightModeText.color = Color.red;
            }
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (flightModeEnabled)
        {
            if (!isGrounded)
            {
                Vector3 move = transform.forward;  // player will never tilt up/down (or l/r)

                controller.Move(move * flightSpeed * Time.deltaTime);
            }

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;  // delta Y = 0.5 * g * delta T ^ 2

            // Cut off calculated downward velocity if soaring
            if (Input.GetButton("Fire3"))
            {
                velocity.y = Mathf.Max(velocity.y, downwardSoarVelocity);
            }

            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;  // delta Y = 0.5 * g * delta T ^ 2

            controller.Move(velocity * Time.deltaTime);
        }
    }
}
