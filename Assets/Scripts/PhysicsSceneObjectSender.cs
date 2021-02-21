using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSceneObjectSender : MonoBehaviour
{
    void FixedUpdate()
    {
        SendPositionAndRotation();
    }

    private void SendPositionAndRotation()
    {
        ServerSend.SendSceneObjectPositionAndRotation(GetComponent<PhysicsSceneObject>().id, transform.position, transform.rotation);
    }
}
