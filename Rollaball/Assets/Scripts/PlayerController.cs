using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private bool onGround = true;
    private int currJump = 0;
    private int maxJump = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 5)
        {
            winTextObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
        
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        if (rb.transform.position.y <= 0.5)
        {
            currJump = 0;
        }

        if (Input.GetKeyDown("space") && (rb.transform.position.y <= 0.5 || currJump < maxJump))
        {
            if (currJump < maxJump)
            {
                Vector3 jump = new Vector3(0.0f, 300.0f, 0.0f);

                rb.AddForce(jump);
                currJump++;
            }

        }


    }

    /*void Update()
    {

        if (Input.GetKeyDown ("space") )
        {
            Vector3 jump = new Vector3 (0.0f, 100.0f, 0.0f);

            rb.AddForce(jump);
        }
    }*/

    void onCollisionEnter(Collision other)
    {
        onGround = true;
        currJump = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
    }
}
