using UnityEngine;
using System.Collections;

public class SideCamera : MonoBehaviour {
    public Transform target;
    public float distance = 4f;
    public float heightOffset = 1f;

    public static SideCamera Instance;

    void Awake() {
        if(Instance == null) { 
            Instance = this;
        } else {
            Debug.LogWarning("Overriding instance of SideCamera");
        }
    }

    void FixedUpdate() {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, 0.5f), 
            Mathf.Lerp(transform.position.y, target.position.y + heightOffset, 0.2f), 
            -distance * SceneManager.Instance.Scale);

    }
}
