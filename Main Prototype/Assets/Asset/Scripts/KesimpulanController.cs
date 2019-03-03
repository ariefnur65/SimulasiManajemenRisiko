using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KesimpulanController : MonoBehaviour
{

    // Use this for initialization
    public RisikoController rc;
    public KartuKesimpulan kk;
    public Text textRisiko, textDana, textHari, textRespon, textDanaRespon, textNamaPekerjaan;
    public Text textContingency, textDurasi, textEstimasi, textTotal, textSisa, textK3, textMM;
    public float dana, contingency, total, sisa, durasi, estimasi, danaRespon;
    public Text RABKesimpulan, RAPKesimpulan, CashFlowKesimpulan;
    //public int hari;
    public Image iRespon;
    public Sprite accept, mitigate, avoid;
    public string respon;
    public GameObject prefabRisiko, panelRisiko, newRisk, risikoTerjadiUtama;
    public Button next, prev;
    public static KesimpulanController instance;
    public int K3, MM;

    //Tambahan 
    public GameObject prefabRisikoTerpilih;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GenerateKesimpulan(List<HappendRisk> listRisikoTerjadi, int tahap)
    {
        //used in UI Manager to summon Kesimpulan dari menu list tahap
        textNamaPekerjaan.text = DataManager.instance.gameInstance.ListTahap[tahap].NamaTahap;
        dana = 0;
        contingency = 0;
        total = 0;
        sisa = 0;
        danaRespon = 0;

        K3 = 0;
        MM = 0;
        if (panelRisiko.transform.childCount > 0) //hancurkan child yang ada di panel dulu
        {
            for (int i = 0; i < panelRisiko.transform.childCount; i++)
            {
                Transform child = panelRisiko.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }
        Debug.Log("Risk Terjadi = " + listRisikoTerjadi.Count);
        //if (riskTerjadi == 0) {
        //	SetText (riskTerjadi, 0); //ini nggak kepake
        //} else {
        //	SetText (0, 0); //ini nggak kepake
        for (int i = 0; i < listRisikoTerjadi.Count; i++)
        {
            newRisk = Instantiate(prefabRisiko, panelRisiko.transform);
            SetPrefabText(listRisikoTerjadi[i]);
        }
        //}
        danaRespon = DataManager.instance.gameInstance.ListTahap[tahap].DanaResponRisiko;
        textDanaRespon.text = danaRespon.ToString("N0", new CultureInfo("id-ID"));
        contingency = DataManager.instance.gameInstance.ListTahap[tahap].DanaTersedia;
        textContingency.text = contingency.ToString("N0", new CultureInfo("id-ID"));
        textTotal.text = total.ToString("N0", new CultureInfo("id-ID"));
        sisa = contingency - danaRespon - total;
        textSisa.text = sisa.ToString("N0", new CultureInfo("id-ID"));
        durasi = DataManager.instance.gameInstance.ListTahap[tahap].WaktuTerjadi;
        textDurasi.text = durasi.ToString();
        estimasi = DataManager.instance.gameInstance.ListTahap[tahap].WaktuEstimasi;
        textEstimasi.text = estimasi.ToString();
        RABKesimpulan.text = DataManager.instance.gameInstance.ListTahap[tahap].RABTahap.ToString("N0", new CultureInfo("id-ID"));
        RAPKesimpulan.text = DataManager.instance.gameInstance.ListTahap[tahap].RAPTahap.ToString("N0", new CultureInfo("id-ID"));
        CashFlowKesimpulan.text = DataManager.instance.gameInstance.ListTahap[tahap].DanaTahap.ToString("N0", new CultureInfo("id-ID"));

        GenerateRisikoTerpilih(tahap);

    }

    public void GenerateKesimpulan(int riskTerjadi)
    {
        textNamaPekerjaan.text = rc.textJudulTahap.text.ToString();
        dana = 0;
        contingency = 0;
        total = 0;
        sisa = 0;
        danaRespon = 0;
        //riskTerjadi -= 1;

        /**if (newRisk != null) {
			Destroy (newRisk.gameObject);
		}

		if (risikoTerjadiUtama != null) {
			Destroy (risikoTerjadiUtama.gameObject);
		}*/
        if (panelRisiko.transform.childCount > 0)
        {
            for (int i = 0; i < panelRisiko.transform.childCount; i++)
            {
                Transform child = panelRisiko.transform.GetChild(i);
                //child.parent = null;
                Destroy(child.gameObject);
            }
        }
        Debug.Log("Risk Terjadi = " + riskTerjadi);
        //if (riskTerjadi == 0) {
        //	SetText (riskTerjadi, 0); //ini nggak kepake
        //} else {
        //	SetText (0, 0); //ini nggak kepake
        for (int i = 1; i <= riskTerjadi; i++)
        {
            newRisk = Instantiate(prefabRisiko, panelRisiko.transform);
            SetPrefabText(rc.angkaRisiko[i - 1], i - 1);
        }
        //}
        danaRespon = rc.totalDana;
        textDanaRespon.text = danaRespon.ToString("N0", new CultureInfo("id-ID"));
        contingency = rc.danaTersedia;
        textContingency.text = contingency.ToString("N0", new CultureInfo("id-ID"));
        textTotal.text = total.ToString("N0", new CultureInfo("id-ID"));
        sisa = contingency - danaRespon - total;
        textSisa.text = sisa.ToString("N0", new CultureInfo("id-ID"));
        durasi = rc.estimasi;
        textDurasi.text = durasi.ToString();
        estimasi = durasi - rc.DampakHariGame;
        textEstimasi.text = estimasi.ToString();
        GenerateRisikoTerpilih();
        textK3.text = K3.ToString();
        textMM.text = MM.ToString();
    }


    public void GenerateRisikoTerpilih(int tahap)
    {
        List<RisikoTerpilih> ListRisikoTerpilih = DataManager.instance.gameInstance.ListTahap[tahap].ListRisikoTerpilih;

        K3 = 0;
        MM = 0;
        foreach (RisikoTerpilih risiko in ListRisikoTerpilih)
        {

            dana = 0;
            newRisk = Instantiate(prefabRisikoTerpilih, panelRisiko.transform);
            textRisiko = newRisk.transform.Find("Check Risiko").GetComponentInChildren<Text>();
            textRisiko.text = risiko.NamaRisiko;
            textHari = newRisk.transform.Find("Dampak Hari").GetComponent<Text>();
            textHari.text = "-";
            respon = risiko.ResponRisiko;
            textRespon = newRisk.transform.Find("Respon").GetComponentInChildren<Text>();
            iRespon = newRisk.transform.Find("Respon").GetComponent<Image>();

            if (respon != "")
            {
                textRespon.text = respon.Substring(0, 1).ToUpper() + respon.Substring(1);
            }
            if (respon == "accept")
            {
                iRespon.enabled = true;
                iRespon.sprite = accept;
            }
            else if (respon == "mitigate")
            {
                iRespon.enabled = true;
                iRespon.sprite = mitigate;
            }
            else if (respon == "avoid")
            {
                iRespon.enabled = true;
                iRespon.sprite = avoid;
            }

            if (risiko.K3kah)
            {
                K3++;
                Debug.Log("Risiko k3 ini :"+ K3);
            }
            if (risiko.MMkah)
            {
                MM++;
                Debug.Log("Risiko MM ini :"+MM);
            }
            dana = risiko.HargaResponRisiko;


            total += dana;
            textDana = newRisk.transform.Find("Dana Risiko").GetComponent<Text>();
            textDana.text = dana.ToString("N0", new CultureInfo("id-ID"));
            kk = newRisk.GetComponent<KartuKesimpulan>();
            kk.rc = rc;
            kk.next = next;
            kk.prev = prev;
            kk.namaKartu = risiko.IDRisiko;
            kk.hargaAvoid = risiko.Avoid;
            kk.risikoTerpilih = risiko;
            kk.hargaAvoid = (risiko.Avoid / 150f) * PlayerPrefs.GetFloat("danaAwal");

            kk.hargaMitigate = (risiko.Mitigate / 150f) * PlayerPrefs.GetFloat("danaAwal");
            kk.happendKah = false;

        }
        textK3.text = K3.ToString();
        textMM.text = MM.ToString();

    }

    public void GenerateRisikoTerpilih()
    {
        rc.CekRisikoTerpilih();
        Debug.Log("Risk Terpilih = " + rc.listRisikoTerpilih.Count);
        K3 = 0;
        MM = 0;
        foreach (RisikoObject risiko in rc.listRisikoTerpilih)
        {
            if (risiko.btnRisikoOn.isOn)
            {
                dana = 0;
                newRisk = Instantiate(prefabRisikoTerpilih, panelRisiko.transform);
                textRisiko = newRisk.transform.Find("Check Risiko").GetComponentInChildren<Text>();
                textRisiko.text = risiko.namaRisiko.text.ToString();
                textHari = newRisk.transform.Find("Dampak Hari").GetComponent<Text>();
                textHari.text = "-";
                respon = risiko.StatusRespon;
                textRespon = newRisk.transform.Find("Respon").GetComponentInChildren<Text>();
                iRespon = newRisk.transform.Find("Respon").GetComponent<Image>();

                if (respon != "")
                {
                    textRespon.text = respon.Substring(0, 1).ToUpper() + respon.Substring(1);
                }
                if (respon == "accept")
                {
                    iRespon.enabled = true;
                    iRespon.sprite = accept;
                }
                else if (respon == "mitigate")
                {
                    iRespon.enabled = true;
                    iRespon.sprite = mitigate;
                }
                else if (respon == "avoid")
                {
                    iRespon.enabled = true;
                    iRespon.sprite = avoid;
                }

                if (risiko.K3Kah)
                {
                    K3++;
                }
                if (risiko.MMkah)
                {
                    MM++;
                }
                //dana = risiko.danaRespon;
                if (dana <= 0)
                {
                    dana = 0;
                }
                dana = risiko.danaRespon;

                total += dana;
                textDana = newRisk.transform.Find("Dana Risiko").GetComponent<Text>();
                textDana.text = dana.ToString("N0", new CultureInfo("id-ID"));
                kk = newRisk.GetComponent<KartuKesimpulan>();
                kk.rc = rc;
                kk.next = next;
                kk.prev = prev;
                kk.namaKartu = risiko.NamaKartu;
                kk.hargaAvoid = risiko.HargaAvoid;
                kk.hargaMitigate = risiko.HargaMitigate;
                kk.objectMuncul = risiko;
                kk.risikoTerpilih = new RisikoTerpilih(risiko);
                kk.happendKah = false;
            }
        }
    }

    public void SetPrefabText(HappendRisk risikoTerjadi)
    {
        //digunakan jika menu kesimpulan di panggil dari menu list pekerjaan

        textRisiko = newRisk.transform.Find("Check Risiko").GetComponentInChildren<Text>();
        textRisiko.text = risikoTerjadi.NamaRisiko;

        textHari = newRisk.transform.Find("Dampak Hari").GetComponent<Text>();
        textHari.text = risikoTerjadi.DampakWaktuTerjadi.ToString();

        respon = risikoTerjadi.ResponRisiko.ToLower();
        textRespon = newRisk.transform.Find("Respon").GetComponentInChildren<Text>();
        iRespon = newRisk.transform.Find("Respon").GetComponent<Image>();
        dana = risikoTerjadi.DampakDanaTerjadi;

        if (dana <= 0)
        {
            dana = 0;
        }
        if (respon != "")
        { textRespon.text = respon.Substring(0, 1).ToUpper() + respon.Substring(1); }
        if (respon == "accept")
        {
            iRespon.enabled = true;
            iRespon.sprite = accept;

        }
        else if (respon == "mitigate")
        {
            iRespon.enabled = true;
            iRespon.sprite = mitigate;
        }
        else if (respon == "avoid")
        {
            iRespon.enabled = true;
            iRespon.sprite = avoid;
        }
        else
        {
            iRespon.enabled = false;
            textRespon.text = "Tidak Direspon";
        }


        total += dana;
        textDana = newRisk.transform.Find("Dana Risiko").GetComponent<Text>();
        textDana.text = dana.ToString("N0", new CultureInfo("id-ID"));
        kk = newRisk.GetComponent<KartuKesimpulan>();
        kk.rc = rc;
        kk.next = next;
        kk.prev = prev;
        kk.namaKartu = risikoTerjadi.IDRisiko;
        kk.hargaAvoid = (risikoTerjadi.Avoid / 150f) * PlayerPrefs.GetFloat("danaAwal");
        kk.hargaMitigate = (risikoTerjadi.Mitigate / 150f) * PlayerPrefs.GetFloat("danaAwal");
        kk.risikoTerjadi = risikoTerjadi;
        kk.happendKah = true;

        //Debug.Log("Risiko " + (i+1) + " adalah " + rc.objectRisiko[i].namaRisiko.text + " dan terjadi pada " + rc.waktuRisikoTerjadi[x] + "%");

    }


    public void SetPrefabText(int i, int x)
    {
        textRisiko = newRisk.transform.Find("Check Risiko").GetComponentInChildren<Text>();
        textRisiko.text = rc.objectRisiko[i].namaRisiko.text.ToString();

        //textHari= GameObject.Find("haririsiko").GetComponentInChildren<Text>();
        //estimasi += rc.DampakWaktu [x];
        //estimasi = durasi - estimasi;
        //textEstimasi.text = estimasi.ToString ();
        textHari = newRisk.transform.Find("Dampak Hari").GetComponent<Text>();
        textHari.text = rc.DampakWaktu[x].ToString();

        respon = rc.objectRisiko[i].StatusRespon;
        textRespon = newRisk.transform.Find("Respon").GetComponentInChildren<Text>();
        iRespon = newRisk.transform.Find("Respon").GetComponent<Image>();
        if (respon != "")
        { textRespon.text = respon.Substring(0, 1).ToUpper() + respon.Substring(1); }
        if (respon == "accept")
        {
            iRespon.enabled = true;
            iRespon.sprite = accept;
            /**if (rc.objectRisiko[i].HargaAvoid != 0)
			{
				dana = rc.objectRisiko[i].HargaAvoid;
			}
			else
			{
				dana = rc.objectRisiko[i].HargaMitigate * 2;
			}*/
            dana = rc.objectRisiko[i].dampakDana;
        }
        else if (respon == "mitigate")
        {
            iRespon.enabled = true;
            iRespon.sprite = mitigate;
            //dana = rc.objectRisiko [i].HargaMitigate;
            dana = rc.objectRisiko[i].dampakDana - rc.objectRisiko[i].HargaMitigate;
            if (dana <= 0)
            {
                dana = 0;
            }
        }
        else if (respon == "avoid")
        {
            iRespon.enabled = true;
            iRespon.sprite = avoid;
            dana = 0;
            if (dana <= 0)
            {
                dana = 0;
            }
        }
        else
        {
            iRespon.enabled = false;
            textRespon.text = "Tidak Direspon";
            /**if (rc.objectRisiko[i].HargaAvoid != 0)
			{
				dana = rc.objectRisiko[i].HargaAvoid;
			}
			else
			{
				dana = rc.objectRisiko[i].HargaMitigate * 2;
			}*/
            dana = rc.objectRisiko[i].dampakDana;
        }
        total += dana;
        textDana = newRisk.transform.Find("Dana Risiko").GetComponent<Text>();
        textDana.text = dana.ToString("N0", new CultureInfo("id-ID"));
        kk = newRisk.GetComponent<KartuKesimpulan>();
        kk.rc = rc;
        kk.next = next;
        kk.prev = prev;
        kk.namaKartu = rc.objectRisiko[i].NamaKartu;
        kk.hargaAvoid = rc.objectRisiko[i].HargaAvoid;
        kk.hargaMitigate = rc.objectRisiko[i].HargaMitigate;
        kk.objectMuncul = rc.objectRisiko[i];
        kk.happendKah = false;

        //Debug.Log("Risiko " + (i+1) + " adalah " + rc.objectRisiko[i].namaRisiko.text + " dan terjadi pada " + rc.waktuRisikoTerjadi[x] + "%");

    }


    //Reset object Risiko that was created and risk data that was recorded

}
