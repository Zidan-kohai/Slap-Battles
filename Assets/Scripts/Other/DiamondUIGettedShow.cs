using System.Collections;
using UnityEngine;

public class DiamondUIGettedShow : MonoBehaviour
{
    public float animationDuration;
    public Vector3 startLocalPosition;

    private void Start()
    {
        startLocalPosition = transform.localPosition;
    }
    public void Run()
    {
        gameObject.SetActive(true);
        StartCoroutine(Animaition());
    }

    private IEnumerator Animaition()
    {
        float lastedTime = 0f;
        while(lastedTime < animationDuration)
        {
            lastedTime += Time.deltaTime;
            gameObject.transform.position += new Vector3(0, 0.01f, 0);
            yield return null;
        }

        gameObject.SetActive(false);
        transform.localPosition = startLocalPosition;
    }
}
