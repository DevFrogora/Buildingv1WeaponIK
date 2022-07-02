using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15;
    Camera mainCamera;
    InputActionMap landActionMap;
    InputAction escape;
    InputAction alt;

    public Transform cameraLookAt;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    public bool toggleMouseLock;

    private void Awake()
    {
        xAxis.SetInputAxisProvider(0, GetComponent<Cinemachine.CinemachineInputProvider>());
        yAxis.SetInputAxisProvider(1, GetComponent<Cinemachine.CinemachineInputProvider>());
    }

    void Start()
    {
        landActionMap = ActionMapManager.playerInput.actions.FindActionMap(ActionMapManager.ActionMap.Land);
        mainCamera = Camera.main;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        GameManager.instance.changeActionMap += ChangeActionMap;
        RegisterAction();

    }

    void ChangeActionMap(string actionMap)
    {
        if (actionMap == ActionMapManager.ActionMap.Land)
        {
            //RegisterActionMap();
            Debug.Log("Player Land Activate");
        }
        else
        {
            UnRegisterActionMap();
        }
    }

    void RegisterAction()
    {
        escape = landActionMap["Esc"];
        alt = landActionMap["Alt"];
        escape.performed += escapePerformed;

    }

    private void escapePerformed(InputAction.CallbackContext obj)
    {
        toggleMouseLock = !toggleMouseLock;
        if (toggleMouseLock)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void UnRegisterActionMap()
    {
        landActionMap.Disable();
    }

    void FixedUpdate()
    {

        if (!toggleMouseLock) return;
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);

        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        if(!alt.IsPressed())
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }
}

