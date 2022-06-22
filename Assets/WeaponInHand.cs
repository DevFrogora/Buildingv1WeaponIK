using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class WeaponInHand : MonoBehaviour
{
    InputActionMap landActionMap;
    InputAction leftMouse;
    public Rig aimLayer;
    // Start is called before the first frame update
    private void Start()
    {
        landActionMap = ActionMapManager.playerInput.actions.FindActionMap(ActionMapManager.ActionMap.Land);
        GameManager.instance.changeActionMap += ChangeActionMap;
        RegisterAction();
    }
    private void Update()
    {
        if(leftMouse.IsPressed())
        {
            Debug.Log("mouse pressed");
        }
    }

    void ChangeActionMap(string actionMap)
    {
        if (actionMap == ActionMapManager.ActionMap.Land)
        {
            RegisterAction();
            Debug.Log("Player Mouse Activate");
        }
        else
        {
            UnRegisterActionMap();
        }
    }

    void RegisterAction()
    {
        leftMouse = landActionMap["LeftMouse"];

    }
    private void OnDisable()
    {
        UnRegisterActionMap();
    }

    void UnRegisterActionMap()
    {
        landActionMap.Disable();
    }
}
