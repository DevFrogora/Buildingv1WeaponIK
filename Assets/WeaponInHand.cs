using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class WeaponInHand : MonoBehaviour
{
    InputActionMap landActionMap;
    InputAction mouse;
    public Rig aimLayer;
    // Start is called before the first frame update
    private void Start()
    {
        landActionMap = ActionMapManager.playerInput.actions.FindActionMap(ActionMapManager.ActionMap.Land);
        GameManager.instance.changeActionMap += ChangeActionMap;
    }
    private void Update()
    {
        
    }

    void ChangeActionMap(string actionMap)
    {
        if (actionMap == ActionMapManager.ActionMap.Land)
        {
            RegisterAction();
            Debug.Log("Player Land Activate");
        }
        else
        {
            UnRegisterActionMap();
        }
    }

    void RegisterAction()
    {
        mouse = landActionMap["Mouse"];

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
