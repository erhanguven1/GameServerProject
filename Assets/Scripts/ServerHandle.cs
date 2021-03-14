﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{
    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        Server.clients[_fromClient].SendIntoGame(_username);
    }

    public static void BoneSync(int _fromClient, Packet _packet)
    {
        float _vertical = _packet.ReadFloat();
        float _horizontal = _packet.ReadFloat();
        Vector3 _camPos = _packet.ReadVector3();
        Quaternion _camRot = _packet.ReadQuaternion();
        float _mouseY = _packet.ReadFloat();

        bool[] _inputs = new bool[8];

        for (int i = 0; i < 8; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }

        float _jump = _packet.ReadFloat();

        Server.clients[_fromClient].player.SetInput(_vertical, _horizontal, _camPos, _camRot, _mouseY, _inputs, _jump);
    }
}