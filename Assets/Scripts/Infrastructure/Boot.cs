using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    [SerializeField] int delayFrames;
    void Start()
    {
        StartCoroutine(DelayedBoot());
    }

    IEnumerator DelayedBoot()
    {
        while (delayFrames > 0)
        {
            delayFrames--;
            yield return null;
        }

        //SceneManager.LoadScene(0);

        Geekplay.Instance.LoadScene(1, 0f, () =>
        {
            Geekplay.Instance.GameReady();
        });
    }
}