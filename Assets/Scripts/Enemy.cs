using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
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
            timeBetweenShot = 0f;

            // Only shoot if the bullet might hit the player
            float angle = Mathf.Atan2(CCMovement.player.transform.position.y - transform.position.y, CCMovement.player.transform.position.x - transform.position.x) * 180f / Mathf.PI;
            Ray bulletRay = new Ray(transform.position, new Vector3(0f, 0f, angle).normalized);
            RaycastHit hitInfo;
            
            if(Physics.Raycast(bulletRay, out hitInfo)) {
                // If the raycast hit the player, shoot
                Debug.Log("Ray");
                if(hitInfo.transform.gameObject.layer == 8) { 
                    Debug.Log("Shoot");
                    Shoot(bulletRay);
                }
            }
        }
    }

    private void Shoot(Ray ray) {
        Instantiate(projectile, ray.origin, Quaternion.Euler(0f, 0f, ray.direction.z));
    }

    void OnDrawGizmos() {
        if(Application.isPlaying == false) return;

        float angle = Mathf.Atan2(CCMovement.player.transform.position.y - transform.position.y, CCMovement.player.transform.position.x - transform.position.x) * 180f / Mathf.PI;
        Ray bulletRay = new Ray(transform.position, new Vector3(0f, 0f, angle / 180f).normalized);
        Debug.Log((angle / 180f) + " " + bulletRay);
        Gizmos.DrawRay(bulletRay);
    }
}
