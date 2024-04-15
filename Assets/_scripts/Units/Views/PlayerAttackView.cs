using System.Collections;
using UnityEngine;

public class PlayerAttackView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer leftAtkRadiusBG;
    [SerializeField] private SpriteRenderer rightAtkRadiusBG;
    [SerializeField] private Color seeTargetColor;
    [SerializeField] private Color noTargetColor;

    private Player player;
    private PlayerAttackModule attackModule;

    private void Awake()
    {
        player = GetComponent<Player>();
        player.OnInited += Init;
    }

    public void Init()
    {
        attackModule = player.AttackModule;
    }

    private void Update()
    {
        UpdateAttackRadiusBg();
    }

    private void UpdateAttackRadiusBg()
    {
        leftAtkRadiusBG.color = attackModule.LeftTarget == null ?
            noTargetColor : seeTargetColor;

        rightAtkRadiusBG.color = attackModule.RightTarget == null ?
            noTargetColor : seeTargetColor;
    }
}