using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPositionGenerator : MonoBehaviour
{
    public List<GameObject> monsterGameobjectsTeamML;
    public List<GameObject> monsterGameobjectsTeamGOAP;

    List<Vector3> startPositionsTeamML = new List<Vector3>();
    List<Vector3> startPositionsTeamGOAP = new List<Vector3>();

    Quaternion startRotationTeamML = new Quaternion(0, 0, 0, 0);
    Quaternion startRotationTeamGOAP = new Quaternion(0, 0, 0, 0);

    public TeamController teamMLController;
    public TeamController teamGOAPController;


    int listIndexTeamML = 0;
    int listIndexTeamGOAP = 0;

    bool startPositionsInitialized = false;


    void Start()
    {

    }

    public void AssignRandomStartPositionsForAllMonsters()
    {
        RandomizePositions();

        foreach (GameObject monsterML in monsterGameobjectsTeamML)
        {
            if (listIndexTeamML < startPositionsTeamML.Count)
            {
                monsterML.transform.position = startPositionsTeamML[listIndexTeamML++];
                monsterML.transform.rotation = startRotationTeamML;

                Rigidbody rb = monsterML.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }

        foreach (GameObject monsterGOAP in monsterGameobjectsTeamGOAP)
        {
            if (listIndexTeamGOAP < startPositionsTeamGOAP.Count)
            {
                monsterGOAP.transform.position = startPositionsTeamGOAP[listIndexTeamGOAP++];
                monsterGOAP.transform.rotation = startRotationTeamGOAP;

                Rigidbody rb = monsterGOAP.GetComponent<Rigidbody>();
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
            foreach (GameObject monsterML in monsterGameobjectsTeamML)
            {
                startPositionsTeamML.Add(monsterML.transform.position);
            }
            foreach (GameObject monsterGOAP in monsterGameobjectsTeamGOAP)
            {
                startPositionsTeamGOAP.Add(monsterGOAP.transform.position);
            }
            startPositionsInitialized = true;
        }

        //shuffle Positions
        KnuthShuffle(startPositionsTeamML);
        KnuthShuffle(startPositionsTeamGOAP);

        //reset Index
        listIndexTeamML = 0;
        listIndexTeamGOAP = 0;
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
        if (monsterEntity.tag == "mlMonster")
        {
            int randomIndex = Random.Range(0, startPositionsTeamML.Count - 1);
            monsterRB.transform.position = startPositionsTeamML[randomIndex];
            monsterRB.transform.rotation = startRotationTeamML;
        }
        else if (monsterEntity.tag == "goapMonster")
        {
            int randomIndex = Random.Range(0, startPositionsTeamGOAP.Count - 1);
            monsterRB.transform.position = startPositionsTeamGOAP[randomIndex];
            monsterRB.transform.rotation = startRotationTeamGOAP;
        }
    }
}
