using UnityEngine;
using System.Collections;

public class AddForce : MonoBehaviour {
    public Vector3 force;

    void Start() {
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}
