using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly MonoBehaviour mono;
    private readonly GameObject curtain;
    private readonly TextMeshProUGUI curtainText;

    public SceneLoader(MonoBehaviour monoBehaviour, GameObject curtain)
    {
        mono = monoBehaviour;
        this.curtain = curtain;
        curtainText = curtain.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void LoadScene(int index, UnityAction onLoad = null)
    {
        if (!Geekplay.Instance.mobile)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (index == 1)
        {
            if(Geekplay.Instance.language == "ru")
            {
                curtainText.text = "טה¸ל ג ץאב";
            }
            else if (Geekplay.Instance.language == "en")
            {
                curtainText.text = "let's go to the hub";
            }
            else if(Geekplay.Instance.language == "tr")
            {
                curtainText.text = "hadi merkeze gidelim";
            }
        }
        else
        {
            if (Geekplay.Instance.language == "ru")
            {
                curtainText.text = "ןמהבמנ כמבבט";
            }
            else if (Geekplay.Instance.language == "en")
            {
                curtainText.text = "lobby selection";
            }
            else if (Geekplay.Instance.language == "tr")
            {
                curtainText.text = "lobi seסimi";
            }
        }

        Geekplay.Instance.Leaderboard("Points", Geekplay.Instance.PlayerData.LeaderboardSlap);
        Debug.Log($"Leaderbord: " + Geekplay.Instance.PlayerData.LeaderboardSlap);

        curtain.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        mono.StartCoroutine(Loading(operation, onLoad));
    }

    private IEnumerator Loading(AsyncOperation operation, UnityAction onLoad)
    {
        while (!operation.isDone)
        {
            yield return new WaitForEndOfFrame(); 
        }
        onLoad?.Invoke();
        curtain.SetActive(false);
    }
}
