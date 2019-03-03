using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class AlertPopUP : MonoBehaviour {
    //Object 
    public GameObject PopUpPanel;
    public GameObject PopUpYesOnly;
    public GameObject PopUpYesNo;
    public Text JudulPopUp, JudulYesONly;
    public Text DeskripsiPopUp, DescYesONly;
    public Button btnYES, btnNO, btnYesOnly;
    public UnityAction HideMethod;
    


    //SingleTon
    public static AlertPopUP instance;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
    }

    public void ShowAlert(string judul, string Deskripsi, UnityAction NoMethod, UnityAction YesMethod)
    {
        PopUpPanel.SetActive(true);
        PopUpYesNo.SetActive(true);
        PopUpYesOnly.SetActive(false);
        SetJudulPopUp(judul);
        SetDescPopUp(Deskripsi);

        SetYesMethod(YesMethod);
        SetNoMethod(NoMethod);
    }


    public void ShowAlert(string judul, string Deskripsi)
    {

        PopUpPanel.SetActive(true);
        PopUpYesNo.SetActive(false);
        PopUpYesOnly.SetActive(true);

        SetJudulYesOnlyPopUp(judul);
        SetDescYesOnlyPopUp(Deskripsi);
        btnYES.onClick.RemoveAllListeners();
        btnYES.onClick.AddListener(() =>
        {
            HideAlert();
        }
        );
    }

    public void ShowAlert(string judul, string Deskripsi, UnityAction YesMethod)
    {

        PopUpPanel.SetActive(true);
        PopUpYesNo.SetActive(true);
        PopUpYesOnly.SetActive(false);

        SetJudulPopUp(judul);
        SetDescPopUp(Deskripsi);

        SetYesMethod(YesMethod);
        }


    public void HideAlert()
    {
        PopUpPanel.SetActive(false);
        PopUpYesOnly.SetActive(false);
        PopUpYesNo.SetActive(false);
    }


    public void SetYesOnlyMethod(UnityAction method)
    {
        btnYES.onClick.RemoveAllListeners();
        btnYES.onClick.AddListener(method);
    }

    public void SetYesMethod(UnityAction method)
    {
        btnYES.onClick.RemoveAllListeners();

        btnYES.onClick.AddListener(method);
    }

    public void SetNoMethod(UnityAction method)
    {
        btnYES.onClick.RemoveAllListeners();

        btnNO.onClick.AddListener(method);
    }
    public void SetJudulPopUp(string JudulPopUp)
    {
        this.JudulPopUp.text = JudulPopUp;
    
    }
    public void SetDescPopUp(string DescPopUp)
    {
        this.DeskripsiPopUp.text = DescPopUp;
    }

    public void SetJudulYesOnlyPopUp(string JudulPopUp)
    {
        this.JudulYesONly.text = JudulPopUp;

    }
    public void SetDescYesOnlyPopUp(string DescPopUp)
    {
        this.DescYesONly.text = DescPopUp;
    }
}
