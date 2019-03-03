using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TahapPekerjaan : MonoBehaviour {
    public Text NamaTahap;
    public Text CountRisikoTerjadi;
    public Text CountMM;
    public Text CountK3;
    public Text RAB, RAP, Contingency, Sisa;
    public Text EstWaktu;
    public Text DurasiWaktu;

    public void IsiKesimpulanTahap(Tahap tahap)
    {
        NamaTahap.text = tahap.NamaTahap;
        CountRisikoTerjadi.text = tahap.ListRisikoTerjadi.Count.ToString();
        CountMM.text = tahap.CountMMTahap().ToString();
        CountK3.text = tahap.CountK3Tahap().ToString();
        RAB.text = tahap.RABTahap.ToString("N0", new CultureInfo("id-ID"));
        RAP.text = tahap.RAPTahap.ToString("N0", new CultureInfo("id-ID"));
        Contingency.text = tahap.HitungJumlahContingency().ToString("N0", new CultureInfo("id-ID"));
        Sisa.text = tahap.DanaSisaContingency.ToString("N0", new CultureInfo("id-ID"));

        EstWaktu.text = tahap.WaktuEstimasi.ToString();
        DurasiWaktu.text = tahap.WaktuTerjadi.ToString();
    }

}
