using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 20.0f;
    private float horizontalInput;
    private Rigidbody playerRb;
    private Vector3 forceDirection;
    private ConstantForce cForce;

    public float xRange = 20.0f;
    public float lowerBound = 0.0f;
    public float upperbound = 30.0f;
    public float jumpForce;
    public bool isOnGround = true;
    public bool GameOver = false;
    public bool CanGlide = true;
    // Start is called before the first frame update
    void Start()
    {
    playerRb = GetComponent <Rigidbody> ();
    cForce = GetComponent <ConstantForce>();
    }

    // Update is called once per frame
    void Update()
    {//left & right input
        horizontalInput = Input.GetAxis ("Horizontal");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * horizontalInput);

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

         
    //jumping
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
        GetComponent<Rigidbody>().drag=0;
        }
        //gliding
        if (CanGlide == true && Input.GetKeyUp(KeyCode.Space))
        {
        GetComponent<Rigidbody>().drag=10;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {//keeps the player from infinitly jumping
        if(collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
        }

       
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("elytra"))
        {
            CanGlide = true;
            Destroy (GameObject.FindWithTag("elytra"));
        }

        if(collision.gameObject.CompareTag("ground"))
        {
            transform.position = new Vector3(transform.position.x, lowerBound, transform.position.z);
        }
        if (transform.position.y > upperbound)
        {
            transform.position = new Vector3(transform.position.x, upperbound, transform.position.z);
        }
    }
}
