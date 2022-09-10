using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour { 

    public GameOverScreen GameOverScreen;
    public GameObject Player;
    float currentTime = 0f;
    float startingTime = 10f;

    [SerializeField] TextMeshProUGUI countdown;

    void GameOver()
    {
        GameOverScreen.Setup();
    }


    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        currentTime -= 1 * Time.unscaledDeltaTime;
        //Debug.Log(currentTime);
        //countdown.text = currentTime.ToString();
        countdown.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            Player = GameObject.Find("Player");
            Player.SetActive(false);

            GameOver();
            
            
        }
    }
}
