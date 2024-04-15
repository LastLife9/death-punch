using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IDamagable
{
    public Action OnInited { get; set; }
    public Action<float> OnTakeDamage { get; set; }
    public Action OnDie { get; set; }
    public float Health { get => health; set => health = value; }
    public float Damage { get => damage; set => damage = value; }
    public float AttackRadius { get => attackRadius; set => attackRadius = value; }
    public float UnitRadius { get => unitRadius; set => unitRadius = value; }
    public bool Dead { get => dead; set => dead = value; }

    protected float health = 0f;
    protected float damage = 0f;
    protected float attackRadius = 0f;
    protected float unitRadius = 0f;

    protected bool inited = false;
    protected bool dead = false;

    public abstract void Init();

    public virtual void Attack(IDamagable target)
    {
        target.TakeDamage(Damage);
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        OnTakeDamage?.Invoke(damage);

        if (Health <= 0)
        {
            if (Dead) return;
            Die();
        }
    }

    public abstract void Die();
}
