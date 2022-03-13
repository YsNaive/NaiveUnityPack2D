using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
	[CustomEditor(typeof(UI_canvas))]

	public class UI_canvas_Editor : Editor
	{

		public GameObject cavanButton,customButton,image;
		private UI_canvas UI_canvas;
		private SerializedProperty buttonType;

		private void OnEnable()
		{
			UI_canvas = target as UI_canvas;
			buttonType = serializedObject.FindProperty("buttonType");
			Tools.hidden = true;
		}
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			serializedObject.Update();

			GUILayout.Space(10);
			EditorGUILayout.PropertyField(buttonType, new GUIContent("Object Type"));


			switch (buttonType.enumNames[buttonType.enumValueIndex])
			{
				case "canvasButton":
					UI_canvas.objectName = EditorGUILayout.TextField("Object Name", UI_canvas.objectName);
					UI_canvas.isOpenOtherObject = EditorGUILayout.Toggle("Open other Object", UI_canvas.isOpenOtherObject);
					if(UI_canvas.isOpenOtherObject) UI_canvas.targetObject = (GameObject)EditorGUILayout.ObjectField( "    TargetObject", UI_canvas.targetObject, typeof(GameObject),true);
					UI_canvas.isCloseSelfCanvas = EditorGUILayout.Toggle("Close self Canvas", UI_canvas.isCloseSelfCanvas);
					break;
				case "customButton":
					UI_canvas.objectName = EditorGUILayout.TextField("Image Name", UI_canvas.objectName);
					break;
				case "image":
					UI_canvas.objectName = EditorGUILayout.TextField("Image Name", UI_canvas.objectName);
					UI_canvas.image = (Sprite)EditorGUILayout.ObjectField("Image", UI_canvas.image, typeof(Sprite), true);
					break;
				default:
					break;
			}
			UI_canvas.isCloseClickOutside = EditorGUILayout.Toggle("Close when click outside", UI_canvas.isCloseClickOutside);
			if (GUI.changed)
			{
				EditorUtility.SetDirty(target);
			}
			serializedObject.ApplyModifiedProperties();

			// «ö¶s
			if (GUILayout.Button("Generate Object"))
			{
				switch (buttonType.enumNames[buttonType.enumValueIndex])
				{
					case "canvasButton":
						UI_canvas.addObject(cavanButton);
						break;
					case "customButton":
						UI_canvas.addObject(customButton);
						break;
					case "image":
						UI_canvas.addObject(image);
						break;
					default:
						break;
				}
			}


		}
	}


}

