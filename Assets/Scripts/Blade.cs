using UnityEngine;

public class Blade : MonoBehaviour
{
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;
    public AudioClip sliceSound; // Assign the slicing sound in the Inspector
    public AudioClip bombSound; // Assign the slicing sound in the Inspector

    private Camera mainCamera;
    private Collider sliceCollider;
    private TrailRenderer sliceTrail;
    private AudioSource audioSource;

    public Vector3 direction { get; private set; }
    public bool slicing { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StopSlice();
    }

    private void OnDisable()
    {
        StopSlice();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlice();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlice();
        }
        else if (slicing)
        {
            ContinueSlice();
        }
    }

    private void StartSlice()
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        transform.position = position;

        slicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
        sliceTrail.Clear();
    }

    private void StopSlice()
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlice()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        sliceCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the blade collides with a fruit
        if (other.CompareTag("Fruit"))
        {
            // Play the slicing sound
            if (audioSource != null && sliceSound != null)
            {
                audioSource.PlayOneShot(sliceSound);
                //PlayOneShot(sliceSound);
            }

            // Add logic to handle fruit slicing (e.g., splitting fruit, scoring)
            Debug.Log("Fruit sliced!");
        }
        else if (other.CompareTag("Bomb"))
        {
            // Play the slicing sound
            if (audioSource != null && bombSound != null)
            {
                audioSource.PlayOneShot(bombSound);
            }

            // Add logic to handle fruit slicing (e.g., splitting fruit, scoring)
            Debug.Log("Bomb sliced!");
        }
    }
}
