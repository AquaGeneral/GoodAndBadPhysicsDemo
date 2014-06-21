using UnityEngine;
using System.Collections;

public class SetMinPenetration : MonoBehaviour {
    public float minPenetrationForPenalty = 0.01f;

    void Awake() {
        //Physics.minPenetrationForPenalty = minPenetrationForPenalty;
    }

    void FixedUpdate() {
        Physics.minPenetrationForPenalty = minPenetrationForPenalty;
    }
}
