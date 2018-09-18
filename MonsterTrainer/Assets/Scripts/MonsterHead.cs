using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHead : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        //avoids that method gets called twice on Gameobject mHead or aHead,
        //(because it has two Colliders - one Trigger, one not)
        if (col.isTrigger)
        {
            return;
        }

        Debug.Log("Hit! " + col.gameObject);
        if (this.gameObject.tag == "mHead")
        {
            if (col.gameObject.tag == "aMonster")
            {
                //get knocked and damaged
                Vector3 vect = col.gameObject.GetComponentInParent<Rigidbody>().transform.position - this.GetComponentInParent<Rigidbody>().transform.position;
                col.gameObject.GetComponentInParent<Monster>().GetKnockedBack(vect.normalized, true);
                //Rewarding Agent
                this.gameObject.GetComponentInParent<MMonsterAgent>().RewardAgentForDamagingEnemy();
            }
            else if (col.gameObject.tag == "aHead")
            {
                //get knocked
                Vector3 vect = col.gameObject.GetComponentInParent<Rigidbody>().transform.position - this.GetComponentInParent<Rigidbody>().transform.position;
                col.gameObject.GetComponentInParent<Monster>().GetKnockedBack(vect.normalized, false);

                //Rewarding Agent
                this.gameObject.GetComponentInParent<MMonsterAgent>().RewardAgentForKnockingEnemy();
            }

        }

        //TODO refactor this duplicate code!
        if (this.gameObject.tag == "aHead")
        {
            if (col.gameObject.tag == "mMonster")
            {
                //get knocked and damaged
                Vector3 vect = col.gameObject.GetComponentInParent<Rigidbody>().transform.position - this.GetComponentInParent<Rigidbody>().transform.position;
                col.gameObject.GetComponentInParent<Monster>().GetKnockedBack(vect.normalized, true);
            }
            else if (col.gameObject.tag == "mHead")
            {
                //get knocked
                Vector3 vect = col.gameObject.GetComponentInParent<Rigidbody>().transform.position - this.GetComponentInParent<Rigidbody>().transform.position;
                col.gameObject.GetComponentInParent<Monster>().GetKnockedBack(vect.normalized, false);
            }
        }
    }
}
