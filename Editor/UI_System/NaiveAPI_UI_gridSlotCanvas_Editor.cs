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
		gridSlotCanvas.isShowByItemList = EditorGUILayout.Toggle("Display by itemList", gridSlotCanvas.isShowByItemList);

		if (!gridSlotCanvas.isShowByItemList)
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
        else
        {
			gridSlotCanvas.backGround = (Sprite)EditorGUILayout.ObjectField("BackGround", gridSlotCanvas.backGround, typeof(Sprite), false);
			gridSlotCanvas.displayList = (NaiveAPI_item_itemList)EditorGUILayout.ObjectField("Item List",gridSlotCanvas.displayList, typeof(NaiveAPI_item_itemList),false);
			if(!gridSlotCanvas.isGenerateByIcon)
            {
				if (GUILayout.Button("Generate By Prefab")) gridSlotCanvas.isGenerateByIcon = true;
			}
            else
            {
				if (GUILayout.Button("Generate By icon")) gridSlotCanvas.isGenerateByIcon = false;
			}
			
			if (GUILayout.Button("Display")) gridSlotCanvas.reflushByItemList();
		}
		
	}
}
