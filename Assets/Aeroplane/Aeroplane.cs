using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aeroplane : MonoBehaviour
{

    public GameObject[] propellers;
    public float speed;

    public Light middleLight;
    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(LightBlinker());
    }

    IEnumerator LightBlinker()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(LightBlinker());
        middleLight.enabled = !middleLight.enabled;
    }

    void Update()
    {
        foreach(var propeller in propellers)
        {
            propeller.transform.Rotate(0, 0, 30f);
        }
        //transform.Translate(transform.forward * speed *Time.deltaTime,Space.World);
    }
}
