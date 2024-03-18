using TMPro;
using UnityEngine;

public class StaticEnviromentTextTranslator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveText;
    [SerializeField] private TextMeshProUGUI WIconText;
    [SerializeField] private TextMeshProUGUI SIconText;
    [SerializeField] private TextMeshProUGUI AIconText;
    [SerializeField] private TextMeshProUGUI DIconText;
    [SerializeField] private TextMeshProUGUI jumpText1;
    [SerializeField] private TextMeshProUGUI jumpIconText1;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI superPowerText;
    [SerializeField] private TextMeshProUGUI EIconText;
    [SerializeField] private TextMeshProUGUI rotateText;
    [SerializeField] private TextMeshProUGUI getDiamondText;
    [SerializeField] private TextMeshPro hub;
    void Start()
    {
        if (Geekplay.Instance.language == "ru")
        {
            moveText.text = "Передвижение";
            WIconText.text = "Ц";
            SIconText.text = "Ы";
            AIconText.text = "Ф";
            DIconText.text = "В";
            jumpText1.text = "прыжок";
            jumpIconText1.text = "пробел";
            attackText.text = "Шлепок";
            superPowerText.text = "суперсила";
            EIconText.text = "У";
            rotateText.text = "Обзор камерой";
            hub.text = "Хаб";
            getDiamondText.text = "шлепни 35 раз и получи 1";
        }
        else if(Geekplay.Instance.language == "en")
        {
            moveText.text = "Move";
            WIconText.text = "W";
            SIconText.text = "S";
            AIconText.text = "A";
            DIconText.text = "D";
            jumpText1.text = "jump";
            jumpIconText1.text = "space";
            attackText.text = "Slap";
            superPowerText.text = "superpower";
            EIconText.text = "E";
            rotateText.text = "Camera view";
            hub.text = "Hub";
            getDiamondText.text = "Slaps 35 times and get 1";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            moveText.text = "Tasi";
            WIconText.text = "W";
            SIconText.text = "S";
            AIconText.text = "A";
            DIconText.text = "D";
            jumpText1.text = "atlama";
            jumpIconText1.text = "bosluk";
            attackText.text = "Tokat";
            superPowerText.text = "super guc";
            EIconText.text = "E";
            rotateText.text = "Kamera gorunumu";
            hub.text = "Hub";
            getDiamondText.text = "35 kez tokat at ve 1 puan al";
        }
    }
}
