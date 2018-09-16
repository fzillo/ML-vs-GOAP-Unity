using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPositionGenerator : MonoBehaviour
{
    public List<GameObject> monstersTeamM;
    public List<GameObject> monstersTeamA;

    List<Vector3> startPositionsTeamM = new List<Vector3>();
    List<Vector3> startPositionsTeamA = new List<Vector3>();

    Quaternion startRotationTeamM = new Quaternion(0, 0, 0, 0);
    Quaternion startRotationTeamA = new Quaternion(0, 0, 0, 0);


    int listIndexTeamM = 0;
    int listIndexTeamA = 0;

    bool startPositionsInitialized = false;


    void Start()
    {
        //monstersTeamM = new List<GameObject>(teamMController.teamMonster);
        //Debug.Log(monstersTeamM.Count);
        //monstersTeamA = teamAControl.teamMonster;
    }

    public void AssignRandomStartPositionsForMonsters()
    {
        RandomizePositions();

        foreach (GameObject monsterM in monstersTeamM)
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
            //TODO else Exception?
        }

        foreach (GameObject monsterA in monstersTeamA)
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
            //TODO else Exception?
        }
    }

    public void RandomizePositions()
    {
        Debug.Log(startPositionsInitialized);

        //only once at start!
        if (!startPositionsInitialized)
        {
            foreach (GameObject monsterM in monstersTeamM)
            {
                startPositionsTeamM.Add(monsterM.transform.position);
            }
            foreach (GameObject monsterA in monstersTeamA)
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

}
