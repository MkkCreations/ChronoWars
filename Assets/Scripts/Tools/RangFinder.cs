#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RangeFinder
{
    public List<Tile> GetTilesInRange(Vector2Int location, int range)
    {
        var startingTile = GridManager.Instance.map[location];
        var inRangeTiles = new List<Tile>();
        int stepCount = 0;

        inRangeTiles.Add(startingTile);

        //Should contain the surroundingTiles of the previous step. 
        var tilesForPreviousStep = new List<Tile>();
        tilesForPreviousStep.Add(startingTile);
        while (stepCount < range)
        {
            var surroundingTiles = new List<Tile>();

            foreach (var item in tilesForPreviousStep)
            {
                surroundingTiles.AddRange(GridManager.Instance.GetSurroundingTiles(new Vector2Int((int)item.transform.position.x, (int)item.transform.position.y)));
            }

            inRangeTiles.AddRange(surroundingTiles);
            tilesForPreviousStep = surroundingTiles.Distinct().ToList();
            stepCount++;
        }

        return inRangeTiles.Distinct().ToList();
    }

}
