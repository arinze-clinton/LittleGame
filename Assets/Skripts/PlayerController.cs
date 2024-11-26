using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;

    public AudioSource backgroundMusic;

    public AudioClip collectSound;
    public AudioClip deathSound;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;
    public GameObject winTextObject;

    private Rigidbody rb;

    private AudioSource audioSource;

    private float movementX;
    private float movementY;

    private int count = 0;
    private int maxCount = 0;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // Set the count to zero 
        count = 0;
        maxCount = GameObject.FindGameObjectsWithTag("Diamond").Length;

        SetCountText();

        // Deactivate the Win-Loose Text
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = new Vector3(movementX, 0, movementY);

        rb.AddForce(direction * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Diamond"))
        {
            audioSource.PlayOneShot(collectSound);

            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            count = count + 1;

            // Run the 'SetCountText()' function (see below)
            SetCountText();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            backgroundMusic.Stop();
            audioSource.PlayOneShot(deathSound);

            rb.isKinematic = true;

            // Set the text value of your 'winText'
            winText.text = "G A M E   O V E R";
            winTextObject.SetActive(true);

            Invoke(nameof(BackToMenu), 5f);
        }

    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + " | " + maxCount.ToString();

        if (count >= maxCount)
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);

            Invoke(nameof(BackToMenu), 5f);
        }
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
