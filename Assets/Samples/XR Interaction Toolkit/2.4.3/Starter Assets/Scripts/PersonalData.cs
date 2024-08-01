using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalData : MonoBehaviour
{
    public Collider CoverCollider { get=>GetComponentInChildren<Collider>(); }
    public FilterCover filterCover;
}
