using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AutoSelectFieldTextOnEnable : UIBehaviour
{
    public GameObject inputPopup;
    public TMP_InputField _field;
    public Promocode promocode;
    private void Awake()
    {
        base.Awake();
        _field.onFocusSelectAll = true;
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
    }
}