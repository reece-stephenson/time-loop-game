using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapInteraction : MonoBehaviour
{
    [SerializeField]
    private Vector2 _trapPosition;

    [SerializeField]
    private TrapController _trapController;

    [SerializeField]
    private bool _keepOff;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _trapController.DeactivateTrap(_trapPosition);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!_keepOff)
        {
            _trapController.ActivateTrap(_trapPosition);
        }
    }
}
