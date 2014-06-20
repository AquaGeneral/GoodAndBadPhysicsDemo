using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 2f;
    public int ignoreLayer = 8; // The layer that will be ignored from this bullet

    // TODO: What happens if this in just Update?
    void FixedUpdate() {
        transform.Translate(new Vector3(speed * SceneManager.Instance.Scale * Time.fixedDeltaTime, 0f, 0f));

        // Check if the bulet is out of bounds
        Vector3 pos = transform.position;
        if(pos.x < -50 || pos.x > 50 || pos.y < -50 || pos.y > 50f) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.layer != ignoreLayer && col.isTrigger == false) { 
            // If the collider we hit has a component of type Actor, we need to register that we hit the collider with a bullet
            Actor actor = col.GetComponent<Actor>();
            Rigidbody rBody = col.GetComponent<Rigidbody>();

            if(actor != null) {
                actor.OnBulletHit(transform.position);
            } else if(rBody != null) {
                rBody.AddForceAtPosition((col.transform.position - transform.position).normalized * 4f, col.transform.position, ForceMode.Impulse);
            }

            Destroy(gameObject);
        }
    }
}
