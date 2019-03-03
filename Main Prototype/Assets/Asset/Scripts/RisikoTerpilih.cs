using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RisikoTerpilih : Risiko{
    public bool MMkah;
    public bool K3kah;
    public string ResponRisiko;
    public float HargaResponRisiko;
    public RisikoTerpilih()
    {

    }

    public RisikoTerpilih(RisikoObject risikoTerpilih)
    {
        this.NamaRisiko = risikoTerpilih.namaRisiko.text;
        this.IDRisiko = risikoTerpilih.NamaKartu;
        this.DampakWaktu = risikoTerpilih.dampakWaktu;
        this.DampakDana = risikoTerpilih.dampakDana;
        this.Probabilitas = risikoTerpilih.Probabilitas;
        this.TipeRespon = risikoTerpilih.tipeRespon;
        this.Avoid = (int)((risikoTerpilih.HargaAvoid * 150f) / PlayerPrefs.GetFloat("danaAwal"));
        this.Mitigate = (int)((risikoTerpilih.HargaMitigate * 150f) / PlayerPrefs.GetFloat("danaAwal"));
        this.HargaResponRisiko = risikoTerpilih.danaRespon;
        this.MMkah = risikoTerpilih.MMkah;
        this.K3kah = risikoTerpilih.K3Kah;
        this.ResponRisiko = risikoTerpilih.StatusRespon;

    }
}
