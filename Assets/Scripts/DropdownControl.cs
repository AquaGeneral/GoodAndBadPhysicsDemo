using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DropdownControl {
    private Rect rect, viewRect;
    private Vector2 scrollPosition;
    private int currentOption;
    private string[] options;

    private bool isBeingEdited;

    public DropdownControl(Rect rect, Rect viewRect, Vector2 scrollPosition, string[] options) {
        this.rect = rect;
        this.viewRect = viewRect;
        this.scrollPosition = scrollPosition;
        this.currentOption = 0;
        this.options = options;
    }

    public void Draw() {
        if(isBeingEdited) {
            GUI.BeginScrollView(rect, scrollPosition, viewRect);
            for(int i = 0; i < options.Length; i++) {
                if(GUILayout.Button(options[i], GUILayout.Width(100f))) {
                    currentOption = i;
                }
            }
            GUI.EndScrollView();
        } else { 
            if(GUILayout.Button(options[currentOption], GUILayout.Width(100f))) {
                isBeingEdited = true;
            }
        }
    }
}
