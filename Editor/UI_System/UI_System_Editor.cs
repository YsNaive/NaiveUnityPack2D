using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
	[CustomEditor(typeof(UI_System))]
	public class UI_System_Editor : Editor
	{
		public GameObject normalCanvas,gridSlotCanvas;
		public UI_System UI_System;

		public enum CanvasType
		{
			normalCanvas,
			gridSlotCanvas
		}
		public CanvasType canvasType;
		private void OnEnable()
		{
			UI_System = target as UI_System;
			Tools.hidden = true;
		}
		public override void OnInspectorGUI()
		{
			canvasType = (CanvasType)EditorGUILayout.EnumPopup(canvasType);
			UI_System.canvasName = EditorGUILayout.TextField("Canvas Name", UI_System.canvasName);
			UI_System.isCloseClickOutside = EditorGUILayout.Toggle("Close when click outside", UI_System.isCloseClickOutside);
			if (GUILayout.Button("Generate Canvas"))
			{
				switch (canvasType)
				{
					case CanvasType.normalCanvas:
						UI_System.addCanvas(normalCanvas);
						break;
					case CanvasType.gridSlotCanvas:
						UI_System.addCanvas(gridSlotCanvas);
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
				UI_System.loadUIstructure();
			}
			if (GUILayout.Button("Display"))
			{
				UI_System.reflush();
			}
			if (GUILayout.Button("Clear"))
			{
				UI_System.clearAll();
			}


		}
	}
}



