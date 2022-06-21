using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptManager : MonoBehaviour
{
    //Handling State
    //string previousScript;
    //string currentScript;
    //bool isFirst;
    CharacterLocomotion characterLandController;
    void Start()
    {
        GameManager.instance.changeActionMap += ChangeActionMap;

        characterLandController = GetComponent<CharacterLocomotion>();
    }

    void ChangeActionMap(string actionName)

    {
        if (actionName == ActionMapManager.ActionMap.Land)
        {
            characterLandController.enabled = true;
        }
        else
        {
            characterLandController.enabled = false;
        }

    }
}
