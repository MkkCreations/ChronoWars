using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Instance unique de la classe
    public static GridManager Instance;

    [SerializeField] private int _width, _hight;
    [SerializeField] private Tile _grassPrefab, _waterPrefab;
    [SerializeField] private Transform _cam;
    public Tile HoveredTile;

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

    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2Int, Tile>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _hight; y++)
            {
                Tile instTile;
                if ((x >= _width/3 && x <= 2*(_width/3)) && (y >= _hight/3 && y <= 2*_hight/3))
                {
                    instTile = Instantiate(_waterPrefab, new Vector3(x, y), Quaternion.identity);
                    instTile.name = $"Tile {x} {y}";

                    instTile.isBlocked = true;
                } else
                {
                    instTile = Instantiate(_grassPrefab, new Vector3(x, y), Quaternion.identity);
                    instTile.name = $"Tile {x} {y}";
                }

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);

                instTile.Init(isOffset);

                _tiles[new Vector2Int(x, y)] = instTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_hight / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.SetTeams);

    }

    public Tile GetHeroSpawnTile()
    {
        return _tiles.Where(t => t.Key.x < _width / 3 && t.Value.walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _width / 3 && t.Value.walkable).OrderBy(t => Random.value).First().Value;
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
