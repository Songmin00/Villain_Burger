using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using GoogleSheetsToUnity;

public abstract class GoogleSheetParser<T> : ScriptableObject where T : new()
{
    [Header("구글 스프레드 시트 주소(d/~/edit 사이)")]
    public string associatedSheet;
    [Header("시트명")]
    public string associatedWorksheet;

    [Header("행 인덱스 설정 (1부터 시작)")]
    [Header("변수명 행")]
    public int headerRow = 2;       // 변수 이름 행
    [Header("타입명 행")]
    public int typeRow = 1;         // 타입 명시 행
    [Header("데이터 시작 행")]
    public int dataStartRow = 3;    // 데이터 시작 행

    [Header("결과 데이터")]
    private Dictionary<string, T> _dataDic = new Dictionary<string, T>();
    public List<T> DataList = new List<T>();

    // 에디터에서 호출할 파싱 함수
    public void ParseRow(List<GSTU_Cell> rowCells, List<string> columnNames, List<string> columnTypes)
    {
        T instance = new T();

        for (int i = 0; i < rowCells.Count; i++)
        {
            if (i >= columnNames.Count) break;

            string fieldName = columnNames[i];
            string typeName = columnTypes[i];
            string value = rowCells[i].value;

            FieldInfo field = typeof(T).GetField(fieldName);
            if (field != null)
            {
                field.SetValue(instance, ConvertValue(value, field.FieldType, typeName));
            }
        }
        DataList.Add(instance);
    }

    private object ConvertValue(string value, Type targetType, string typeName)
    {
        if (string.IsNullOrEmpty(value) || value == "-") return GetDefault(targetType);

        try
        {
            if (targetType == typeof(int)) return int.TryParse(value, out var i) ? i : 0;
            if (targetType == typeof(float)) return float.TryParse(value, out var f) ? f : 0f;
            if (targetType == typeof(bool)) return value.ToLower() == "true" || value == "1" || value.ToLower() == "y";
            if (targetType == typeof(string)) return value;
            if (targetType.IsEnum) return Enum.Parse(targetType, value, true);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"'{typeName}' 타입 변환 실패 (값: {value})");
        }

        return GetDefault(targetType);
    }

    private object GetDefault(Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;

    public T GetData(string id)
    {
        // 딕셔너리가 비어있다면 (첫 호출 시) 자동으로 채움
        if (_dataDic.Count == 0 && DataList.Count > 0)
        {
            GenerateDictionary();
        }

        if (_dataDic.TryGetValue(id, out T value)) return value;

        Debug.LogWarning($"ID '{id}'를 찾을 수 없습니다.");
        return default;
    }

    private void GenerateDictionary()
    {
        _dataDic.Clear();
        foreach (var data in DataList)
        {
            var idField = typeof(T).GetFields()[0];
            string idValue = idField.GetValue(data).ToString();

            if (!_dataDic.ContainsKey(idValue))
                _dataDic.Add(idValue, data);
        }
    }
}