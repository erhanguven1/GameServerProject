using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    void Awake()
    {
        instance = this;
    }

    private Dictionary<Team, int> teamScores = new Dictionary<Team, int>();

    void Start()
    {
        teamScores = new Dictionary<Team, int>
        {
            { Team.team1, 0 },
            { Team.team2, 1 }
        };
    }


    public void IncrementScore(Team _scoredTeam)
    {
        teamScores[_scoredTeam]++;
        TimeManager.instance.StopTicking();
    }
}
