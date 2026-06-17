using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAbleSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> Planes;
    [SerializeField] private int CountPlanesVisible;

    public void SetPlanesAbels(int typeOfPlane)
    {
        for (int i = 0; i < Planes.Count; i++)
        {
            Planes[i].SetActive(true);
        }
        int razriv = Planes.Count - typeOfPlane;
        if(razriv > CountPlanesVisible)
        {
            for (int i = typeOfPlane + CountPlanesVisible; i < Planes.Count; i++)
            {
                Planes[i].SetActive(false);
            }
        }
        Debug.Log("Да");
    }
}
