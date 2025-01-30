using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Objects to hide when pause menu")]
    [SerializeField] private List<GameObject> _objectsToHide = new();


    [SerializeField] GameObject pausePanel;
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;
    //[SerializeField] Collider2D blockCollider;
    private bool paused = false;


    private void Start()
    {
        button1.onClick.AddListener(PlayClickSound);
        button2.onClick.AddListener(PlayClickSound);
        button3.onClick.AddListener(PlayClickSound);
    }

    private void OnDestroy()
    {
        button1.onClick.RemoveListener(PlayClickSound);
        button2.onClick.RemoveListener(PlayClickSound);
        button3.onClick.RemoveListener(PlayClickSound);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        foreach (var obj in _objectsToHide)
        {
            obj.SetActive(false);
        }

        pausePanel.SetActive(true);
        paused = true;
        //blockCollider.enabled = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        foreach (var obj in _objectsToHide)
        {
            obj.SetActive(true);
        }

        Time.timeScale = 1.0f;
        paused = false;
        //blockCollider.enabled = false;
        pausePanel.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        paused = false;
        //blockCollider.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        paused = false;
        //blockCollider.enabled = false;
        SceneManager.LoadScene(0);
    }

    private void PlayClickSound()
    {
        int rand = Random.Range(1, 3);
        PlayRandomSound(rand);
    }

    private void PlayRandomSound(int caseRandom)
    {
        switch (caseRandom)
        {
            case 1:
                AudioManager.instance.PlayAudio(SFXType.ClickSound1);
                break;
            case 2:
                AudioManager.instance.PlayAudio(SFXType.ClickSound2);
                break;
            default:
                break;

        }
    }
}
