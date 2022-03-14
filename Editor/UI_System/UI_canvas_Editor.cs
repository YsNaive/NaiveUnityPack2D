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

		public GameObject objectButton, customButton,image,gridSlot;
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
			UI_canvas.objectName = EditorGUILayout.TextField("Object Name", UI_canvas.objectName);
			
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
					case "objectButton":
						UI_canvas.addObject(objectButton);
						break;
					case "customButton":
						UI_canvas.addObject(customButton);
						break;
					case "image":
						UI_canvas.addObject(image);
						break;
					case "gridSlot":
						UI_canvas.addObject(gridSlot);
						break;
					default:
						break;
				}
			}


		}
	}


}

