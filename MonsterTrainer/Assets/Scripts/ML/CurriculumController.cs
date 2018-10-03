using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurriculumController : MonoBehaviour
{

    public bool curriculumActive = false;

    public enum Phase { phaseConquerOneArea, phaseConquerBothAreas, phasePickupBomb, phaseDeliverBomb };

    public int maximumPhase = (int)Phase.phaseConquerOneArea;
}
