using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class AutoSave : MonoBehaviour
{
    [SerializeField] private float SecBetweenSaves;

    void Start()
    {
        StartCoroutine(Save());
    }
    IEnumerator Save()
    {
        yield return new WaitForSeconds(SecBetweenSaves);
        YG2.SaveProgress();
        YG2.SetLeaderboard("LeaderBoard", InventorySystem.Instance.TreesCount);
        StartCoroutine(Save());
        Debug.Log("Сохранил прогресс");
    }
}
