using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
    public float speed = 2f;

    // TODO: What happens if this in just Update?
    void FixedUpdate() {
        transform.Translate(new Vector3(speed * Time.fixedDeltaTime, 0f, 0f));
    }

    void OnTriggerEnter(Collider collider) {
        // Ignore the player
        if(collider.gameObject.layer != 8) { 
            Destroy(gameObject);
        }
    }
}
