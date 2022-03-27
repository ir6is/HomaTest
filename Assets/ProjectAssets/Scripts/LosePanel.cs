using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro.Examples;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TeleType _teleType;
    public IEnumerator Show()
    {
        _canvasGroup.DOFade(1, .3f);
        _teleType.enabled = true;
        yield return new WaitForSeconds(.3f);

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
    }
}
