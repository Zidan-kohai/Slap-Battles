public class Boss : Enemy
{
    public override void Death()
    {
        gameObject.SetActive(false);
        eventManager.InvokeActionsOnBossDeath();
    }
}