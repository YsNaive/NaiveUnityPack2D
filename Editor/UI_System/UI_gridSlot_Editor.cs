using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace NaiveAPI
{
	[CustomEditor(typeof(UI_gridSlot))]
	public class UI_gridSlot_Editor : Editor
	{
		UI_gridSlot UI_gridSlot;
		private void OnEnable()
		{
			UI_gridSlot = target as UI_gridSlot;
			Tools.hidden = true;
		}
		public override void OnInspectorGUI()
		{
			if (!UI_gridSlot.isGenerateByItemList && !UI_gridSlot.isShowByInventory)
			{
				UI_gridSlot.isShowByInventory = EditorGUILayout.Toggle("Display by inventory", UI_gridSlot.isShowByInventory);
				UI_gridSlot.isGenerateByItemList = EditorGUILayout.Toggle("Display by itemList", UI_gridSlot.isGenerateByItemList);
			}
			else if (UI_gridSlot.isGenerateByItemList)
				UI_gridSlot.isGenerateByItemList = EditorGUILayout.Toggle("Display by itemList", UI_gridSlot.isGenerateByItemList);
			else
				UI_gridSlot.isShowByInventory = EditorGUILayout.Toggle("Display by inventory", UI_gridSlot.isShowByInventory);

			if (UI_gridSlot.isShowByInventory)
			{

				UI_gridSlot.displayInventory = (item_inventory)EditorGUILayout.ObjectField("Inventory", UI_gridSlot.displayInventory, typeof(item_inventory), true);
				if (!UI_gridSlot.isGenerateByIcon)
				{
					if (GUILayout.Button("Generate By Prefab")) UI_gridSlot.isGenerateByIcon = true;
				}
				else
				{
					if (GUILayout.Button("Generate By icon")) UI_gridSlot.isGenerateByIcon = false;
				}

				if (GUILayout.Button("Display")) UI_gridSlot.reflushByInventory();
			}
			else if (UI_gridSlot.isGenerateByItemList)
			{
				UI_gridSlot.displayItemList = (item_itemList)EditorGUILayout.ObjectField("Item List", UI_gridSlot.displayItemList, typeof(item_itemList), false);
				if (!UI_gridSlot.isGenerateByIcon)
				{
					if (GUILayout.Button("Generate By Prefab")) UI_gridSlot.isGenerateByIcon = true;
				}
				else
				{
					if (GUILayout.Button("Generate By icon")) UI_gridSlot.isGenerateByIcon = false;
				}
				if (GUILayout.Button("Display"))
				{
					UI_gridSlot.reflushByItemList();
				}
			}

			GUILayout.Space(30);
			base.OnInspectorGUI();
			serializedObject.Update();


			if (GUILayout.Button("Add Slot"))
			{
				UI_gridSlot.addSlot("Slot", UI_gridSlot.icon, null, false);
			}
			if (GUILayout.Button("Add preview Slot"))
				UI_gridSlot.addSlot("previewSlot", UI_gridSlot.icon, null, true);
			if (GUILayout.Button("Clear preview slot"))
				UI_gridSlot.clearPreview();
			
		}
	}
}
