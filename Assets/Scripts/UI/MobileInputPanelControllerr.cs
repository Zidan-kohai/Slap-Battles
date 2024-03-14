using UnityEngine;
using UnityEngine.UI;

public class MobileInputPanelControllerr : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private SwipeDetector swipePanel;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button superPowerButton;
    [SerializeField] private Button jumpButton;
    [SerializeField] private SlapPower slapPower; 
    void Start()
    {
        if(Geekplay.Instance.currentMode == Modes.Hub)
        {
            joystick.gameObject.SetActive(true);
            jumpButton.gameObject.SetActive(true);
            swipePanel.gameObject.SetActive(true);
            attackButton.gameObject.SetActive(false);
            superPowerButton.gameObject.SetActive(false);
        }
        else
        {
            bool hasSuperPower = slapPower.HasSuperPower; 

            joystick.gameObject.SetActive(true);
            jumpButton.gameObject.SetActive(true);
            swipePanel.gameObject.SetActive(true);
            attackButton.gameObject.SetActive(true);
            superPowerButton.gameObject.SetActive(true);

            if (!hasSuperPower)
            {
                superPowerButton.gameObject.SetActive(false);
            }
        }
    }
}
