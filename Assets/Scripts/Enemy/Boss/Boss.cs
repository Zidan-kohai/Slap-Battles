public class Boss : Enemy
{
    public override void Death(bool playDeathAnimation = true)
    {
        gameObject.SetActive(false);
        eventManager.InvokeActionsOnBossDeath();
    }
}