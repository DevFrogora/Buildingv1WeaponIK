using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BagUIBroadcast : MonoBehaviour
{
    public static BagUIBroadcast instance;
    private void Awake()
    {
        instance = this;
    }

    public event Action<GameObject> slot1AssultAdded,
    slot2AssultAdded,slot1SightAdded,slotMixItemAdded;

    public void Slot1AssultAdded(GameObject item)
    {
        slot1AssultAdded?.Invoke(item);
    }

    public void Slot1SightAdded(GameObject item)
    {
        slot1SightAdded?.Invoke(item);
    }

    public void Slot2AssultAdded(GameObject item)
    {
        slot2AssultAdded?.Invoke(item);
    }

    public void SlotMixItemAdded(GameObject item)
    {
        slotMixItemAdded?.Invoke(item);
    }
}
