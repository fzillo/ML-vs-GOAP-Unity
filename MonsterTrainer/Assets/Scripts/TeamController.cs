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

    public RespawnMonster(Monster monsterEntity)
    {
        startPosGen.AssignRandomStartPositionsForMonster(monsterEntity);
        monsterEntity.Reset();
    }
}
