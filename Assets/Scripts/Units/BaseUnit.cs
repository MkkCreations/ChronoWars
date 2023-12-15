using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private int _moveRange = 3;
    [SerializeField] private float _speed = 2f;
    private ArrowTranslator _arrowTranslator;

    public RangeFinder rangeFinder;
    public PathFinder pathFinder;

    public List<Tile> rangeFinderTiles;
    public List<Tile> path;

    public Tile currentTile;
    public Tile tileToMove;

    public bool isMoving = false;
    public bool isSelect = false;

    public void Start()
    {
        rangeFinder = new RangeFinder();
        rangeFinderTiles = new List<Tile>();
        pathFinder = new PathFinder();
        path = new List<Tile>();
        _arrowTranslator = new ArrowTranslator();
    }

    public void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 2;

        if (isSelect) GetInRangeTiles();

        tileToMove = GetHoveredTile();


        if (isSelect)
        {
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
                    print(arrow);
                    path[i].SetSprite(arrow);
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
        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
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

