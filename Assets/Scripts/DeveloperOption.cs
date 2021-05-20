using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 개발시에만 필요한 기능을 모아둘 클래스
// 이 클래스를 삭제해도 다른 컴포넌트에 지장이 없게 만들어야 함.

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
