using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private int sceneIndex;
    //find by going into the scene build list in build profiles

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// change scene
    /// </summary>
    /// <param name="newScene">set to -1 to use the scripts default set setter</param>
    public void SetScene(int newScene = -1)
    {
        if (newScene < 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            SceneManager.LoadScene(newScene);
        }
    }
}
