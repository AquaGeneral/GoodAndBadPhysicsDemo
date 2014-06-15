using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
    public static SceneManager Instance;

    public GameObject sceneRoot;

    private bool tenTimesScale = false;
    public float Scale {
        get {
            return tenTimesScale ? 10f : 1f;
        }
    }

    private Rect bounds;
    public Rect Bounds {
        get {
            return bounds;
        }
    }

    void Awake() {
        Instance = this;

        SetScale();

        DontDestroyOnLoad(gameObject);
    }

    void OnGUI() {
        if(GUILayout.Button(tenTimesScale ? "Reset Scale" : "10x Scale")) {
            tenTimesScale = !tenTimesScale;

            SetScale();

            bounds = new Rect(-50f * Scale, -20f * Scale, 100f * Scale, 20f * Scale);
        }
    }

    public static void CreateBullet(GameObject projectile, Vector3 projectilePosition, Quaternion projectileAngle) {
        Transform spawned = ((GameObject)Instantiate(projectile, projectilePosition, projectileAngle)).transform;
        spawned.parent = Instance.sceneRoot.transform;
        spawned.localScale = spawned.localScale * Instance.Scale;
    }

    private void SetScale() {
        if(tenTimesScale) {
            sceneRoot.transform.localScale = new Vector3(10f, 10f, 10f);
        } else {
            sceneRoot.transform.localScale = Vector3.one;
        }
    }
}