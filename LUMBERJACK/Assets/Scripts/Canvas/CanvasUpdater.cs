using System.Collections;
using TMPro;
using UnityEngine;
using YG;
using UnityEngine.SceneManagement;

public class CanvasUpdater : MonoBehaviour
{
    //public static CanvasUpdater Instance { get; private set; }
    [Header("MenuPanel")]
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject LeaderBoardPanel;
    [SerializeField] GameObject PanelLevels;
    [SerializeField] private TextMeshProUGUI treesCountAdvGiftMP; //MP - MenuPanel
    [Header("PlayablePanel")]
    [SerializeField] private GameObject PlayablePanel;
    [SerializeField] private TextMeshProUGUI treesCount;
    [SerializeField] GameObject PanelSuccess;
    [SerializeField] GameObject Joystick;
    [SerializeField] TextMeshProUGUI TextAdvTimer;
    [Header("ShopPanel")]
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private TextMeshProUGUI treesCountAdvGiftSP; //SP - ShopPanel

    void Awake()
    {
        //Debug.Log(Instance);
        // if(Instance != null && Instance != this)
        // {
        //     Debug.Log("Уничтожаем клона");
        //     Destroy(gameObject);
        //     return;
        // }
        // Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        EventManager.OnTreeDead += ChangeTreesCount;
        EventManager.OnChangeTypeOfPlane += ChangeTreesCount;
        EventManager.OnChangeTypeOfPlane += ChangeTreesAdvGift;
        EventManager.OnAdv += ShowAd;
    }

    void OnDisable()
    {
        EventManager.OnTreeDead -= ChangeTreesCount;
        EventManager.OnChangeTypeOfPlane -= ChangeTreesCount;
        EventManager.OnChangeTypeOfPlane -= ChangeTreesAdvGift;
        EventManager.OnAdv -= ShowAd;
    }

    void Start()
    {
        ChangeTreesCount();
        ChangeTreesAdvGift();
    }

    public void ChangeTreesCount()
    {
        treesCount.text = "" + InventorySystem.Instance.TreesCount;
    }
   

    public void ChangeTreesAdvGift()
    {
        treesCountAdvGiftSP.text = "" + CurrentTreesCountAdvGift();
        treesCountAdvGiftMP.text = "" + CurrentTreesCountAdvGift();
    }

    public void Play()
    {
        MenuPanel.SetActive(false);
        PlayablePanel.SetActive(true);
        EventManager.OnChangeGameState?.Invoke(EventManager.GameState.Game);
    }
    public void Menu()
    {
        MenuPanel.SetActive(true);
        PlayablePanel.SetActive(false);
        ShopPanel.SetActive(false);
        EventManager.OnChangeGameState?.Invoke(EventManager.GameState.Menu);
    }
    public void Shop()
    {
        ShopPanel.SetActive(true);
        MenuPanel.SetActive(false);
        EventManager.OnChangeGameState?.Invoke(EventManager.GameState.Shop);
    }

    public void LeaderBoard()
    {
        if(LeaderBoardPanel.activeSelf) LeaderBoardPanel.SetActive(false);
        else LeaderBoardPanel.SetActive(true);
    }

    public void Panellevels()
    {
        if(PanelLevels.activeSelf) PanelLevels.SetActive(false);
        else PanelLevels.SetActive(true);
    }

    public void PlayRewardedAdd()
    {
        
        YG2.RewardedAdvShow("Trees", () =>
        {
            InventorySystem.Instance.AddTrees(CurrentTreesCountAdvGift());
            ChangeTreesCount();
        });
    }

    int CurrentTreesCountAdvGift()
    {
        int treesGiftCount = 0;
        string currentScene = SceneManager.GetActiveScene().name;
        switch (InventorySystem.Instance.OpenedLevels)
        {
            case 1:
                treesGiftCount = DataBalance.RewardsPricesLevel1[InventorySystem.Instance.TypePlane-1];
                break;
            case 2:
                if(currentScene == "Level1") treesGiftCount = DataBalance.RewardsPricesLevel2[InventorySystem.Instance.TypePlane2-1];
                else treesGiftCount = DataBalance.RewardsPricesLevel2[InventorySystem.Instance.TypePlane-1];
                break;
        }
        return treesGiftCount;
    }

    void ShowAd()
    {
        if (!YG2.nowAdsShow)
        {
            YG2.InterstitialAdvShow();
        } 
    }

    public IEnumerator ShowSuccessPanel()
    {
        PanelSuccess.SetActive(true);
        Joystick.SetActive(false);
        int sec_adv = 2;
        EventManager.OnStopMovingPlayer?.Invoke(sec_adv);
        for(int i = sec_adv; i > 0; i--)
        {
            if(YG2.envir.language == "ru") TextAdvTimer.text = "Реклама через: " + i;
            else TextAdvTimer.text = "Advertising in: " + i;
            yield return new WaitForSeconds(1);
        }
        ShowAd();
        PanelSuccess.SetActive(false);
        Joystick.SetActive(true);
    }
}
