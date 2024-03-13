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
            if(!other.TryGetComponent(out IHealthObject enemy)) return;

            if (enemy == parent) return;

            enemy.GetDamageWithoutRebound(15f, out bool isDeath, out int gettedSlap);
            //player?.SetStolenSlaps(gettedSlap);

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Enemy == enemy)
                {
                    return;
                }
            }

            if (isDeath) return;

            enemies.Add(new EnemyIntoLego(enemy, 0f));
        }
    }


    private void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].LastedTime += Time.deltaTime;
            if(enemies[i].LastedTime >= attackDelta)
            {
                enemies[i].LastedTime = 0;
                enemies[i].Enemy.GetDamageWithoutRebound(15f, out bool isDeath, out int gettedSlap);

                if(isDeath)
                {
                    enemies.Remove(enemies[i]);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            for(int i =0; i < enemies.Count; i++)
            {
                if (enemies[i].Enemy == enemy)
                {
                    enemies.Remove(enemies[i]);
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