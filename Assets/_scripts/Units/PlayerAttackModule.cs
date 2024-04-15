using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackModule
{
    private float attackRadius;
    private float attackDamage;
    private float playerRadius;
    private Transform attackPoint;

    public Transform LeftTarget { get; set; }
    public Transform RightTarget { get; set; }

    public PlayerAttackModule(Transform atkPoint, float atkRadius, 
        float atkDamage, float playerRadius)
    {
        attackPoint = atkPoint;
        attackDamage = atkDamage;
        attackRadius = atkRadius;
        this.playerRadius = playerRadius;
    }

    public void LookForTargets()
    {
        RaycastHit2D leftHit = RaycastHit(Vector2.left);
        RaycastHit2D rightHit = RaycastHit(Vector2.right);

        LeftTarget = null;
        RightTarget = null;

        Debug.DrawRay(attackPoint.position, Vector2.left * (attackRadius + playerRadius),
            Color.red);
        Debug.DrawRay(attackPoint.position, Vector2.right * (attackRadius + playerRadius),
            Color.red);

        if (leftHit.collider != null)
            if (leftHit.collider.TryGetComponent(out IDamagable damagable))
                if (!damagable.Dead)
                {
                    LeftTarget = leftHit.transform;
                    Debug.DrawRay(attackPoint.position, Vector2.left * (attackRadius + playerRadius),
                        Color.green);
                }

        if (rightHit.collider != null)
            if (rightHit.collider.TryGetComponent(out IDamagable damagable))
                if (!damagable.Dead)
                {
                    RightTarget = rightHit.transform;
                    Debug.DrawRay(attackPoint.position, Vector2.right * (attackRadius + playerRadius),
                            Color.green);
                }
    }

    public void Attack(Vector2 direction)
    {
        RaycastHit2D hit = RaycastHit(direction);

        if (hit.collider != null)
            if (hit.collider.TryGetComponent(out IDamagable damagable))
                if (!damagable.Dead) damagable.TakeDamage(attackDamage);
    }

    private RaycastHit2D RaycastHit(Vector2 direction)
    {
        Vector2 raycastOrigin = attackPoint.position;
        float length = attackRadius + playerRadius;

        return Physics2D.Raycast(raycastOrigin, direction, length, StaticValues.EnemyLayer);
    }
}
