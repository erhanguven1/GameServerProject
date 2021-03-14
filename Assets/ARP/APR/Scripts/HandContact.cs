using UnityEngine;


//-------------------------------------------------------------
    //--APR Player
    //--Hand Contact
    //
    //--Unity Asset Store - Version 1.0
    //
    //--By The Famous Mouse
    //
    //--Twitter @FamousMouse_Dev
    //--Youtube TheFamouseMouse
    //-------------------------------------------------------------


namespace ARP.APR.Scripts
{
    public class HandContact : MonoBehaviour
    {
        public APRController APR_Player;
    
        //Is left or right hand
        public bool Left;
    
        //Have joint/grabbed
        public bool hasJoint;

        private BallController ball;

        void Update()
        {
            if(APR_Player.useControls)
            {
                //Left Hand
                //On input release destroy joint
                if(Left)
                {
                    if(hasJoint && !transform.root.GetComponent<Player>().leftClick)
                    {
                        this.gameObject.GetComponent<FixedJoint>().breakForce = 0;
                        hasJoint = false;
                        if (ball != null)
                        {
                            ball.ReleaseHand(transform.root.GetComponent<Player>(), true);
                        }
                    }

                    if(hasJoint && this.gameObject.GetComponent<FixedJoint>() == null)
                    {
                        if (hasJoint != false)
                        {
                            if (ball != null)
                            {
                                ball.ReleaseHand(transform.root.GetComponent<Player>(), true);
                            }
                        }
                        hasJoint = false;
                    }
                }

                //Right Hand
                //On input release destroy joint
                if(!Left)
                {
                    if(hasJoint && !transform.root.GetComponent<Player>().rightClick)
                    {
                        this.gameObject.GetComponent<FixedJoint>().breakForce = 0;
                        hasJoint = false;
                        if (ball != null)
                        {
                            ball.ReleaseHand(transform.root.GetComponent<Player>(), false);
                        }
                    }

                    if(hasJoint && this.gameObject.GetComponent<FixedJoint>() == null)
                    {
                        if (hasJoint != false)
                        {
                            if (ball != null)
                            {
                                ball.ReleaseHand(transform.root.GetComponent<Player>(), false);
                            }
                        }
                        hasJoint = false;
                    }
                }
            }
        }

        //Grab on collision when input is used
        void OnCollisionEnter(Collision col)
        {
            if(APR_Player.useControls)
            {
                //Left Hand
                if(Left)
                {
                    if(col.gameObject.tag == "CanBeGrabbed" && col.gameObject.layer != LayerMask.NameToLayer(APR_Player.thisPlayerLayer) && !hasJoint)
                    {
                        if(transform.root.GetComponent<Player>().leftClick && !hasJoint && !transform.root.GetComponent<Player>().q)
                        {
                            hasJoint = true;
                            this.gameObject.AddComponent<FixedJoint>();
                            this.gameObject.GetComponent<FixedJoint>().breakForce = Mathf.Infinity;
                            this.gameObject.GetComponent<FixedJoint>().connectedBody = col.gameObject.GetComponent<Rigidbody>();

                            ball = col.gameObject.GetComponent<BallController>();

                            if (ball != null)
                            {
                                ball.GrabBall(transform.root.GetComponent<Player>(), true);
                            }
                        }
                    }
                
                }

                //Right Hand
                if(!Left)
                {
                    if(col.gameObject.tag == "CanBeGrabbed" && col.gameObject.layer != LayerMask.NameToLayer(APR_Player.thisPlayerLayer) && !hasJoint)
                    {
                        if(transform.root.GetComponent<Player>().rightClick && !hasJoint && !transform.root.GetComponent<Player>().e)
                        {
                            hasJoint = true;
                            this.gameObject.AddComponent<FixedJoint>();
                            this.gameObject.GetComponent<FixedJoint>().breakForce = Mathf.Infinity;
                            this.gameObject.GetComponent<FixedJoint>().connectedBody = col.gameObject.GetComponent<Rigidbody>();

                            ball = col.gameObject.GetComponent<BallController>();

                            if (ball != null)
                            {
                                ball.GrabBall(transform.root.GetComponent<Player>(), false);
                            }
                        }
                    }
                }
            }
        }

        public void ReleaseHand()
        {
            this.gameObject.GetComponent<FixedJoint>().breakForce = 0;
            hasJoint = false;
        }
    }
}
