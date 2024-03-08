using UnityEngine;

public class Slap : MonoBehaviour
{
    [SerializeField] private SlapPowerType type;
    [SerializeField] private float attackPower;
    public float rollBackTime;
    public string descriptionOfSuperPower;
    public string ru;
    public string en;
    public string tr;
    public SlapPowerType GetSlapPowerType() { return type; }
    public float AttackPower { get { return attackPower; } set { attackPower = value; } }

    private void Start()
    {
        
    }

    public void Attack()
    {

    }

    public virtual void SuperAttack()
    {

    }

    public void ChangeLanguageOfDescription()
    {
        if (Geekplay.Instance.language == "ru")
        {
            descriptionOfSuperPower = ru;
        }
        if (Geekplay.Instance.language == "en")
        {
            descriptionOfSuperPower = en;
        }
        if (Geekplay.Instance.language == "tr")
        {
            descriptionOfSuperPower = tr;
        }
    }
}
