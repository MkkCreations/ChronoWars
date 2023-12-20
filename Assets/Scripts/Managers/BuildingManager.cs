using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    [SerializeField] private List<ScriptableBuilding> _blueBuildings;
    [SerializeField] private List<ScriptableBuilding> _redBuildings;
    private Building _selectedBuilding;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        // Retourne tous les batiments dans le dossier Prefabs/Buildings/Blue
        _blueBuildings = Resources.LoadAll<ScriptableBuilding>("Buildings/Blue").ToList();
    }

    public void SpawnBuildingTeamOne()
    {

        // récupère un batiment "Caserne" dans la liste des batiments
        var groundBuildingPrefab = GetGroundBuilding<Building>(TypeUnitBuilding.Ground);

        var spawnGroundBuilding = Instantiate(groundBuildingPrefab);
        var spawnOnTile = GridManager.Instance.GetBuildingTilesTeamOne();

        spawnOnTile.SetBuilding(spawnGroundBuilding);
        Team team = TeamManager.Instance.TeamHero;
        spawnGroundBuilding.team = team;
        team.AddBuilding(spawnGroundBuilding);

        GameManager.Instance.ChangeState(GameState.SpawnHeroes);
    }

    public void SpawnBuildingTeamTwo()
    {

    }

    private T GetGroundBuilding<T>(TypeUnitBuilding typeUnitBuilding) where T : Building
    {
        return (T)_blueBuildings.Where(u => u.typeUnitBuilding == typeUnitBuilding).First().buildingPrefab;
    }

    // Getter et Setter
    public Building SelectedBuilding
    {
        get => _selectedBuilding;
        set => _selectedBuilding = value;
    }


}