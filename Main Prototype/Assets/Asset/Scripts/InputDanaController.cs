using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class InputDanaController : MonoBehaviour
{

    public float RABProyek, RAPProyek, KeuntunganProyek, sisaDanaProyek;
    public float RABPekerjaan, RAPPekerjaan, KeuntunganPekerjaan;
    public Text RAPProyekText, KeuntunganProyekText, sisaDanaProyekText;
    public Text RAPPekerjaanText, KeuntunganPekerjaanText;
    public InputField RABProyekInput, RABPekerjaanInput;
    public RisikoController rc;
    public Button LanjutProyek, LanjutPekerjaan;
    public static InputDanaController instance;

    //Revisi 20 Juli
    public Text CashFlow;
    public Text RABKesimpulan;
    public Text RAPKesimpulan;
    public Text CashFlowOverview, RABOverview, RAPOverview;

    //Tambahan UI Floating Text
    public GameObject FloatingRAB;
    public Text TextFloatingRAB;


    public GameObject FloatingRABPekerjaan;
    public Text TextFloatingRABPekerjaan;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        CheckRABProyek();
    }

    public void LockInputProyek()
    {
        RABProyekInput.interactable = false;

    }
    public void CheckInputPekerjaan()
    {
        if (RABPekerjaanInput.text == "")
        {
            RABPekerjaan = 0;
            RABPekerjaanInput.text = "0";
        }
        FloatingRABPekerjaan.SetActive(false);

    }
    public void UnLockInputProyek()
    {
        RABProyekInput.interactable = true;
        RABProyekInput.text = "0";
        RABProyek = 0;
    }
    public void CheckRABProyek()
    {
        if (PlayerPrefs.HasKey("danaAwal") && PlayerPrefs.GetFloat("danaAwal") > 0)
        { //Pernah mengisi sebelumnya
            KeuntunganProyek = PlayerPrefs.GetFloat("danaAwal");
            KeuntunganProyekText.text = KeuntunganProyek.ToString("N0", new CultureInfo("id-ID"));
            LockInputProyek();
            RABProyek = PlayerPrefs.GetFloat("DanaTotalProyek");
            RABProyekInput.text = RABProyek.ToString("N0");
            if (RABProyek == 10000000000)
            {
                RAPProyekText.text = "9.000.000.000";
            }
            else
            {
                RAPProyekText.text = RAPProyek.ToString("N0", new CultureInfo("id-ID"));

            }
            RAPProyek = 0.9f * RABProyek;
        }
        if (RABProyek < 1000000000)
        {
            LanjutProyek.interactable = false;

        }
        else
        {
            LanjutProyek.interactable = true;
        }
        if (RABPekerjaan < 100000000)
        {
            LanjutPekerjaan.interactable = false;
        }
        else
        {
            LanjutPekerjaan.interactable = true;
        }
        if (RABProyekInput.interactable == false)
        {
            FloatingRAB.SetActive(false);


        }
        

    }
    public void LateUpdate()
    {
        //sisaDana = rc.danaTersedia;
        //sisaDanaText.text = sisaDana.ToString ("N0", new CultureInfo ("id-ID"));
        //if (RABProyek < 1000000000)
        //{
        //    LanjutProyek.interactable = false;

        //}
        //else
        //{
        //    LanjutProyek.interactable = true;
        //}
        //if (RABPekerjaan < 100000000)
        //{
        //    LanjutPekerjaan.interactable = false;
        //}
        //else
        //{
        //    LanjutPekerjaan.interactable = true;
        //}
        //if (RABProyekInput.interactable == false)
        //{
        //    FloatingRAB.SetActive(false);


        //}



        /**if (sisaDana != 0 && RAB > sisaDana) {
			RABInput.interactable = false;
			RAB = sisaDana;
			RABInput.text = RAB.ToString ();
			RAP = 0.9f * RAB;
			Keuntungan = 0.1f * RAB;
			RAPText.text = RAP.ToString ("N0", new CultureInfo ("id-ID"));
			KeuntunganText.text = Keuntungan.ToString ("N0", new CultureInfo ("id-ID"));
		}*/
    }

    public void InputProyek()
    {
        if (RABProyekInput.text == "")
        {
            RABProyek = 0;

        }
        else
        {
            LanjutProyek.interactable = true;
            RABProyek = float.Parse(RABProyekInput.text);


        }

        

        if (RABProyek < 1000000000 && RABProyek != 0)
        {
            FloatingRAB.SetActive(true);
            FloatingRAB.GetComponent<Image>().color = Color.red;
            TextFloatingRAB.color = Color.white;
            TextFloatingRAB.text = "RAB : Rp. " + RABProyek.ToString("N0", new CultureInfo("id-ID")) + " \nMinimal: Rp. 1.000.000.000";
            LanjutProyek.interactable = false;

        }
        else if (RABProyekInput.interactable == true && RABProyek >= 1000000000)
        {
            FloatingRAB.SetActive(true);

            FloatingRAB.GetComponent<Image>().color = Color.white;
            TextFloatingRAB.color = Color.black;
            TextFloatingRAB.text = "RAB: Rp. " + RABProyek.ToString("N0", new CultureInfo("id-ID"));
        }
        if (RABProyek >= 10000000000)
        {
            //nilai maks
            RABProyek = 10000000000;
            RABProyekInput.text = "10000000000";
            FloatingRAB.SetActive(true);
            FloatingRAB.GetComponent<Image>().color = Color.black;
            TextFloatingRAB.color = Color.yellow;
            TextFloatingRAB.text = "RAB : Rp. " + RABProyek.ToString("N0", new CultureInfo("id-ID")) + " \nMaksimal: Rp. 10.000.000.000";
        }

        KeuntunganProyek = (float)0.1d * RABProyek;
        RAPProyek = (float) 0.9d * RABProyek ;

        if (RABProyek == 10000000000)
        {
            RAPProyekText.text = "9.000.000.000";
        }
        else
        {
            RAPProyekText.text = RAPProyek.ToString("N0", new CultureInfo("id-ID"));

        }
        KeuntunganProyekText.text = KeuntunganProyek.ToString("N0", new CultureInfo("id-ID"));
    }

    public void InputPekerjaan()
    {

        if (RABPekerjaanInput.text == "")
        {
            RABPekerjaan = 0;

        }
        else
        {
            RABPekerjaan = float.Parse(RABPekerjaanInput.text);

        }


        if (RABPekerjaan < 100000000 && RABPekerjaan != 0)
        {
            FloatingRABPekerjaan.SetActive(true);
            FloatingRABPekerjaan.GetComponent<Image>().color = Color.red;
            TextFloatingRABPekerjaan.color = Color.white;
            TextFloatingRABPekerjaan.text = "RAB : Rp. " + RABPekerjaan.ToString("N0", new CultureInfo("id-ID")) + " \nMinimal: Rp. 100.000.000";
            LanjutPekerjaan.interactable = false;

        }
        else if (RABPekerjaanInput.interactable == true && RABPekerjaan >= 100000000 && RABPekerjaan < PlayerPrefs.GetFloat("DanaTotalProyek") * 0.2)
        {
            FloatingRABPekerjaan.SetActive(true);

            FloatingRABPekerjaan.GetComponent<Image>().color = Color.white;
            TextFloatingRABPekerjaan.color = Color.black;
            TextFloatingRABPekerjaan.text = "RAB: Rp. " + RABPekerjaan.ToString("N0", new CultureInfo("id-ID"));
        }

        if (RABPekerjaan >= PlayerPrefs.GetFloat("DanaTotalProyek") * 0.2 && RABPekerjaan < RABProyek)
        {
            //nilai maks
            FloatingRABPekerjaan.SetActive(true);
            FloatingRABPekerjaan.GetComponent<Image>().color = Color.yellow;
            TextFloatingRABPekerjaan.color = Color.black;
            TextFloatingRABPekerjaan.text = "RAB : Rp. " + RABPekerjaan.ToString("N0", new CultureInfo("id-ID")) + " \nTerlalu besar";
        }
        if (RABPekerjaan >= sisaDanaProyek)
        {
            //nilai maks
            RABPekerjaan = sisaDanaProyek;
            FloatingRABPekerjaan.SetActive(true);
            RABPekerjaanInput.text = string.Format("{0:F0}", RABPekerjaan);

            FloatingRABPekerjaan.GetComponent<Image>().color = Color.black;
            TextFloatingRABPekerjaan.color = Color.yellow;
            TextFloatingRABPekerjaan.text = "RAB : Rp. " + RABPekerjaan.ToString("N0", new CultureInfo("id-ID")) + " \nMaksimal: Rp. "+ sisaDanaProyek.ToString("N0", new CultureInfo("id-ID"));
        }

        
        RAPPekerjaan = 0.9f * RABPekerjaan;
        KeuntunganPekerjaan = 0.1f * RABPekerjaan;
        RAPPekerjaanText.text = RAPPekerjaan.ToString("N0", new CultureInfo("id-ID"));
        KeuntunganPekerjaanText.text = KeuntunganPekerjaan.ToString("N0", new CultureInfo("id-ID"));


    }

    public void EndInputProyek()
    {
        //Write dana Awal pada data Manager
        DataManager.instance.WriteDanaAwal(KeuntunganProyek);
        DataManager.instance.WriteDanaTotalProyek(RABProyek);
        PlayerPrefs.SetFloat("danaAwal", KeuntunganProyek);
        PlayerPrefs.SetFloat("DanaTotalProyek", RABProyek);
        FloatingRAB.SetActive(false);

        if (RABProyek < 1000000000)
        {
            LanjutProyek.interactable = false;

        }
        else
        {
            LanjutProyek.interactable = true;
        }

        //		sisaDanaProyek = RABProyek
        //		sisaDanaProyekText.text = sisaDanaProyek.ToString("N0", new CultureInfo("id-ID"));
    }

    public void EndInputPekerjaan()
    {
        if (RABPekerjaanInput.text == "")
        {
            RABPekerjaan = 0;
            RABPekerjaanInput.text = "0";
        }
        else
        {
            RABPekerjaan = float.Parse(RABPekerjaanInput.text);

        }
        if (RABPekerjaan > sisaDanaProyek)
        {
            //Jika Input Lebih besar dari sisaDanaProyek
            Debug.Log("RAB > sisa dana proyek");
            Debug.Log(sisaDanaProyek);
            RABPekerjaan = sisaDanaProyek;
            RABPekerjaanInput.text = RABPekerjaan.ToString("N0", new CultureInfo("id-ID"));
            RAPPekerjaan = 0.9f * RABPekerjaan;
            KeuntunganPekerjaan = 0.1f * RABPekerjaan;
            RAPPekerjaanText.text = RAPPekerjaan.ToString("N0", new CultureInfo("id-ID"));
            KeuntunganPekerjaanText.text = KeuntunganPekerjaan.ToString("N0", new CultureInfo("id-ID"));

        }
        else
        {
            Debug.Log("RAB < sisa dana proyek");
        }
        if (RABPekerjaan < 100000000)
        {
            LanjutPekerjaan.interactable = false;
        }
        else
        {
            LanjutPekerjaan.interactable = true;
        }
        if (RABPekerjaan >= sisaDanaProyek)
        {
            //nilai maks
            RABPekerjaan = sisaDanaProyek;
            FloatingRABPekerjaan.SetActive(true);
            RABPekerjaanInput.enabled = false;


            RABPekerjaanInput.text = string.Format("{0:F0}", RABPekerjaan);
            RABPekerjaanInput.enabled = true;

            FloatingRABPekerjaan.GetComponent<Image>().color = Color.black;
            TextFloatingRABPekerjaan.color = Color.yellow;
            TextFloatingRABPekerjaan.text = "RAB : Rp. " + RABPekerjaan.ToString("N0", new CultureInfo("id-ID")) + " \nMaksimal: Rp. " + sisaDanaProyek.ToString("N0", new CultureInfo("id-ID"));
        }
        else
        {
            FloatingRABPekerjaan.SetActive(false);

        }

        //Kesimpulan
        RAPKesimpulan.text = RAPPekerjaanText.text;
        RABKesimpulan.text = RABPekerjaan.ToString("N0", new CultureInfo("id-ID"));
        CashFlow.text = sisaDanaProyek.ToString("N0", new CultureInfo("id-ID"));
        //Overview
        CashFlowOverview.text = sisaDanaProyek.ToString("N0", new CultureInfo("id-ID"));
        RAPOverview.text = RAPPekerjaanText.text;
        RABOverview.text = RABPekerjaan.ToString("N0", new CultureInfo("id-ID"));

    }

    //RAB Proyek Update

    public void GoAnalisis()
    {
        //		dieksekus ketika tombol lanjut menu info tahap pekerjaan di klik
        DataManager.instance.WriteDanaRAPRABPekerjaan(RABPekerjaan, RAPPekerjaan);
        DataManager.instance.WriteDanaTersediaPekerjaan(KeuntunganPekerjaan);
        //Kesimpulan
        RAPKesimpulan.text = RAPPekerjaanText.text;
        RABKesimpulan.text = RABPekerjaan.ToString("N0", new CultureInfo("id-ID"));
        CashFlow.text = sisaDanaProyek.ToString("N0", new CultureInfo("id-ID"));
        //Overview
        CashFlowOverview.text = sisaDanaProyek.ToString("N0", new CultureInfo("id-ID"));
        RAPOverview.text = RAPPekerjaanText.text;
        RABOverview.text = RABPekerjaan.ToString("N0", new CultureInfo("id-ID"));

        sisaDanaProyek -= RABPekerjaan;
        DataManager.instance.WriteDanaSisaProyek(sisaDanaProyek);
        //		DataManager.instance.WriteRisikoTerjadi();
        rc.SetDanaTersedia(KeuntunganPekerjaan);
    }

    public void BackAnalisis()
    {//Dieksekusi ketika tombol kembali di analisis di pencet
        sisaDanaProyek = DataManager.instance.GetDanaTahapPekerjaan();
        sisaDanaProyekText.text = sisaDanaProyek.ToString("N0", new CultureInfo("id-ID"));
    }

}
