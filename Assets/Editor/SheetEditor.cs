using UnityEditor;
using UnityEngine;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[CustomEditor(typeof(ScriptableObject), true)]
public class SheetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var baseType = target.GetType().BaseType;
        if (baseType != null && baseType.IsGenericType &&
            baseType.GetGenericTypeDefinition() == typeof(GoogleSheetParser<>))
        {
            EditorGUILayout.Space(10);
            if (GUILayout.Button("데이터 호출", GUILayout.Height(30)))
            {
                SyncData();
            }
        }
    }

    private void SyncData()
    {
        var script = target;
        var type = script.GetType();

        string sheet = (string)type.GetField("associatedSheet").GetValue(script);
        string worksheet = (string)type.GetField("associatedWorksheet").GetValue(script);
        int headerIdx = (int)type.GetField("headerRow").GetValue(script);
        int typeIdx = (int)type.GetField("typeRow").GetValue(script);
        int startIdx = (int)type.GetField("dataStartRow").GetValue(script);

        if (string.IsNullOrEmpty(sheet)) { Debug.LogError("Sheet ID가 비어있습니다."); return; }

        SpreadsheetManager.Read(new GSTU_Search(sheet, worksheet), ss =>
        {
            var dataListField = type.GetField("DataList");
            var listInstance = dataListField.GetValue(script) as System.Collections.IList;
            listInstance.Clear();

            
            List<string> headers = ss.rows[headerIdx].Select(c => c.value).ToList();
            List<string> types = ss.rows[typeIdx].Select(c => c.value).ToList();

            
            var rowsField = ss.rows.GetType().GetField("primaryDictionary", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            var primaryDict = rowsField.GetValue(ss.rows) as System.Collections.IDictionary;

            if (primaryDict == null)
            {
                Debug.LogError("시트 데이터를 읽어오는 데 실패했습니다 (Internal Dictionary Error)");
                return;
            }

            
            List<int> sortedKeys = new List<int>();
            foreach (var key in primaryDict.Keys) if (key is int i) sortedKeys.Add(i);
            sortedKeys.Sort();

            
            foreach (int rowIdx in sortedKeys)
            {
                if (rowIdx < startIdx) continue;

                var rowCells = ss.rows[rowIdx];
                if (rowCells == null || rowCells.Count == 0 || string.IsNullOrEmpty(rowCells[0].value))
                    continue;

                type.GetMethod("ParseRow").Invoke(script, new object[] { rowCells, headers, types });
            }

            EditorUtility.SetDirty(script);
            AssetDatabase.SaveAssets();
            Debug.Log($"<color=cyan><b>{target.name}</b></color> 동기화 완료! ({listInstance.Count} Rows)");
        });
    }
}