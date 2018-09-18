using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPositionGenerator : MonoBehaviour
{
    public List<GameObject> monsterGameobjectsTeamM;
    public List<GameObject> monsterGameobjectsTeamA;

    List<Vector3> startPositionsTeamM = new List<Vector3>();
    List<Vector3> startPositionsTeamA = new List<Vector3>();

    Quaternion startRotationTeamM = new Quaternion(0, 0, 0, 0);
    Quaternion startRotationTeamA = new Quaternion(0, 0, 0, 0);

    public TeamController teamMController;
    public TeamController teamAController;


    int listIndexTeamM = 0;
    int listIndexTeamA = 0;

    bool startPositionsInitialized = false;


    void Start()
    {
        /*
        //TODO this needs to happen before Academy starts
        monsterGameobjectsTeamM = new List<GameObject>(teamMController.teamMonsterList);
        Debug.Log("monsterGameobjectsTeamM.Count :" + monsterGameobjectsTeamM.Count);
        monsterGameobjectsTeamA = new List<GameObject>(teamAController.teamMonsterList);
        Debug.Log("monsterGameobjectsTeamA.Count :" + monsterGameobjectsTeamA.Count);
        */
    }

    public void AssignRandomStartPositionsForAllMonsters()
    {
        RandomizePositions();

        foreach (GameObject monsterM in monsterGameobjectsTeamM)
        {
            if (listIndexTeamM < startPositionsTeamM.Count)
            {
                monsterM.transform.position = startPositionsTeamM[listIndexTeamM++];
                monsterM.transform.rotation = startRotationTeamM;

                Rigidbody rb = monsterM.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }

        foreach (GameObject monsterA in monsterGameobjectsTeamA)
        {
            if (listIndexTeamA < startPositionsTeamA.Count)
            {
                monsterA.transform.position = startPositionsTeamA[listIndexTeamA++];
                monsterA.transform.rotation = startRotationTeamA;

                Rigidbody rb = monsterA.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    public void RandomizePositions()
    {
        Debug.Log(startPositionsInitialized);

        //only once at start!
        if (!startPositionsInitialized)
        {
            foreach (GameObject monsterM in monsterGameobjectsTeamM)
            {
                startPositionsTeamM.Add(monsterM.transform.position);
            }
            foreach (GameObject monsterA in monsterGameobjectsTeamA)
            {
                startPositionsTeamA.Add(monsterA.transform.position);
            }
            startPositionsInitialized = true;
        }

        //shuffle Positions
        KnuthShuffle(startPositionsTeamM);
        KnuthShuffle(startPositionsTeamA);

        //reset Index
        listIndexTeamM = 0;
        listIndexTeamA = 0;
    }


    // knuth shuffle algorithm
    public List<Vector3> KnuthShuffle(List<Vector3> listToShuffle)
    {
        for (int t = 0; t < listToShuffle.Count; t++)
        {
            Vector3 tmp = listToShuffle[t];
            int r = Random.Range(t, listToShuffle.Count);
            listToShuffle[t] = listToShuffle[r];
            listToShuffle[r] = tmp;
        }

        return listToShuffle;
    }

    public void AssignRandomStartPositionsForMonster(Monster monsterEntity)
    {
        Rigidbody monsterRB = monsterEntity.GetComponent<Rigidbody>();
        monsterRB.velocity = Vector3.zero;
        monsterRB.angularVelocity = Vector3.zero;
        if (monsterEntity.tag == "mMonster")
        {
            int randomIndex = Random.Range(0, startPositionsTeamM.Count - 1);
            monsterRB.transform.position = startPositionsTeamM[randomIndex];
            monsterRB.transform.rotation = startRotationTeamM;
        }
        else if (monsterEntity.tag == "aMonster")
        {
            int randomIndex = Random.Range(0, startPositionsTeamA.Count - 1);
            monsterRB.transform.position = startPositionsTeamA[randomIndex];
            monsterRB.transform.rotation = startRotationTeamM;
        }
    }
}
