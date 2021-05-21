using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// 개발시에만 필요한 기능을 모아둘 클래스
// 이 클래스를 삭제해도 다른 컴포넌트에 지장이 없게 만들어야 함.

public class DeveloperOption : MonoBehaviour
{
    #region UI 필드
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
    private MovementProvider movementProvider; // 이동 설정 변경을 위해 선언
    [SerializeField]
    private CustomController customController; // X,A 버튼 입력값을 받아오기 위해 선언

    private void Start()
    {
        // 버튼에 이동 설정 변경 이벤트 추가
        setFreeMove.onClick.AddListener(() => movementProvider.moveType = MovementProvider.MoveType.FreeMove);
        setGoFront.onClick.AddListener(() => movementProvider.moveType = MovementProvider.MoveType.GoFront);
    }
    private void Update()
    {
        SetUI();
        //debugText.text = customController.IsPrimaryButtonPressed().ToString();
    }

    private void SetUI() // 콘트롤러의 x,a 버튼이 눌렸으면 UI를 껏다 킨다. 
    {
        if (customController.IsPrimaryButtonPressed())
        {
            childObject.gameObject.SetActive(!childObject.activeSelf);
        }
    }
}
