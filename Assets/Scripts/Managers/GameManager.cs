using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    [SerializeField] private List<Team> _teams;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Team _currentTeam;
    //[SerializeField] private int _day = 1;
    // Selector
    [SerializeField] private GameObject _selector;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SetTeams:
                TeamManager.Instance.CreateTeams();
                break;
            case GameState.SpawnBuildings:
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeros();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.HeroesTurn:
                TeamManager.Instance.HerosTurn();
                break;
            case GameState.EnemiesTurn:
                TeamManager.Instance.EnemyTurn();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState
{
    GenerateGrid,
    SetTeams,
    SpawnBuildings,
    SpawnHeroes,
    SpawnEnemies,
    HeroesTurn,
    EnemiesTurn
}