using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Team> _teams;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Team _currentTeam;
    [SerializeField] private int _day = 1;
    // Selector
    [SerializeField] private GameObject _selector;
    
}