using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Animator fadeAnimator;
    private bool _isPaused = false;
    public GameObject pausePanel;
    public GameObject cluesPanel;
    public TextMeshProUGUI cluesText;

    public GameObject mapPanel;
    public GameObject howToPlayPanel;
    public GameObject toast;
    public GameObject storyPanel;
    private Animator _animator;
    private TextMeshProUGUI _toastText;
    private bool _isStoryOpen = true;
    public GameObject winPanel;
    void Start()
    {
        Application.targetFrameRate = 50;
        FadeIn();
        _toastText = toast.GetComponentInChildren<TextMeshProUGUI>();
        _animator = toast.GetComponent<Animator>();
        AudioManager.instance.Stop("MainTheme");
        AudioManager.instance.Play("Ambient");
        AudioManager.instance.Play("Flute");
        AudioManager.instance.Play("Roll");
    }

    private void Update()
    {
        if(GameManager.Instance.DidWin)
            return;
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (_isStoryOpen)
                CloseStory();
            else
                OpenStory();
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!_isMapOpen)
            {
                Time.timeScale = 0;
                ShowMap();
            }
            else
                PauseGame();
        }
    }

    private bool _isMapOpen = false;
    public void PauseGame()
    {
        AudioManager.instance.Play("Click");
        pausePanel.SetActive(true);
        cluesPanel.SetActive(false);
        mapPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        Time.timeScale = 0;
        _isPaused = true;
        _isMapOpen = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ContinueGame()
    {
        AudioManager.instance.Play("Click");
        pausePanel.SetActive(false);
        cluesPanel.SetActive(false);
        mapPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        Time.timeScale = 1;
        _isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void MainMenu()
    {
        AudioManager.instance.Play("Click");
        Time.timeScale = 1;
        FadeOut();
        StartCoroutine(LoadSceneAfterSeconds(1));
    }

    private HashSet<string> _clues = new HashSet<string>();

    public void ShowClues()
    {
        AudioManager.instance.Play("Click");
        pausePanel.SetActive(false);
        cluesPanel.SetActive(true);
        mapPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        var finalString = "";
        var i = 1;
        foreach (var clue in _clues)
        {
            finalString += i + ". " + clue + "\n\n";
            i++;
        }

        cluesText.text = finalString;
        if (_clues.Count == 0)
            cluesText.text = "You haven't found any clues yet.";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowMap()
    {
        _isMapOpen = true;
        AudioManager.instance.Play("Click");
        pausePanel.SetActive(false);
        cluesPanel.SetActive(false);
        mapPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowHowTo()
    {
        AudioManager.instance.Play("Click");
        pausePanel.SetActive(false);
        cluesPanel.SetActive(false);
        mapPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenToast(string toastText)
    {
        _toastText.text = toastText;
        _animator.SetTrigger("ToastIn");
        StartCoroutine(CloseToast());
    }

    IEnumerator CloseToast()
    {
        yield return new WaitForSeconds(7);
        print("AAAAAAAAAAAAAAAAA");
        _animator.SetTrigger("ToastOut");
    }


    public void AddClue(string clue)
    {
        _clues.Add(clue);
        OpenToast(clue);
    }


    IEnumerator LoadSceneAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);
    }

    public void FadeIn()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        fadeAnimator.SetTrigger("FadeOut");
    }

    public void CloseStory()
    {
        storyPanel.SetActive(false);
        _isStoryOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenStory()
    {
        storyPanel.SetActive(true);
        _isStoryOpen = true;


        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public TextMeshProUGUI timeText;
    public void Win(float time)
    {
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        winPanel.SetActive(true);
        var min = (int) time;
        var seconds = (int)((time - min) * 60f);
        timeText.text = min + ":" + seconds;
    }
}