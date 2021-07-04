using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //references
    public GameObject GameOverCanvas;
    public GameObject score;


    public static Vector2 bottomLeft;

    //game state
    public static bool gameOver;
    public static bool gameHasStarted;

    private void Awake()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        gameHasStarted = false;
    }

    public void GameOver()
    {
        gameOver = true;
        score.SetActive(false);
        GameOverCanvas.SetActive(true);
    }

    public void GameHasStarted()
    {
        gameHasStarted = true;
        score.SetActive(true);
    }

    public void OnOkBtnPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
