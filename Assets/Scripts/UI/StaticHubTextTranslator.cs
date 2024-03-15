using TMPro;
using UnityEngine;

public class StaticHubTextTranslator : MonoBehaviour
{

    [Header("Portal")]
    [SerializeField] private TextMeshPro portalStandartText;
    [SerializeField] private TextMeshPro portalOnePunchText;
    [SerializeField] private TextMeshPro portalBattleRoyalText;
    [SerializeField] private TextMeshPro portalTheifText;
    [SerializeField] private TextMeshPro portalAceText;
    [SerializeField] private TextMeshPro portalBossText;
    [SerializeField] private TextMeshPro portalTimeText;

    [Header("Skine Shop")]
    [SerializeField] private TextMeshPro skineShopMainHeaderText;
    [SerializeField] private TextMeshProUGUI skineShopHeaderText;
    [SerializeField] private TextMeshProUGUI manGenderText;
    [SerializeField] private TextMeshProUGUI womanGenderText;
    [SerializeField] private TextMeshProUGUI buffText;

    [Header("Slap Shop")]
    [SerializeField] private TextMeshPro slapShopMainHeaderText;
    [SerializeField] private TextMeshProUGUI slapShopHeaderText;

    [Header("In App Shop")]
    [SerializeField] private TextMeshPro inAppShopMainHeaderText;
    [SerializeField] private TextMeshProUGUI inAppShopHeaderText;
    [SerializeField] private TextMeshProUGUI item1Cost;
    [SerializeField] private TextMeshProUGUI item2Cost;
    [SerializeField] private TextMeshProUGUI item3Cost;
    [SerializeField] private TextMeshProUGUI item4Cost;
    [SerializeField] private TextMeshProUGUI item5Cost;
    [SerializeField] private TextMeshProUGUI item6Cost;
    [SerializeField] private TextMeshProUGUI item7Cost;
    [SerializeField] private TextMeshProUGUI item8Cost;
    [SerializeField] private TextMeshProUGUI item9Cost;
    [SerializeField] private TextMeshProUGUI item10Cost;
    [SerializeField] private TextMeshProUGUI item11Cost;
    [SerializeField] private TextMeshProUGUI item11SpecialText;

    [Header("Tutor")]
    [SerializeField] private TextMeshProUGUI tutorHeaderText;
    [SerializeField] private TextMeshProUGUI tutorBodyText;
    [SerializeField] private TextMeshProUGUI tutorCloseText;

    [Header("Other")]
    [SerializeField] private TextMeshProUGUI GameDescription;
    [SerializeField] private TextMeshProUGUI Rules;
    [SerializeField] private TextMeshProUGUI leaderboardName;
    [SerializeField] private TextMeshProUGUI otherGamesIconText;
    [SerializeField] private TextMeshProUGUI otherGamesHeaderText;
    [SerializeField] private TextMeshProUGUI otherGamesButtonIconText;
    [SerializeField] private TextMeshProUGUI bonusText;
    [SerializeField] private TextMeshProUGUI speedKeyPauseText;
    [SerializeField] private TextMeshProUGUI speedKeyTelegramText;
    [SerializeField] private TextMeshProUGUI speedKeyInAppShoptext;
    [SerializeField] private TextMeshProUGUI speedKeyIncreasePowerText;
    [SerializeField] private TextMeshProUGUI speedKeyIncreaseSpeedText;
    [SerializeField] private TextMeshProUGUI speedKeyIncreaseHPText;
    [SerializeField] private TextMeshProUGUI speedKeyDoubleSlapText;
    [SerializeField] private TextMeshProUGUI promocodeGetText;
    [SerializeField] private TextMeshProUGUI telegramText;

