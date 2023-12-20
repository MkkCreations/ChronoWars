using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance;
    public Team TeamHero;
    public Team TeamEnemy;
    public Team CurrentTeam;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentTeam = TeamHero;
    }

    public void CreateTeams()
    {
        GameManager.Instance.ChangeState(GameState.SpawnBuildings);
    }

    public void HerosTurn()
    {
        CurrentTeam = TeamHero;
    }

    public void EnemyTurn()
    {
        CurrentTeam = TeamEnemy;
    }

    public void ChangeTurn()
    {
        if (CurrentTeam = TeamHero) CurrentTeam = TeamEnemy;
        else CurrentTeam = TeamHero;
    }

}

