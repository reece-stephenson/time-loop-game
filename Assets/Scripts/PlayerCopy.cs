using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCopy : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    public Vector2 startPosition { get; set; }

    public Queue<Vector2> _movements { get; set; }
    public Queue<Vector2> _movementsOriginal { get; set; }

    private IEnumerator _enumerator;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.position = startPosition;
        _enumerator = _movements.GetEnumerator();
        _enumerator.MoveNext();
    }

    void Update()
    {
        //if (_movements != null)
        //    if (_movements.Count > 0)
        //    {
        //        transform.position = Vector2.MoveTowards(_rigidBody.position, _movements.Dequeue(), 1000f);
        //    }
        //    else
        //    {
        //        _rigidBody.position = startPosition;
        //        _movements = GetDeepCopy(_movementsOriginal);
        //    }

        transform.position = Vector2.MoveTowards(_rigidBody.position, (Vector2)_enumerator.Current, 1000f);
        
        if (!_enumerator.MoveNext())
        {
            _enumerator.Reset();
            _enumerator.MoveNext();
        }
    }

    private Queue<Vector2> GetDeepCopy(Queue<Vector2> queue)
    {
        var arr = queue.ToArray();
        var ret = new Queue<Vector2>();

        for (int i = 0; i < arr.Length; i++)
        {
            ret.Enqueue(arr[i]);
        }

        return ret;
    }
}
