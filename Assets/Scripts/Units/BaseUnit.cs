using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BaseUnit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private int _moveRange = 3;
    [SerializeField] private float _speed = 2f;
    public string UnitName;
    private ArrowTranslator _arrowTranslator;

    public Faction Faction;

    public RangeFinder rangeFinder;
    public PathFinder pathFinder;

    public List<Tile> rangeFinderTiles;
    public List<Tile> path;

    public Tile currentTile;
    public Tile tileToMove;

    public bool isMoving = false;
    public bool isSelect = false;

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
        if (isSelect && !isMoving)
        {
            tileToMove = GetHoveredTile();
            foreach (var item in rangeFinderTiles)
            {
                GridManager.Instance.map[item.grid2DLocation].SetSprite(ArrowTranslator.ArrowDirection.None);
            }

            if (tileToMove)
            {
                path = pathFinder.FindPath(currentTile, tileToMove, rangeFinderTiles);

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
                    tileToMove.gameObject.GetComponent<Tile>().HideTile();
                }
            }
        }


        if (path.Count > 0 && isMoving)
        {
            MoveAlongPath();
        }
    }

    private Tile? GetHoveredTile()
    {
        return rangeFinderTiles.Where(tile => tile.isHovered()).FirstOrDefault();
    }

    public void MoveAlongPath()
    {
        var step = _speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, path[0].transform.position, step);
        transform.position = new Vector3Int((int)transform.position.x, (int)transform.position.y);

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
        currentTile = tile;
    }

    public void GetInRangeTiles()
    {
        rangeFinderTiles = rangeFinder.GetTilesInRange(new Vector2Int((int)currentTile.transform.position.x, (int)currentTile.transform.position.y), _moveRange);

        foreach (Tile item in rangeFinderTiles)
        {
            item.ShowTile();
        }
    }

}

