using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class OverviewRisk : RisikoTerpilih{
    public bool terpilih;
    public OverviewRisk() { }
    public OverviewRisk(RisikoObject risikoOverview)
    {
        this.terpilih = risikoOverview.btnRisikoOn.isOn;
        this.NamaRisiko = risikoOverview.namaRisiko.text;
        this.IDRisiko = risikoOverview.NamaKartu;
        this.DampakWaktu = risikoOverview.dampakWaktu;
        this.DampakDana = risikoOverview.dampakDana;
        this.Probabilitas = risikoOverview.Probabilitas;
        this.TipeRespon = risikoOverview.tipeRespon;
        this.Avoid = (int)((risikoOverview.HargaAvoid * 150f) / PlayerPrefs.GetFloat("danaAwal"));
        this.Mitigate = (int)((risikoOverview.HargaMitigate * 150f) / PlayerPrefs.GetFloat("danaAwal"));
        this.HargaResponRisiko = risikoOverview.danaRespon;
        this.MMkah = risikoOverview.MMkah;
        this.K3kah = risikoOverview.K3Kah;
        this.ResponRisiko = risikoOverview.StatusRespon;

    }
}
