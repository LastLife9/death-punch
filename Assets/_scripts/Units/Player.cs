using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{
    [SerializeField] private Transform attackPoint;

    private bool facedRight = true;
    public bool FacedRight { get => facedRight; }

    private IInput input;
    public PlayerAttackModule AttackModule { get; private set; }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        if (inited) return;
        inited = true;
        Dead = false;

        Health = 1f;
        Damage = 10f;
        AttackRadius = 3f;
        UnitRadius = 0.4f;

        AttackModule = new PlayerAttackModule(attackPoint,
            AttackRadius, Damage, UnitRadius);

        input = new PCInput();
        input.LeftAttack += LeftAttack;
        input.RightAttack += RightAttack;

        OnInited?.Invoke();
    }

    private void Update()
    {
        if (!inited) return;

        input.InputProcces();
        AttackModule.LookForTargets();
    }

    public override void Die()
    {
        Dead = true;
        OnDie?.Invoke();

        gameObject.SetActive(false);
    }

    private void LeftAttack()
    {
        facedRight = false;

        AttackModule.Attack(Vector2.left);
    }

    private void RightAttack()
    {
        facedRight = true;

        AttackModule.Attack(Vector2.right);
    }
}
