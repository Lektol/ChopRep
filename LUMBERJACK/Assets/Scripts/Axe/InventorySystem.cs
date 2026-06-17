using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }
    //private int treesCount;
    public int TreesCount
    {
        get{ return YG2.saves.trees; }
        private set 
        { 
            YG2.saves.trees = value; 
            //YG2.SaveProgress();
        }
    }
    public float SpeedRotation = 1;
    public int TypePlane
    {
        get{ 
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            switch (sceneName)
            {
                case "Level1":
                    return YG2.saves.typePlane;
                case "Level2":
                    return YG2.saves.typePlane2;
            }
            return YG2.saves.typePlane;
            }
        set 
        { 
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            switch (sceneName)
            {
                case "Level1":
                    YG2.saves.typePlane = value;
                    break;
                case "Level2":
                    YG2.saves.typePlane2 = value;
                    break;
            }
            YG2.SaveProgress();
        }
    }

    public int TypePlane2
    {
        get{ return YG2.saves.typePlane2; }
    }

    public int OpenedLevels
    {
        get{ return YG2.saves.openedLevels; }
        set 
        { 
            YG2.saves.openedLevels = value; 
            YG2.SaveProgress();
        }
    }

    public List<bool> Axes
    {
        get{ return YG2.saves.axes; }
        set 
        { 
            YG2.saves.axes = value; 
            YG2.SaveProgress();
        }
    }
    //public GameObject[] AllAxesInGame;
    //public int CountAllAxes() => AllAxesInGame.Length;


    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Debug.Log("Уничтожаем клон " + gameObject.name);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddTrees(int count)
    {
        if(count < 0)
        {
            throw new System.Exception("Прибавляй только положительное число");
        } 
        TreesCount += count;
        //YG2.SetLeaderboard("LeaderBoard", TreesCount);
    }

    public void MinusTrees(int count)
    {
        if(count < 0)
        {
            throw new System.Exception("Отнимай только положительное число");
        } 
        if(TreesCount < count)
        {
            throw new System.Exception("Недостаточно деревьев");
        }
        TreesCount -= count;
        //YG2.SetLeaderboard("LeaderBoard", TreesCount);
    }
}
