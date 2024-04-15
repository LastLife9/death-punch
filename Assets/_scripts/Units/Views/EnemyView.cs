using System.Collections;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Transform leftEye;
    [SerializeField] private Transform rightEye;
    [SerializeField] private GameObject aliveSkin;
    [SerializeField] private GameObject deadSkin;

    private Enemy enemyController;

    private void Start()
    {
        enemyController = GetComponent<Enemy>();
    }

    private void Update()
    {
        UpdateSkin();
        UpdateEyes();
    }

    private void UpdateSkin()
    {
        aliveSkin.SetActive(!enemyController.Dead);
        deadSkin.SetActive(enemyController.Dead);
    }

    private void UpdateEyes()
    {
        if (enemyController.Target != null)
        {
            Vector3 targetPosition = enemyController.Target.position;
            Vector3 currentPosition = transform.position;

            if (targetPosition.x > currentPosition.x)
                enemyController.FacedRight = true;
            else
                enemyController.FacedRight = false;
        }

        leftEye.gameObject.SetActive(!enemyController.FacedRight);
        rightEye.gameObject.SetActive(enemyController.FacedRight);
    }
}