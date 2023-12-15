using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _selector;
    [SerializeField] private GameObject _unit = null;
    private ArrowTranslator arrowTranslator;

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
    }

    void OnMouseExit()
    {
        _selector.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (_unit != null)
        {
            _unit.GetComponent<BaseUnit>().isSelect = true;
        }
    }

    public bool isHovered()
    {
        return _selector.activeSelf;
    }


    public void SetUnite(GameObject unit)
    {
        _unit = unit;
    }

    public GameObject GetUnite()
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
