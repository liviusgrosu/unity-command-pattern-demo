using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public int width, height;
    private int[,] gridData;

    public enum BlockType
    {
        populated = 1,
        unpopulated = 0
    }

    void Awake()
    {
        RefreshGrid();
    }

    void OnDrawGizmos()
    {
        // Draw grid of boxes
        for(int w = 0; w < width; w++)
        {
            for(int h = 0; h < height; h++)
            {
                // Draw a black box if the box was traversed
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

    public void PopulateGrid(Vector3 position, BlockType blockType)
    {
        // Mark grid position as populated
        (int x, int z) = GetGridPosition(position - transform.position);
        gridData[x, z] = (int)blockType;
    }

    public void RefreshGrid()
    {
        gridData = new int[width, height];
    }

    public bool IsWithinBoundary(Vector3 nextPos)
    {
        // Check if next position is out of boundary
        (int x, int z) = GetGridPosition(nextPos - transform.position);
        return x < width && x >= 0 && z < height && z >= 0;
    }

    private (int, int) GetGridPosition(Vector3 position)
    {
        // Get grid position from vector position
        int x = (int)position.x;
        int z = (int)position.z;
        return (x, z);
    }
}
