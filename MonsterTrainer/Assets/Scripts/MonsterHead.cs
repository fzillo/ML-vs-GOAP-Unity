using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHead : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        //avoids that method gets called twice on Gameobject mlMonsterHead or goapMonsterHead,
        //(because it has two Colliders - one Trigger, one not)
        if (col.isTrigger)
        {
            return;
        }

        Debug.Log("Hit! " + col.gameObject);
        if (this.gameObject.tag == "mlMonsterHead")
        {
            if (col.gameObject.tag == "goapMonster")
            {
                //get knocked and damaged
                knockCollidingGameObject(col.gameObject, true);

                //Rewarding MLAgent
                this.gameObject.GetComponentInParent<MLMonsterAgent>().RewardAgentForDamagingEnemy();
            }
            else if (col.gameObject.tag == "goapMonsterHead")
            {
                //get knocked
                knockCollidingGameObject(col.gameObject, false);

                //Rewarding MLAgent
                this.gameObject.GetComponentInParent<MLMonsterAgent>().RewardAgentForKnockingEnemy();
            }

        }

        if (this.gameObject.tag == "goapMonsterHead")
        {
            if (col.gameObject.tag == "mlMonster")
            {
                //get knocked and damaged
                knockCollidingGameObject(col.gameObject, true);
            }
            else if (col.gameObject.tag == "mlMonsterHead")
            {
                //get knocked
                knockCollidingGameObject(col.gameObject, false);
            }
        }
    }

    void knockCollidingGameObject(GameObject collidingGameObject, bool takeDamage)
    {
        Vector3 vect = collidingGameObject.GetComponentInParent<Rigidbody>().transform.position - this.GetComponentInParent<Rigidbody>().transform.position;
        collidingGameObject.GetComponentInParent<Monster>().GetKnockedBack(vect.normalized, takeDamage);
    }
}
