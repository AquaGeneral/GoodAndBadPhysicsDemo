using UnityEngine;
using System.Collections;

public class CharacterSceneManager : MonoBehaviour {
    public static CharacterSceneManager Instance;

    void Awake() {
        Instance = this;
    }

    public static void CreateBullet(GameObject projectile, Vector3 projectilePosition, Quaternion projectileAngle) {
        Transform spawned = ((GameObject)Instantiate(projectile, projectilePosition, projectileAngle)).transform;
        spawned.parent = Instance.transform;
    }
}