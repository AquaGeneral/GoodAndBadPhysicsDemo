using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
    public static SceneManager Instance;

    public float Scale { get; private set; }
    public float Bounciness { get; private set; }
    public float Drag { get; private set; }
    public float AngularDrag { get; private set; }
    public float MaxAngularVelocity { get; private set; }
    public bool UnsyncedPhysics { get; private set; }
    
    private bool isSelectingScenario;
    private int selected = -1;
    
    void Awake() {
        Instance = this;

        Scale = 1f;
        Bounciness = 1f;
        Drag = 1f;
        AngularDrag = 1f;
        MaxAngularVelocity = 50f;
        UnsyncedPhysics = false;
    }

    void OnGUI() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            isSelectingScenario = !isSelectingScenario;
        }

        if(isSelectingScenario == false) return;

        selected = GUI.SelectionGrid(new Rect(50f, 75f, Screen.width - 100f, Screen.height - 150f), selected, new string[]{"Normal", "Incorrect Scale \n(10x Scale)", 
            "Rigidbody & Character Controller\nTogether", "Directly Modifying a Rigidbody's\nTransform", "Objects Rolling Forever", "Objects Without Bounciness", 
            "Rigidbodies Partially Sinking Into\nGeometry", "Instantiating At The Wrong Time", "Too Low FixedTimestep"}, 2);
        
        if(selected != -1) {
            Debug.Log(selected);
            switch(selected) {
                case 0:
                    Scale = 1f;
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    break;
                case 1:
                    Scale = 10f;
                    transform.localScale = new Vector3(10f, 10f, 10f);
                    break;
                case 2:

                    break;
            }

            GameStateSaver.ResetPositions();

            selected = -1;
        }
    }

    public static void CreateBullet(GameObject projectile, Vector3 projectilePosition, Quaternion projectileAngle) {
        Transform spawned = ((GameObject)Instantiate(projectile, projectilePosition, projectileAngle)).transform;
        spawned.parent = Instance.transform;
        spawned.localScale = spawned.localScale * Instance.Scale;
    }
}