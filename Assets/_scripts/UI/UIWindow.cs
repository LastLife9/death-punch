using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum WindowType
{
    Menu,
    Lose,
    Game
}

public class UIWindow : MonoBehaviour
{
    public WindowType WindowType;

    public virtual void Init()
    {

    }

    public void Show(bool animated, Action onComplete = null)
    {
        if (animated)
        {
            Vector3 curScale = transform.localScale;
            transform.localScale = Vector3.zero;
            gameObject.SetActive(true);

            transform.DOScale(curScale, 0.1f)
                .SetEase(Ease.Flash)
                .OnComplete(() =>
                {
                    transform.localScale = curScale;
                    onComplete?.Invoke();
                })
                .SetUpdate(true);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void Hide(bool animated, Action onComplete = null)
    {
        if (animated)
        {
            Vector3 curScale = transform.localScale;

            transform.DOScale(Vector3.zero, 0.1f)
                .SetEase(Ease.Flash)
                .OnComplete(() =>
                {
                    transform.localScale = curScale;
                    gameObject.SetActive(false);
                    onComplete?.Invoke();
                })
                .SetUpdate(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
