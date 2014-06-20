using UnityEngine;
using System.Collections.Generic;

// Stores the positions of every transform found it the root of the scene.
public class GameStateSaver : MonoBehaviour {
    private Dictionary<Transform, Vector3> transformPositions;

    public static GameStateSaver Instance;

    void Awake() {
        Instance = this;

        transformPositions = new Dictionary<Transform,Vector3>();

        PopulateTransforms(transform);
    }

    private void PopulateTransforms(Transform transform) {
        foreach(Transform t in transform) {
            transformPositions.Add(t, t.position);

            foreach(Transform t2 in t) {
                PopulateTransforms(t2);
            }
        }
    }

    public static void ResetPositions() {
        foreach(KeyValuePair<Transform, Vector3> pair in Instance.transformPositions) {
            pair.Key.position = pair.Value;
        }
    }
}
