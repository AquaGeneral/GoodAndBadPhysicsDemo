using UnityEngine;
using System.Collections;

public class MoveConstantly : MonoBehaviour {
    public Vector3 movement;

    void Update() {
        transform.position += movement * Time.deltaTime;
    }
}
