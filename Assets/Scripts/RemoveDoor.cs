using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class removeDoor : MonoBehaviour
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

    [SerializeField]
    private float _rotation = 0;

    private int _collisionCount = 0;
    private AudioSource _audioSource;

    void Start()
    {

    }


    public void Activate()
    {

        foreach (var tile in _activatePaintTiles)
        {
            var currentCell = new Vector3Int((int)tile.x, (int)tile.y);
            _tilemap.SetTile(currentCell, _doorTile);
            _tilemap.SetTransformMatrix(currentCell, Matrix4x4.Rotate(Quaternion.Euler(0, 0, _rotation)));
        }

        foreach (var tile in _activateUnPaintTiles)
        {
            var currentCell = new Vector3Int((int)tile.x, (int)tile.y);
            _tilemap.SetTile(currentCell, null);
            _tilemap.SetTransformMatrix(currentCell, Matrix4x4.Rotate(Quaternion.Euler(0, 0, _rotation)));
        }
    }

    public void Deactivate()
    {

        foreach (var tile in _deactivatePaintTiles)
        {

            var currentCell = new Vector3Int((int)tile.x, (int)tile.y);
            _tilemap.SetTile(currentCell, _doorTile);
            _tilemap.SetTransformMatrix(currentCell, Matrix4x4.Rotate(Quaternion.Euler(0, 0, _rotation)));
        }

        foreach (var tile in _deactivateUnPaintTiles)
        {

            var currentCell = new Vector3Int((int)tile.x, (int)tile.y);
            _tilemap.SetTile(currentCell, null);
            _tilemap.SetTransformMatrix(currentCell, Matrix4x4.Rotate(Quaternion.Euler(0, 0, _rotation)));
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
