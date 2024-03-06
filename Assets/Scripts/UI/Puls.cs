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
        StartCoroutine(RunAnimation());
    }

    private IEnumerator RunAnimation()
    {
        while (true)
        {
            Icon.DOScale(new Vector3(1.2f, 1.2f, 1.2f), duration).SetEase(ease);

            yield return new WaitForSeconds(duration + 0.1f);

            Icon.DOScale(Vector3.one, duration).SetEase(ease);

            yield return new WaitForSeconds(duration + 0.1f);
        }
    }
}
