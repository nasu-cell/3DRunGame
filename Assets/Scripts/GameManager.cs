using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
public class GameManager : MonoBehaviour
{
 
    public AudioSource audioSource;
    public int score;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
 
    public void UpdateScore()
    {
        audioSource.Play();
        score++;
        scoreText.text = "Score: " + score;
    }
}