using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityAction OnTreeDead; 
    public enum GameState { Menu = 1, Shop = 2, Game = 3 }
    public static UnityAction<GameState> OnChangeGameState;
    public static UnityAction OnChangeTypeOfPlane;
    public static UnityAction OnAdv;
    public static UnityAction<float> OnStopMovingPlayer;
}
