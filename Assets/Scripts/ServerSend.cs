using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSend
{
    private static void SendTCPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    private static void SendUDPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }

    private static void SendTCPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }
    private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
    }

    private static void SendUDPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }
    private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
    }

    #region Packets
    public static void Welcome(int _toClient, string _msg)
    {
        using (Packet _packet = new Packet((int)ServerPackets.welcome))
        {
            _packet.Write(_msg);
            _packet.Write(_toClient);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void SpawnPlayer(int _toClient, Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.username);
            _packet.Write(_player.transform.position);
            _packet.Write(_player.transform.rotation);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void BonePositions(Player _player, List<Transform> bones)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
        {
            _packet.Write(_player.id);
            for (int i = 0; i < bones.Count; i++)
            {
                _packet.Write(bones[i].position);
                _packet.Write(bones[i].rotation);
            }

            SendUDPDataToAll(_packet);
        }
    }

    public static void SendSceneObjectPositionAndRotation(int id, Vector3 _position, Quaternion _rotation)
    {
        using (Packet _packet = new Packet((int)ServerPackets.sceneObjectPositionAndRotation))
        {
            _packet.Write(id);
            _packet.Write(_position);
            _packet.Write(_rotation);

            SendUDPDataToAll(_packet);
        }
    }

    public static void SendServerTime(float _remainingTime)
    {
        using (Packet _packet = new Packet((int)ServerPackets.serverTime))
        {
            _packet.Write(_remainingTime);

            SendUDPDataToAll(_packet);
        }
    }

    public static void SendTouchdown(Team _team)
    {
        using (Packet _packet = new Packet((int)ServerPackets.touchdown))
        {
            _packet.Write((int)_team);

            SendTCPDataToAll(_packet);
        }
    }

    #endregion
}