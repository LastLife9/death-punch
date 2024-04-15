using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string Key;
    public float MoveSpeed;
}

public class Enemy : UnitBase
{
    private Transform _transform;
    private Collider2D _collider;
    private Rigidbody2D _rb;

    private float moveSpeed = 0f;
    private bool facedRight = true;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public bool FacedRight { get => facedRight; set => facedRight = value; }
    public Transform Target { get; private set; }

    private void Awake()
    {
        _transform = transform;
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void Init()
    {
        if (inited) return;

        _collider.enabled = true;
        _rb.velocity = Vector2.zero;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        inited = true;
        Dead = false;

        Health = 1f;
        Damage = 10f;
        AttackRadius = 0.5f;
        UnitRadius = 0.4f;

        OnInited?.Invoke();
    }

    private void Update()
    {
        if (!inited) return;
        if (Target == null)
        {
            SetTarget();
            return;
        }

        Move();
    }

    private void Move()
    {
        if (Target == null) return;
        if (Target.TryGetComponent(out IDamagable damagable))
            if (damagable.Dead) return;

        Vector3 targetPosition = Target.position;

        if (Vector3.Distance(_transform.position, targetPosition) <=
            AttackRadius + UnitRadius)
        {
            Attack();
            return;
        }

        _transform.position = Vector3.MoveTowards(_transform.position, 
            targetPosition, MoveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        if(Target.TryGetComponent(out IDamagable damagable))
            if(!damagable.Dead) damagable.TakeDamage(Damage);
    }

    private void SetTarget()
    {
        var player = FindObjectOfType<Player>();

        if (player == null)
            return;

        Target = player.transform;
    }

    public override void Die()
    {
        inited = false;
        Dead = true;
        OnDie?.Invoke();

        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        #region DieAnimation
        _collider.enabled = false;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.centerOfMass = new Vector2(0f, 0.75f);
        _rb.AddForce(Vector2.up * Random.Range(1f, 3f) * MoveSpeed, ForceMode2D.Impulse);
        _rb.AddForce((FacedRight ? Vector2.left : Vector2.right) * Random.Range(2f, 4f) * MoveSpeed / 2, ForceMode2D.Impulse);
        _rb.AddTorque((FacedRight ? 1 : -1) * Random.Range(2f, 4f) * MoveSpeed / 2, ForceMode2D.Impulse);
        #endregion

        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
    }
}
