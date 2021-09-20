using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSet : MonoBehaviour
{
    public GameObject PositionGrid, EmptyGrid, Tester;
    void Awake()
    {
        int DX = PlayerPrefs.GetInt("dimensionX");
        int DY = PlayerPrefs.GetInt("dimensionY");
        int DZ = PlayerPrefs.GetInt("dimensionZ");

        for (int i = 0; i < DX; i++)
        {
            for (int j = 0; j < DY; j++)
            {
                for (int k = 0; k < DZ; k++)
                {
                    var x = Instantiate(EmptyGrid, PositionGrid.transform);
                    x.transform.position = new Vector3(i * 5, j * 5, k * 5);
                    x.name = "" + i + "" + j + "" + k;
                }
            }
        }

    }

}
