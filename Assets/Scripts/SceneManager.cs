using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
    public static SceneManager Instance;

    public GameObject sceneRoot;

    public float Scale { get; private set; }
    public float Bounciness { get; private set; }
    public float Drag { get; private set; }
    public float AngularDrag { get; private set; }
    public float Friction { get; private set; }
    public float MaxAngularVelocity { get; private set; }
    public bool UnsyncedPhysics { get; private set; }
    
    private Rect bounds;
    public Rect Bounds {
        get {
            return bounds;
        }
    }
    
    void Awake() {
        Instance = this;

        Scale = 1f;
        Bounciness = 1f;
        Drag = 1f;
        AngularDrag = 1f;
        Friction = 1f;
        MaxAngularVelocity = 50f;
        UnsyncedPhysics = false;

        ApplyModifiers();

        DontDestroyOnLoad(gameObject);
    }

    void OnGUI() {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();
        GUILayout.Label("Scale: " + Scale.ToString("0.##"));
        Scale = GUILayout.HorizontalSlider(Scale, 0.3f, 10f, GUILayout.Width(150f));
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUILayout.Label("Bounciness: " + Bounciness.ToString("0.##"));
        Bounciness = GUILayout.HorizontalSlider(Bounciness, 0f, 1f, GUILayout.Width(130f));
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUILayout.Label("Drag: " + Drag.ToString("0.##"));
        Drag = GUILayout.HorizontalSlider(Drag, 0f, 1f, GUILayout.Width(130f));
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUILayout.Label("Angular Drag: " + AngularDrag.ToString("0.##"));
        AngularDrag = GUILayout.HorizontalSlider(AngularDrag, 0f, 1f, GUILayout.Width(130f));
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUILayout.Label("Max Angular Velocity: " + MaxAngularVelocity.ToString("0.##"));
        MaxAngularVelocity = GUILayout.HorizontalSlider(MaxAngularVelocity, 1f, 60f, GUILayout.Width(150f));
        GUILayout.EndVertical();

        UnsyncedPhysics = GUILayout.Toggle(UnsyncedPhysics, "Unsynced Physics");

        if(GUI.Button(new Rect(Screen.width - 150, 50, 140, 30), "Update Scene")) { 
            ApplyModifiers();
        }

        GUILayout.EndHorizontal();
    }

    public static void CreateBullet(GameObject projectile, Vector3 projectilePosition, Quaternion projectileAngle) {
        Transform spawned = ((GameObject)Instantiate(projectile, projectilePosition, projectileAngle)).transform;
        spawned.parent = Instance.sceneRoot.transform;
        spawned.localScale = spawned.localScale * Instance.Scale;
    }

    private void ApplyModifiers() {
        // Update Scale
        bounds = new Rect(-50f * Scale, -20f * Scale, 100f * Scale, 20f * Scale);
        sceneRoot.transform.localScale = new Vector3(Scale, Scale, Scale);

        // Update Max Angular Velocity
        Physics.maxAngularVelocity = MaxAngularVelocity;

        // Apply Unsynced Physics
        if(UnsyncedPhysics) {
            Time.fixedDeltaTime = 0.03f;
        } else {
            Time.fixedDeltaTime = 0.015f;
        }
    }
}