using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero1 : BaseHero
{

    [SerializeField] private int _moveRange = 3;
    [SerializeField] private float _speed = 2f;

    void Start()
    {
        rangeFinder = new RangeFinder();
        rangeFinderTiles = new List<Tile>();
        pathFinder = new PathFinder();
        path = new List<Tile>();
        _arrowTranslator = new ArrowTranslator();
    }

    void Update()
    {
        if (UnitManager.Instance.SelectedHero == this && !isMoving)
        {
            GetInRangeTiles();
            tileToMove = GridManager.Instance.HoveredTile;
            if (rangeFinderTiles.Contains(tileToMove))
            {
                path = pathFinder.FindPath(currentTile, tileToMove, rangeFinderTiles);

                RemoveArrows();

                for (int i = 0; i < path.Count; i++)
                {
                    var previousTile = i > 0 ? path[i - 1] : currentTile;
                    var futureTile = i < path.Count - 1 ? path[i + 1] : null;

                    var arrow = _arrowTranslator.TranslateDirection(previousTile, path[i], futureTile);
                    path[i].SetSprite(arrow);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (!rangeFinderTiles.Contains(tileToMove) || currentTile == tileToMove)
                {
                    isMoving = false;
                    GetInRangeTiles();
                    return;
                }
                else
                {
                    isMoving = true;
                    tileToMove.HideTile();
                }
            }
        }


        if (path.Count > 0 && isMoving)
        {
            MoveAlongPath();
        }
    }

    public void MoveAlongPath()
    {
        var step = _speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, path[0].transform.position, step);

        if (Vector2.Distance(transform.position, path[0].transform.position) < 0.00001f)
        {
            PositionCharacterOnLine(path[0]);
            path.RemoveAt(0);
        }

        if (path.Count == 0)
        {
            GetInRangeTiles();
            isMoving = false;
        }
    }

    public void PositionCharacterOnLine(Tile tile)
    {
        transform.position = new Vector2(tile.transform.position.x, tile.transform.position.y + 0.0001f);
        GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        tile.SetUnit(this);
    }

    public void GetInRangeTiles()
    {
        List<Tile> allRange = rangeFinder.GetTilesInRange(new Vector2Int((int)currentTile.transform.position.x, (int)currentTile.transform.position.y), _moveRange);
        rangeFinderTiles = new List<Tile>();
        foreach (Tile item in allRange)
        {
            if (item.TileType == TileType.Water || item.OccupiedUnit)
            {
                continue;
            }
            rangeFinderTiles.Add(item);
            item.ShowTile();
        }
    }

    private void RemoveArrows()
    {
        foreach (var item in rangeFinderTiles)
        {
            GridManager.Instance.map[item.grid2DLocation].SetSprite(ArrowTranslator.ArrowDirection.None);
        }
    }
}
