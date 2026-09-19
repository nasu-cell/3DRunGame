using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public GameObject ResultPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject newRecordText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResultShow(int score, int bestScore, bool changeRecord)
    {
        newRecordText.SetActive(changeRecord);
        scoreText.text = "Your Score: " + score;
        bestScoreText.text = "Best Score: " + bestScore;
        ResultPanel.SetActive(true);
    }
}
