using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private float enemyRotatespeed = 10f;
    private Rigidbody playerRB;
    public float pushStrength = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 pushPlayer = (collision.gameObject.transform.position - transform.position).normalized;

            playerRigidbody.AddForce(pushPlayer * pushStrength, ForceMode.Impulse);
        }
        
    }
}
