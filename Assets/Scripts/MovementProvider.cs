using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementProvider : MonoBehaviour
{
    public enum MoveType
    {
        FreeMove = 0,
        GoFront
    }

    public float speed = 1.0f; // �̵� �ӵ�
    public float gravityMultiplier = 1.0f; // �߷¿� ������ �޴� ��츦 ó��
    public List<XRController> controllers = null; // ��Ʈ�ѷ� ����Ʈ (��Ȳ�� ���� 1�� Ȥ�� n���� ���� �� �� �ִ�.)
    private CharacterController characterController = null; // VR Rig�� ĳ���� ��Ʈ�ѷ�
    private GameObject head = null; // ī�޶��� ��� ��ġ
    public MoveType moveType = MoveType.FreeMove; //  �÷��̾� �̵� ��� ����


    private void Awake() // ��ũ��Ʈ ����� ���� ���� �ѹ� ����, setActive(false) ���¿��� ���� �ȴ�.
    {
        characterController = GetComponent<CharacterController>(); // ĳ���� ��Ʈ�ѷ� �Ҵ�
        head = GetComponent<XRRig>().cameraGameObject; // ��ϵǾ� �ִ� player Camera ��ü�� �����´�.
    }

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� ������ ó��
        PositionController();
    }

    // �̵� ó���� �Էº��� ���� ����ȴ�, �Է¿� �ﰢ������ ȭ���� ���ŵǸ� �������� �����̴�. 
    void Update()
    {
        PositionController(); // ���� ��ġ�� �°� ��ġ�� ������

        if(moveType == MoveType.FreeMove)
        {
            FreeMove(); // ������ ��Ʈ�ѷ� �߿��� ��ǲ �Է��� �ִٸ� �̵�ó���� ��
        }
        else if (moveType == MoveType.GoFront)
        {
            GoFront(); // Z�� �������� �̵���
        }
    }
    void GoFront()
    {
        Vector3 movement = Vector3.forward * speed;
        characterController.Move(movement * Time.deltaTime);
    }
    void PositionController()
    {
        // �ε巯�� �̵��� ���ؼ� 1(�ּ�) 2(�ִ�)���� head�� y ���� ���� �ǵ��� ��! 
        float headHeight = Mathf.Clamp(head.transform.localPosition.y, 0.1f, 0.1f);
        characterController.height = headHeight;

        Vector3 newCenter = Vector3.zero;
        newCenter.x = head.transform.localPosition.x;
        newCenter.z = head.transform.localPosition.z;

        characterController.center = newCenter; // ĳ������ ��ġ�� �Ӹ��� x,z ��ǥ
    }

    void FreeMove()
    {
        foreach (XRController controller in controllers)
        {
            if (controller.enableInputActions) // ��Ʈ�ѷ����� �׼��� �߻��ߴٸ�
            {
                CheckForMovement(controller.inputDevice);
            }
        }
    }

    void CheckForMovement(InputDevice device)
    {
        // ������ primary2DAxis ������ �о� �ü� �ִ�.
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
        {
            StartMove(position);
        }
    }
    void StartMove(Vector2 position)
    {
        Vector3 direction = new Vector3(position.x, 0, position.y); // y ���� ���� ���� �ʴ´�.
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0); // Player Camera�� ȸ�� ������ ����

        direction = Quaternion.Euler(headRotation) * direction; // Player Camera�� �ٶ󺸴� �������� ȸ���Ѵ�.

        Vector3 movement = direction * speed;
        characterController.Move(movement * Time.deltaTime);
    }
}