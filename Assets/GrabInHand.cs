using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class GrabInHand : MonoBehaviour
{
    public UnityEngine.Animations.Rigging.Rig handIK;
    public TwoBoneIKConstraint LeftHandIk;
    public TwoBoneIKConstraint RightHandIk;
    public Transform weaponPivot;

    public static GrabInHand instance;
    private void Awake()
    {
        instance = this;
        handIK.weight = 0f;

    }

    private void Start()
    {
        GrabUIBroadCast.instance.grabItemAdded += GrabItemAdded; // make it for grab


    }


    public GameObject activeBox;
    public Box boxScriptRef; // we have to make interface so same method work on different type of weapon



    void GrabItemAdded(GameObject activeBox)
    {
        //activeBox = BagInventory.instance.slot1.assultPrefab;
        boxScriptRef = activeBox.GetComponent<Box>();
        boxScriptRef.collideTriggerStatus += BoxScriptRef_collideTriggerStatus;
        boxScriptRef.OnPickup();
        handIK.weight = 1f;
        activeBox.gameObject.transform.SetParent(weaponPivot, false);
        StartCoroutine(HandPosition());

    }

    private void BoxScriptRef_collideTriggerStatus(bool obj)
    {
        if(obj)
        {
            weaponPivot.transform.localPosition = new Vector3(0, 1.186f, 0.2f);
            weaponPivot.transform.localRotation =Quaternion.Euler(40.0f, 0 , 0);
        }
        else
        {
            weaponPivot.transform.localPosition = new Vector3(0, 1.186f, 0.464f);
            weaponPivot.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    IEnumerator HandPosition()
    {
        yield return new WaitForEndOfFrame();
        RightHandIk.data.target.position = (boxScriptRef.rightHandGrip.position);
        RightHandIk.data.target.rotation = boxScriptRef.rightHandGrip.rotation;

        LeftHandIk.data.target.position = boxScriptRef.leftHandGrip.position;
        LeftHandIk.data.target.rotation = boxScriptRef.leftHandGrip.rotation;
    }


}
