using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DiamondUIGettedShow : MonoBehaviour
{
    public float animationDuration;
    public Vector3 startLocalPosition;
    [SerializeField] private Transform targetToRotate;

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

            Vector3 forward = (targetToRotate.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);
            Vector3 euler = rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, euler.y, 0);

            yield return null;
        }

        gameObject.SetActive(false);
        transform.localPosition = startLocalPosition;
    }
}
