using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    public GameObject StartZone;
    public List<Monster> teamMonsterList;

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

        if (academy == null)
        {
            academy = FindObjectOfType<MonsterTrainerAcademy>();
        }

        if (!academy.attackEnemiesCurriculum)
        {
            startPosGen.AssignRandomPositionsForMultipleMonstersFromList(teamMonsterList, spawnPositionList);
        }
        else
        {
            startPosGen.AssignRandomPositionForMultipleMonstersInRandomSpawnZone(teamMonsterList);
        }
    }

    public void RespawnMonster(Monster monsterEntity)
    {
        if (!academy.attackEnemiesCurriculum)
        {
            startPosGen.AssignRandomPositionForMonsterFromList(monsterEntity, spawnPositionList);
        }
        else
        {
            startPosGen.AssignRandomPositionForMonsterInRandomSpawnZone(monsterEntity);
        }

        monsterEntity.Reset();
    }

    public void ResetAllMonsters()
    {
        foreach (Monster monsterEntity in teamMonsterList)
        {
            Debug.Log("!monsterEntity.deactivatedAtStart " + !monsterEntity.deactivatedAtStart + " monsterEntity " + monsterEntity);
            if (!monsterEntity.deactivatedAtStart)
            {
                monsterEntity.Reset();
            }
        }
    }
}
