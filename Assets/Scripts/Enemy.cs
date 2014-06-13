using UnityEngine;
using System.Collections;

public class Enemy : Actor {
    public GameObject projectile;

    private const float distanceUntilFires = 10f;
    private const float shootRate = 1f;

    private float timeBetweenShot = 0f;
    
    void Update() {
        timeBetweenShot += Time.deltaTime;
    }

    void OnTriggerStay(Collider collider) {
        // Only shoot if the player has entered the trigger area
        if(collider.gameObject.layer != 8) return;

        if(timeBetweenShot >= shootRate) {
            // Only shoot if the bullet might hit the player
            Vector3 direction = (CCMovement.player.transform.position - transform.position).normalized;
            Ray bulletRay = new Ray(transform.position, direction);
            RaycastHit hitInfo;
            
            if(Physics.Raycast(bulletRay, out hitInfo)) {
                // If the raycast hit the player, shoot
                Debug.Log("Ray");
                if(hitInfo.transform.gameObject.layer == 8) { 
                    Debug.Log("Shoot");
                    Shoot(bulletRay);

                    timeBetweenShot = 0f;
                }
            }
        }
    }

    private void Shoot(Ray ray) {
        Debug.Log(ray.direction * 360f);
        Instantiate(projectile, ray.origin, Quaternion.FromToRotation(Vector3.right, ray.direction));
    }

    void OnDrawGizmos() {
        if(Application.isPlaying == false) return;

        //float angle = Mathf.Atan2(CCMovement.player.transform.position.y - transform.position.y, CCMovement.player.transform.position.x - transform.position.x) * 180f / Mathf.PI;
        Vector3 direction = (CCMovement.player.transform.position - transform.position).normalized;
        
        Ray bulletRay = new Ray(transform.position, direction);
        Gizmos.DrawRay(bulletRay);
    }

    internal override IEnumerator Die(Vector3 hitPoint) {
        // Make the enemy capsule fall over using a rigidbody
        Rigidbody rBody = gameObject.AddComponent<Rigidbody>();
        
        yield return new WaitForFixedUpdate();
        Vector3 vec = (hitPoint - transform.position).normalized;

        rBody.AddForceAtPosition((transform.position - hitPoint).normalized * 4f, hitPoint, ForceMode.Impulse);

        Destroy(this);
    }

    //internal override void Die(Vector3 hitPoint) {
    //    // Make the enemy capsule fall over using a rigidbody
    //    gameObject.AddComponent<Rigidbody>();
    //    rigidbody.AddForceAtPosition((collider.transform.position - transform.position).normalized * 2f, collider.transform.position, ForceMode.Impulse);
    //    Destroy(this);
    //}
}
