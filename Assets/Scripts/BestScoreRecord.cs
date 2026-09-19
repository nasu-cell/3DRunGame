using UnityEngine;
using UnityEngine.SceneManagement;


public class BestScoreRecord : MonoBehaviour
{
    public static BestScoreRecord Instance;
    public int bestScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
