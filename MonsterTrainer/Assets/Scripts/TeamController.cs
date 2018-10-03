using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    public GameObject StartZone;
    public List<GameObject> teamMonsterList;

    List<Vector3> spawnPositionList = new List<Vector3>();

    public GameObject teamBomb;
    public GameObject bombTargetArea;

    StartPositionGenerator startPosGen;
    MonsterTrainerAcademy academy;

    void Start()
    {
        academy = FindObjectOfType<MonsterTrainerAcademy>();
    }

    public void SetStartPostitionsForAllMonster()
    {
        if (startPosGen == null)
        {
            startPosGen = FindObjectOfType<StartPositionGenerator>();
        }
        if (spawnPositionList.Count == 0)
        {
            spawnPositionList = startPosGen.GetStartPositions(teamMonsterList);
        }

        startPosGen.AssignRandomStartPositionsForAllMonsters(teamMonsterList, spawnPositionList);
    }

    public void RespawnMonster(Monster monsterEntity)
    {
        if (!academy.attackEnemyCurriculum)
        {
            startPosGen.AssignRandomStartPositionForMonster(monsterEntity, spawnPositionList);
        }
        else
        {
            startPosGen.AssignRandomStartPositionForMonsterAnywhere(monsterEntity);
        }

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
