﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;
    public GameObject[] obstaclePrefabs;
    public GameObject[] zombiePrefabs;
    public Transform[] lanes;
    public float min_ObstacleDelay = 10f, max_ObstacleDelay = 40f;

    private float halfGroundSize;
    private BaseController playerController;
    private BaseController playerContoller;

    private Text scoreText;
    private int zombieKillCount;

    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject gameoverPanel;
    [SerializeField]
    private Text finalScore;


    private void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        halfGroundSize = GameObject.Find("GroundBlock Main").GetComponent<GroundBlock>().blockLength / 2;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseController>();
        StartCoroutine("GenerateObstacles");
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    IEnumerator GenerateObstacles()
    {
        float timer = Random.Range(min_ObstacleDelay, max_ObstacleDelay) / playerController.speed.z;
        yield return new WaitForSeconds(timer);

        CreateObstacles(playerController.gameObject.transform.position.z + halfGroundSize);
        StartCoroutine("GenerateObstacles");
    }

    void CreateObstacles(float zPos)
    {
        int r = Random.Range(0, 10);
        if(0 <= r && r < 7)
        {
            int obstacleLane = Random.Range(0, lanes.Length);

            AddObstacle(new Vector3(lanes[obstacleLane].transform.position.x, 0f, zPos), Random.Range(0, obstaclePrefabs.Length));
            int zombieLane = 0;

            if (obstacleLane == 0)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 2;
            } else if (obstacleLane == 1)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 0 : 2;
            } else if (obstacleLane == 2)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 0;
            }

            AddZombies(new Vector3(lanes[zombieLane].transform.position.x, 0.15f, zPos));
        }
    }

    void AddObstacle(Vector3 position, int type)
    {
        GameObject obstacle = Instantiate(obstaclePrefabs[type], position, Quaternion.identity);
        bool mirror = Random.Range(0, 2) == 1;
        switch(type)
        {
            case 0:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20f, 0f);
                break;
            case 1:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20f, 0f);
                break;
            case 2:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -1 : 1f, 0f);
                break;
            case 3:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -170 : 170f, 0f);
                break;
            default:
                Debug.Log("[AddObstacle] Type not found");
                break;
        }

        obstacle.transform.position = position;
    }

    void AddZombies(Vector3 pos)
    {
        int count = Random.Range(0, 3) + 1;
        for(int i = 0; i < count; i ++)
        {
            Vector3 shift = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(1f, 10) * i);
            Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], pos + shift * i, Quaternion.identity);
        }
    }

    public void IncreaseScore()
    {
        zombieKillCount++;
        scoreText.text = zombieKillCount.ToString();
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameoverPanel.SetActive(true);
        finalScore.text = "Killed: " + zombieKillCount.ToString();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }
}
