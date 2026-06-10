using System;
using UnityEngine;


[Serializable]
public class StringData
{
    //변수명은 칼럼 명과 동일하게 설정(대소문자 정확)
    public string key;
    public string Kr;
    public string En;
}

[CreateAssetMenu(fileName = "StringDB", menuName = "DataSO/String")]
public class StringDB : GoogleSheetParser<StringData> { }