using UnityEngine;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class Codes
{
    public string code;
    public int codeNumber;
    public UnityEvent rewardEvent;
}

public class Promocode : MonoBehaviour
{
	[SerializeField] private GameObject inputPopup; //куда вводим промокод
	[SerializeField] private TMP_InputField inputText; //куда вводим промокод
	[SerializeField] private Codes[] codes; //список кодов и наград (как реварды и иннапы)
    [SerializeField] private HubEventManager eventManager;    
    //функция для кнопки "взять"
    public void ClaimBtn()
    {
    	for (int i = 0; i < codes.Length; i++)
        {
            if (inputText.text == codes[i].code && !Geekplay.Instance.PlayerData.codes.Contains(codes[i].codeNumber))
            {
                codes[i].rewardEvent.Invoke();
                Geekplay.Instance.PlayerData.codes.Add(codes[i].codeNumber);
                Geekplay.Instance.Save();
            }
        }
    }

    //функции, которые будут привязаны к событиям
    public void SLAP2024()
    {
    	Geekplay.Instance.PlayerData.money += 1000;
    	Geekplay.Instance.PlayerData.DiamondMoney += 10;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }

    public void MORE_SLAPS()
    {
        Geekplay.Instance.PlayerData.money += 2000;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
    public void GOLDBATTLE()
    {
        Geekplay.Instance.PlayerData.DiamondMoney += 10;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            inputPopup.gameObject.SetActive(true);
                
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Geekplay.Instance.canPause = false;

            Geekplay.Instance.StopOrResumeWithoutPausePanel();
        }
    }
}
