using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    public GameObject loadingOverlay;
    public GameObject loadingText;
    public GameObject transitionOverlay;
    public GameObject pauseMenu;
    public PlayerController playerController;

    [HideInInspector]
    public bool isPaused;

    private Image loadingImage;
    private Image transitionImage;
    private Color tempColor;

    private void Start()
    {
        loadingOverlay.SetActive(true);
        transitionOverlay.SetActive(false);
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;

        loadingImage = loadingOverlay.GetComponent<Image>();
        transitionImage = transitionOverlay.GetComponent<Image>();
        StartCoroutine(FadeLoadingOverlay());
    }

    IEnumerator FadeLoadingOverlay()
    {
        yield return new WaitForSeconds(2);

        loadingText.SetActive(false);

        while (loadingImage.color.a > 0)
        {
            tempColor = loadingImage.color;
            tempColor.a -= Time.deltaTime / 2;
            loadingImage.color = tempColor;
            yield return null;
        }

        yield return null;
        loadingOverlay.SetActive(false);
    }

    IEnumerator FadeTransition(string scene)
    {
        if (!transitionOverlay.activeSelf)
        {
            transitionOverlay.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            while (transitionImage.color.a < 1)
            {
                tempColor = transitionImage.color;
                tempColor.a += Time.deltaTime / 2;
                transitionImage.color = tempColor;
                yield return null;
            }

            yield return null;
            SceneManager.LoadScene(scene);
        }
    }

    private void Update()
    {
        if (!loadingOverlay.activeSelf && !transitionOverlay.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ManagePause();
        }
    }

    public void ResumeBtn()
    {
        Debug.Log("Resume");
        ManagePause();
    }

    public void ReloadBtn()
    {
        Debug.Log("Reload");
        ManagePause();
        StartCoroutine(FadeTransition(SceneManager.GetActiveScene().name));
    }

    public void SettingsBtn()
    {
        Debug.Log("Settings");
    }

    public void ExitBtn()
    {
        Debug.Log("Exit");
        ManagePause();
        StartCoroutine(FadeTransition("MainMenu"));
    }

    public void ManagePause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Cursor.visible = !Cursor.visible;
        isPaused = !isPaused;
        playerController.paused = !playerController.paused;

        if (isPaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
