using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Risiko
{
    public string NamaRisiko;
    public string IDRisiko;
    public string DampakWaktu;
    public float DampakDana;
    public float Probabilitas;
    public string TipeRespon;
    public int Avoid;
    public int Mitigate;

    public Risiko() { }
    public Risiko(string IDRisiko)
    {
        switch (IDRisiko)
        {
            #region Tahap 1
            case "T1R1":
                NamaRisiko = "Hasil daya dukung tiang yang tidak memenuhi syarat";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 8;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T1R2":
                NamaRisiko = "Terganggunya sistem dewatering";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 10f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T1R3":
                NamaRisiko = "Kesulitan pengeboran akibat kondisi tanah";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 5.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T1R4":
                NamaRisiko = "Terjadinya kesalahan penembakan titik pengeboran";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T1R5":
                NamaRisiko = "Keruntuhan tanah permukaan di sekeliling lubang bor";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T1R6":
                NamaRisiko = "Tingginya muka air tanah";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 2;
                Mitigate = 1;
                break;
            case "T1R7":
                NamaRisiko = "Kenaikan harga material";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 18.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T1R8":
                NamaRisiko = "Kerusakan peralatan mesin";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 10.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T1R9":
                NamaRisiko = "Penurunan pondasi berlebihan";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 5.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T1R10":
                NamaRisiko = "Terjadinya perubahan atau review desain untuk konstruksi yang belum terbangun";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 5.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;

            #endregion
            #region Tahap 2
            case "T2R1":
                NamaRisiko = "Rendahnya Produktivitas Peralatan";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 8;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T2R2":
                NamaRisiko = "Kesulitan penggalian akibat batu / tanah keras";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 4f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T2R3":
                NamaRisiko = "Kegagalan penanganan pengecoran akibat volume beton besar";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 2;
                Mitigate = 1;
                break;
            case "T2R4":
                NamaRisiko = "Kesalahan kedalaman penggalian tanah";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T2R5":
                NamaRisiko = "Spesifikasi material tidak sesuai";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 8.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T2R6":
                NamaRisiko = "Produktivitas tenaga kerja yang rendah";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 5.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T2R7":
                NamaRisiko = "Pencurian material";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 16f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T2R8":
                NamaRisiko = "Kondisi cuaca ekstrem";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 16.0f;
                Probabilitas = 0.7f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T2R9":
                NamaRisiko = "Public Objection (Protes warga, Penutupan jalan)";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 20.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T2R10":
                NamaRisiko = "Keterbatasan peralatan dan sumber daya material";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;

            #endregion
            #region Tahap 3
            case "T3R1":
                NamaRisiko = "Terganggunya sistem dewatering";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 10;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T3R2":
                NamaRisiko = "Kesulitan penggalian akibat batu / tanah keras";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 4f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T3R3":
                NamaRisiko = "Lokasi pekerjaan terkena banjir";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 5f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 1;
                break;
            case "T3R4":
                NamaRisiko = "Kegagalan penanganan pengecoran akibat volume beton besar";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 2;
                Mitigate = 1;
                break;
            case "T3R5":
                NamaRisiko = "Kesalahan kedalaman penggalian tanah";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T3R6":
                NamaRisiko = "Tingginya muka air tanah";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 2;
                Mitigate = 1;
                break;
            case "T3R7":
                NamaRisiko = "Hasil pembetonan kurang baik";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 10f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T3R8":
                NamaRisiko = "Kesulitan transportasi material atau peralatan pekerjaan";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T3R9":
                NamaRisiko = "Kondisi cuaca ekstrem";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 16.0f;
                Probabilitas = 0.7f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T3R10":
                NamaRisiko = "Keterlambatan pembayaran oleh owner";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 5.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;

            #endregion
            #region Tahap 4
            case "T4R1":
                NamaRisiko = "Runtuhnya wall abutmen";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 4;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T4R2":
                NamaRisiko = "Jebolnya bekisting saat pengecoran";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 5f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T4R3":
                NamaRisiko = "Kegagalan penanganan pengecoran akibat volume beton besar";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 2;
                Mitigate = 1;
                break;
            case "T4R4":
                NamaRisiko = "Keterlambatan pembayaran oleh owner";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 5.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T4R5":
                NamaRisiko = "Public Objection (Protes warga, Penutupan jalan)";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 20.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T4R6":
                NamaRisiko = "Kerusakan peralatan mesin";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 8.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 2;
                Mitigate = 1;
                break;
            case "T4R7":
                NamaRisiko = "Hasil konstruksi yang tidak sesuai gambar kerja";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 4f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T4R8":
                NamaRisiko = "Terjadinya perubahan atau review desain untuk konstruksi yang telah terbangun";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 8.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T4R9":
                NamaRisiko = "Hasil pembetonan yang kurang baik";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 10.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 2;
                break;
            case "T4R10":
                NamaRisiko = "Metode pelaksanaan salah";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 8.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;

            #endregion
            #region Tahap 5
            case "T5R1":
                NamaRisiko = "Jebolnya bekisting saat pengecoran";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 5;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T5R2":
                NamaRisiko = "Pekerja kesulitan memasang bekisting di ketinggian";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 8f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T5R3":
                NamaRisiko = "Kesalahan pemadatan tanah timbunan";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T5R4":
                NamaRisiko = "Rendahnya produktivitas peralatan";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 8.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T5R5":
                NamaRisiko = "Kesulitan transportasi material atau peralatan";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T5R6":
                NamaRisiko = "Pemborosan material";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 18.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T5R7":
                NamaRisiko = "Kondisi cuaca ekstrem";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 16f;
                Probabilitas = 0.7f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T5R8":
                NamaRisiko = "Keterlambatan pembayaran oleh owner";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 5.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T5R9":
                NamaRisiko = "Pemogokan tenaga kerja";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T5R10":
                NamaRisiko = "Terjadinya perubahan atau review desain untuk konstruksi yang belum terbangun";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 5.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;

            #endregion
            #region Tahap 6
            case "T6R1":
                NamaRisiko = "Jebolnya bekisting saat pengecoran";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 5;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T6R2":
                NamaRisiko = "Pekerja kesulitan memasang bekisting di ketinggian";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 8f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T6R3":
                NamaRisiko = "Kondisi cuaca ekstrem";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 16f;
                Probabilitas = 0.7f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T6R4":
                NamaRisiko = "Hasil konstruksi tidak sesuai gambar kerja";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 4.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T6R5":
                NamaRisiko = "Metode pelaksanaan salah";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 8.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T6R6":
                NamaRisiko = "Hasil pembetonan kurang baik";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 10.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T6R7":
                NamaRisiko = "Keterlambatan pembayaran oleh owner";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 5f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T6R8":
                NamaRisiko = "Penggambaran gambar kerja membutuhkan waktu lama";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 4f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T6R9":
                NamaRisiko = "Kesalahan alokasi pekerja";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T6R10":
                NamaRisiko = "Keterlambatan material atau peralatan";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;

            #endregion
            #region Tahap 7
            case "T7R1":
                NamaRisiko = "Jatuhnya girder saat instalasi";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 20;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T7R2":
                NamaRisiko = "Putusnya strand saat stressing";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 2;
                break;
            case "T7R3":
                NamaRisiko = "Rusaknya alat jacking force";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 2;
                break;
            case "T7R4":
                NamaRisiko = "Kesulitan erection girder";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 2;
                Mitigate = 1;
                break;
            case "T7R5":
                NamaRisiko = "Kesalahan cetak girder";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.1f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 2;
                break;
            case "T7R6":
                NamaRisiko = "Kinerja sub-kontraktor buruk";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 8.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T7R7":
                NamaRisiko = "Keterbatasan peralatan dan material";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T7R8":
                NamaRisiko = "Terjadinya perubahan atau review desain untuk konstruksi yang telah terbangun";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 8f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T7R9":
                NamaRisiko = "Penggambaran gambar kerja membutuhkan waktu lama";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T7R10":
                NamaRisiko = "Public objection";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 20.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;

            #endregion
            #region Tahap 8
            case "T8R1":
                NamaRisiko = "Pekerja kesulitan memasang bekisting di ketinggian";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 8;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T8R2":
                NamaRisiko = "Pembetonan plat bergelombang";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T8R3":
                NamaRisiko = "Terjadinya perubahan atau review desain untuk konstruksi yang belum terbangun";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 5f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T8R4":
                NamaRisiko = "Pemborosan material";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 18.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T8R5":
                NamaRisiko = "Penggambaran gambar kerja membutuhkan waktu lama";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T8R6":
                NamaRisiko = "Metode pelaksanaan salah";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 8.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T8R7":
                NamaRisiko = "Kecelakaan kerja";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 8f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T8R8":
                NamaRisiko = "Keterlambatan pembayaran oleh owner";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 5f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T8R9":
                NamaRisiko = "Public objection";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 20.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T8R10":
                NamaRisiko = "Hasil konstruksi tidak sesuai gambar kerja";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 4.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;

            #endregion
            #region Tahap 9 
            case "T9R1":
                NamaRisiko = "Kesalahan pembuatan marka jalan";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 5;
                Probabilitas = 0.1f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T9R2":
                NamaRisiko = "Rendahnya produktivitas peralatan";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 8f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T9R3":
                NamaRisiko = "Kondisi cuaca ekstrem";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "High";
                DampakDana = 16f;
                Probabilitas = 0.7f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T9R4":
                NamaRisiko = "Tidak terhitung biaya spesifik proyek";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 8.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T9R5":
                NamaRisiko = "Spesifikasi material tidak sesuai";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 8.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T9R6":
                NamaRisiko = "Kenaikan harga material";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 18.0f;
                Probabilitas = 0.5f;
                TipeRespon = "Both";
                Avoid = 8;
                Mitigate = 4;
                break;
            case "T9R7":
                NamaRisiko = "Produktivitas tenaga kerja rendah";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Moderate";
                DampakDana = 5f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 6;
                Mitigate = 3;
                break;
            case "T9R8":
                NamaRisiko = "Keterlambatan material atau peralatan";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T9R9":
                NamaRisiko = "Perakitan tulangan tidak sesuai spesifikasi";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;
            case "T9R10":
                NamaRisiko = "Kerusakan material";
                this.IDRisiko = IDRisiko;
                DampakWaktu = "Low";
                DampakDana = 4.0f;
                Probabilitas = 0.3f;
                TipeRespon = "Both";
                Avoid = 4;
                Mitigate = 1;
                break;

                #endregion
        }
    }

}
