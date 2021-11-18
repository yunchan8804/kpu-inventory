using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "아이템 데이터베이스", menuName = "KPU/아이템 데이터베이스 만들기", order = 0)]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> itemDatas;
}
