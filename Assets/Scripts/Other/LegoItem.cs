using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LegoItem : MonoBehaviour
{
    [SerializeField] private IHealthObject parent;
    [SerializeField] private List<EnemyIntoLego> enemies = new List<EnemyIntoLego>();
    public float attackDelta;

    public List<GameObject> lego;
    public float radius = 5f;

    private void OnEnable()
    {
        for (int i = 0; i < lego.Count; i++)
        {
            float angle = i * Mathf.PI * 2f / lego.Count;
            Vector3 newPosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            lego[i].transform.position = transform.position + newPosition;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 || other.gameObject.layer == 7)
        {
            IHealthObject enemy = other.GetComponent<IHealthObject>();
            if (enemy == parent) return;

            enemy.GetDamageWithoutRebound(15f, out bool isDeath, out int gettedSlap);
            //player?.SetStolenSlaps(gettedSlap);

            foreach (EnemyIntoLego enemyIntoLego in enemies)
            {
                if (enemyIntoLego.Enemy == enemy)
                {
                    return;
                }
            }
            enemies.Add(new EnemyIntoLego(enemy, 0f));
        }
    }


    private void Update()
    {
        foreach (var item in enemies)
        {
            item.LastedTime += Time.deltaTime;
            if(item.LastedTime >= attackDelta)
            {
                item.LastedTime = 0;
                item.Enemy.GetDamageWithoutRebound(15f, out bool isDeath, out int gettedSlap);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            foreach (EnemyIntoLego enemyIntoLego in enemies)
            {
                if (enemyIntoLego.Enemy == enemy)
                {
                    enemies.Remove(enemyIntoLego);
                    return;
                }
            }
        }
    }

    private void OnDisable()
    {
        enemies = new List<EnemyIntoLego>();
    }
}



[Serializable]
public class EnemyIntoLego
{
    public IHealthObject Enemy;
    public float LastedTime;

    public EnemyIntoLego(IHealthObject enemy, float v)
    {
        Enemy = enemy;
        LastedTime = v;
    }
}