using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoSelectFieldTextOnEnable : UIBehaviour
{
    public GameObject inputPopup;
    public InputField _field;
    public Promocode promocode;
    private void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _field.Select();
    }

    public void OnEndEdit()
    {
        Debug.Log("End Edit");
        promocode.ClaimBtn();
        Geekplay.Instance.StopOrResumeWithoutPausePanel();
        inputPopup.SetActive(false);
        Geekplay.Instance.ShowInterstitialAd();

    }
}