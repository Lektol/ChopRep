using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBalance : MonoBehaviour
{
    public static readonly int[] RewardsPricesLevel1 = {50, 300, 1500, 5000, 10000};
    public static readonly int[] RewardsPricesLevel2 = {25000, 90000, 200000, 750000, 999999};
    // Урон топоров
    public int[] axeDamage = { 5, 15, 40, 100, 250, 600, 1400, 3200, 7000, 15000 };

// Цены топоров
    public int[] axePrices = { 0, 800, 4000, 20000, 80000, 200000, 500000, 1000000, 2000000, 4000000 };

// Цена перехода на следующий биом
    public int[] biomeUnlockPrices = { 500, 3000, 15000, 60000, 150000, 350000, 700000, 1500000, 3000000 };

// Прочность деревьев
    public int[] treeHealth = { 30, 100, 280, 700, 1500, 3000, 5500, 10000, 18000, 30000 };

// Очки за дерево (прочность ×2)
    public int[] treeReward = { 60, 200, 560, 1400, 3000, 6000, 11000, 20000, 36000, 60000 };
}
