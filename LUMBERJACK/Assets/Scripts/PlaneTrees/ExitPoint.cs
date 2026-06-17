using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    [Header("Канвас")]
    [SerializeField] private CanvasUpdater _canvasUpdater;
    [SerializeField] TextMeshProUGUI TreesCountText;
    [Header("НеКанвас :)")]
    [SerializeField] int TreesCount;
    [SerializeField] GameObject Wall;
    [SerializeField] GameObject Bridge;
    BoxCollider _boxCollider;
    private Plane spawnTrees;


    void Start()
    {
        TreesCountText.text = TreesCount + "";
        spawnTrees = GetComponentInParent<Plane>();
        _boxCollider = GetComponent<BoxCollider>();
        CheckSave();
    }
    IEnumerator OnTriggerEnter()
    {
        int treesCount = InventorySystem.Instance.TreesCount;
        if(treesCount >= TreesCount) 
        {
            _boxCollider.enabled = false;
            yield return _canvasUpdater.ShowSuccessPanel();
            OpenNextMap();
            InventorySystem.Instance.MinusTrees(TreesCount);
            InventorySystem.Instance.TypePlane += 1;
            EventManager.OnChangeTypeOfPlane?.Invoke();
            TreesCountText.text = "";
        }
        //else звук типа нет иди нахуй
    }
    void OpenNextMap()
    {
        Bridge.SetActive(true);
        Destroy(Wall);
        Destroy(gameObject);
    }

    void CheckSave()
    {
        if(InventorySystem.Instance.TypePlane > spawnTrees.TypeOfPlane)
        {
            OpenNextMap();
        }
    }
}
