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
    [SerializeField] private TextMeshProUGUI jumpText2;
    [SerializeField] private TextMeshProUGUI jumpIconText2;
    [SerializeField] private TextMeshProUGUI superPowerText;
    [SerializeField] private TextMeshProUGUI EIconText;
    [SerializeField] private TextMeshProUGUI rotateText;
    void Start()
    {
        if (Geekplay.Instance.language == "ru")
        {
            moveText.text = "������������";
            WIconText.text = "�";
            SIconText.text = "�";
            AIconText.text = "�";
            DIconText.text = "�";
            jumpText1.text = "������";
            jumpIconText1.text = "������";
            jumpText2.text = "������";
            jumpIconText2.text = "������";
            superPowerText.text = "���������";
            EIconText.text = "�";
            rotateText.text = "����� �������";
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
            jumpText2.text = "jump";
            jumpIconText2.text = "space";
            superPowerText.text = "superpower";
            EIconText.text = "E";
            rotateText.text = "Camera view";
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
            jumpText2.text = "atlama";
            jumpIconText2.text = "bosluk";
            superPowerText.text = "super guc";
            EIconText.text = "E";
            rotateText.text = "Kamera gorunumu";
        }
    }
}
