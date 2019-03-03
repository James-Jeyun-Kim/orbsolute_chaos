using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.SceneManagement;

public class DetectStart : MonoBehaviour
{
    public GameObject p1_rdy;
    public GameObject p2_rdy;
    public int curr_scene = 0;

    private bool p1_pressed = false;
    private bool p2_pressed = false;
    private string next_scene;

    // Start is called before the first frame update
    void Start()
    {
        if(curr_scene == 0)
        {
            next_scene = "Tutorial";
        }
        else if(curr_scene == 1)
        {
            next_scene = "Battle";
        }
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad active_gamepad = Gamepad.all[0];
        Gamepad active_gamepad2 = Gamepad.all[1];

        if (active_gamepad.startButton.wasPressedThisFrame)
        {
            p1_rdy.SetActive(true);
            p1_pressed = true;
        }
        if (active_gamepad2.startButton.wasPressedThisFrame)
        {
            p2_rdy.SetActive(true);
            p2_pressed = true;
        }


        if(p1_pressed && p2_pressed)
        {
            SceneManager.LoadScene(next_scene, LoadSceneMode.Single);
        }
    }
}
