using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class WeaponInHand : MonoBehaviour
{
    InputActionMap landActionMap;
    InputAction leftMouse,rightMouse;
    public float aimDuration = 0.3f;

    public UnityEngine.Animations.Rigging.Rig handIK;

    public Rig aimLayer;
    // Start is called before the first frame update

    public static WeaponInHand instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        landActionMap = ActionMapManager.playerInput.actions.FindActionMap(ActionMapManager.ActionMap.Land);
        GameManager.instance.changeActionMap += ChangeActionMap;
        BagUIBroadcast.instance.activeSlot += ActiveSlot;
        BagUIBroadcast.instance.droppedSlot += DroppedSlot;
        BagUIBroadcast.instance.slot1ShotType += SetShootType;
        BagUIBroadcast.instance.slot1AssultAdded += SetSlot1;
        RegisterAction();
    }

    private void SetSlot1(GameObject obj)
    {
    }


    public GameObject activeWeapon;
    int activeSlotNumber;
    public m416 weaponScriptRef; // we have to make interface so same method work on different type of weapon

    //WeaponShotType.ShotType shotType = WeaponShotType.ShotType.Auto;

    private void LateUpdate()
    {
        if(activeWeapon != null)
        {
            if (weaponScriptRef.isFiring)
            {
                
                //Debug.Log("mouse pressed");
                aimLayer.weight += Time.deltaTime / aimDuration;
                weaponScriptRef.shoot();
            }
            else
            {
                if(rightMouse.IsPressed())
                {
                    aimLayer.weight += Time.deltaTime / aimDuration;
                }
                else
                {
                    aimLayer.weight -= Time.deltaTime / aimDuration;
                }
            }
        }
        else
        {
            handIK.weight = 0.0f;
        }
    }

    

    void ActiveSlot( int slotNumber,bool activeState)
    {
        if(slotNumber == 1)
        {
            activeSlotNumber = slotNumber;
            activeWeapon =  BagInventory.instance.slot1.assultPrefab;
            weaponScriptRef = activeWeapon.GetComponent<m416>();
            weaponScriptRef.mouse = leftMouse;
            BagUIBroadcast.instance.Slot1AmmoTextUpdate(weaponScriptRef.currentAmmo + " / inf");
            handIK.weight = 1f;
        }
    }

    void DroppedSlot(int slotNumber, bool activeState)
    {
        if (slotNumber == activeSlotNumber)
        {
            activeWeapon = null;
            handIK.weight = 0f;
        }
    }

    public void SetShootType(WeaponShotType.ShotType _shotType)
    {
        if(activeWeapon != null)
        {
            weaponScriptRef.shotType = _shotType.Next();
            Debug.Log(weaponScriptRef.shotType);
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
        rightMouse = landActionMap["RightMouse"];
        leftMouse.performed += LeftMouse_performed;
        leftMouse.canceled += LeftMouse_canceled;
        leftMouse.performed += RightMouse_performed;
        leftMouse.canceled += RightMouse_canceled;

    }


    private void RightMouse_performed(InputAction.CallbackContext obj)
    {
        //aimLayer.weight = 1f;
    }

    private void RightMouse_canceled(InputAction.CallbackContext obj)
    {
        //aimLayer.weight = 0f;
    }


    private void LeftMouse_canceled(InputAction.CallbackContext obj)
    {
        if (activeWeapon != null)
        {
            weaponScriptRef.StopFiring();
        }
    }

    private void LeftMouse_performed(InputAction.CallbackContext obj)
    {
        if(activeWeapon != null)
        {
            weaponScriptRef.StartFiring();

        }
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
