using System;
using UnityEngine;


public enum burgerType //enum은 사용되는 개별 데이터 클래스에서 정의하도록 함(최초 사용 기준)
{
    None, Beef, Chicken, Pork, Shrimp
}

[Serializable]
public class BurgerData
{
    //변수명은 칼럼 명과 동일하게 설정(대소문자 정확)
    public string id;
    public string nameKey;
    public string descKey;
    public burgerType type;    
    public int unlockDate;
    public int BasicBun;
    public int BriocheBun;
    public int BeefPatty;
    public int PorkPatty;
    public int ChickenPatty;
    public int ShrimpPatty;
    public int Lettuce;
    public int Tomato;
    public int Onion;
    public int Pickle;
    public int KetchupSauce;
    public int MayonnaiseSauce;
    public int MustardSauce;
    public int BarbequeSauce;
    public int TartarSauce;
    public int TeriyakiSauce;
    public int Cheese;
    public int Bacon;
    public int Jalapeno;
    public int Pineapple;
}

[CreateAssetMenu(fileName = "BurgerDB", menuName = "DataSO/Burger")]
public class BurgerDB : GoogleSheetParser<BurgerData> { }