using UnityEngine;
using System.Collections;

public class RBMovement : MonoBehaviour {
    public float speed = 12f;
    public float gravity = 1.7f;
    public float jumpForce = 1f;

    private Vector3 acceleration;

    private float accelerationModifier = 1f;

    void FixedUpdate() {
        if(Input.GetButtonDown("Jump")) {
            rigidbody.AddForce(0f, jumpForce, 0f);
        }

        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

        //rigidbody.AddForce(Input.GetAxisRaw("Horizontal"), 0f, 0f);
    }
}
