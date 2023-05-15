using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // velocidad de movimiento del personaje
    private bool canMove = false; // indica si el personaje puede moverse o no
    private Vector3 touchPosition; // posición del toque del dedo
    public float dragSpeed = 2f; // velocidad de movimiento horizontal al arrastrar el dedo
    public float bounceForce = 1f; // Fuerza de rebote ajustable

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchPosition = touch.position;
                    canMove = true;
                    break;

                case TouchPhase.Moved:
                    float xMovement = touch.position.x - touchPosition.x;
                    float horizontalMoveSpeed = xMovement * dragSpeed * Time.deltaTime;
                    //transform.position += new Vector3(horizontalMoveSpeed, 0, 0);
                    GetComponent<Rigidbody>().AddForce(horizontalMoveSpeed * transform.right, ForceMode.VelocityChange);
                    //GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(horizontalMoveSpeed, 0, 0));

                    touchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    canMove = false;
                    break;
            }
        }
    }
    void FixedUpdate()
    {
        //transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, moveSpeed), ForceMode.Force);
        //GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain1"))
        {
            Debug.Log("COLLISION");
            // Calcula el vector de rebote reflejando la dirección actual del personaje
            Vector3 reflectionDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);

            // Aplica la dirección de rebote como nueva dirección de movimiento del personaje
            GetComponent<Rigidbody>().velocity = reflectionDirection * moveSpeed * bounceForce; 

        }
    }
}
