using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BaseUnit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    public string UnitName;
    protected ArrowTranslator _arrowTranslator;

    public Team team;
    public Faction faction;

    public RangeFinder rangeFinder;
    public PathFinder pathFinder;

    public List<Tile> rangeFinderTiles;
    public List<Tile> path;

    public Tile currentTile;
    public Tile tileToMove;


    public bool isMoving = false;
    public bool isBlocked = false;

}

