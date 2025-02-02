using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GameState { Ready, Playing, Ended }
public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.Ready;
    public RawImage background, platform;
    public float parallaxSpeed = 0.2f;
    public GameObject uiReady, uiScore;



    void Update()
    {
        bool action2 = Input.GetKeyDown("space");
        bool action1 = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter);


        HandleJump(action2);
        HandleCollisions();
        UpdateParallax();
        UpdateGameState(action1);
        HandleExit();
        
    }

    void HandleJump(bool action2)
    {
        if (gameState == GameState.Playing && action2)
        {
            PlayerManager.Instance.setAnimation("PlayerJump");
        }

    }

    void UpdateGameState(bool action1)
    {
        if (gameState == GameState.Ready && action1 ){
            gameState = GameState.Playing;
            
            uiReady.SetActive(false);
            uiScore.SetActive(true);

            PlayerManager.Instance.setAnimation("PlayerRun");
            SpawnManager.Instance.StartSpawn();
            SpeedManager.Instance.StartSpeedIncrease();
        }
        else if (gameState == GameState.Ended && action1)
        {
            HandleRestart();
        }

    }

    void UpdateParallax()
    {
        if (gameState == GameState.Playing)
        {
            float finalSpeed = parallaxSpeed * Time.deltaTime;
            background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
            platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
        }

    }

     void HandleCollisions()
    {
        if (gameState == GameState.Playing && PlayerManager.Instance.enemyCollision)
        {
            gameState = GameState.Ended;
            PlayerManager.Instance.setAnimation("PlayerDie");
            SpawnManager.Instance.StopSpawn();
            SpeedManager.Instance.ResetSpeed();
        }
    }

     void HandleRestart()
    {
        SceneManager.LoadScene("Main");
    }

    void HandleExit() 
    {
        if (Input.GetKeyDown("escape")) Application.Quit();
    }

}
