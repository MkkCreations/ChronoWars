using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _hight;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _hight; y++)
            {
                var instTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                instTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);

                instTile.Init(isOffset);

                _tiles[new Vector2(x, y)] = instTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_hight / 2 - 0.5f, -10);

    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}
