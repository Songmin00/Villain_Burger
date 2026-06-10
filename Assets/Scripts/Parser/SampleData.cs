using System;
using UnityEngine;

/// <summary>
/// 테이블 파싱용 개별 데이터 클래스 생성 시 참고용 예시
/// </summary>

public enum SampleEnum //enum은 사용되는 개별 데이터 클래스에서 정의하도록 함(최초 사용 기준)
{
    None, Korean, Chinese, American
}

[Serializable]
public class SampleData
{
    //변수명은 칼럼 명과 동일하게 설정(대소문자 정확)
    public string Id;
    public string Name;
    public int Level;
    public float Value;
    public bool IsMan;
    public SampleEnum Nation;
}

[CreateAssetMenu(fileName = "SampleDB", menuName = "DataSO/Sample")]
public class SampleDB : GoogleSheetParser<SampleData> { }