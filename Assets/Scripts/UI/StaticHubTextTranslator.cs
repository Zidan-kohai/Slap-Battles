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

    private void Start()
    {
        if(Geekplay.Instance.language == "ru")
        {
            portalStandartText.text = "Стандартный";
            portalOnePunchText.text = "Один удар";
            portalBattleRoyalText.text = "Королевская битва";
            portalTheifText.text = "Вор шлепков";
            portalAceText.text = "Леденой режим";
            portalBossText.text = "Босс";
            portalTimeText.text = "Режим таймер";


            skineShopMainHeaderText.text = "Магазин скинов";
            skineShopHeaderText.text = "Магазин скинов";
            manGenderText.text = "парень";
            womanGenderText.text = "девушка";
            buffText.text = "Здоровья";


            slapShopMainHeaderText.text = "Магазин шлепков";
            slapShopHeaderText.text = "Магазин шлепков";


            inAppShopMainHeaderText.text = "Ресурсы";
            inAppShopHeaderText.text = "Ресурсы";
            item1Cost.text = "2 ян";
            item2Cost.text = "15 ян";
            item3Cost.text = "35 ян";
            item4Cost.text = "50 ян";
            item5Cost.text = "80 ян";
            item6Cost.text = "5 ян";
            item7Cost.text = "20 ян";
            item8Cost.text = "40 ян";
            item9Cost.text = "60 ян";
            item10Cost.text = "90 ян";
            item11Cost.text = "100 ян";
            item11SpecialText.text = "лучшее";


            GameDescription.text = "Заходи в понравившийся портал, шлепай других игроков и набирай очки!\r\n\r\nТрать шлепки на новые порталы, перчатки и скины!";
            Rules.text = "Правила";
            leaderboardName.text = "Лидеры";
            otherGamesIconText.text = "другие игры";
            otherGamesHeaderText.text = "наши игры";
            otherGamesButtonIconText.text = "другие игры";
            bonusText.text = "Бонусы";
            speedKeyPauseText.text = "нажмите Tab";
            speedKeyTelegramText.text = "нажмите Е";
            speedKeyInAppShoptext.text = "нажмите Ш";
            speedKeyIncreasePowerText.text = "нажмите З";
            speedKeyIncreaseSpeedText.text = "нажмите Ч";
            speedKeyIncreaseHPText.text = "нажмите Р";
            speedKeyDoubleSlapText.text = "нажмите А";

        }
        if (Geekplay.Instance.language == "en")
        {
            portalStandartText.text = "Standard";
            portalOnePunchText.text = "One Punch";
            portalBattleRoyalText.text = "Battle Royal";
            portalTheifText.text = "Slap Thief";
            portalAceText.text = "Ice mode";
            portalBossText.text = "Boss";
            portalTimeText.text = "Timer mode";


            skineShopMainHeaderText.text = "Skin Store";
            skineShopHeaderText.text = "Skin Store";
            manGenderText.text = "guy";
            womanGenderText.text = "girl";
            buffText.text = "Health";


            slapShopMainHeaderText.text = "Slap Shop";
            slapShopHeaderText.text = "Slap Shop";


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
        }
        if (Geekplay.Instance.language == "tr")
        {
            portalStandartText.text = "Standart";
            portalOnePunchText.text = "Tek Yumruk";
            portalBattleRoyalText.text = "Battle Royal";
            portalTheifText.text = "Tokat Hirsizi";
            portalAceText.text = "Buz modu";
            portalBossText.text = "Patron";
            portalTimeText.text = "Zamanlayici modu";


            skineShopMainHeaderText.text = "Dis Gorunum Magazasi";
            skineShopHeaderText.text = "Dis Gorunum Magazasi";
            manGenderText.text = "erkek";
            womanGenderText.text = "kiz";
            buffText.text = "Saglik";


            slapShopMainHeaderText.text = "Tokat Magazasi";
            slapShopHeaderText.text = "Tokat Magazasi";


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
        }
    }
}
