using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float maxTime;
    private float currenTime;
    public Image image;
    public void Run(float maxTime)
    {
        this.maxTime = maxTime;
        image.fillAmount = 0;
        currenTime = 0;
    }

    private void Update()
    {
        currenTime += Time.deltaTime;
        image.fillAmount = currenTime / maxTime;
    }
}
