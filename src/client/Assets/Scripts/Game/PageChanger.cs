using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject nextPage;

    [SerializeField]
    private GameObject lastPage;

    [SerializeField]
    private GameObject page;

    [SerializeField]
    private GameObject ChallengePage;

    [SerializeField]
    private GameObject MapsPage;

    public void NextPage()
    {
        nextPage.SetActive(true);
        page.SetActive(false);
    }
    public void LastPage()
    {
        lastPage.SetActive(true);
        page.SetActive(false);
    }
    public void ChallengePageActivate()
    {
        ChallengePage.SetActive(true);
        page.SetActive(false);
    }
    public void ChallengePageDeactivate()
    {
        MapsPage.SetActive(true);
        ChallengePage.SetActive(false);
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
