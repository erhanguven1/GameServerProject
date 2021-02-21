using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARP.APR.Scripts;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public APRController aprController;
    public float moveSpeed = 5f;

    public float verticalValue, horizontalValue, mouseYValue;
    public bool leftClick, rightClick, q, e;

    public List<Transform> bones = new List<Transform>();

    public Vector3[] bonePositions;
    public Quaternion[] boneRotations;

    private bool[] inputs;

    private void Start()
    {
        if (id > 1)
        {
            GetComponentInChildren<APRController>().thisPlayerLayer = "Player_2";
            foreach (var item in GetComponentsInChildren<Collider>())
            {
                item.gameObject.layer = 11;
            }
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
    }

    private void SendBonePositions()
    {
        ServerSend.BonePositions(this, bones);
        //ServerSend.PlayerRotation(this);
    }

    public void SetInput(float _vertical, float _horizontal, float _mouseY, bool[] _inputs)
    {
        verticalValue = _vertical;
        horizontalValue = _horizontal;
        mouseYValue = _mouseY;

        leftClick = _inputs[0];
        rightClick = _inputs[1];
        q = _inputs[2];
        e = _inputs[3];
    }
}