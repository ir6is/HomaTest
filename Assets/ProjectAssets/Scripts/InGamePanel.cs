using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InGamePanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public IEnumerator Show()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        _canvasGroup.DOFade(0, .3f);
    }
}