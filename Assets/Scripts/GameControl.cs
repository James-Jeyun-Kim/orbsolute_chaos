using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;

    public int remakeTime = 500;
    public GameObject GameOverText;
    public GameObject player1;
    public GameObject player2;
    public bool gameOver = false;

    private Text text;
    private Text text2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Screen.SetResolution(256 * 4, 768, true);
        text = GameOverText.GetComponent<Text>();
        text2 = GameOverText.transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<PlayerManager>().dead)
        {
            gameOver = true;
            text.text = "Game Over \n" + "Blue Orb Won!";
        }
        else if (player2.GetComponent<PlayerManager>().dead)
        {
            gameOver = true;
            text.text = "Game Over \n" + "Pink Orb Won!";
        }
        if (gameOver)
        {
            StartCoroutine(RemakeGame());
        }
    }


    private IEnumerator RemakeGame()
    {
        float waited = 0;
        while (waited < remakeTime)
        {
            waited += Time.deltaTime;
            text2.text = "\n \n \n \n New game in.. " + (remakeTime - waited).ToString("F0");
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
