using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPositionGenerator : MonoBehaviour
{
    public GameObject areaForRandomSpawning;
    Bounds spawnAreaBounds;

    //TODO better determine dynamically
    float monsterSpawnHeight = 0.45f;

    void Start()
    {

        spawnAreaBounds = areaForRandomSpawning.GetComponent<BoxCollider>().bounds;
        areaForRandomSpawning.SetActive(false);
    }

    public List<Vector3> GetStartPositions(List<Monster> monsterList)
    {
        List<Vector3> positionList = new List<Vector3>();

        foreach (Monster monsterEntity in monsterList)
        {
            positionList.Add(monsterEntity.transform.position);
        }

        return positionList;
    }

    public void AssignRandomPositionsForMultipleMonstersFromList(List<Monster> monsterList, List<Vector3> positionList)
    {
        if (monsterList.Count != positionList.Count)
        {
            Debug.Log("ERROR - monsterList.Count != positionList.Count");
            //TODO Throw Exception?
        }

        RandomizePositions(positionList);

        int listIndex = 0;

        foreach (Monster monsterEntity in monsterList)
        {
            if (listIndex < positionList.Count)
            {
                Rigidbody monsterRB = monsterEntity.GetComponent<Rigidbody>();
                monsterRB.transform.position = positionList[listIndex];
                monsterRB.transform.rotation = new Quaternion(0, 0, 0, 0);
                monsterRB.velocity = Vector3.zero;
                monsterRB.angularVelocity = Vector3.zero;

                listIndex++;
            }
        }
    }

    public void AssignRandomPositionForMonsterFromList(Monster monsterEntity, List<Vector3> positionList)
    {
        Rigidbody monsterRB = monsterEntity.GetComponent<Rigidbody>();
        int randomIndex = Random.Range(0, positionList.Count - 1);
        monsterRB.transform.position = positionList[randomIndex];
        monsterRB.transform.rotation = new Quaternion(0, 0, 0, 0);
        monsterRB.velocity = Vector3.zero;
        monsterRB.angularVelocity = Vector3.zero;

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

    public void AssignRandomPositionForMultipleMonstersInRandomSpawnZone(List<Monster> monsterList)
    {
        foreach (Monster monsterEntity in monsterList)
        {
            AssignRandomPositionForMonsterInRandomSpawnZone(monsterEntity);
        }
    }

    public void AssignRandomPositionForMonsterInRandomSpawnZone(Monster monsterEntity)
    {
        //Random Position
        Vector3 randomSpawnPosition = GeneratePositionInRandomSpawnZone();

        //Random Rotation
        Quaternion randomSpawnRotation = GenerateRandomRotation();

        //Debug.Log("monsterEntity " + monsterEntity + " randomSpawnPosition " + randomSpawnPosition + " randomSpawnRotation " + randomSpawnRotation);

        //Assign
        Rigidbody monsterRB = monsterEntity.GetComponent<Rigidbody>();
        monsterRB.transform.position = randomSpawnPosition;
        //monsterRB.transform.Rotate(new Vector3(0, randomRotY, 0));
        monsterRB.transform.rotation = randomSpawnRotation;
        monsterRB.velocity = Vector3.zero;
        monsterRB.angularVelocity = Vector3.zero;
    }

    public Vector3 GeneratePositionInRandomSpawnZone()
    {
        //Random Position
        Vector3 randomSpawnPosition = Vector3.zero;
        float randomPosX = Random.Range(-spawnAreaBounds.extents.x,
                                        spawnAreaBounds.extents.x);
        float randomPosZ = Random.Range(-spawnAreaBounds.extents.z,
                                        spawnAreaBounds.extents.z);

        //TODO remove magic number!
        randomSpawnPosition = areaForRandomSpawning.transform.position + new Vector3(randomPosX, monsterSpawnHeight, randomPosZ);
        return randomSpawnPosition;
    }

    public Quaternion GenerateRandomRotation()
    {
        float randomRotY = Random.Range(0, 360);
        Quaternion randomSpawnRotation = Quaternion.Euler(0, randomRotY, 0);

        return randomSpawnRotation;
    }
}
