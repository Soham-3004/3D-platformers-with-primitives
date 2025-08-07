using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody enemyRB;
    public float speed = 8.0f;
    private GameObject player;
    private bool playerDetected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRB.AddForce(lookDirection * speed);
        }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true; // Player is now in range, activate chase
            Debug.Log("Player entered enemy detection zone! Initiating chase.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            Debug.Log("Player exited enemy detection zone! Stopping chase.");
        }
    }
}
