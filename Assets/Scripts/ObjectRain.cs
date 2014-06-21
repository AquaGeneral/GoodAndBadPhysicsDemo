using UnityEngine;
using System.Collections;

public class ObjectRain : MonoBehaviour {
    public GameObject prefab;
    public Vector3 initialVelocity;
    public int maxObjects = 60;

    private int objectCount = 0;
    
    private bool spawningObjects = true;

    IEnumerator Start() {
        while(spawningObjects) { 
            for(int i = 0; i < 16; i++) {
                float radius = Random.Range(1f, 3f);

                // Spawn a bunch of objects around a circle with random angularVelocity
                Vector3 position = new Vector3(radius * Mathf.Cos(i * 0.4f) - 7f, 1f, radius * Mathf.Sin(i * 0.4f));
                Rigidbody spawnedRB = ((GameObject) Instantiate(prefab, position, Quaternion.identity)).GetComponent<Rigidbody>();
                spawnedRB.velocity = initialVelocity;
                spawnedRB.angularVelocity = Random.insideUnitSphere;

                if(objectCount++ > maxObjects) {
                    spawningObjects = false;
                    yield break;
                }
            }

            yield return new WaitForSeconds(1.5f);
        }
    }
}
