using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// This class is used to find the shortest path between two tiles
public class PathFinder
{
    private Dictionary<Vector2Int, Tile> searchableTiles;

    public List<Tile> FindPath(Tile start, Tile end, List<Tile> inRangeTiles)

    {
        searchableTiles = new Dictionary<Vector2Int, Tile>();

        List<Tile> openList = new List<Tile>();
        HashSet<Tile> closedList = new HashSet<Tile>();

        if (inRangeTiles.Count > 0)
        {
            foreach (var item in inRangeTiles)
            {
                Vector2Int location = new Vector2Int((int)item.transform.position.x, (int)item.transform.position.y);
                searchableTiles.Add(location, GridManager.Instance.map[location]);
            }
        }
        else
        {
            searchableTiles = GridManager.Instance.map;
        }

        openList.Add(start);

        // While there are still tiles to check
        while (openList.Count > 0)
        {
            Tile currentTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            if (currentTile == end)
            {
                return GetFinishedList(start, end);
            }

            // Check all the neighbours of the current tile
            foreach (var tile in GetNeightbourTiles(currentTile))
            {
                // If the tile is already in the closed list or is not on the same level as the current tile, skip it
                if ( closedList.Contains(tile) || Mathf.Abs(currentTile.transform.position.z - tile.transform.position.z) > 1)
                {
                    continue;
                }

                // Calculate the G and H values for the tile
                tile.G = GetManhattenDistance(start, tile);
                tile.H = GetManhattenDistance(end, tile);

                tile.Previous = currentTile;

                if (!openList.Contains(tile))
                {
                    openList.Add(tile);
                }
            }
        }

        return new List<Tile>();
    }

    // Get the finished list of tiles
    private List<Tile> GetFinishedList(Tile start, Tile end)
    {
        List<Tile> finishedList = new List<Tile>();
        Tile currentTile = end;

        // Add all the tiles to the list
        while (currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.Previous;
        }

        finishedList.Reverse();

        return finishedList;
    }

    // Calculate the manhatten distance between two tiles
    private int GetManhattenDistance(Tile start, Tile tile)
    {
        return (int)Mathf.Abs(start.transform.position.x - tile.transform.position.x) + (int)Mathf.Abs(start.transform.position.y - tile.transform.position.y);
    }

    // Get all the neighbours of a tile
    private List<Tile> GetNeightbourTiles(Tile currentTile)
    {
        var map = GridManager.Instance.map;
        int x = (int)currentTile.transform.position.x;
        int y = (int)currentTile.transform.position.y;

        List<Tile> neighbours = new List<Tile>();

        // Right
        Vector2Int locationToCheck = new Vector2Int(x + 1, y);

        if (searchableTiles.ContainsKey(locationToCheck))
            neighbours.Add(searchableTiles[locationToCheck]);

        // Left
        locationToCheck = new Vector2Int(x - 1, y);

        if (searchableTiles.ContainsKey(locationToCheck))
            neighbours.Add(searchableTiles[locationToCheck]);

        // Top
        locationToCheck = new Vector2Int(x, y + 1);

        if (searchableTiles.ContainsKey(locationToCheck))
            neighbours.Add(searchableTiles[locationToCheck]);

        // Bottom
        locationToCheck = new Vector2Int(x, y - 1);

        if (searchableTiles.ContainsKey(locationToCheck))
            neighbours.Add(searchableTiles[locationToCheck]);

        return neighbours;
    }
}
