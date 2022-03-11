using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(gridSlotCanvas))]
public class NaiveAPI_UI_gridSlotCanvas_Editor : Editor
{
	gridSlotCanvas gridSlotCanvas;
	private void OnEnable()
	{
		gridSlotCanvas = target as gridSlotCanvas;
		Tools.hidden = true;
	}
	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginHorizontal();
		if(!gridSlotCanvas.isGenerateByItemList && !gridSlotCanvas.isShowByInventory)
        {
			gridSlotCanvas.isShowByInventory = EditorGUILayout.Toggle("Display by itemList", gridSlotCanvas.isShowByInventory);
			gridSlotCanvas.isGenerateByItemList = EditorGUILayout.Toggle("Display by itemList", gridSlotCanvas.isGenerateByItemList);
		}
		else if(gridSlotCanvas.isGenerateByItemList)
			gridSlotCanvas.isGenerateByItemList = EditorGUILayout.Toggle("Display by itemList", gridSlotCanvas.isGenerateByItemList);
		else
			gridSlotCanvas.isShowByInventory = EditorGUILayout.Toggle("Display by itemList", gridSlotCanvas.isShowByInventory);


		if (gridSlotCanvas.isShowByInventory)
		{
			gridSlotCanvas.backGround = (Sprite)EditorGUILayout.ObjectField("BackGround", gridSlotCanvas.backGround, typeof(Sprite), false);
			gridSlotCanvas.displayInventory = (NaiveAPI_item_inventory)EditorGUILayout.ObjectField("Inventory",gridSlotCanvas.displayInventory, typeof(NaiveAPI_item_inventory),false);
			if(!gridSlotCanvas.isGenerateByIcon)
            {
				if (GUILayout.Button("Generate By Prefab")) gridSlotCanvas.isGenerateByIcon = true;
			}
            else
            {
				if (GUILayout.Button("Generate By icon")) gridSlotCanvas.isGenerateByIcon = false;
			}
			
			if (GUILayout.Button("Display")) gridSlotCanvas.reflushByInventory();
		}
		else if (gridSlotCanvas.isGenerateByItemList)
        {
			gridSlotCanvas.backGround = (Sprite)EditorGUILayout.ObjectField("BackGround", gridSlotCanvas.backGround, typeof(Sprite), false);
			gridSlotCanvas.displayItemList = (NaiveAPI_item_itemList)EditorGUILayout.ObjectField("Inventory", gridSlotCanvas.displayItemList, typeof(NaiveAPI_item_itemList), false);
			if (!gridSlotCanvas.isGenerateByIcon)
			{
				if (GUILayout.Button("Generate By Prefab")) gridSlotCanvas.isGenerateByIcon = true;
			}
			else
			{
				if (GUILayout.Button("Generate By icon")) gridSlotCanvas.isGenerateByIcon = false;
			}
			if (GUILayout.Button("Display")) gridSlotCanvas.reflushByItemList();
		}
        else
        {			
			base.OnInspectorGUI();
			serializedObject.Update();


			if (GUILayout.Button("Add Slot"))
			{
				gridSlotCanvas.addSlot("Slot", gridSlotCanvas.icon, gridSlotCanvas.backGround, gridSlotCanvas.itemW, gridSlotCanvas.itemH,false);
			}
			if (GUILayout.Button("Add preview Slot"))
				gridSlotCanvas.addSlot("previewSlot", null, null, gridSlotCanvas.itemW, gridSlotCanvas.itemH,true);
			if (GUILayout.Button("Clear preview slot"))
				gridSlotCanvas.clearPreview();
		}
		
	}
}
