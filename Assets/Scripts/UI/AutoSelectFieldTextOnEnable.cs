using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AutoSelectFieldTextOnEnable : UIBehaviour
{
    private TMP_InputField _field;
    public Promocode promocode;
    private void Awake()
    {
        base.Awake();
        _field = GetComponent<TMP_InputField>();
        _field.onFocusSelectAll = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _field.Select();
    }

    //public void OnDeselect()
    //{
    //    Debug.Log("Deselect");
    //    promocode.ClaimBtn();
    //    Geekplay.Instance.StopOrResumeWithoutPausePanel();
    //    gameObject.SetActive(false);
    //}

    public void OnEndEdit()
    {
        Debug.Log("End Edit");
        promocode.ClaimBtn();
        Geekplay.Instance.StopOrResumeWithoutPausePanel();
        gameObject.SetActive(false);
    }
}