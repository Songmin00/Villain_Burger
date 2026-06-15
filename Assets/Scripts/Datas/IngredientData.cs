using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientDB", menuName = "DataSO/Ingredient")]
public class IngredientDB : GoogleSheetParser<IngredientData> { }

public enum ingType //enum은 사용되는 개별 데이터 클래스에서 정의하도록 함(최초 사용 기준)
{
    None, Bun, Patty, Vegetable, Sauce, Topping
}

[Serializable]
public class IngredientData
{
    //변수명은 칼럼 명과 동일하게 설정(대소문자 정확)
    public string id;
    public string nameKey;
    public string descKey;
    public ingType type;
    public int price;
    public int unlockDate;    
}