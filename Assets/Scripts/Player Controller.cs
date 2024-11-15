using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(movementX, 0, movementY);
        rb.AddForce(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Diamond collected");
        other.GameObject.SetActive(false);
        audiosource PlayOneShot(collectSound);
    }
}
