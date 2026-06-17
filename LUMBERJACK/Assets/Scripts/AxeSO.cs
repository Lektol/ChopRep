using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Axe",menuName = "ScriptableObject/Axe")]
public class AxeSO : ScriptableObject
{
    public string RuName;
    public string EnName;
    public int Cost;
    public int Damage;
}
