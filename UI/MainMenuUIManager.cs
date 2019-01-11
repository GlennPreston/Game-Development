using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public GameObject loadingOverlay;
    public GameObject loadingText;
    public GameObject transitionOverlay;
    public Button continueGameBtn;
    public GameObject newGameConfirmOverlay;
    public GameController gameController;

    private Image loadingImage;
    private Image transitionImage;
    private Color tempColor;

    private void Start()
    {
        loadingOverlay.SetActive(true);
        transitionOverlay.SetActive(false);
        newGameConfirmOverlay.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (!File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            continueGameBtn.interactable = false;
        }

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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

    public void ContinueBtn()
    {
        Debug.Log("Loading game");
        if(gameController.Load())
        {
            Debug.Log("Game data found");
            StartCoroutine(FadeTransition(gameController.stage));
        }
        else
        {
            Debug.Log("Game data not found");
            continueGameBtn.interactable = false;
        }
    }

    public void NewGameBtn()
    {
        Debug.Log("New game");
        newGameConfirmOverlay.SetActive(true);
    }

    public void NewGameConfirm(bool confirm)
    {
        Debug.Log("New game confirm");
        if (confirm)
        {
            gameController.stage = "Stage_1";
            gameController.level = "Level_1";
            gameController.Save("Stage_1", "Level_1");
            StartCoroutine(FadeTransition("Stage_1"));
        }
        else
        {
            newGameConfirmOverlay.SetActive(false);
        }
    }

    public void SettingsBtn()
    {
        Debug.Log("Settings");
    }

    public void ExitGameBtn()
    {
        Debug.Log("Exiting game");
        Application.Quit();
    }
}
