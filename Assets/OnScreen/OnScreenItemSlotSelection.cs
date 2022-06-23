using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class OnScreenItemSlotSelection : MonoBehaviour
{
    public Image slot1Image;
    public Image slot2Image;
    public Image slot1ShotTypeImage;
    public TextMeshProUGUI slot1ShotTypeText;
    public TextMeshProUGUI slot1AmmoText;
    public Image slot2ShotTypeImage;


    private void Start()
    {
        slot1AmmoText.text = "";
        BagUIBroadcast.instance.slot1AssultAdded += SetSlot1;
        BagUIBroadcast.instance.slot2AssultAdded += SetSlot2;
        BagUIBroadcast.instance.slot1AmmoTextUpdate += Slot1AmmoTextUpdate;
        //BagUIBroadcast.instance.Slot1ShootType += 

    }

    void Slot1AmmoTextUpdate(string text)
    {
        slot1AmmoText.text = text;
    }

    void SetSlot1(GameObject item)
    {
        if (item == null)
        {
            slot1Image.sprite = null;
            slot1AmmoText.text = "";
        }
        else
        {
            Debug.Log("WeaponOnscreenSlot1");
            slot1Image.sprite = item.GetComponent<IInventoryItem>().spriteImage;
            if(BagInventory.instance.activeSlot1)
            {
                slot1ShotTypeImage.gameObject.SetActive(true);

            }
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
        if(BagInventory.instance.activeSlot1)
        {
            slot1ShotTypeImage.gameObject.SetActive(true);
        }
        else
        {
            slot1ShotTypeImage.gameObject.SetActive(false);
        }
    }

    public void Slot2Clicked()
    {
        BagInventory.instance.ActiveSlot2(!BagInventory.instance.activeSlot2);
    }

    WeaponShotType.ShotType shotType = WeaponShotType.ShotType.Auto;

    public void Slot1ShotTypeClicked()
    {
        if(WeaponInHand.instance.activeWeapon)
        {
            BagInventory.instance.SetSlot1ShotType(WeaponInHand.instance.weaponScriptRef.shotType);
            slot1ShotTypeText.text = WeaponInHand.instance.weaponScriptRef.shotType.ToString();
        }
    }




}
