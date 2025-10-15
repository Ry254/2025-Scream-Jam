using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class DeathManager : MonoBehaviour
{
    // Singleton Implementation
    public static DeathManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [HideInInspector] public string DeathFlavorText = "You seem to have died... spooky~...";
    [HideInInspector] public string HelpText = "Try not dying";

    private bool _isDying = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CauseDeath(string flavorText, string helpText)
    {
        if (_isDying) return;

        _isDying = true;
        DeathFlavorText = "\"" + flavorText + "\"";
        HelpText = helpText;

        // High score
        int oldScore = PlayerPrefs.GetInt("Score", 0);
        if (Movement.totalScore > oldScore)
        {
            PlayerPrefs.SetInt("Score", (int)Movement.totalScore);
            PlayerPrefs.Save();
        }

        StartCoroutine(LoadDeathScene());
    }

    private IEnumerator LoadDeathScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Death", LoadSceneMode.Single);

        while (!loading.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        UpdateDeathUI();
        _isDying = false;
    }

    private void UpdateDeathUI()
    {
        GameObject[] taggedItems = GameObject.FindGameObjectsWithTag("Death Text");

        if (taggedItems.Length < 2)
        {
            Debug.LogError("Missing death text objects");
            return;
        }

        if (taggedItems[0].name.Contains("__flavor__"))
        {
            taggedItems[0].GetComponent<TextMeshProUGUI>().text = DeathFlavorText;
            taggedItems[1].GetComponent<TextMeshProUGUI>().text = HelpText;
        }
        else
        {
            taggedItems[1].GetComponent<TextMeshProUGUI>().text = DeathFlavorText;
            taggedItems[0].GetComponent<TextMeshProUGUI>().text = HelpText;
        }
    }
}
