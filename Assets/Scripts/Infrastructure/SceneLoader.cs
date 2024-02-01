using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void LoadScene(int index, UnityAction onLoad = null)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        Loading(operation, onLoad);
    }

    IEnumerator Loading(AsyncOperation operation, UnityAction onLoad)
    {
        while(!operation.isDone)
        {
            yield return new WaitForEndOfFrame(); 
        }

        onLoad?.Invoke();
    }
}
