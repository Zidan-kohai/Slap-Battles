using UnityEngine;

public class SocialNetwork : MonoBehaviour
{
    public SocialNetworkType channel;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;

        switch(channel)
        {
            case SocialNetworkType.Telegram:
                Application.OpenURL("https://t.me/+uQFcFVwGmwM3ZDNi");
                break;
        }
    }


    public enum SocialNetworkType
    {
        Telegram
    }
}