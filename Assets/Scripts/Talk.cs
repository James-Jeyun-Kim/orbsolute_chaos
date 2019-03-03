using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.SceneManagement;

public class Talk : MonoBehaviour
{
    public GameObject msg1;
    public GameObject msg2;
    public GameObject msg3;
    public GameObject msg4;
    public GameObject msg5;
    public GameObject msg6;
    public GameObject next;

    private GameObject[] msgArr = new GameObject[6];
    private int ind = -1;

    // Start is called before the first frame update
    void Start()
    {
        msgArr = new GameObject[] { msg2, msg3, msg4, msg5, msg6 };
        StartCoroutine(waitMsg());
    }

    IEnumerator waitMsg()
    {
        yield return new WaitForSeconds(2f);
        next.SetActive(true);
        while (true)
        {
            Gamepad active_gamepad = Gamepad.all[0];
            Gamepad active_gamepad2 = Gamepad.all[1];
            if(active_gamepad.yButton.wasPressedThisFrame || active_gamepad2.yButton.wasPressedThisFrame)
            {
                if(ind == -1)
                {
                    msg1.SetActive(false);
                }
                else
                {
                    msgArr[ind].SetActive(false);
                }
                ind++;
                msgArr[ind].SetActive(true);
                next.SetActive(false);
                yield return new WaitForSeconds(2f);
                if(ind != 4)
                {
                    next.SetActive(true);
                }
                else
                {
                    break;
                }
            }
            yield return null;
        }
    }
}