    private void Start()
    {
        if(Geekplay.Instance.language == "ru")
        {
            portalStandartText.text = "�����������";
            portalOnePunchText.text = "���� ����";
            portalBattleRoyalText.text = "����������� �����";
            portalTheifText.text = "��� �������";
            portalAceText.text = "������� �����";
            portalBossText.text = "����";
            portalTimeText.text = "����� ������";


            skineShopMainHeaderText.text = "������� ������";
            skineShopHeaderText.text = "������� ������";
            manGenderText.text = "������";
            womanGenderText.text = "�������";
            buffText.text = "��������";


            slapShopMainHeaderText.text = "������� ��������";
            slapShopHeaderText.text = "������� ��������";


            inAppShopMainHeaderText.text = "�������";
            inAppShopHeaderText.text = "�������";
            item1Cost.text = "2 ��";
            item2Cost.text = "15 ��";
            item3Cost.text = "35 ��";
            item4Cost.text = "50 ��";
            item5Cost.text = "80 ��";
            item6Cost.text = "5 ��";
            item7Cost.text = "20 ��";
            item8Cost.text = "40 ��";
            item9Cost.text = "60 ��";
            item10Cost.text = "90 ��";
            item11Cost.text = "100 ��";
            item11SpecialText.text = "������";


            GameDescription.text = "������ � ������������� ������, ������ ������ ������� � ������� ����!\r\n\r\n����� ������ �� ����� �������, �������� � �����!";
            Rules.text = "�������";
            leaderboardName.text = "������";
            otherGamesIconText.text = "������ ����";
            otherGamesHeaderText.text = "���� ����";
            otherGamesButtonIconText.text = "������ ����";
            bonusText.text = "������";
            speedKeyPauseText.text = "������� Tab";
            speedKeyTelegramText.text = "������� �";
            speedKeyInAppShoptext.text = "������� �";
            speedKeyIncreasePowerText.text = "������� �";
            speedKeyIncreaseSpeedText.text = "������� �";
            speedKeyIncreaseHPText.text = "������� �";
            speedKeyDoubleSlapText.text = "������� �";
            promocodeGetText.text = "�������";

            tutorHeaderText.text = "������!";
            tutorBodyText.text = "�� ���������� � ����. ��������� �������, ����� ������ � ������ � ��������, ����� �������������. �����!";
            tutorCloseText.text = "�������";
            telegramText.text = "��������";

        }
        if (Geekplay.Instance.language == "en")
        {
            portalStandartText.text = "Standard";
            portalOnePunchText.text = "One Punch";
            portalBattleRoyalText.text = "Battle Royal";
            portalTheifText.text = "Slap Thief";
            portalAceText.text = "Snow mode";
            portalBossText.text = "Boss";
            portalTimeText.text = "Timer mode";


            skineShopMainHeaderText.text = "Skin Store";
            skineShopHeaderText.text = "Skin Store";
            manGenderText.text = "guy";
            womanGenderText.text = "girl";
            buffText.text = "Health";


            slapShopMainHeaderText.text = "Glove shop";
            slapShopHeaderText.text = "Glove shop";


            inAppShopMainHeaderText.text = "Resources";
            inAppShopHeaderText.text = "Resources";
            item1Cost.text = "2 yan";
            item2Cost.text = "15 yan";
            item3Cost.text = "35 yan";
            item4Cost.text = "50 yan";
            item5Cost.text = "80 yan";
            item6Cost.text = "5 yan";
            item7Cost.text = "20 yan";
            item8Cost.text = "40 yan";
            item9Cost.text = "60 yan";
            item10Cost.text = "90 yan";
            item11Cost.text = "100 yan";
            item11SpecialText.text = "Top";


            GameDescription.text = "Enter the portal you like, spank other players and score points!\r\n\r\nSpend your spanks on new portals, gloves and skins!";
            Rules.text = "Rules";
            leaderboardName.text = "Leaders";
            otherGamesIconText.text = "other games";
            otherGamesHeaderText.text = "our games";
            otherGamesButtonIconText.text = "other games";
            bonusText.text = "Bonuses";
            speedKeyPauseText.text = "press Tab";
            speedKeyTelegramText.text = "press T";
            speedKeyInAppShoptext.text = "press I";
            speedKeyIncreasePowerText.text = "press P";
            speedKeyIncreaseSpeedText.text = "press X";
            speedKeyIncreaseHPText.text = "press H";
            speedKeyDoubleSlapText.text = "press F";
            promocodeGetText.text = "Get";

            tutorHeaderText.text = "Hello!";
            tutorBodyText.text = "You are in the hub. Use portals to play and go to stores to upgrade. Good luck!";
            tutorCloseText.text = "Close";
            telegramText.text = "Telegram";
        }
        if (Geekplay.Instance.language == "tr")
        {
            portalStandartText.text = "Standart";
            portalOnePunchText.text = "Tek Yumruk";
            portalBattleRoyalText.text = "Battle Royal";
            portalTheifText.text = "Tokat Hirsizi";
            portalAceText.text = "Kar modu";
            portalBossText.text = "Patron";
            portalTimeText.text = "Zamanlayici";


            skineShopMainHeaderText.text = "Dis Gorunum Magazasi";
            skineShopHeaderText.text = "Dis Gorunum Magazasi";
            manGenderText.text = "erkek";
            womanGenderText.text = "kiz";
            buffText.text = "Saglik";


            slapShopMainHeaderText.text = "Eldiven magazasi";
            slapShopHeaderText.text = "Eldiven magazasi";


            inAppShopMainHeaderText.text = "Kaynaklar";
            inAppShopHeaderText.text = "Kaynaklar";
            item1Cost.text = "2 yan";
            item2Cost.text = "15 yan";
            item3Cost.text = "35 yan";
            item4Cost.text = "50 yan";
            item5Cost.text = "80 yan";
            item6Cost.text = "5 yan";
            item7Cost.text = "20 yan";
            item8Cost.text = "40 yan";
            item9Cost.text = "60 yan";
            item10Cost.text = "90 yan";
            item11Cost.text = "100 yan";
            item11SpecialText.text = "sey";


            GameDescription.text = "Begendiginiz portala girin, diger oyunculara saplak atin ve puan kazanin!\r\n\r\nTokatlarinizi yeni portallara, eldivenlere ve gorunumlere harcayin!";
            Rules.text = "Kurallar";
            leaderboardName.text = "Liderler";
            otherGamesIconText.text = "diger oyunlar";
            otherGamesHeaderText.text = "oyunlarimiz";
            otherGamesButtonIconText.text = "diger oyunlar";
            bonusText.text = "Bonuslar";
            speedKeyPauseText.text = "Tab tusuna basin";
            speedKeyTelegramText.text = "T tusuna basin";
            speedKeyInAppShoptext.text = "I tusuna basin";
            speedKeyIncreasePowerText.text = "P tusuna basin";
            speedKeyIncreaseSpeedText.text = "X tusuna basin";
            speedKeyIncreaseHPText.text = "H tusuna basin";
            speedKeyDoubleSlapText.text = "F tusuna basin";
            promocodeGetText.text = "almak";

            tutorHeaderText.text = "Merhaba!";
            tutorBodyText.text = "Merkezdesiniz. Oynamak icin portallari kullanin ve yukseltme yapmak icin magazalara gidin. Iyi sanslar!";
            tutorCloseText.text = "Kapat";
            telegramText.text = "Telegram";
        }
    }
}
