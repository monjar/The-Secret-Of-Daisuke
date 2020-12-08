using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator fadeAnimator;
    public GameObject howToPanel;

    void Start()
    {
        graphicsText.text = "Graphics " + QualitySettings.names[QualitySettings.GetQualityLevel()];
        FadeIn();
        AudioManager.instance.Play("MainTheme");
    }

    void Update()
    {
    }

    public void PlayGame()
    {
        AudioManager.instance.Play("Click");
        FadeOut();
        StartCoroutine(LoadSceneAfterSeconds(1));
    }

    IEnumerator LoadSceneAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        AudioManager.instance.Play("Click");
        Application.Quit();
    }

    public void BackToMain()
    {
        AudioManager.instance.Play("Click");
        howToPanel.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        AudioManager.instance.Play("Click");
        howToPanel.SetActive(true);
    }

    public void FadeIn()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        fadeAnimator.SetTrigger("FadeOut");
    }

    public TextMeshProUGUI graphicsText;
    private int _graphics;

    public void ToggleGraphics()
    {
        AudioManager.instance.Play("Click");
        QualitySettings.SetQualityLevel((QualitySettings.GetQualityLevel() + 1) % QualitySettings.names.Length, true);
        graphicsText.text = "Graphics " + QualitySettings.names[QualitySettings.GetQualityLevel()];
    }
}