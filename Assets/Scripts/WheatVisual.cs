using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class WheatVisual : MonoBehaviour
{
    private Transform startTransform;
    private Transform currentTransform;

    [SerializeField] private Wheat wheat;

    private void Start()
    {
        startTransform = transform;
        currentTransform = startTransform;
        wheat.cutEvent.AddListener(Cut);
        wheat.growEvent.AddListener(Grow);
    }

    public void Cut()
    {
        float scaleDecreasePercentage = 0.6f;
        float duration = 0.1f;

        float height = currentTransform.localScale.y;
        height *= scaleDecreasePercentage;
        transform.DOScaleY(height, duration);
    }

    public void Grow()
    {
        float duration = 1f;
        float height = 0.3f;
        transform.DOScaleY(height, duration);
    }
}
