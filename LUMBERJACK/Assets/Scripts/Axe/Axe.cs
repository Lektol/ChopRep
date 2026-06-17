using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public string RuName;
    public string EnName;
    public int Damage;
    public float SpeedRotation;
    public int Cost;
    private bool CanDamage = false;

    void OnEnable()
    {
        EventManager.OnChangeGameState += ChangeState;
    }

    void OnDisable()
    {
        EventManager.OnChangeGameState -= ChangeState;
    }
    void OnTriggerEnter(Collider collider)
    {
        Tree tree = collider.gameObject.GetComponent<Tree>();
        if (tree != null && tree.CanTakeDamage && CanDamage)
        {
            tree.GetDamage(Damage);
        }
    }

    void ChangeState(EventManager.GameState gameState)
    {
        if(gameState != EventManager.GameState.Game) CanDamage = false;
        else CanDamage = true;
    }
}
