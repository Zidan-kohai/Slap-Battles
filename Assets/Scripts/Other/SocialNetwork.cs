using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialNetwork : MonoBehaviour
{
    public Channel channel;

    private void OnTriggerEnter(Collider other)
    {
        switch(channel)
        {
            case Channel.Telegram:
                Application.OpenURL("https://t.me/+uQFcFVwGmwM3ZDNi");
                break;
        }
    }

    public enum Channel
    {
        Telegram
    }
}