using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartZoneController : MonoBehaviour
{

    MasterAreaController masterAreaControl;

    public bool isDetonated;

    void Start()
    {
        masterAreaControl = FindObjectOfType<MasterAreaController>();
        isDetonated = false;
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (this.gameObject.tag == "teamMLStartZone" && col.gameObject.tag == "goapMonster" && col.gameObject.GetComponent<Monster>().hasBomb)
        {
            masterAreaControl.StartZoneMDetonates();
        }
        else if (this.gameObject.tag == "teamGOAPStartZone" && col.gameObject.tag == "mlMonster" && col.gameObject.GetComponent<Monster>().hasBomb)
        {
            masterAreaControl.StartZoneADetonates();
        }
    }
}
