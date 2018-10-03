using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPositionGenerator : MonoBehaviour
{
    public GameObject areaForRandomSpawning;
    Bounds spawnAreaBounds;

    void Start()
    {
        areaForRandomSpawning.SetActive(false);
    }

    public List<Vector3> GetStartPositions(List<GameObject> monsterGOList)
    {
        List<Vector3> positionList = new List<Vector3>();

        foreach (GameObject monsterGO in monsterGOList)
        {
            positionList.Add(monsterGO.transform.position);
        }

        return positionList;
    }

    public void AssignRandomStartPositionsForAllMonsters(List<GameObject> monsterGOList, List<Vector3> positionList)
    {
        RandomizePositions(positionList);

        int listIndex = 0;

        foreach (GameObject monsterML in monsterGOList)
        {
            if (listIndex < positionList.Count)
            {
                Rigidbody monsterRB = monsterML.GetComponent<Rigidbody>();
                monsterRB.transform.position = positionList[listIndex];
                monsterRB.transform.rotation = new Quaternion(0, 0, 0, 0);
                monsterRB.velocity = Vector3.zero;
                monsterRB.angularVelocity = Vector3.zero;

                listIndex++;
            }
        }
    }

    // knuth shuffle algorithm
    public List<Vector3> RandomizePositions(List<Vector3> positionList)
    {
        for (int t = 0; t < positionList.Count; t++)
        {
            Vector3 tmp = positionList[t];
            int r = Random.Range(t, positionList.Count);
            positionList[t] = positionList[r];
            positionList[r] = tmp;
        }

        return positionList;
    }

    public void AssignRandomStartPositionForMonster(Monster monsterEntity, List<Vector3> positionList)
    {
        Rigidbody monsterRB = monsterEntity.GetComponent<Rigidbody>();
        int randomIndex = Random.Range(0, positionList.Count - 1);
        monsterRB.transform.position = positionList[randomIndex];
        monsterRB.transform.rotation = new Quaternion(0, 0, 0, 0);
        monsterRB.velocity = Vector3.zero;
        monsterRB.angularVelocity = Vector3.zero;

    }

    public void AssignRandomStartPositionForMonsterAnywhere(Monster monsterEntity)
    {
        spawnAreaBounds = areaForRandomSpawning.GetComponent<Collider>().bounds;

        //Random Position
        Vector3 randomSpawnPosition = Vector3.zero;
        float randomPosX = Random.Range(-spawnAreaBounds.extents.x,
                                        spawnAreaBounds.extents.x);
        float randomPosZ = Random.Range(-spawnAreaBounds.extents.z,
                                        spawnAreaBounds.extents.z);

        randomSpawnPosition = areaForRandomSpawning.transform.position + new Vector3(randomPosX, 0.45f, randomPosZ);

        //Random Rotation
        Quaternion randomSpawnRotation = new Quaternion(0, 0, 0, 0);
        float randomRotY = Random.Range(0, 360);
        randomSpawnRotation = new Quaternion(0, randomRotY, 0, 0);

        //Assign
        Rigidbody monsterRB = monsterEntity.GetComponent<Rigidbody>();
        monsterRB.transform.position = randomSpawnPosition;
        monsterRB.transform.rotation = randomSpawnRotation;
        monsterRB.velocity = Vector3.zero;
        monsterRB.angularVelocity = Vector3.zero;
    }
}
