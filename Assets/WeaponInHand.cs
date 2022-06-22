using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class WeaponInHand : MonoBehaviour
{
    InputActionMap landActionMap;
    InputAction leftMouse;
    public float aimDuration = 0.3f;

    public UnityEngine.Animations.Rigging.Rig handIK;

    public Rig aimLayer;
    // Start is called before the first frame update
    private void Start()
    {
        landActionMap = ActionMapManager.playerInput.actions.FindActionMap(ActionMapManager.ActionMap.Land);
        GameManager.instance.changeActionMap += ChangeActionMap;
        BagUIBroadcast.instance.activeSlot += ActiveSlot1;
        BagUIBroadcast.instance.droppedSlot += DroppedSlot1;

        RegisterAction();
    }

    public GameObject activeWeapon;
    int activeSlotNumber;
    private void Update()
    {
        if(activeWeapon != null)
        {
            if (leftMouse.IsPressed())
            {
                //Debug.Log("mouse pressed");
                aimLayer.weight += Time.deltaTime / aimDuration;
            }
            else
            {
                aimLayer.weight -= Time.deltaTime / aimDuration;
            }
        }
        else
        {
            handIK.weight = 0.0f;
        }


    }

    void ActiveSlot1( int slotNumber)
    {
        if(slotNumber == 1)
        {
            activeSlotNumber = slotNumber;
            activeWeapon =  BagInventory.instance.slot1.assultPrefab;
            handIK.weight = 1f;
        }
    }

    void DroppedSlot1(int slotNumber)
    {
        if (slotNumber == activeSlotNumber)
        {
            activeWeapon = null;
            handIK.weight = 0f;
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
