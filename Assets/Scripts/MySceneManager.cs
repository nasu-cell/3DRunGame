using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理に必要

public class MySceneManager : MonoBehaviour
{
    // インスペクターから遷移先のシーン名を入力できるようにする
    public string targetSceneName;
    public GameObject ResultPanel;

    // シーンを切り替えるための関数
    public void SceneChange()
    {
        if(ResultPanel != null)
        {
            ResultPanel.SetActive(false);
        }
        // targetSceneNameに設定されたシーンへ遷移する
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogWarning("遷移先のシーン名（targetSceneName）が設定されていません。");
        }
    }
}