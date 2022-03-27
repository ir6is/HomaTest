using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private Transform _finishLine;
    [SerializeField] private GameObject _finishVirtualCamera;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            StartCoroutine(EnableParticles(player));
        }
    }

    private IEnumerator EnableParticles(Player player)
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(.1f);
        }

        _finishVirtualCamera.SetActive(true);
        _finishLine.DOScale(Vector3.zero, .3f);
        yield return new WaitForSeconds(.3f);
        player.Win();
    }
}
