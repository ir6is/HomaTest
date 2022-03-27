using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed=10;
    [SerializeField] private ParticleSystem _particle;
    private void Update()
    {
        transform.Rotate(_rotateSpeed * Vector3.up * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            _particle.gameObject.SetActive(true);
            player.ChangePlatformCount(1);
            transform.DOScale(Vector3.zero, .3f);
            Destroy(gameObject,1);
        }
    }
}