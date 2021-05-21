using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status")]
public class Status : ScriptableObject
{
  public bool isAiming;
  public bool isSprint;
  public bool isGround;
}
