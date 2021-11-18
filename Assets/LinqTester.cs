using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinqTester : MonoBehaviour
{
    public ItemDatabase itemDatabase;

    void Start()
    {
        var isUnder100 = itemDatabase.itemDatas
            .Any(item => item.itemCost <= 100);

        if (isUnder100)
        {
            print("100 이하 아이템 존재");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
