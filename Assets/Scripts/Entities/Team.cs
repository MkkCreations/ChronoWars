using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    [SerializeField] private List<BaseUnit> _units;
    [SerializeField] private List<Building> _buildings;
    [SerializeField] private Color _color;
    [SerializeField] public Faction Faction;


    public void AddUnit(BaseUnit unit)
    {
        _units.Add(unit);
    }

    public void RemoveUnit(BaseUnit unit)
    {
        _units.Remove(unit);
    }

    public void AddBuilding(Building building)
    {
        _buildings.Add(building);
    }

    public void RemoveBuilding(Building building)
    {
        _buildings.Remove(building);
    }
}
