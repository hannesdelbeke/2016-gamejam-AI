﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelOnClick : MonoBehaviour
{
    public string SceneName;
    public string AlternateButton = "A";

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button == null)
            Debug.LogError("LoadLevelOnClick needs to be attached to a UI button");
        else
        {
            button.onClick.AddListener(OnClick);
        }
    }

    void Update()
    {
        if (Input.GetAxisRaw(AlternateButton + "_1") > 0.5)
            OnClick();
    }

    void OnClick()
    {
        SceneManager.LoadScene(SceneName);
    }
}
