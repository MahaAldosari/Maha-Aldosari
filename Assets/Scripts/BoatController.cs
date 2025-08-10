using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 8f;
    public float mouseSensitivity = 200f;
    public int fishCount = 0;
    public int fishNeededToWin = 1; 
    public UIManager uiManager;       
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveInput = new Vector3(horizontal, 0f, vertical);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            fishCount++;
            uiManager.UpdateFishCount(fishCount);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Flag"))
        {
            if (fishCount >= fishNeededToWin)
            {
                SceneManager.LoadScene("YouWon");  // Replace with your scene name
            }
            else
            {
                Debug.Log("Collect more fish before finishing!");
            }
        }
    }
    void FixedUpdate()
    {
        Vector3 move = transform.TransformDirection(moveInput) * speed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("KillZone"))
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}