using UnityEngine;
using System.Collections;

public interface ICommand
{
    void UpdateGrid();
    bool BoundaryCheck(Vector3 nextPos);
}