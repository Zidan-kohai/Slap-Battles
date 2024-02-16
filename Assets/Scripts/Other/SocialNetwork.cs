using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialNetwork : MonoBehaviour
{
    public SocialChannel channel;

    private void OnTriggerEnter(Collider other)
    {
        switch(channel)
        {
            case SocialChannel.Telegram:
                Application.OpenURL("https://t.me/+uQFcFVwGmwM3ZDNi");
                break;
        }
    }

    public enum SocialChannel
    {
        Telegram
    }
}