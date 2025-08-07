using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private float horizontalInput;
    private float verticalInput;
    private float fallTimer;
    private float outOfBoundsTime = 6f;
    public float gravityModifier = 2f;
    public float speed = 10;
    public float jumpForce = 10;
    public Transform cameraTransform;
    public ParticleSystem explosionParticles;
    public bool gameOver = false;
    public bool isGrounded = true;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI winText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = (camForward * verticalInput + camRight * horizontalInput).normalized;

        transform.Translate(moveDir * speed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        CheckOutOfBounds();
    }

    private void CheckOutOfBounds()
    {
        if (!isGrounded) 
        {
            fallTimer += Time.deltaTime; 
        }
        else 
        {
            fallTimer = 0f; 
        }

        if(fallTimer >= outOfBoundsTime)
        {
            StartCoroutine(Restart());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded=true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("You Win!");
            explosionParticles.Play();
            gameOver = true;
            StartCoroutine(Restart());
            winText.gameObject.SetActive(true);
        }

        if (other.gameObject.CompareTag("Hazard") || other.gameObject.CompareTag("Enemy"))
        {
            explosionParticles.Play();
            gameOver = true;
            StartCoroutine(Restart());
            loseText.gameObject.SetActive(true);
        }    
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
  

