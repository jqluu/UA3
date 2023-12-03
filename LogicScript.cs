using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    SaveData saveData;
    public int playerScore = 0;
    
    [SerializeField]
    TextMeshProUGUI scoreText;  

    [SerializeField]
    GameObject gameOverScreen;

    [SerializeField]
    GameObject victoryScreen;

    float height, width;
    Camera main;

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString() + "/30";
    }


    void Start()
    {
        DateTime d = new DateTime();
        DateTime.TryParse(PlayerPrefs.GetString("LastOnline", DateTime.Now.ToString()),out d);
        Debug.Log(DateTime.Now - d);

        saveData = ScriptableObject.CreateInstance<SaveData>();
        if(File.Exists(Application.persistentDataPath+"/CurScore.dat"))
        {
            Debug.Log(Application.persistentDataPath+"/CurScore.dat");
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath+"/CurScore.dat"),saveData);
            playerScore = saveData.score;
        }
    
        scoreText.text = playerScore.ToString() + "/30";
        main = Camera.main;
        height = main.orthographicSize;
        width = height * main.aspect;
    }


    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void victory()
    {
        victoryScreen.SetActive(true);
    }


    void OnDestroy()
    {
        ShuttingDown();
    }
    public void ShuttingDown()
    {
        PlayerPrefs.SetString("LastOnline",DateTime.Now.ToString());
        saveData.score = playerScore;
        File.WriteAllText(Application.persistentDataPath+"/CurScore.dat", JsonUtility.ToJson(saveData));

    }

    
}