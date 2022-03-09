using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NaiveAPI_UI_System))]
public class NaiveAPI_UI_System_Editor : Editor
{
	public GameObject normalCanvas;
	public NaiveAPI_UI_System NaiveAPI_UI_System;
	private void OnEnable()
	{
		NaiveAPI_UI_System = target as NaiveAPI_UI_System;
		Tools.hidden = true;
	}
	public override void OnInspectorGUI()
    {		
		NaiveAPI_UI_System.canvasName = EditorGUILayout.TextField("Canvas Name", NaiveAPI_UI_System.canvasName);
		NaiveAPI_UI_System.isCloseClickOutside = EditorGUILayout.Toggle("Close when click outside", NaiveAPI_UI_System.isCloseClickOutside);
		if (GUILayout.Button("Generate Canvas"))
		{
			NaiveAPI_UI_System.addCanvas(normalCanvas);
		}

		GUILayout.Space(20);

		base.OnInspectorGUI();
		serializedObject.Update();
		GUILayout.Space(20);

		if (GUILayout.Button("Load UI structure"))
        {
			NaiveAPI_UI_System.loadUIstructure();
        }
		if (GUILayout.Button("Display"))
		{
			NaiveAPI_UI_System.displayReflush();
		}
		if (GUILayout.Button("Clear"))
		{
			NaiveAPI_UI_System.clearAll();
		}


	}
}


