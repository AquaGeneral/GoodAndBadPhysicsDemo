using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
    private int selectedScenario = 0;
    private string[] scenarios = new string[] { "10x Scale", "0.1x Scale", "30 Hz Physics", "No Bounciness", "Too Much Restitution", "No Drag" };

    void OnGUI() {
        selectedScenario = GUI.Toolbar(new Rect(5f, 5f, 700f, 25f), selectedScenario, scenarios);
    }
}
