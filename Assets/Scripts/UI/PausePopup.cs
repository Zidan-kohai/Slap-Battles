using UnityEngine;
using UnityEngine.UI;

public class PausePopup : MonoBehaviour
{
    [SerializeField] private Toggle volumeToggle;
    [SerializeField] private GameObject pausePopup;

    private void Start()
    {
        Geekplay.Instance.pausePopup = pausePopup;  

        volumeToggle.isOn = Geekplay.Instance.PlayerData.IsVolumeOn;
    }

    public void ClosePausePanel()
    {
        AudioListener.volume = volumeToggle.isOn ? 1: 0;
        Time.timeScale = 1;

        pausePopup.SetActive(false);

        Geekplay.Instance.PlayerData.IsVolumeOn = volumeToggle.isOn;
        Geekplay.Instance.Save();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeToggle.isOn ? 1 : 0;

        Geekplay.Instance.PlayerData.IsVolumeOn = volumeToggle.isOn;
        Geekplay.Instance.Save();
    }
}
