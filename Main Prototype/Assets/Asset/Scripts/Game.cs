using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[System.Serializable]
public class Game {
    //Semua variabel yang akan di simpan di game
    public int countertahap;
    public float DanaTotalProyek;
    public float danaAwal;
    [XmlArray("ListTahap")]
    public List<Tahap> ListTahap = new List<Tahap>();
    public Game()
    {
        //Reset Game
        countertahap = 0;
        DanaTotalProyek = 0;
        danaAwal = 0;
        //Membuat 9 tahap Mulai Dari 0
        for (int i = 0; i < 9; i++)
        {
            Tahap tahap = new Tahap(i);
            ListTahap.Add(tahap);
        }


    }
    public int GetTotalRisikoTerjadi()
    {
        int TotalRisikoTerjadi =0;
        foreach (Tahap tahap in ListTahap)
        {
            TotalRisikoTerjadi += (tahap.ListRisikoTerjadi.Count + tahap.ListRisikoTerpilih.Count);
        }

        return TotalRisikoTerjadi;

    }
    public int CountMMTahap()
    {
        int CountMMTotal = 0;
        foreach (Tahap tahap in ListTahap)
        {
            CountMMTotal += tahap.CountMMTahap();
        }

        return CountMMTotal;

    }
    public int CountK3Tahap()
    {
        int CountMMTotal = 0;
        foreach (Tahap tahap in ListTahap)
        {
            CountMMTotal += tahap.CountK3Tahap();
        }

        return CountMMTotal;

    }
    public int CountWaktuEstimasi()
    {
        int WaktuEstimasiTotal = 0;
        foreach (Tahap tahap in ListTahap)
        {
            WaktuEstimasiTotal += tahap.WaktuEstimasi;
        }

        return WaktuEstimasiTotal;

    }
    public int CountWaktuDurasi()
    {
        int WaktuDurasiTotal = 0;
        foreach (Tahap tahap in ListTahap)
        {
            WaktuDurasiTotal += tahap.WaktuTerjadi;
        }

        return WaktuDurasiTotal;

    }
    public float CountTotalContingency()
    {
        float totalContingency = 0;
        foreach (Tahap tahap in ListTahap)
        {
            totalContingency += tahap.HitungJumlahContingency();
        }
        return totalContingency;
    }
    public float CountTotalRAP()
    {
        float totalRAP = 0;
        foreach (Tahap tahap in ListTahap)
        {
            totalRAP += tahap.RAPTahap;
        }
        return totalRAP;
    }
    //public Game()
    //{
    //    counterTahap = 0;
    //    DanaTotalProyek = 0;
    //    DanaAwal = 0;
    //    Tahap = new Tahap[9];
    //}

}
