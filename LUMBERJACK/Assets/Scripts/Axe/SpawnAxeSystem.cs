using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAxeSystem : MonoBehaviour
{
    [SerializeField] private Transform[] SpawnPoints;
    [SerializeField] private int LevelNum;
    void Start()
    {
        int sceneIndex = InventorySystem.Instance.TypePlane - 1;
        gameObject.transform.position = SpawnPoints[sceneIndex].position;
    }
}
