using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabTree;
    private ObjectPool objectPool;
    public int TypeOfPlane;
    [SerializeField] private float maxXZpos;
    [SerializeField] private int CountTrees;
    [SerializeField] private float SecToCheckTreesCount;

    void Start()
    {
        objectPool = GetComponent<ObjectPool>();
        for (int i = 0; i != CountTrees; i += 1)
        {
            objectPool.AddObject(InstantiateTree());
        }
        StartCoroutine(SpawnTreesVoid());
    }


    GameObject InstantiateTree()
    {
        GameObject newTree = Instantiate(prefabTree[Random.Range(0, prefabTree.Length)]);
        SetPosition(newTree);
        return newTree;
    }

    IEnumerator SpawnTreesVoid()
    {   
        yield return new WaitForSeconds(SecToCheckTreesCount);
        GameObject newTree = objectPool.Get();
        if(newTree != null) 
        {
            SetPosition(newTree);
        }
        StartCoroutine(SpawnTreesVoid());
    }

    void SetPosition(GameObject instance)
    {
        float x = Random.Range(-maxXZpos, maxXZpos);
        float z = Random.Range(-maxXZpos, maxXZpos);
        int rotY = Random.Range(0, 360);
        instance.transform.position = transform.position + new Vector3(x,0,z);
        instance.transform.eulerAngles = new Vector3(instance.transform.eulerAngles.x, rotY, instance.transform.eulerAngles.z);
    }
}
