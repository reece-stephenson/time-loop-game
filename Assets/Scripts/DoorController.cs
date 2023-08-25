using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorController : MonoBehaviour
{

    [SerializeField]
    private Tilemap _tilemap;

    [SerializeField]
    private Vector2[] _activatePaintTiles;

    [SerializeField]
    private Vector2[] _activateUnPaintTiles;

    [SerializeField]
    private Vector2[] _deactivatePaintTiles;

    [SerializeField]
    private Vector2[] _deactivateUnPaintTiles;

    [SerializeField]
    private Tile _doorTile;

    private int _collisionCount = 0;

    public void Activate()
    {
        foreach (var tile in _activatePaintTiles)
        {
            _tilemap.SetTile(new Vector3Int((int)tile.x, (int)tile.y), _doorTile);
        }

        foreach (var tile in _activateUnPaintTiles)
        {
            _tilemap.SetTile(new Vector3Int((int)tile.x, (int)tile.y), null);
        }
    }

    public void Deactivate()
    {
        foreach (var tile in _deactivatePaintTiles)
        {
            _tilemap.SetTile(new Vector3Int((int)tile.x, (int)tile.y), _doorTile);
        }

        foreach (var tile in _deactivateUnPaintTiles)
        {
            _tilemap.SetTile(new Vector3Int((int)tile.x, (int)tile.y), null);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collisionCount++;
        UpdateDoor();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _collisionCount--;
        UpdateDoor();
    }

    private void UpdateDoor()
    {
        if (_collisionCount % 2 == 0)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }
}
