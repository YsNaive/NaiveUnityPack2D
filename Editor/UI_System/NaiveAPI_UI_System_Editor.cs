using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NaiveAPI_UI_System))]
public class NaiveAPI_UI_System_Editor : Editor
{
	public GameObject normalCanvas,gridSlotCanvas;
	public NaiveAPI_UI_System NaiveAPI_UI_System;

	public enum CanvasType
    {
		normalCanvas,
		gridSlotCanvas
	}
	public CanvasType canvasType;
	private void OnEnable()
	{
		NaiveAPI_UI_System = target as NaiveAPI_UI_System;
		Tools.hidden = true;
	}
	public override void OnInspectorGUI()
    {
		canvasType = (CanvasType)EditorGUILayout.EnumPopup(canvasType);
		NaiveAPI_UI_System.canvasName = EditorGUILayout.TextField("Canvas Name", NaiveAPI_UI_System.canvasName);
		NaiveAPI_UI_System.isCloseClickOutside = EditorGUILayout.Toggle("Close when click outside", NaiveAPI_UI_System.isCloseClickOutside);
		if (GUILayout.Button("Generate Canvas"))
		{
            switch (canvasType)
            {
				case CanvasType.normalCanvas:
					NaiveAPI_UI_System.addCanvas(normalCanvas);
					break;
				case CanvasType.gridSlotCanvas:
					NaiveAPI_UI_System.addCanvas(gridSlotCanvas);
					break;
				default:
					break;
			}
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


