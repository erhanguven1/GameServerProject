using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController instance;
    void Awake()
    {
        instance = this;
    }

    public Player owner;
    public bool leftHand, rightHand;
    public Rigidbody rb;

    public void GrabBall(Player _player, bool isLeft)
    {
        if (owner != null && _player == owner)
        {
            if (isLeft)
            {
                leftHand = true;
            }
            else
            {
                rightHand = true;
            }
        }
        if (owner == null)
        {
            owner = _player;
            owner.aprController.ball = this;
            owner.hasBall = true;

            if (isLeft)
            {
                leftHand = true;
            }
            else
            {
                rightHand = true;
            }
        }
    }

    public void ReleaseHand(Player _player, bool isLeft)
    {
        if (_player == owner)
        {
            if (isLeft)
            {
                leftHand = false;
            }
            else
            {
                rightHand = false;
            }

            if (!leftHand && !rightHand) //ikisi de bırakıldıysa owner'ı boşalt
            {
                RemoveOwner();
            }
        }
    }

    public void ThrowBall(Player _player, Vector3 _direction, bool isLeft)
    {
        if (_player == owner)
        {
            if (isLeft)
            {
                leftHand = false;
            }
            else
            {
                rightHand = false;
            }

            rb.AddForce(_direction * 50, ForceMode.Impulse);

            if (!leftHand && !rightHand) //ikisi de bırakıldıysa owner'ı boşalt
            {
                RemoveOwner();
            }
        }
    }

    void RemoveOwner()
    {
        owner.hasBall = false;
        owner = null;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
