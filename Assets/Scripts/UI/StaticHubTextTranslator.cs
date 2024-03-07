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
    [SerializeField] private TextMeshPro leaderboardDescription;
    [SerializeField] private TextMeshProUGUI leaderboardName;
    [SerializeField] private TextMeshProUGUI otherGamesIconText;
    [SerializeField] private TextMeshProUGUI otherGamesButtonIconText;
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

        }
        if (Geekplay.Instance.language == "en")
        {

        }
        if (Geekplay.Instance.language == "tr")
        {

        }
    }
}
