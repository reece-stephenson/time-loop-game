using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TrapController : MonoBehaviour
{

    [SerializeField]
    private Tile[] _tiles;

    private Trap[] _traps =
    {
        new Trap
        {
            IsActive = true,
            StartPosition = new Vector2(-34, -6),
            Direction = Direction.UP,
            Tiles = new int[] { 0 }
        }
    };

    private Tilemap _tilemap;

    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    public void ActivateTrap(Vector2 position)
    {
        var (trap, trapPositions) = GetTrap(position);
        var currentCell = new Vector3Int((int)trap.StartPosition.x, (int)trap.StartPosition.y);

        _tilemap.SetTile(currentCell, _tiles[trap.Tiles[0]]);

        for (int i = 1; i < trapPositions.Length; i++)
        {
            var trapPosition = trapPositions[i];
            currentCell = new Vector3Int((int)trapPosition.x, (int)trapPosition.y);
            _tilemap.SetTile(currentCell, _tiles[trap.Tiles[i]]);
        }
        
    }

    public void DeactivateTrap(Vector2 position)
    {
        var (trap, trapPositions) = GetTrap(position);
        var currentCell = new Vector3Int((int)trap.StartPosition.x, (int)trap.StartPosition.y);

        _tilemap.SetTile(currentCell, _tiles[trap.Tiles[0]]);

        for (int i = 1; i < trapPositions.Length; i++)
        {
            var trapPosition = trapPositions[i];
            currentCell = new Vector3Int((int)trapPosition.x, (int)trapPosition.y);
            _tilemap.SetTile(currentCell, null);
        }
    }

    private (Trap, Vector2[]) GetTrap(Vector2 position)
    {
        foreach (var trap in _traps)
        {
            var trapPositions = GetTrapTilePositions(trap);
            if (trapPositions.Contains(position))
                return (trap, trapPositions);
        }

        return (null, null);
    }

    private Vector2[] GetTrapTilePositions(Trap trap)
    {
        List<Vector2> trapPositions = new List<Vector2>();
        trapPositions.Add(trap.StartPosition);

        for (int i=1; i< trap.Tiles.Length; i++)
        {
            float x = trapPositions.LastOrDefault().x;
            float y = trapPositions.LastOrDefault().y;

            if (trap.Direction == Direction.UP)
            {
                y += 1;
            }
            else if (trap.Direction == Direction.DOWN)
            {
                y -= 1;
            }
            else if (trap.Direction == Direction.LEFT)
            {
                x -= 1;
            }
            else if (trap.Direction == Direction.RIGHT)
            {
                x += 1;
            }

            trapPositions.Add(new Vector2(x, y));
        }

        return trapPositions.ToArray();
    }

    public class Trap : MonoBehaviour
    {
        public bool IsActive { get; set; }
        public int[] Tiles { get; set; }
        public Vector2 StartPosition { get; set; }
        public Direction Direction { get; set; }
    }

    public enum Direction
    {
        UP = 0, DOWN = 1, LEFT = 2, RIGHT = 3
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            collision.gameObject.GetComponent<PlayerMovement>().IsDead = true;
        }
        catch
        {
            collision.gameObject.GetComponent<PlayerCopy>().IsDead = true;
        }
    }
}
