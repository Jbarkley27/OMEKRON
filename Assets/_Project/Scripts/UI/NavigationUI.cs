using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class NavigationUI : MonoBehaviour
{
    public enum NavigationState
    {
        Kit = 0,
        Map = 1,
        Controls = 2,
        Options = 3
    }

    [Header("Navigation")]
    public NavigationState currentNavigationState;
    public GameObject navigationUIRoot;
    public GameObject kitUIRoot;
    public GameObject mapUIRoot;
    public GameObject controlsUIRoot;
    public GameObject optionsUIRoot;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;

    [Header("Announcer")]
    public GameObject announcerUIRoot;
    public TMP_Text announcerText;
    public TMP_Text announcerSubText;
    public CanvasGroup announcerCanvasGroup;
    public string kitAnnouncement;
    public string mapAnnouncement;
    public string controlsAnnouncment;
    public string optionsAnnouncement;


    [Header("UI")]
    public Button kitButton;
    public Button mapButton;
    public Button controlsButton;
    public Button optionsButton;
    public Button activeButton;


    private void Start() 
    {
        SetNavigationState((int)NavigationState.Kit);
    }


    private void Update() 
    {
        if(activeButton) activeButton.Select();

        if(InputManager.instance.currentDevice == InputManager.ControllerType.KB)
        {
            leftArrowButton.SetActive(false);
            rightArrowButton.SetActive(false);
        }
        else
        {
            leftArrowButton.SetActive(true);
            rightArrowButton.SetActive(true);
        }
    }

    private void OnEnable() 
    {
        announcerCanvasGroup.alpha = 0;
        announcerCanvasGroup.DOFade(1, 1f).SetEase(Ease.Linear);
    }

    public void SetNavigationState(int index)
    {
        NavigationState state = (NavigationState)index;

        if (currentNavigationState == state)
        {
            return;
        }

        currentNavigationState = state;


        switch (state)
        {
            case NavigationState.Kit:
                kitUIRoot.SetActive(true);
                mapUIRoot.SetActive(false);
                controlsUIRoot.SetActive(false);
                optionsUIRoot.SetActive(false);
                announcerText.text = kitAnnouncement;
                announcerSubText.text = "View your Kit";
                activeButton = kitButton;
                break;
            case NavigationState.Map:
                kitUIRoot.SetActive(false);
                mapUIRoot.SetActive(true);
                controlsUIRoot.SetActive(false);
                optionsUIRoot.SetActive(false);
                announcerText.text = mapAnnouncement;
                announcerSubText.text = "Solar System";
                activeButton = mapButton;
                break;
            case NavigationState.Controls:
                kitUIRoot.SetActive(false);
                mapUIRoot.SetActive(false);
                controlsUIRoot.SetActive(true);
                optionsUIRoot.SetActive(false);
                announcerText.text = controlsAnnouncment;
                announcerSubText.text = "Update Input Controls";
                activeButton = controlsButton;
                break;
            case NavigationState.Options:
                kitUIRoot.SetActive(false);
                mapUIRoot.SetActive(false);
                controlsUIRoot.SetActive(false);
                optionsUIRoot.SetActive(true);
                announcerText.text = optionsAnnouncement;
                announcerSubText.text = "Update Game Settings";
                activeButton = optionsButton;
                break;
        }
    }


}
