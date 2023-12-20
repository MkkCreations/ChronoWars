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
}
