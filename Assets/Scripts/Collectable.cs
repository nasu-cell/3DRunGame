using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Collectable : MonoBehaviour
{
    public int rotateSpeed = 1;
 
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
 
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.UpdateScore();
            Destroy(gameObject);
        }
    }
}