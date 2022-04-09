using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
    [CustomEditor(typeof(item_itemType))]
    public class item_itemType_Editor : Editor
    {
        private List<item_windowAPI.itemCustomInfoData> dataList = new List<item_windowAPI.itemCustomInfoData>();
        private int dataListIndex = 0;
        public item_itemType Target
        {
            get
            {
                return this.target as item_itemType;
            }
        }
        private void OnEnable()
        {
            loadData();
            if (Target.infomation != null)
            {
                for (int i = 0; i < dataList.Count; i++)
                {
                    if (Target.infomation.GetType().ToString() == dataList[i].name)
                        dataListIndex = i + 1;
                }
            }
        }
        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();
            infoLayout();
            serializedObject.Update();



            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
            serializedObject.ApplyModifiedProperties();
        }
        public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height)
        {
            Texture2D newIcon = new Texture2D(width, height);


            if (Target.icon != null)
            {
                EditorUtility.CopySerialized(Target.icon, newIcon);
                return newIcon;
            }
            else
            {

                Texture2D defaultCustomIcon = AssetDatabase.LoadAssetAtPath("Assets/WinxProduction/Editor/Editor Default Resources/StateMachine Icon.png", typeof(Texture2D)) as Texture2D;

                if (defaultCustomIcon != null)
                {




                    EditorUtility.CopySerialized(defaultCustomIcon, newIcon);

                    Target.icon = newIcon;

                    AssetDatabase.AddObjectToAsset(newIcon, Target);

                    EditorUtility.SetDirty(Target);

                    return newIcon;
                }
            }

            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }
        protected void OnClickBrowseForNewIcon()
        {

            string path = EditorUtility.OpenFilePanelWithFilters("Select icon", "Assets", new string[] { "Icon files", "png", });

            if (!string.IsNullOrEmpty(path))
            {

                Target.icon = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabaseUtility.AbsoluteUrlToAssets(path));

                EditorUtility.SetDirty(Target);

            }
        }
        public static class AssetDatabaseUtility
        {

            public static string AbsoluteUrlToAssets(string absoluteUrl)
            {
                Uri fullPath = new Uri(absoluteUrl, UriKind.Absolute);
                Uri relRoot = new Uri(Application.dataPath, UriKind.Absolute);

                return relRoot.MakeRelativeUri(fullPath).ToString();


            }
        }

        private void infoLayout()
        {
            GUILayout.Space(15);
            if (dataListIndex != 0)
            {
                GUILayout.Label("[ " + dataList[dataListIndex-1].name + " ]");
                SerializedObject serializedObject = new SerializedObject(Target.getInfomation<UnityEngine.Object>());
                foreach (item_windowAPI.itemCustomInfoData.valueData i in dataList[dataListIndex - 1].valueDatas)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(i.name));
                }
                serializedObject.ApplyModifiedProperties();
                GUILayout.Space(10);
            }
        }
        private void loadData()
        {
            item_windowAPI.saveList<item_windowAPI.itemCustomInfoData> saveList = new item_windowAPI.saveList<item_windowAPI.itemCustomInfoData>();
            file_System.LoadDataAsJson(item_windowAPI.dataPath.customInfoList, saveList);
            dataList = saveList.list;
        }

    }

}
