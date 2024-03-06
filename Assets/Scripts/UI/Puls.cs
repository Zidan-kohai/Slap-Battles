using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Puls : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Transform Icon;
    public Ease ease;
    private void Start()
    {
        Scale();
    }

    public void Scale()
    {
        Icon.DOScale(new Vector3(1.2f, 1.2f, 1.2f), duration).SetEase(ease).SetDelay(0.1f).OnComplete(() =>
        {
            Icon.DOScale(Vector3.one, duration).SetEase(ease).SetDelay(0.1f).OnComplete(() =>
            {
                Scale();
            });
        });
    }
}
