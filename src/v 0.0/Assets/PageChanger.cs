using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageChanger : MonoBehaviour
{
    public GameObject nextPage;
    public GameObject lastPage;
    public GameObject page;
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
}
