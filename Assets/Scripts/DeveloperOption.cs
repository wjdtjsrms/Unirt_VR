using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// ���߽ÿ��� �ʿ��� ����� ��Ƶ� Ŭ����
// �� Ŭ������ �����ص� �ٸ� ������Ʈ�� ������ ���� ������ ��.

public class DeveloperOption : MonoBehaviour
{
    #region UI �ʵ�
    [SerializeField]
    private Button setGoFront;
    [SerializeField]
    private Button setFreeMove;
    [SerializeField]
    private Text debugText;
    [SerializeField]
    private GameObject childObject;
    #endregion

    [SerializeField]
    private MovementProvider movementProvider; // �̵� ���� ������ ���� ����
    [SerializeField]
    private CustomController customController; // X,A ��ư �Է°��� �޾ƿ��� ���� ����

    private void Start()
    {
        // ��ư�� �̵� ���� ���� �̺�Ʈ �߰�
        setFreeMove.onClick.AddListener(() => movementProvider.moveType = MovementProvider.MoveType.FreeMove);
        setGoFront.onClick.AddListener(() => movementProvider.moveType = MovementProvider.MoveType.GoFront);
    }
    private void Update()
    {
        SetUI();
        //debugText.text = customController.IsPrimaryButtonPressed().ToString();
    }

    private void SetUI() // ��Ʈ�ѷ��� x,a ��ư�� �������� UI�� ���� Ų��. 
    {
        if (customController.IsPrimaryButtonPressed())
        {
            childObject.gameObject.SetActive(!childObject.activeSelf);
        }
    }
}
