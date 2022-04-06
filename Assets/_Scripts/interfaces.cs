using UnityEngine;
using System.Collections;

public interface ICommand
{
    void UpdateGrid(WorldGrid.BlockType blockType);
    void RefreshGrid();
    bool IsWithinBoundary(Vector3 nextPos);
}