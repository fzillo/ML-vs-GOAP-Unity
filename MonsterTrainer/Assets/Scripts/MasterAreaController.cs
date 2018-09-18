using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAreaController : MonoBehaviour
{
    public AreaController areaNorth;
    public AreaController areaSouth;

    public Bomb bombTeamM;
    public Bomb bombTeamA;

    MonsterTrainerAcademy academy;

    void Start()
    {
        academy = FindObjectOfType<MonsterTrainerAcademy>();
    }

    void Update()
    {

    }

    public void NotifyAreaChanged()
    {
        activateBombIfBothConqueredByOneTeam();
    }

    public void ResetAllAreas()
    {
        areaNorth.ResetArea();
        bombTeamM.ResetBomb();

        areaSouth.ResetArea();
        bombTeamA.ResetBomb();
    }

    public void StartZoneMDetonates()
    {
        Debug.Log("Team A wins the Game!");
        bombTeamA.DetonateBomb();
        academy.Done();
    }

    public void StartZoneADetonates()
    {
        Debug.Log("Team M wins the Game!");
        bombTeamM.DetonateBomb();
        academy.Done();
    }

    private void activateBombIfBothConqueredByOneTeam()
    {
        if (areaNorth.conqueredByTeamM && areaSouth.conqueredByTeamM)
        {
            bombTeamM.ActivateBomb();
        }
        else if (areaNorth.conqueredByTeamA && areaSouth.conqueredByTeamA)
        {
            bombTeamA.ActivateBomb();
        }
    }
}
