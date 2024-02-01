using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void LoadSscene(int index, UnityAction onLoad)
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
