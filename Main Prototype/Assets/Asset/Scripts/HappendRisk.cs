using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HappendRisk : Risiko{
    public float DampakDanaTerjadi;
    public float DampakWaktuTerjadi;
    public bool MMKah;
    public bool K3Kah;
    public string ResponRisiko;

    public HappendRisk()
    {
    
    }

    public HappendRisk(RisikoObject risikoterjadi, float ListDampakDana, float DampakWaktu, bool MMkah, bool K3kah)
    {
        this.NamaRisiko = risikoterjadi.namaRisiko.text;
        this.IDRisiko = risikoterjadi.NamaKartu;
        this.DampakWaktu = risikoterjadi.dampakWaktu;
        this.DampakDana = risikoterjadi.dampakDana;
        this.Probabilitas = risikoterjadi.Probabilitas;
        this.TipeRespon = risikoterjadi.tipeRespon;
        this.Avoid = (int) ((risikoterjadi.HargaAvoid * 150f) / PlayerPrefs.GetFloat("danaAwal"));
        this.Mitigate = (int)((risikoterjadi.HargaMitigate * 150f) / PlayerPrefs.GetFloat("danaAwal"));
        this.DampakDanaTerjadi = ListDampakDana;
        this.DampakWaktuTerjadi = DampakWaktu;
        this.MMKah = MMkah;
        this.K3Kah = K3kah;
        this.ResponRisiko = risikoterjadi.StatusRespon;

    }



}
