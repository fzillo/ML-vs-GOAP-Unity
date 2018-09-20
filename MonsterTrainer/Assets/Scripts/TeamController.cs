using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    public GameObject StartZone;
    public List<GameObject> teamMonsterList;

    public GameObject teamBomb;
    public GameObject bombTargetArea;

    StartPositionGenerator startPosGen;

    void Start()
    {
        startPosGen = FindObjectOfType<StartPositionGenerator>();
    }

    public void RespawnMonster(Monster monsterEntity)
    {
        startPosGen.AssignRandomStartPositionsForMonster(monsterEntity);
        monsterEntity.Reset();
    }

    public void ResetAllMonsters()
    {
        foreach (GameObject monsterGameObject in teamMonsterList)
        {
            Monster monsterEntity = monsterGameObject.GetComponent<Monster>();
            Debug.Log("!monsterEntity.deactivatedAtStart " + !monsterEntity.deactivatedAtStart + " monsterEntity " + monsterEntity);
            if (!monsterEntity.deactivatedAtStart)
            {
                monsterEntity.Reset();
            }
        }
    }
}
