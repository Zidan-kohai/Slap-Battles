using System.Collections;
using UnityEngine;

public class SocialNetwork : MonoBehaviour
{
    public SocialNetworkType channel;

    private bool canOpenShop = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7 || !canOpenShop) return;

        canOpenShop = false;
        switch(channel)
        {
            case SocialNetworkType.Telegram:
                    OpenTelegram();
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            StartCoroutine(CanOpenShop());
        }
    }
    private IEnumerator CanOpenShop()
    {
        yield return new WaitForSeconds(1.5f);
        canOpenShop = true;
    }

    public void OpenTelegram()
    {
        Application.OpenURL("https://t.me/+uQFcFVwGmwM3ZDNi");
        Analytics.Instance.SendEvent("Open_TG");
    }

    public enum SocialNetworkType
    {
        Telegram
    }
}