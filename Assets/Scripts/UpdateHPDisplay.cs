using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHPDisplay : MonoBehaviour
{
    public GameObject assos_player;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = assos_player.GetComponent<PlayerManager>().curr_health.ToString();
    }
}
