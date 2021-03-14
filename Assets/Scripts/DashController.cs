using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public ARP.APR.Scripts.APRController aprController;

    public bool[] keys; //W;A;S;D
    public bool[] pressedKeys; //W;A;S;D
    public int[] pressedKeysNumber; //W;A;S;D (1st pressed, 2nd pressed)
    public float[] passedTimes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (keys[i])
            {
                if (!pressedKeys[i])
                {
                    pressedKeysNumber[i]++;
                    if (pressedKeysNumber[i] == 2)
                    {
                        pressedKeys[i] = false;
                        pressedKeysNumber[i] = 0;
                        if (passedTimes[i] < .3f)
                        {
                            print("dash to: " + (i == 0 ? "w" : (i == 1 ? "a" : (i == 2 ? "s" : "d"))) + "!");
                            aprController.Dash(new Vector3((i % 2) * Mathf.Sign(i - 2), 0, -((i + 1) % 2) * Mathf.Sign(i - 1)));

                        }
                        passedTimes[i] = 0;
                    }
                    pressedKeys[i] = true;
                }

            }
            else
            {
                pressedKeys[i] = false;
            }
            if (pressedKeysNumber[i] > 0)
            {
                passedTimes[i] += Time.deltaTime;
                if (passedTimes[i] >= .3f)
                {
                    pressedKeysNumber[i] = 0;
                    passedTimes[i] = 0;
                }
            }
        }
    }
}
