using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 2f;
    public int ignoreLayer = 8; // The layer that will be ignored from this bullet

    // TODO: What happens if this in just Update?
    void FixedUpdate() {
        transform.Translate(new Vector3(speed * Time.fixedDeltaTime, 0f, 0f));
    }

    void OnTriggerEnter(Collider collider) {
        // Ignore the player
        if(collider.gameObject.layer != ignoreLayer) { 
            Destroy(gameObject);
        }
    }
}
