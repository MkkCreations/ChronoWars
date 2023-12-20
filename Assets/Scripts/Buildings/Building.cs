using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    public string BuildingName;

    public Team team;
    public Faction faction;
    public TypeUnitBuilding typeUnitBuilding;
    public int rangeDeploiement;
    public int health;

    public bool isBlocked = false;

    public RangeFinder rangeFinder;
    public List<Tile> rangeFinderTiles;
    public Tile currentTile;
    
    // Getters & Setters
    public SpriteRenderer Renderer
    {
        get => _renderer;
        set => _renderer = value;
    }
    public Faction Faction
    {
        get => faction;
        set => faction = value;
    }
    public Team Team
    {
        get => team;
        set => team = value;
    }
    public TypeUnitBuilding TypeUnitBuilding
    {
        get => typeUnitBuilding;
        set => typeUnitBuilding = value;
    }
    public int RangeDeploiement
    {
        get => rangeDeploiement;
        set => rangeDeploiement = value;
    }
    public int Health
    {
        get => health;
        set => health = value;
    }
    public bool IsBlocked
    {
        get => isBlocked;
        set => isBlocked = value;
    }
    public RangeFinder RangeFinder
    {
        get => rangeFinder;
        set => rangeFinder = value;
    }
    public List<Tile> RangeFinderTiles
    {
        get => rangeFinderTiles;
        set => rangeFinderTiles = value;
    }
    public Tile CurrentTile
    {
        get => currentTile;
        set => currentTile = value;
    }

}