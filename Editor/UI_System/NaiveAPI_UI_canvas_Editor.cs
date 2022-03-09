using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(NaiveAPI_UI_canvas))]

public class NaiveAPI_UI_canvas_Editor : Editor
{

	public GameObject cavanButton,customButton,image;
	private NaiveAPI_UI_canvas NaiveAPI_UI_canvas;
	private SerializedProperty buttonType;

	private void OnEnable()
	{
		NaiveAPI_UI_canvas = target as NaiveAPI_UI_canvas;
		buttonType = serializedObject.FindProperty("buttonType");
		Tools.hidden = true;
	}
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		serializedObject.Update();

		GUILayout.Space(10);
		EditorGUILayout.PropertyField(buttonType, new GUIContent("Button Type"));


		switch (buttonType.enumNames[buttonType.enumValueIndex])
        {
			case "canvasButton":
				NaiveAPI_UI_canvas.objectName = EditorGUILayout.TextField("Button Name", NaiveAPI_UI_canvas.objectName);
				NaiveAPI_UI_canvas.isOpenOtherObject = EditorGUILayout.Toggle("Open other Object", NaiveAPI_UI_canvas.isOpenOtherObject);
				if(NaiveAPI_UI_canvas.isOpenOtherObject) NaiveAPI_UI_canvas.targetObject = (GameObject)EditorGUILayout.ObjectField( "    TargetCanvas", NaiveAPI_UI_canvas.targetObject, typeof(GameObject),true);
				NaiveAPI_UI_canvas.isCloseSelfCanvas = EditorGUILayout.Toggle("CloseSelf", NaiveAPI_UI_canvas.isCloseSelfCanvas);
				break;
			case "customButton":
				NaiveAPI_UI_canvas.objectName = EditorGUILayout.TextField("Image Name", NaiveAPI_UI_canvas.objectName);
				break;
			case "gridSlot":
				break;
			case "image":
				NaiveAPI_UI_canvas.objectName = EditorGUILayout.TextField("Image Name", NaiveAPI_UI_canvas.objectName);
				NaiveAPI_UI_canvas.image = (Sprite)EditorGUILayout.ObjectField("Image", NaiveAPI_UI_canvas.image, typeof(Sprite), true);
				break;
			default:
				break;
        }
		NaiveAPI_UI_canvas.isCloseClickOutside = EditorGUILayout.Toggle("Close when click outside", NaiveAPI_UI_canvas.isCloseClickOutside);
		if (GUI.changed)
        {
			EditorUtility.SetDirty(target);
        }
		serializedObject.ApplyModifiedProperties();

		// «ö¶s
		if (GUILayout.Button("Generate Button"))
		{
			switch (buttonType.enumNames[buttonType.enumValueIndex])
			{
				case "canvasButton":
					NaiveAPI_UI_canvas.addObject(cavanButton);
					break;
				case "customButton":
					NaiveAPI_UI_canvas.addObject(customButton);
					break;
				case "gridSlot":
					break;
				case "image":
					NaiveAPI_UI_canvas.addObject(image);
					break;
				default:
					break;
			}
		}


	}
}


