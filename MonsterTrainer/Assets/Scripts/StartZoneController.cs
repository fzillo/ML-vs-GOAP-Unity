using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartZoneController : MonoBehaviour
{

    MasterGameAreaController gAreaController;

    public bool isDetonated;

    void Start()
    {
        gAreaController = FindObjectOfType<MasterGameAreaController>();
        isDetonated = false;
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (this.gameObject.tag == "startZoneM" && col.gameObject.tag == "aMonster" && col.gameObject.GetComponent<Monster>().hasBomb)
        {
            gAreaController.StartZoneMDetonates();
        }
        else if (this.gameObject.tag == "startZoneA" && col.gameObject.tag == "mMonster" && col.gameObject.GetComponent<Monster>().hasBomb)
        {
            gAreaController.StartZoneADetonates();
        }
    }
}
