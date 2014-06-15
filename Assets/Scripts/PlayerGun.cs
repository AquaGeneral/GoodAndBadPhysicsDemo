using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {
    public Transform player;
    public GameObject reticle;
    public GameObject projectile;

    private Transform spawnedReticle;

    /**
    * The area of the screen buttons are at. When the player clicks in this area, it is not possible to shoot in case
    * they were simply clicking a button
    */
    private Rect buttonArea = new Rect(0, 0, 200, 30); 

    void Start() {
        spawnedReticle = ((GameObject)Instantiate(reticle)).GetComponent<Transform>() as Transform;
    }

    void LateUpdate() {
        Vector3 reticlePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
            Input.mousePosition.y, SideCamera.Instance.distance));
        spawnedReticle.position = reticlePosition;
    }

    void Update() {
        if(buttonArea.Contains(Input.mousePosition)) return;

        if(Input.GetButtonDown("Fire1")) {
            Vector3 reticlePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
            Input.mousePosition.y, SideCamera.Instance.distance * SceneManager.Instance.Scale));

            float angle = Mathf.Atan2(reticlePosition.y - player.position.y, reticlePosition.x - player.position.x) * 180f / Mathf.PI;

            //Quaternion projectileAngle = Quaternion.LookRotation((player.position - reticlePosition).normalized, Vector3.forward);
            Quaternion projectileAngle = Quaternion.Euler(0f, 0f, angle);

            //Vector3 projectilePosition = player.position - new Vector3(2f * Mathf.Cos(angle), 2f * Mathf.Sin(angle), 0f);
            Vector3 projectilePosition = player.position;

            SceneManager.CreateBullet(projectile, projectilePosition, projectileAngle);

        }
    }
}
