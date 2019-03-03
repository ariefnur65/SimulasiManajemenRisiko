using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tahap {
    public string NamaTahap;
    public float DanaTahap;
    public float RABTahap;
    public float RAPTahap;
    public int WaktuEstimasi;
    public int WaktuTerjadi;
    public float DanaTersedia;
    public float DanaResponRisiko;
    public float DanaPerbaikan;
    public float DanaSisaContingency;
    public float DanaTersisa;

    public List<Risiko> ArrayofRisiko = new List<Risiko>();
    public List<HappendRisk> ListRisikoTerjadi = new List<HappendRisk>();
    public List<RisikoTerpilih> ListRisikoTerpilih = new List<RisikoTerpilih>();
    public List<OverviewRisk> ListRisikoOverview = new List<OverviewRisk>();

    public Tahap() { }
    public Tahap(int NomorTahap)
    {
        switch (NomorTahap)
        {
            case 0:
                NamaTahap = "Bore Pile";
                WaktuEstimasi = 27;
                for (int i = 1; i < 11; i++)
                {
                    Risiko tahap = new Risiko("T"+(NomorTahap+1)+"R"+i);
                    ArrayofRisiko.Add(tahap);
                }
                break;
            case 1:
                NamaTahap = "Footing Abutmen";
                WaktuEstimasi = 41;
                for (int i = 1; i < 11; i++)
                {
                    Risiko tahap = new Risiko("T" + (NomorTahap + 1) + "R" + i);
                    ArrayofRisiko.Add(tahap);
                }
                break;
            case 2:
                NamaTahap = "Footing Pilar";
                WaktuEstimasi = 35;
                for (int i = 1; i < 11; i++)
                {
                    Risiko tahap = new Risiko("T" + (NomorTahap + 1) + "R" + i);
                    ArrayofRisiko.Add(tahap);
                }
                break;
            case 3:
                NamaTahap = "Wall Abutmen";
                WaktuEstimasi = 26;
                for (int i = 1; i < 11; i++)
                {
                    Risiko tahap = new Risiko("T" + (NomorTahap + 1) + "R" + i);
                    ArrayofRisiko.Add(tahap);
                }
                break;
            case 4:
                NamaTahap = "Pilar";
                WaktuEstimasi = 27;
                for (int i = 1; i < 11; i++)
                {
                    Risiko tahap = new Risiko("T" + (NomorTahap + 1) + "R" + i);
                    ArrayofRisiko.Add(tahap);
                }
                break;
            case 5:
                NamaTahap = "Pilar Head";
                WaktuEstimasi = 23;
                for (int i = 1; i < 11; i++)
                {
                    Risiko tahap = new Risiko("T" + (NomorTahap + 1) + "R" + i);
                    ArrayofRisiko.Add(tahap);
                }
                break;
            case 6:
                NamaTahap = "Erection Girder";
                WaktuEstimasi = 26;
                for (int i = 1; i < 11; i++)
                {
                    Risiko tahap = new Risiko("T" + (NomorTahap + 1) + "R" + i);
                    ArrayofRisiko.Add(tahap);
                }
                break;
            case 7:
                NamaTahap = "Plat Jembatan";
                WaktuEstimasi = 15;
                for (int i = 1; i < 11; i++)
                {
                    Risiko tahap = new Risiko("T" + (NomorTahap + 1) + "R" + i);
                    ArrayofRisiko.Add(tahap);
                }
                break;
            case 8:
                NamaTahap = "Finishing";
                WaktuEstimasi = 12;
                for (int i = 1; i < 11; i++)
                {
                    Risiko tahap = new Risiko("T" + (NomorTahap + 1) + "R" + i);
                    ArrayofRisiko.Add(tahap); 
                }
                break;
        }
    }

    public float HitungJumlahContingency()
    { float JumlahContingency = 0;
        JumlahContingency = DanaPerbaikan + DanaResponRisiko;
        return JumlahContingency;
    }

    public int CountMMTahap()
    {
        int CountMM = 0;
        foreach (RisikoTerpilih Rt in ListRisikoTerpilih)
        {
            if (Rt.MMkah)
                CountMM++;
        }

        return CountMM;

    }



    public int CountK3Tahap()
    {
        int CountK3 = 0;
        foreach (RisikoTerpilih Rt in ListRisikoTerpilih)
        {
            if (Rt.K3kah)
                CountK3++;
        }

        return CountK3;

    }

    //public Tahap() 
    //{
    //    NamaTahap = "";
    //    DanaTahap = 0f;
    //    RABTahap = 0f;
    //    RAPTahap = 0f;
    //    DanaTersedia = 0f;
    //    DanaResponRisiko = 0f;
    //    DanaPerbaikan = 0f;
    //    DanaSisaContingency = 0f;
    //    DanaTersisa = 0f;

    //    ArrayofRisiko = new Risiko[10];
    //}
}
