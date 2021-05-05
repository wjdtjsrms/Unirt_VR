using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CustomController : MonoBehaviour
{
    // device Ư������ ��� ����
    public InputDeviceCharacteristics characteristics;
    [SerializeField]
    private List<GameObject> controllerModels; // ��� ������ �� ����Ʈ��
    private GameObject controllerInstance; // ������ ��Ʈ�ѷ� �ν��Ͻ��� �����ϴ� ����
    private InputDevice availableDevice; // ���� ������� ��Ʈ�ѷ�

    public bool renderController; // Hand�� Controller ���̸� ������ ����
    public GameObject handModel; // �ڵ� �� prefab
    private GameObject handInstance; // ������ �ڵ� �ν��Ͻ��� �����ϴ� ����

    private Animator handModelAnimator; // �ڵ� �� �ִϸ��̼� ����

    public GameObject handGun; // �� ��

    bool triggerButton;

    void Start()
    {
        TryInitiaiize();
    }
    void Update()
    {
        // ������ ������ ����Ʈ�� ������ ���� �ʾҴٸ� �ٽ� �ʱ�ȭ�� �����Ѵ�.
        if (!availableDevice.isValid)
        {
            TryInitiaiize();
        }

        // �ڵ� �𵨰� ��Ʈ�ѷ� ���� �����Ѵ�.
        if (renderController)
        {
            handInstance.SetActive(false);
            controllerInstance.SetActive(true);
        }
        else
        {
            handInstance.SetActive(true);
            controllerInstance.SetActive(false);
            UpdateHandAnimation(); // �ڵ� �ִϸ��̼��� ���⼭�� �����Ѵ�.

        }

        if(handGun != null) // �� ���� �ִٸ� Ʈ���� ��ư�� �������� �߻��Ѵ�.
        {
            bool triggerButtonValue;
            if(availableDevice.TryGetFeatureValue(CommonUsages.triggerButton,out triggerButtonValue) && triggerButtonValue)
            {
                if(triggerButton == false) // Ʈ���� ��ư�� ������ ���� ���¿����� �߻�ȴ�.
                {
                    handGun.GetComponent<GunShoot>().Shoot();
                    triggerButton = true;
                }
                
            }
            else
            {
                triggerButton = false;
            }
        }

        if(true) // GameManager.Instance.isGameOver �׽�Ʈ�ÿ���
        {
            bool menuButtonValue;

            if (availableDevice.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonValue) && menuButtonValue)
            {
                GameManager.Instance.RestartGame();
            }
         }
    }

    void TryInitiaiize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);

        foreach(var device in devices)
        {
            Debug.Log($"Available Device Name : {device.name}, Characteristic  {device.characteristics}");
        }
        if(devices.Count > 0)
        {
            availableDevice = devices[0];
            string name = "";


            if("Oculus Touch Controller - Left" == availableDevice.name)
            {
                name = "Oculus Quest Controller - Left";
            }
            else if ("Oculus Touch Controller - Right" == availableDevice.name)
            {
                name = "Oculus Quest Controller - Right";
            }

            // availableDevice �̸��� ��ġ�ϴ� controllerModels�� ���Ұ� �ִٸ� �����Ѵ�.
            GameObject currentControllerModel = controllerModels.Find(controller => controller.name == name);
            if(currentControllerModel)
            {
                controllerInstance = Instantiate(currentControllerModel, transform);
            }
            else
            {
                // ������ ��ü�� ã�� ���ϸ� �⺻ ��ü�� �����Ѵ�.
                Debug.LogError("Didn't get sutiable controller model");
                controllerInstance = Instantiate(controllerModels[0], transform);
            }

            handInstance = Instantiate(handModel, transform); // �ڵ� �ν��Ͻ� �߰�
            handModelAnimator = handInstance.GetComponent<Animator>(); // �ڵ� �ν��Ͻ��� �߰��Ǿ� �ִ� �ִϸ����͸� �����´�.
        }
    }

    void UpdateHandAnimation()
    {
        // ��Ʈ�ѷ��� trigger ���� �ִϸ������� trigger ���� �����Ѵ�.
      
        if (availableDevice.TryGetFeatureValue(CommonUsages.trigger,out float triggerValue))
        {
            handModelAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handModelAnimator.SetFloat("Trigger", 0);
        }

        // ��Ʈ�ѷ��� grip ���� �ִϸ������� grip ���� �����Ѵ�.
        if (availableDevice.TryGetFeatureValue(CommonUsages.grip,out float gripValue))
        {
            handModelAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handModelAnimator.SetFloat("Grip", 0);
        }
    }
}