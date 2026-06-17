using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class AllAxesScript : MonoBehaviour
{
    public GameObject[] AllAxes;

    void Start()
    {
        CheckNewAxesAndAddIt();
    }

    void CheckNewAxesAndAddIt()
    {
        Debug.Log("Буллов топоров" + InventorySystem.Instance.Axes.Count);
        Debug.Log("Всего топоров" + AllAxes.Length);
        if(InventorySystem.Instance.Axes.Count != AllAxes.Length)
        {
            int CountNewAxes = AllAxes.Length-InventorySystem.Instance.Axes.Count;
            for(int i = 0; i < CountNewAxes; i++)
            {
                InventorySystem.Instance.Axes.Add(false);
                YG2.SaveProgress();
                Debug.Log("Добавил новый топор");
            }
        }
    }
}
