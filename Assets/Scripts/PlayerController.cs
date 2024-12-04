using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;

    public AudioClip collectSound;
    public AudioClip deathSound;

    public AudioSource backgroundMusic;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI winLooseText;

    private Rigidbody rb;

    private AudioSource audioSource;

    private float movementX;
    private float movementY;

    private int count = 0;
    private int maxCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        maxCount = GameObject.FindGameObjectsWithTag("Diamond").Length;

        SetCountText();
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

        rb.AddForce(direction * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);

            audioSource.PlayOneShot(collectSound);

            count++;

            SetCountText();

            if (count >= maxCount)
            {
                winLooseText.text = "Y I P P I E ! ! ! ! ";
                winLooseText.color = Color.green;

                Invoke(nameof(BackToMenu), 5f);
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(deathSound);

            backgroundMusic.Stop();

            rb.isKinematic = true;

            winLooseText.text = "You have been caught!!";
            winLooseText.color = Color.red;

            Invoke(nameof(BackToMenu), 5f);
        }
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count + " | " + maxCount;
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
