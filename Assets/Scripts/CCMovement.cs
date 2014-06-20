using UnityEngine;
using System.Collections;

public class CCMovement : Actor {
    public float speed = 6f;
    public float gravity = 1.5f;
    public float jumpStrength = 0.5f;

    public static GameObject player;

    private CharacterController characterController;
    private Vector3 acceleration;

    private float accelerationModifier = 1f;

    private bool isGrounded = false;
    private bool IsGrounded {
        get {
            return isGrounded;
        }
        set {
            // Reset the y acceleration if the player just landed back onto the ground
            if(isGrounded != value && value == true && acceleration.y < 0f) {
                acceleration = new Vector3(acceleration.x, 0f, acceleration.z);
            }
            isGrounded = value;
        }
    }

    private int layerMask = 1 >> 8;
    private bool isJumping;
    
    void Start() {
        player = gameObject;
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        RaycastHit hitInfo;

        /**
        * Check if the player is grounded by seeing how much distance the player and the ground. Here the 
        * value 0.58 is just based on how many units there are between the center of the player and plus 
        * some collision error margins (based on the Min Penatration for Panalty multiplied by two, for the
        * two collisions).
        */
        if(Physics.SphereCast(new Ray(transform.position, Vector3.down), 0.25f * SceneManager.Instance.Scale, out hitInfo)) {
            if((transform.position - hitInfo.point).y <= 0.58f * SceneManager.Instance.Scale) {
                IsGrounded = true;
            } else {
                IsGrounded = false;
            }
        } else {
            IsGrounded = false;
        }

        if(Input.GetAxisRaw("Horizontal") != 0f) {
            accelerationModifier = Mathf.Min(accelerationModifier + Time.fixedDeltaTime * 2f, 2f);
        } else {
            accelerationModifier = 1f;
        }

        // Set the horizontal acceleration and move the player back to the center of the Z axis
        acceleration = new Vector3(Input.GetAxisRaw("Horizontal") * speed * accelerationModifier * SceneManager.Instance.Scale * Time.fixedDeltaTime,
            acceleration.y, -transform.position.z * Time.fixedDeltaTime * 2f);

        // Apply gravity only when not grounded
        if(IsGrounded == false) {
            acceleration -= new Vector3(0f, gravity * SceneManager.Instance.Scale * Time.fixedDeltaTime, 0f);
        }

        characterController.Move(acceleration);
    }

    void Update() {
        // Add vertical acceleration if the player has jumped
        if(Input.GetButtonDown("Jump") && IsGrounded) {
            acceleration = new Vector3(acceleration.x, jumpStrength * SceneManager.Instance.Scale, 0f);
            IsGrounded = false;
        }
    }

    void OnGUI() {
        GUILayout.Space(50f);
        GUILayout.Label("Grounded: " + isGrounded);
        GUILayout.Label("Acceleration: " + acceleration);
        GUILayout.Label("Health: " + health);
    }

    internal override IEnumerator Die(Vector3 hitPosition) {
        Application.LoadLevel(0);
        yield return null;
    }
}