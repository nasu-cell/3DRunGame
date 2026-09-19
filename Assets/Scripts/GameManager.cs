using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
public class GameManager : MonoBehaviour
{
 
    public AudioSource audioSource;
    public int score;
    public TextMeshProUGUI scoreText;
    public UIManager uiManager;
    [SerializeField]
    private bool isChangeRecord;
    public BestScoreRecord bestScoreRecord;
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        score = 0;
        bestScoreRecord = GameObject.Find("BestScoreRecord").GetComponent<BestScoreRecord>();
    }
 
    public void UpdateScore()
    {
        audioSource.Play();
        score++;
        scoreText.text = "Score: " + score;
    }
    public void Result()
    {
        Debug.Log("score : " + score);
        Debug.Log("bestScore : " + bestScoreRecord.bestScore);
        Debug.Log("isChangeRecord : " + isChangeRecord);
        if (score > bestScoreRecord.bestScore)
        {
            isChangeRecord = true;
            bestScoreRecord.bestScore = score;
        }
        else
        {
            isChangeRecord = false;
        }
        Debug.Log("isChangeRecord : " + isChangeRecord);
        uiManager.ResultShow(score, bestScoreRecord.bestScore, isChangeRecord);
        
    }
}