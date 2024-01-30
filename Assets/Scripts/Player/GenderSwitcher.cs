using UnityEngine;

public class GenderSwitcher : MonoBehaviour
{
    [SerializeField] private Avatar manAnimationAvatar;
    [SerializeField] private Avatar womanAnimationAvatar;
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject manGameObject;
    [SerializeField] private GameObject womanGameObject;


    private void Start()
    {
        switchGender(Geekplay.Instance.PlayerData.isGenderMan);
    }

    public void switchGender(bool isMan)
    {
        if (isMan)
        {
            animator.avatar = manAnimationAvatar;
            womanGameObject.SetActive(false);
            manGameObject.SetActive(true);
        }
        else
        {
            animator.avatar = womanAnimationAvatar;
            womanGameObject.SetActive(true);
            manGameObject.SetActive(false);
        }

        Geekplay.Instance.PlayerData.isGenderMan = isMan;
        Geekplay.Instance.Save();
    }
}
