using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ScenarioSelector : MonoBehaviour {
    public GUIStyle scenarioCategoriesTitle;
    public GUIStyle gridItem;
    public GUIStyle escapeShortcutTip;
    
    private bool isSelectingScenario = true;
    private int selected = -1;
    
    private string[] options = new string[]{"Normal Scale", "Incorrect Scale \n<color='#777'>(10x Scale)</color>",  
        "Character Controller", "Rigidbody & Character Controller\nTogether", 
        "<b>Not</b> Directly Modifying a Rigidbody's\nTransform", "Directly Modifying a Rigidbody's\nTransform",
        "Objects <b>With</b> Bounciness", "Objects Without Bounciness"};

    /**
    * TODO: Add the following:
    * Rigidbodies Partially Sinking Into Geometry
    */

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void OnGUI() {
        // If the user taps Escape, toggle the visibility of the Scenario Selector
        if(Event.current.type == EventType.KeyDown && Event.current.isKey && Event.current.keyCode == KeyCode.Escape) {
            Debug.Log(isSelectingScenario + " " + !isSelectingScenario);
            isSelectingScenario = !isSelectingScenario;
            Debug.Log("End: " + isSelectingScenario);
        }

        if(isSelectingScenario) {
            GUI.Label(new Rect((Screen.width - 100f) * 0.5f - 195f, 40f, (Screen.width - 100) * 0.5f, 30f), "Good", scenarioCategoriesTitle);
            GUI.Label(new Rect((Screen.width - 100f) * 0.5f + 65f, 40f, (Screen.width - 100) * 0.5f, 30f), "Bad (Not Fixed)", scenarioCategoriesTitle);

            selected = GUI.SelectionGrid(new Rect(90f, 110f, Screen.width - 180f, Screen.height - 200f), selected, options, 2, gridItem);

            // Check if the user clicked one of the buttons
            if(selected != -1) {
                Application.LoadLevel(selected + 1);

                isSelectingScenario = false;
                selected = -1;
            }
        } else {
            GUI.Label(new Rect(5, 5, 250, 25), "Press Escape to show Scenario Selector", escapeShortcutTip);
        }
    }
}