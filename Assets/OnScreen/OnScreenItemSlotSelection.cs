using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenItemSlotSelection : MonoBehaviour
{
    public Image slot1Image;
    public Image slot2Image;


    private void Start()
    {
        BagUIBroadcast.instance.slot1AssultAdded += SetSlot1;
        BagUIBroadcast.instance.slot2AssultAdded += SetSlot2;

    }

    void SetSlot1(GameObject item)
    {
        if (item == null)
        {
            slot1Image.sprite = null;
        }
        else
        {
            Debug.Log("WeaponOnscreenSlot1");
            slot1Image.sprite = item.GetComponent<IInventoryItem>().spriteImage;
        }
    }

    void SetSlot2(GameObject item)
    {
        if (item == null)
        {
            slot2Image.sprite = null;
        }
        else
        {
            Debug.Log("WeaponOnscreenSlot2");
            slot2Image.sprite = item.GetComponent<IInventoryItem>().spriteImage;
        }
    }


    public void Slot1Clicked()
    {
        BagInventory.instance.ActiveSlot1(!BagInventory.instance.activeSlot1);
    }

    public void Slot2Clicked()
    {
        BagInventory.instance.ActiveSlot2(!BagInventory.instance.activeSlot2);
    }
}
