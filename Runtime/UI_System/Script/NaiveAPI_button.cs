using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class NaiveAPI_button : MonoBehaviour
{
    [SerializeField]
    public string ButtonType;

    public NaiveAPI_UI_System UI_System;
    void Awake()
    {
        UI_System = GameObject.Find("UI_Manager").GetComponent<NaiveAPI_UI_System>();
        GetComponent<Button>().onClick.AddListener(onClick);
    }
    private void Update()
    {
    }
    public abstract void localAwake();
    public abstract void onClick();
}
