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

        SceneManager.LoadScene("Hub");
    }
}