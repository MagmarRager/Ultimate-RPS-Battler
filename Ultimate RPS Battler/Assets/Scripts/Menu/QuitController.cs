using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitController : MonoBehaviour
{
    public GameObject QuitPanel;

    private void Start()
    {
        QuitPanel.SetActive(false);
    }
    public void ToggleQuitPanel()
    {
        if (QuitPanel.activeSelf == false)
            QuitPanel.SetActive(true);
        else
            QuitPanel.SetActive(false);
    }





}
