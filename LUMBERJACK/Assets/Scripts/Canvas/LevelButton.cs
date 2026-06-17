using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private bool IsOpen = false;
    [SerializeField] private int NumLevel;
    [SerializeField] private int LevelCost;
    [SerializeField] private GameObject LockImage;
    [SerializeField] private CanvasUpdater _canvasUpdater;

    void Start()
    {
        if(InventorySystem.Instance.OpenedLevels >= NumLevel)
        {
            IsOpen = true;
            //if(LockImage != null) LockImage.SetActive(false);
            LockImage?.SetActive(false);
        }
    }

    public void OnPressButton()
    {
        if (IsOpen)
        {
            if(SceneManager.GetActiveScene().name == "Level" + NumLevel)
            {
                _canvasUpdater.Panellevels();
                return;
            }
            SceneManager.LoadScene("Level" + NumLevel);
        }
        else
        {
            TryOpenNewLevel();
        }
    }

    public void TryOpenNewLevel()
    {
        if(InventorySystem.Instance.TreesCount >= LevelCost)
        {
            IsOpen = true;
            InventorySystem.Instance.MinusTrees(LevelCost);
            InventorySystem.Instance.OpenedLevels++;
            LockImage?.SetActive(false);
        }
    }
}
