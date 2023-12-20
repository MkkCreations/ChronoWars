using UnityEngine;
using System.Collections.Generic;

public class GroundBuilding : Building
{
    void Start()
    {
        BuildingName = "Caserne";
        health = 2;
        typeUnitBuilding = TypeUnitBuilding.Ground;
        rangeDeploiement = 1;
        isBlocked = false;
        currentTile = null;
    }

    void Update()
    {
        GetInRangeTiles();
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