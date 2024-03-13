using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly MonoBehaviour mono;
    private readonly GameObject curtain;

    public SceneLoader(MonoBehaviour monoBehaviour, GameObject curtain)
    {
        mono = monoBehaviour;
        this.curtain = curtain;
    }

    public void LoadScene(int index, UnityAction onLoad = null)
    {
        if (!Geekplay.Instance.mobile)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Geekplay.Instance.Leaderboard("Points", Geekplay.Instance.PlayerData.LeaderboardSlap);
        Debug.Log($"Leaderbord: " + Geekplay.Instance.PlayerData.LeaderboardSlap);

        curtain.SetActive(true);
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
        curtain.SetActive(false);
    }
}
