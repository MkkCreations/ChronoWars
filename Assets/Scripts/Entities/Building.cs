using UnityEngine;

public class Building : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _renderer;

    public Tile _currentTile;

    public bool _isSelect = false;
}