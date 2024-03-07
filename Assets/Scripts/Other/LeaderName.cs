using TMPro;
using UnityEngine;

public class LeaderName : MonoBehaviour
{
    public TextMeshPro place1Name;
    public TextMeshPro place2Name;
    public TextMeshPro place3Name;

    private void Start()
    {
        UpdateLeaderboardName();
    }
    public void UpdateLeaderboardName()
    {
        place1Name.text = Geekplay.Instance.lN[0];
        place2Name.text = Geekplay.Instance.lN[1];
        place3Name.text = Geekplay.Instance.lN[2];
    }
}
