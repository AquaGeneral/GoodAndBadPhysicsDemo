using UnityEngine;
using System.Collections;

public class Enemy : Actor {
    public GameObject projectile;

    [SerializeField]
    private float shootRate = 1f;

    private float timeBetweenShot = 0f;
    
    void Update() {
        timeBetweenShot += Time.deltaTime;
    }

    void OnTriggerStay(Collider collider) {
        // Only shoot if the player has entered the trigger area
        if(collider.gameObject.layer != 8) return;

        if(timeBetweenShot >= shootRate) {
            // Only shoot if the bullet might hit the player
            Vector3 direction = (PlayerMovement.player.transform.position - transform.position).normalized;
            Ray bulletRay = new Ray(transform.position, direction);
            RaycastHit hitInfo;
            
            if(Physics.Raycast(bulletRay, out hitInfo)) {
                // If the raycast hit the player, shoot
                if(hitInfo.transform.gameObject.layer == 8) { 
                    Shoot(bulletRay);

                    timeBetweenShot = 0f;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        // If the enemy is hit by an object in layer 9 or 10 (another enemy or a heavy object), take health away
        if(collision.gameObject.layer == 9 || collision.gameObject.layer == 10) { 
            health -= collision.relativeVelocity.magnitude * collision.rigidbody.mass;

            if(health < 0f) {
                StartCoroutine(Die(collision.contacts[0].point));
            }
        }
    }

    private void Shoot(Ray ray) {
        CharacterSceneManager.CreateBullet(projectile, ray.origin, Quaternion.FromToRotation(Vector3.right, ray.direction));
    }

    void OnDrawGizmos() {
        if(Application.isPlaying == false) return;

        //float angle = Mathf.Atan2(CCMovement.player.transform.position.y - transform.position.y, CCMovement.player.transform.position.x - transform.position.x) * 180f / Mathf.PI;
        Vector3 direction = (PlayerMovement.player.transform.position - transform.position).normalized;
        
        Ray bulletRay = new Ray(transform.position, direction);
        Gizmos.DrawRay(bulletRay);
    }

    internal override IEnumerator Die(Vector3 hitPoint) {
        // Make the enemy capsule fall over using a rigidbody
        Rigidbody rBody = gameObject.AddComponent<Rigidbody>();
        
        //yield return new WaitForFixedUpdate();
        Vector3 vec = (hitPoint - transform.position).normalized;

        rBody.AddForceAtPosition((transform.position - hitPoint).normalized * 4f, hitPoint, ForceMode.Impulse);

        Destroy(this);

        yield break;
    }
}
