using UnityEngine;
using System.Collections;

public class AddForceConstantly : MonoBehaviour {
    public Vector3 force;

    void FixedUpdate() {
        rigidbody.AddForce(force * Time.fixedDeltaTime);
    }
}
