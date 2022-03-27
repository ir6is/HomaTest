using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private CanvasGroup _textCanvasGroup;

    public IEnumerator Show()
    {
        _canvasGroup.DOFade(1, .3f);
        yield return new WaitForSeconds(.3f);


        _textCanvasGroup.DOFade(1, .3f);
        yield return new WaitForSeconds(.3f);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
    }
}
