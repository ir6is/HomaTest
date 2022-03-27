using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _heightChange = 1;
    [SerializeField] private int _platformRemove = 1;

    public int Platform2Remove => _platformRemove;
    public int Height2Change => _heightChange;
    private ObstaclesGroup _group;

    public void Initialize(ObstaclesGroup group)
    {
        _group = group;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();

        if (player)
        {
            if (_group)
            {
                _group.Enter(player, this);
            }
            else
            {
                player.ChangePlatformCount(-_platformRemove);
                player.Height += _heightChange;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();


        if (player)
        {
            if (_group)
            {
                _group.Exit(player, this);
            }
            else
            {
                player.Height -= _heightChange;
            }
        }
    }
}