using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        // If the player collides with an object, move the player slightly to avoid overlap
        if (collision.gameObject.CompareTag("Obstacle")) {
            Vector3 normal = collision.contacts[0].normal;
            transform.position += normal * 0.1f; // Adjust the value based on your needs
        }
    }
}
