using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesGroup : MonoBehaviour
{
    [SerializeField] private Obstacle[] _obstacles;

    private readonly List<Obstacle> _activeObstacles = new List<Obstacle>();
    private int _currentRemovedPlatforms;
    private int _currentHeight;

    public void Enter(Player player, Obstacle obstacle)
    {
        _activeObstacles.Add(obstacle);
        if (obstacle.Platform2Remove > _currentRemovedPlatforms)
        {
            player.ChangePlatformCount(-(obstacle.Platform2Remove - _currentRemovedPlatforms));
            _currentRemovedPlatforms = obstacle.Platform2Remove;
        }

        player.Height = _activeObstacles.Max(a => a.Height2Change);
    }

    public void Exit(Player player, Obstacle obstacle)
    {
        _activeObstacles.Remove(obstacle);

        if (_activeObstacles.Count>0)
        {
            player.Height = _activeObstacles.Max(a => a.Height2Change);
        }
        else
        {
            player.Height = 0;
        }
    }

    private void Awake()
    {
        foreach (var item in _obstacles)
        {
            item.Initialize(this);
        }
    }
}