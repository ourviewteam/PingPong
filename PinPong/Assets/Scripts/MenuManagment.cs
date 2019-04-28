using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagment : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject createMatch;
    [SerializeField]
    private GameObject joinMatch;

    public void OnClickExit()
    {
        Application.Quit();
    }

    private void Start()
    {
        OnClickBack();
    }

    public void OnClickConnect()
    {
        mainMenu.SetActive(false);
        createMatch.SetActive(false);
        joinMatch.SetActive(true);
    }

    public void OnClickCreate()
    {
        mainMenu.SetActive(false);
        joinMatch.SetActive(false);
        createMatch.SetActive(true);
    }

    public void OnClickBack()
    {
        mainMenu.SetActive(true);
        joinMatch.SetActive(false);
        createMatch.SetActive(false);
    }
}
