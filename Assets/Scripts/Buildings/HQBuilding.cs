using UnityEngine;
using System.Collections.Generic;

public class HQBuilding : Building
{
    void Start()
    {
        buildingName = "Quartier général";
        health = 4;
        typeUnitBuilding = TypeUnitBuilding.HQ;
        rangeDeploiement = 0;
        isBlocked = true;
        currentTile = null;
    }

    public void GetInRangeTiles()
    {
        rangeFinderTiles = rangeFinder.GetTilesInRange(new Vector2Int((int)currentTile.transform.position.x, (int)currentTile.transform.position.y), rangeDeploiement);

        foreach (Tile item in rangeFinderTiles)
        {
            item.ShowTile();
        }
    }
}