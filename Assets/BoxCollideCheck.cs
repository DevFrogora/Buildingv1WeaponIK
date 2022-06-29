using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollideCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsInside;

    void OnTriggerEnter(Collider c)
    {
        IsInside = true;
    }

    void OnTriggerExit(Collider c)
    {
        IsInside = false;
        gameObject.GetComponentInParent<Box>().CollideTriggerStatus(false);
    }
}
