using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Actor : MonoBehaviour {
    public float health = 100f;

    internal virtual void OnBulletHit(Vector3 hitPoint) {
        health--;

        if(health <= 0) {
            StartCoroutine(Die(hitPoint));
        }
    }

    internal virtual IEnumerator Die(Vector3 hitPoint) { yield break; }
}
