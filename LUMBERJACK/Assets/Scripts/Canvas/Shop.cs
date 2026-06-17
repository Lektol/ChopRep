using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Shop : MonoBehaviour
{
    private GameObject[] AxesObj;
    [SerializeField] private GameObject ButtonPlay, ButtonBuy;
    [SerializeField] private TextMeshProUGUI NameText, CostText, DamageText, SpeedRotationText;
    [SerializeField] private AllAxesScript _allAxesScript;
    [SerializeField] private CanvasUpdater _canvasUpdater;
    private int CurrentId;
    void Start()
    {
        AxesObj = _allAxesScript.AllAxes;
        CheckSaveForAxeAndEquip();
        ChangeStatsDate();
    }

    // Update is called once per frame
    public void ChangeAxeModel(int i) //i - направление
    {
        AxesObj[CurrentId].SetActive(false);
        int MaxId = InventorySystem.Instance.Axes.Count - 1;
        if(i == 1 && CurrentId == MaxId) CurrentId = 0;
        else if (i == -1 && CurrentId == 0) CurrentId = MaxId;
        else CurrentId += i;
        ChangeStatsDate();
        AxesObj[CurrentId].SetActive(true);
        Axe CurrentAxe = AxesObj[CurrentId].GetComponent<Axe>();
        InventorySystem.Instance.SpeedRotation = CurrentAxe.SpeedRotation;
    }

    public void ChangeButton()
    {
        if (InventorySystem.Instance.Axes[CurrentId])
        {
            ButtonPlay.SetActive(true);
            ButtonBuy.SetActive(false);
        }
        else
        {
            ButtonPlay.SetActive(false);
            ButtonBuy.SetActive(true);
        }
    }

    public void ChangeStatsDate()
    {
        ChangeButton();
        Axe axe = AxesObj[CurrentId].GetComponent<Axe>();
        if(YG2.envir.language == "ru")
        {
            NameText.text = "" + axe.RuName;
            DamageText.text = "Сила: " + axe.Damage;
            SpeedRotationText.text = "Скорость: " + axe.SpeedRotation;
            CostText.text = "Цена: " + axe.Cost;
        }
        else
        {
            NameText.text = "" + axe.EnName;
            DamageText.text = "Power: " + axe.Damage;
            SpeedRotationText.text = "Speed: " + axe.SpeedRotation;
            CostText.text = "Cost: " + axe.Cost;
        }
    }

    public void TryToBuy()
    {
        int Cost = AxesObj[CurrentId].GetComponent<Axe>().Cost;
        if(InventorySystem.Instance.TreesCount >= Cost)
        {
            InventorySystem.Instance.Axes[CurrentId] = true;
            InventorySystem.Instance.MinusTrees(Cost);
            ChangeButton();
            _canvasUpdater.ChangeTreesCount();
            EventManager.OnAdv();
        }
    }

    void CheckSaveForAxeAndEquip()
    {
        for (int i = InventorySystem.Instance.Axes.Count - 1; i > 0; i--)
        {
            if (InventorySystem.Instance.Axes[i])
            {
                AxesObj[0].SetActive(false);
                AxesObj[i].SetActive(true);
                Axe CurrentAxe = AxesObj[i].GetComponent<Axe>();
                InventorySystem.Instance.SpeedRotation = CurrentAxe.SpeedRotation;
                CurrentId = i;
                return;
            }
        }
    }
}
