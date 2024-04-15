using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public bool Dead { get; set; }
    public void TakeDamage(float damage);
    public void Die();
}

public interface IInput
{
    public Action LeftAttack { get; set; }
    public Action RightAttack { get; set; }
    public void InputProcces();
}