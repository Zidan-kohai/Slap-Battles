using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader
{
    private readonly MonoBehaviour mono;
    private readonly GameObject curtain;
    private readonly Image curtainLoadVisual;
    private readonly TextMeshProUGUI curtainText;

    public SceneLoader(MonoBehaviour monoBehaviour, GameObject curtain, Image curtainLoadVisual)
    {
        mono = monoBehaviour;
        this.curtain = curtain;
        this.curtainLoadVisual = curtainLoadVisual;
        curtainText = curtain.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void LoadScene(float waitTime, int index, UnityAction onLoad = null)
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

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Geekplay.Instance.Leaderboard("Points", Geekplay.Instance.PlayerData.LeaderboardSlap);
            Debug.Log($"Leaderbord: " + Geekplay.Instance.PlayerData.LeaderboardSlap);
        }


        curtain.SetActive(true);
        mono.StartCoroutine(Loading(waitTime, index, onLoad));
    }

    private IEnumerator Loading(float waitTime, int index, UnityAction onLoad)
    {
        yield return new WaitForSeconds(waitTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            curtainLoadVisual.fillAmount = operation.progress;
            yield return new WaitForEndOfFrame();
        }
        curtain.SetActive(false);

        onLoad?.Invoke();

    }
}
