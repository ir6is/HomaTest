using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpikeAnim : MonoBehaviour
{
    [SerializeField]
    private float _duration;
    [SerializeField]
    private float _strenght;

    private Tweener _tweener;

    private void Start()
    {
        _tweener = transform.DOShakeScale(_duration, _strenght,5).SetLoops(-1);
    }

    private void OnDestroy()
    {
        _tweener.Kill();
    }
}
