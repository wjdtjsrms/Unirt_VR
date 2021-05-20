using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���߽ÿ��� �ʿ��� ����� ��Ƶ� Ŭ����
// �� Ŭ������ �����ص� �ٸ� ������Ʈ�� ������ ���� ������ ��.

public class DeveloperOption : MonoBehaviour
{
    [SerializeField]
    private Button setGoFront;
    [SerializeField]
    private Button setFreeMove;

    [SerializeField]
    private MovementProvider movementProvider;
    
    private void Start()
    {
        setFreeMove.onClick.AddListener(() => movementProvider.moveType = MovementProvider.MoveType.FreeMove);
        setGoFront.onClick.AddListener(() => movementProvider.moveType = MovementProvider.MoveType.GoFront);
    }

}
