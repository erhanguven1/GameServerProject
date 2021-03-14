using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchdownManager : MonoBehaviour
{
    public static TouchdownManager instance;

    void Awake()
    {
        instance = this;
    }

    public Transform teamOneTouchDownLine, teamTwoTouchDownLine;

    public void TouchDown(Team _team)
    {
        Debug.Log("Touchdown!" + _team);
        ScoreManager.instance.IncrementScore(_team);
        ServerSend.SendTouchdown(_team);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(teamOneTouchDownLine.position - Vector3.right * 50, teamOneTouchDownLine.position + Vector3.right * 50);
        Gizmos.DrawLine(teamTwoTouchDownLine.position - Vector3.right * 50, teamTwoTouchDownLine.position + Vector3.right * 50);
    }
}
