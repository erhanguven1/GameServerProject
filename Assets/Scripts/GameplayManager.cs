using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    void Awake()
    {
        instance = this;
    }

    public bool isWaiting, cantMove;

    public IEnumerator WaitAndRespawn()
    {
        isWaiting = true;

        yield return new WaitForSeconds(5f);
        cantMove = true;
        foreach (var item in Server.clients.Values)
        {
            //Reset player positions
            if (item.player != null)
            {
                if (item.player.aprController.LeftHand.GetComponent<FixedJoint>() || item.player.aprController.RightHand.GetComponent<FixedJoint>())
                {
                    if (item.player.aprController.LeftHand.GetComponent<FixedJoint>())
                    {
                        Destroy(item.player.aprController.LeftHand.GetComponent<FixedJoint>());
                        item.player.aprController.LeftHand.GetComponent<ARP.APR.Scripts.HandContact>().hasJoint = false;
                    }
                    if (item.player.aprController.RightHand.GetComponent<FixedJoint>())
                    {
                        Destroy(item.player.aprController.RightHand.GetComponent<FixedJoint>());
                        item.player.aprController.RightHand.GetComponent<ARP.APR.Scripts.HandContact>().hasJoint = false;
                    }
                }

                item.player.aprController.Root.transform.position = NetworkManager.instance.spawnPoints[item.id - 1].position;
                item.player.aprController.Root.transform.rotation = NetworkManager.instance.spawnPoints[item.id - 1].rotation;
            }
        }


        //Reset ball pos
        BallController.instance.rb.isKinematic = true;
        BallController.instance.rb.velocity = Vector3.zero;
        BallController.instance.rb.angularVelocity = Vector3.zero;

        BallController.instance.transform.position = NetworkManager.instance.ballStartPoint.position;

        TimeManager.instance.StartCountdown();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
