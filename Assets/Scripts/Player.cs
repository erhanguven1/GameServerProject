using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARP.APR.Scripts;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public APRController aprController;
    public DashController dashController;
    public float moveSpeed = 5f;

    public float verticalValue, horizontalValue, mouseYValue, jumpValue;
    public bool leftClick, rightClick, q, e;

    public Transform serverCameraTransform;

    public List<Transform> bones = new List<Transform>();

    public Vector3[] bonePositions;
    public Quaternion[] boneRotations;

    public Team myTeam;
    public bool hasBall;

    private void Start()
    {
        myTeam = (Team)((id - 1) / 3);

        GetComponentInChildren<APRController>().thisPlayerLayer = "Player_" + id.ToString();
        foreach (var item in GetComponentsInChildren<Collider>())
        {
            item.gameObject.layer = (id + 9);
        }

        if (id > 1)
        {
            //
        }
        else
        {
            TimeManager.instance.StartCountdown();
        }
        moveSpeed *= Time.fixedDeltaTime;
    }

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;

        bonePositions = new Vector3[15];
        boneRotations = new Quaternion[15];
    }

    public void FixedUpdate()
    {
        SendBonePositions();
        if (hasBall && !GameplayManager.instance.isWaiting)
        {
            if (myTeam == Team.team1 && bones[0].position.z > TouchdownManager.instance.teamTwoTouchDownLine.position.z)
            {
                TouchdownManager.instance.TouchDown(myTeam);
            }
            if (myTeam == Team.team2 && bones[0].position.z < TouchdownManager.instance.teamOneTouchDownLine.position.z)
            {
                TouchdownManager.instance.TouchDown(myTeam);
            }
        }
    }

    private void SendBonePositions()
    {
        ServerSend.BonePositions(this, bones);
    }

    public void SetInput(float _vertical, float _horizontal, Vector3 _camPos, Quaternion _camRot, float _mouseY, bool[] _inputs, float _jump)
    {
        if (GameplayManager.instance.cantMove)
        {
            verticalValue = 0;
            horizontalValue = 0;

            leftClick = false;
            rightClick = false;
            q = false;
            e = false;
            for (int i = 0; i < 4; i++)
            {
                dashController.keys[i] = false;
            }

            jumpValue = 0;
            return;
        }

        verticalValue = _vertical;
        horizontalValue = _horizontal;

        serverCameraTransform.position = _camPos;
        serverCameraTransform.rotation = _camRot;

        mouseYValue = _mouseY;

        leftClick = _inputs[0];
        rightClick = _inputs[1];
        q = _inputs[2];
        e = _inputs[3];
        for (int i = 0; i < 4; i++)
        {
            dashController.keys[i] = _inputs[i + 4];
        }

        jumpValue = _jump;
    }
}