using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private MonoBehaviour mono;
    public SceneLoader(MonoBehaviour monoBehaviour)
    {
        mono = monoBehaviour;
    }

    public void LoadScene(int index, UnityAction onLoad = null)
    {
        if (!Geekplay.Instance.mobile)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        mono.StartCoroutine(Loading(operation, onLoad));
    }

    IEnumerator Loading(AsyncOperation operation, UnityAction onLoad)
    {
        Debug.Log("Operation none done");
        while (!operation.isDone)
        {
            yield return new WaitForEndOfFrame(); 
        }
        Debug.Log("Operation is done");
        onLoad?.Invoke();
    }
}
