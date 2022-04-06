using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public int width, height;
    private int[,] gridData;

    void Awake()
    {
        gridData = new int[width, height];
    }

    void OnDrawGizmos()
    {
        for(int w = 0; w < width; w++)
        {
            for(int h = 0; h < height; h++)
            {
                if (gridData != null && gridData[w, h] == 1)
                {
                    Gizmos.color = new Color(0, 0, 0, 1);
                }
                else
                {
                    Gizmos.color = new Color(1, 1, 1, 1);
                }
                Gizmos.DrawCube(transform.position + new Vector3(w, 0f, h), new Vector3(0.75f, 0.2f, 0.75f));
            }   
        }
    }

    public void PopulateGrid(Vector3 position)
    {
        Vector3 gridPos = position - transform.position;
        int x = (int)gridPos.x;
        int z = (int)gridPos.z;
        gridData[x, z] = 1;
    }
}
