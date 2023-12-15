using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _selector;
    [SerializeField] private BaseUnit _unit = null;
    [SerializeField] private bool _isWalkable;
    public bool isBlocked = false;
    private ArrowTranslator arrowTranslator;

    public BaseUnit OccupiedUnit;
    public bool walkable => _isWalkable && OccupiedUnit == null;

    // Liste de arrows
    public List<Sprite> arrows;

    // Position dans la grille
    public Vector3Int gridLocation;
    public Vector2Int grid2DLocation { get { return new Vector2Int((int)this.transform.position.x, (int)this.transform.position.y); } }

    // G = distance from start node
    public int G;
    // H = distance from end node
    public int H;
    // F = G + H (total cost)
    public int F
    {
        get { return G + H; }
    }

    // The previous tile in the path
    public Tile Previous;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        arrowTranslator = new ArrowTranslator();

    }

    void OnMouseEnter()
    {
        _selector.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    void OnMouseExit()
    {
        _selector.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.HeroesTurn) return;

        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            else
            {
                if (UnitManager.Instance.SelectedHero != null)
                {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }
        } else
        {
            if (UnitManager.Instance.SelectedHero != null)
            {
                SetUnit(UnitManager.Instance.SelectedHero);
                UnitManager.Instance.SetSelectedHero(null);
            }
        }
    }

    public bool isHovered()
    {
        return _selector.activeSelf;
    }


    public void SetUnit(BaseUnit unit)
    {
        if (!walkable) return;
        if (unit.currentTile != null) unit.currentTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.currentTile = this;
    }

    public BaseUnit GetUnite()
    {
        return _unit;
    }

    public void ShowTile()
    {
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0.6f);

    }

    public void HideTile()
    {
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 1f);
    }

    public void SetSprite(ArrowTranslator.ArrowDirection d)
    {
        SpriteRenderer sprite = GetComponentsInChildren<SpriteRenderer>()[1];
        if (d == ArrowTranslator.ArrowDirection.None)
        {
            sprite.color = new Color(1, 1, 1, 0);
        }
        else
        {
            sprite.color = new Color(1, 1, 1, 1f);
            sprite.sprite = arrows[(int)d];
            sprite.sortingOrder = 2;
        }
    }
}
