using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    public Transform ballStartPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        Server.Start(50, 5555);
    }

    void OnGUI()
    {
        GUILayout.Label(Server.currentState);
    }

    private void OnApplicaitonQuit()
    {
        Server.Stop();
    }

    public Player InstantiatePlayer(int id)
    {
        return Instantiate(playerPrefab, spawnPoints[id-1].position, spawnPoints[id-1].rotation).GetComponent<Player>();
    }
}
