using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> Objects = new List<GameObject>();

    public void AddObject(GameObject instance)
    {
        Objects.Add(instance);
    }
    public GameObject Get()
    {
        foreach(GameObject obj in Objects)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }


    // public void AllDisable()
    // {
    //     foreach(GameObject obj in Objects)
    //     {
    //         obj.SetActive(false);
    //     }
    // }

    // public void AllEnable()
    // {
    //     foreach(GameObject obj in Objects)
    //     {
    //         obj.SetActive(true);
    //     }
    // }
}
