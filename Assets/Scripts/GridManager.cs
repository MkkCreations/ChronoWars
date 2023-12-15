using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Instance unique de la classe
    public static GridManager Instance;
    [SerializeField] private int _width, _hight;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Transform _cam;

    private Dictionary<Vector2Int, Tile> _tiles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2Int, Tile>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _hight; y++)
            {
                var instTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                instTile.name = $"Tile {x} {y}";

                // Placer une unité pour la team 1 en (0,0)
                if (x == 5 && y == 5)
                {
                    var unit = Instantiate(_unitPrefab, new Vector3(x, y), Quaternion.identity);
                    unit.name = $"Unit {x} {y}";
                    unit.currentTile = instTile;
                    instTile.SetUnite(unit.gameObject);
                }

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);

                instTile.Init(isOffset);

                _tiles[new Vector2Int(x, y)] = instTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_hight / 2 - 0.5f, -10);

    }

    public Tile GetTileAtPosition(Vector2Int pos)
    {
        if(_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }

    public Dictionary<Vector2Int, Tile> map
    {
        get
        {
            return _tiles;
        }
        set
        {
            _tiles = value;
        }
    }

    public List<Tile> GetSurroundingTiles(Vector2Int pos)
    {
        var tiles = new List<Tile>();

        var tile = GetTileAtPosition(pos + Vector2Int.up);
        if (tile != null)
        {
            tiles.Add(tile);
        }

        tile = GetTileAtPosition(pos + Vector2Int.down);
        if (tile != null)
        {
            tiles.Add(tile);
        }

        tile = GetTileAtPosition(pos + Vector2Int.left);
        if (tile != null)
        {
            tiles.Add(tile);
        }

        tile = GetTileAtPosition(pos + Vector2Int.right);
        if (tile != null)
        {
            tiles.Add(tile);
        }

        return tiles;
    }
    
}
