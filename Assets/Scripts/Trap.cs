using UnityEngine;

public class Trap : MonoBehaviour
{
    public float bounceForce = 20f;  // Adjust as needed
    public int damage = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered trap");
            HandlePlayerBounce(collision.gameObject);
        }
    }

    private void HandlePlayerBounce(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Debug.Log("Applying bounce force!");

            // Reset Y velocity first to ensure a clean bounce
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            // Directly setting velocity instead of AddForce
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        }
        else
        {
            Debug.LogError("No Rigidbody2D found on Player!");
        }
    }
}