using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform leftAttackRoot;
    [SerializeField] private Transform rightAttackRoot;
    [SerializeField] private Transform leftEye;
    [SerializeField] private Transform rightEye;

    private Player playerController;

    private void Start()
    {
        playerController = GetComponent<Player>();
    }

    private void Update()
    {
        UpdateAttackRadius();
        UpdateEyes();
    }

    private void UpdateAttackRadius()
    {
        Vector3 leftRootScale = leftAttackRoot.localScale;
        Vector3 rightRootScale = rightAttackRoot.localScale;
        leftRootScale.x = playerController.AttackRadius;
        rightRootScale.x = playerController.AttackRadius;
        leftAttackRoot.localScale = leftRootScale;
        rightAttackRoot.localScale = leftRootScale;
    }

    private void UpdateEyes()
    {
        leftEye.gameObject.SetActive(!playerController.FacedRight);
        rightEye.gameObject.SetActive(playerController.FacedRight);
    }
}
