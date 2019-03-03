using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

    //public TextAsset GameData;
    public RisikoObject[] DaftarObjectRisiko;
    public static DataManager instance;
    public UnityAction methodForPopUp;
    public Text textDetection;
    TextAsset assetText;
    public Game gameInstance;
    public string FileName = "Dateispeichern.axr";

    #region KeyForEncryption
    private string password = "TEKKOM JAYA";
    private string salt = "axoreainaxorean";
    private string HashAlgorithm = "SHA1";

    private int passworditeration = 3;
    private string InitialVector = "OEQei21'()pp1dq3";
    private int keySize = 256;
    #endregion


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void UpdatePlayerPref()
    {

        PlayerPrefs.SetInt("counterTahap", gameInstance.countertahap);
        PlayerPrefs.SetFloat("DanaTotalProyek", gameInstance.DanaTotalProyek);
        PlayerPrefs.SetFloat("danaAwal", gameInstance.danaAwal);
    }
    public void RestartGame()
    {

        //String DatainJson;
        PlayerPrefs.DeleteAll();

        //String filepathDefault = Path.Combine(Application.streamingAssetsPath + "/JSON/", "Default.json");
        //DatainJson = File.ReadAllText(filepathDefault);
        gameInstance = new Game();
        UpdatePlayerPref();
        SaveJSON();


        //XmlSerializer serializer = new XmlSerializer(typeof(Game));
        //FileStream stream = new FileStream(Application.streamingAssetsPath + "/XML/Default.xml", FileMode.Open);
        //gameInstance = serializer.Deserialize(stream) as Game;
        //stream.Close();


        //UpdatePlayerPref();
        //SaveXML();
    }
    public bool ByteArrayToFile(string fileName, byte[] byteArray)
    {
        try
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                fs.Write(byteArray, 0, byteArray.Length);
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught in process: {0}", ex);
            return false;
        }
    }

    public byte[] FileByteToRead(string fileName)
    {
        try
        {
            return File.ReadAllBytes(fileName);

        }
        catch (Exception ex)
        {
            throw new Exception("SaveFile Cannot be Read");
        }
    }

    //public void LoadXML()
    //{
    //    try {
    //        XmlSerializer serializer = new XmlSerializer(typeof(Game));
    //        FileStream stream = new FileStream(Application.streamingAssetsPath + "/XML/DataGame.xml", FileMode.Open);
    //        gameInstance = serializer.Deserialize(stream) as Game;
    //        stream.Close();
    //        UpdatePlayerPref();
    //    }
    //    catch (Exception exception)
    //    {
    //        Debug.Log(exception.Message);
    //        RestartGame();
    //    }

    //}
    public void SaveJSON()
    {
        String DatainJson;
        DatainJson = JsonUtility.ToJson(gameInstance);
        String filepath = Path.Combine(Application.streamingAssetsPath, FileName);
        //encryption here
        byte[] DatainJsonByte = Utilities.Encryption.AESEncryption.Encrypt(DatainJson, password,salt,HashAlgorithm,passworditeration,InitialVector,keySize);
        ByteArrayToFile(filepath, DatainJsonByte);
    }
    public void LoadJSON()
    {
        try {
            byte[] DatainJsonByte;
            String filepath = Path.Combine(Application.streamingAssetsPath, FileName);


            DatainJsonByte = FileByteToRead(filepath);
            //decryption here
            string DatainJson = Utilities.Encryption.AESEncryption.Decrypt(DatainJsonByte, password, salt, HashAlgorithm, passworditeration, InitialVector, keySize);

            gameInstance = JsonUtility.FromJson<Game>(DatainJson);
            Debug.Log(DatainJson);
           
            UpdatePlayerPref();
            SaveJSON();
        }
        catch (Exception e)
        {
            String filepath = Path.Combine(Application.streamingAssetsPath, FileName);

            if (!File.Exists(filepath))
            {//Jika file gak ada maka tidak ada popup
                RestartGame();

            }
            else
            {
                AlertPopUP.instance.ShowAlert("Terjadi Kesalahan", "Terjadi kesalahan dalam memuat data game, permainan akan mereset data game");
                RestartGame();
            }
            

        }

    }

    //public void SaveXML()
    //{
    //    XmlSerializer serializer = new XmlSerializer(typeof(Game));
    //    FileStream stream = new FileStream(Application.streamingAssetsPath + "/XML/DataGame.xml", FileMode.Create);
    //    serializer.Serialize(stream, gameInstance);
    //    stream.Close();
    //}
   

    // Use this for initialization
    void Start () {
        methodForPopUp -= ResetTahapDataByID;
        methodForPopUp += ResetTahapDataByID;
        DaftarObjectRisiko = RisikoController.instance.risikoAnalisis;
        LoadJSON();
    }

    //public void LoadData()
    //{
    //    LoadXML();


    //    //TextAsset assetText = Resources.Load<TextAsset>("DataGame");

    //    //string urlFile = Application.dataPath + @"/Resources/DataGame.xml";
    //    //if (File.Exists(urlFile))
    //    //{

    //    //}
    //    //else
    //    //{
    //    //    textDetection.text = assetText.name;

    //    //    throw new System.Exception("File didn't Exist");
    //    //}
    //    //XmlTextReader reader = new XmlTextReader(urlFile);
    //    textDetection.text = gameInstance.countertahap.ToString();

    //    //assetText = Resources.Load<TextAsset>("DataGame");

        

    //    //XmlDocument xmlDoc = new XmlDocument();
    //    //xmlDoc.LoadXml(assetText.text);

    //    //XmlNode tahapNode = xmlDoc.GetElementById("counterTahap");
    //    //PlayerPrefs.SetInt("counterTahap", gameInstance.countertahap);
        
    //    //reader.Close();

    //}


    public void LoadDataTahap()
    {
        int tahapSekarang = PlayerPrefs.GetInt("counterTahap");
        float perhitungan = 0;


        
        //detail.InnerText = perhitungan.ToString("N0");
        #region Judul Tahap
        RisikoController.instance.SetJudul(gameInstance.ListTahap[tahapSekarang].NamaTahap);
        UIManager.instance.SetNameTahap(gameInstance.ListTahap[tahapSekarang].NamaTahap);
        #endregion
        #region Waktu Estimasi
        RisikoController.instance.SetWaktuEst(gameInstance.ListTahap[tahapSekarang].WaktuEstimasi.ToString());
        BuildingManager.instance.waktuPengerjaan[tahapSekarang] = gameInstance.ListTahap[tahapSekarang].WaktuEstimasi;
        UIManager.instance.SetEstHari(gameInstance.ListTahap[tahapSekarang].WaktuEstimasi.ToString());
        #endregion
        #region Dana Tahap
        if (tahapSekarang == 0)
        {
            perhitungan = gameInstance.DanaTotalProyek;

        }
        else if (tahapSekarang > 0)
        {
            //Mengambil dana tersisa tahap sebelumnya
            perhitungan = gameInstance.ListTahap[tahapSekarang - 1].DanaTersisa;
        }

        gameInstance.ListTahap[tahapSekarang].DanaTahap = perhitungan;
        InputDanaController.instance.sisaDanaProyek = perhitungan;

        InputDanaController.instance.sisaDanaProyekText.text = perhitungan.ToString("N0", new CultureInfo("id-ID"));

        #endregion
        #region Load Risiko
        for (int count = 0; count < DaftarObjectRisiko.Length; count++)
        {
            DaftarObjectRisiko[count].namaRisiko.text = gameInstance.ListTahap[tahapSekarang].ArrayofRisiko[count].NamaRisiko;
            DaftarObjectRisiko[count].NamaKartu = gameInstance.ListTahap[tahapSekarang].ArrayofRisiko[count].IDRisiko;
            DaftarObjectRisiko[count].dampakWaktu = gameInstance.ListTahap[tahapSekarang].ArrayofRisiko[count].DampakWaktu;
            DaftarObjectRisiko[count].dampakDana = gameInstance.ListTahap[tahapSekarang].ArrayofRisiko[count].DampakDana;
            DaftarObjectRisiko[count].Probabilitas = gameInstance.ListTahap[tahapSekarang].ArrayofRisiko[count].Probabilitas;
            DaftarObjectRisiko[count].tipeRespon = gameInstance.ListTahap[tahapSekarang].ArrayofRisiko[count].TipeRespon;
            #region Harga Avoid
            float avoidHarga = gameInstance.ListTahap[tahapSekarang].ArrayofRisiko[count].Avoid;
            avoidHarga = (avoidHarga / 150) * PlayerPrefs.GetFloat("danaAwal");
            DaftarObjectRisiko[count].HargaAvoid = avoidHarga;
            #endregion

            #region Harga Mitigate
            float mitigateHarga = gameInstance.ListTahap[tahapSekarang].ArrayofRisiko[count].Mitigate;
            mitigateHarga = (mitigateHarga / 150) * PlayerPrefs.GetFloat("danaAwal");
            DaftarObjectRisiko[count].HargaMitigate = mitigateHarga;
            #endregion
            #region Harga Dampak Dana
            float dampakDanaTerjadi = gameInstance.ListTahap[tahapSekarang].ArrayofRisiko[count].DampakDana;
            dampakDanaTerjadi = (dampakDanaTerjadi / 150) * PlayerPrefs.GetFloat("danaAwal");
            DaftarObjectRisiko[count].dampakDana = dampakDanaTerjadi;
            #endregion

        }
        #endregion
        SaveJSON();

        #region Old Algorithm
        //        switch (tahapSekarang)
        //        {
        //            case 0:
        //                idTahap = "tahapSatu";

        //                break;
        //            case 1:
        //                idTahap = "tahapDua";
        //                break;
        //            case 2:
        //                idTahap = "tahapTiga";
        //                break;
        //            case 3:
        //                idTahap = "tahapEmpat";

        //                break;
        //            case 4:
        //                idTahap = "tahapLima";

        //                break;
        //            case 5:
        //                idTahap = "tahapEnam";

        //                break;
        //            case 6:
        //                idTahap = "tahapTujuh";

        //                break;
        //            case 7:
        //                idTahap = "tahapDelapan";

        //                break;
        //            case 8:
        //                idTahap = "tahapSembilan";
        //                break;
        //            default:
        //                idTahap = "tahapSelesai";

        //                break;

        //        }

        //        //string urlFile = Application.dataPath + @"/Resources/DataGame.xml";
        //        //      XmlTextReader reader = new XmlTextReader(urlFile);
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.LoadXml(assetText.text);

        //        XmlNode tahapNode = xmlDoc.GetElementById(idTahap);
        //        //The data digging begins here
        //        XmlNodeList detailTahap = tahapNode.ChildNodes;
        //        foreach (XmlNode detail in detailTahap)
        //        {
        //            if (detail.Name == "NamaTahap")
        //            {
        //                RisikoController.instance.SetJudul(detail.InnerText);
        //                UIManager.instance.SetNameTahap(detail.InnerText);
        //            }

        //            if (detail.Name == "WaktuEstimasi")
        //            {
        //                RisikoController.instance.SetWaktuEst(detail.InnerText);
        //                BuildingManager.instance.waktuPengerjaan[tahapSekarang] = float.Parse(detail.InnerText);
        //                UIManager.instance.SetEstHari(detail.InnerText);

        //            }

        //			if (detail.Name == "DanaTahap")  //Dana total proyek untuk tahap x
        //			{
        //				//Menghitung dana tahap setiap 
        //				XmlNodeList tahapan = xmlDoc.GetElementsByTagName("tahap");
        //				//UIManager.instance.SetDanaTahap(detail.InnerText);


        //				//if (tahapSekarang == 0)
        //				//{
        //				//	//XmlNode nodeAwal = xmlDoc.GetElementById("DanaTotalProyek");
        //				//	//perhitungan = float.Parse(nodeAwal.InnerText);

        //				//}
        //				//else if (tahapSekarang > 0)
        //				//{
        //				//	//Mengambil dana tersisa tahap sebelumnya
        //				//	XmlNodeList Tahap = tahapan[tahapSekarang - 1].ChildNodes;
        //				//	foreach (XmlNode detailTahapan in Tahap)
        //				//	{
        //				//		if (detailTahapan.Name == "DanaTersisa")
        //				//		{
        //				//			perhitungan = float.Parse(detailTahapan.InnerText);
        //				//		}
        //				//	}


        //				//}
        //				//else
        //				//{ perhitungan = 0; }
        //                perhitungan = PlayerPrefs.GetFloat("DanaTotalProyek");

        //                InputDanaController.instance.sisaDanaProyek = perhitungan;

        //				InputDanaController.instance.sisaDanaProyekText.text = perhitungan.ToString("N0", new CultureInfo("id-ID"));

        //				detail.InnerText = perhitungan.ToString("N0");
        //			}
        ////            if (detail.Name == "DanaTersedia")
        ////            {//mengisi dana tersedia diXML
        ////                
        ////                
        ////                //mengisi dana tersedia di TEXT
        ////                //float setDana = float.Parse(detail.InnerText) * 1000000;    
        ////                //RisikoController.instance.SetDanaTersedia(setDana.ToString("N0", new CultureInfo("id-ID")), float.Parse(detail.InnerText));
        ////            }


        //            if (detail.Name == "ListRisiko")
        //            {//Mengambil Daftar Risiko
        //                XmlNodeList listRisiko = detail.ChildNodes;
        //                for (int count = 0; count < listRisiko.Count; count++)
        //                {//Mengambil Detail Risiko
        //                    XmlNodeList detailRisiko = listRisiko[count].ChildNodes;
        //                    foreach (XmlNode detailNode in detailRisiko)
        //                    {
        //                        Debug.Log("Counter:" + count);
        //                        if (detailNode.Name == "NamaRisiko")
        //                        {
        //                            //Debug.Log(detailNode.Name + ":" + detailNode.InnerText);
        //                            DaftarObjectRisiko[count].namaRisiko.text = detailNode.InnerText;
        //                        }

        //                        if (detailNode.Name == "IDRisiko") //Untuk Kartu
        //                        {
        //                            //Debug.Log(detailNode.Name + ":" + detailNode.InnerText);
        //                            DaftarObjectRisiko[count].NamaKartu = detailNode.InnerText;
        //                        }

        //                        if (detailNode.Name == "DampakWaktu")
        //                        {
        //                            //Debug.Log(detailNode.Name + ":" + detailNode.InnerText);
        //                            DaftarObjectRisiko[count].dampakWaktu = detailNode.InnerText;
        //                        }

        //                        if (detailNode.Name == "DampakDana")
        //                        {
        //                            //Debug.Log(detailNode.Name + ":" + detailNode.InnerText);
        //                            DaftarObjectRisiko[count].dampakDana = detailNode.InnerText;
        //                        }

        //                        if (detailNode.Name == "Probabilitas")
        //                        {
        //                            //Debug.Log(detailNode.Name + ":" + detailNode.InnerText);
        //                            DaftarObjectRisiko[count].Probabilitas = float.Parse(detailNode.InnerText);
        //                        }

        //                        if (detailNode.Name == "TipeRespon")
        //                        {
        //                            //Debug.Log(detailNode.Name + ":" + detailNode.InnerText);
        //                            DaftarObjectRisiko[count].tipeRespon = detailNode.InnerText;
        //                        }

        //                        if (detailNode.Name == "Avoid")
        //                        {
        //                            float avoidHarga = float.Parse(detailNode.InnerText);
        //                            avoidHarga= (avoidHarga / 150) * PlayerPrefs.GetFloat("danaAwal");
        //                            //Debug.Log(detailNode.Name + ":" + detailNode.InnerText);
        //                            DaftarObjectRisiko[count].HargaAvoid = avoidHarga;

        //                        }

        //                        if (detailNode.Name == "Mitigate")
        //                        {
        //                            float MitigateHarga = float.Parse(detailNode.InnerText);
        //                            MitigateHarga = (MitigateHarga / 150) * PlayerPrefs.GetFloat("danaAwal");
        //                            //Debug.Log(detailNode.Name + ":" + detailNode.InnerText);
        //                            DaftarObjectRisiko[count].HargaMitigate = MitigateHarga;

        //                        }
        //                    }

        //                }

        //            }
        //            //Risiko List

        //        }
        //        /*
        //        <!ELEMENT gamedata ANY>
        //        <!ELEMENT countertahap ANY>
        //        <!ELEMENT tahap ANY>
        //        <!ELEMENT NamaTahap ANY>
        //        <!ELEMENT ListRisiko ANY>
        //          <!ELEMENT Risiko ANY>
        //          <!ELEMENT NamaRisiko ANY>
        //          <!ELEMENT IDRisiko ANY>
        //          <!ELEMENT DampakWaktu ANY>
        //          <!ELEMENT DampakDana ANY>
        //          <!ELEMENT Probabilitas ANY>
        //          <!ELEMENT JumlahPenangganan ANY>
        //            <!ELEMENT Avoid ANY>
        //            <!ELEMENT Mitigate ANY>
        //        <!ELEMENT WaktuEstimasi ANY>
        //        <!ELEMENT WaktuTejadi ANY>
        //        <!ELEMENT DanaTersedia ANY>
        //        <!ELEMENT DanaResponRisiko ANY>
        //        <!ELEMENT DanaPerbaikan ANY>
        //        <!ELEMENT DanaTersisa ANY>
        //         */

        //        //The data digging ends here
        //        //reader.Close();
        //        ////Save the xml
        //        //xmlDoc.(urlFile);
        #endregion
    }

    public void ResetTahapDataByID()
    {
        InputDanaController.instance.UnLockInputProyek();
        SoundManager.instance.PlaySingle(UIManager.instance.clickSFX);

        RestartGame();
        UIManager.instance.CekTahapPekerjaan();

    } 

    public void WriteDataByID(string nilaiPengganti)
    {
        gameInstance.countertahap = int.Parse(nilaiPengganti);
		PlayerPrefs.SetInt("counterTahap", gameInstance.countertahap);

        UIManager.instance.CekTahapPekerjaan();
        SaveJSON();
     }

	public void IncreaseCounterTahap()
	{
        gameInstance.countertahap++;
        PlayerPrefs.SetInt("counterTahap", gameInstance.countertahap);


        UIManager.instance.CekTahapPekerjaan();
        SaveJSON();
    }

    public void WriteDanaAwal(float amount)
    {
        gameInstance.danaAwal = amount;

        PlayerPrefs.SetFloat("danaAwal", amount);
        UIManager.instance.CekTahapPekerjaan();
    }

	public void WriteDanaTotalProyek(float amount)
	{
        gameInstance.DanaTotalProyek = amount;
        PlayerPrefs.SetFloat("DanaTotalProyek", amount);
    }

    //    public float  HitungDanaTersedia(int tahapPekerjaan)
    //    {
    //        float DanaTersedia = 0;
    //        string urlFile = Application.dataPath + @"/Resources/DataGame.xml";
    //        XmlTextReader reader = new XmlTextReader(urlFile);
    //        XmlDocument xmlDoc = new XmlDocument();
    //        xmlDoc.Load(reader);
    //        XmlNodeList tahapNode = xmlDoc.GetElementsByTagName("tahap");
    //        if (tahapPekerjaan == 0)
    //        {
    //            float perhitungan;
    //            XmlNode nodeAwal = xmlDoc.GetElementById("danaAwal");
    //            perhitungan = float.Parse(nodeAwal.InnerText);
    //            DanaTersedia = perhitungan;
    //
    //        }
    //        else if (tahapPekerjaan > 0)
    //        {
    //            float perhitungan;
    //            //Mengambil dana tersisa tahap sebelumnya
    //            XmlNodeList Tahap = tahapNode[tahapPekerjaan - 1].ChildNodes;
    //            foreach (XmlNode detailTahap in Tahap)
    //            {
    //                if (detailTahap.Name == "DanaTersisa")
    //                {
    //                    perhitungan = float.Parse(detailTahap.InnerText);
    //                }
    //            }
    //
    //
    //        }
    //
    //        return DanaTersedia;
    //    }

    public void WriteDanaRAPRABPekerjaan(float amountRAB, float amountRAP)
	{
        int tahapSekarang = PlayerPrefs.GetInt("counterTahap");

        gameInstance.ListTahap[tahapSekarang].RABTahap = amountRAB;
        gameInstance.ListTahap[tahapSekarang].RAPTahap = amountRAP;
        
	}

	public void WriteDanaTersediaPekerjaan(float amountContingency)
	{
		int tahapSekarang = PlayerPrefs.GetInt("counterTahap");
        gameInstance.ListTahap[tahapSekarang].DanaTersedia = amountContingency;
        
	}

	public void WriteDanaSisaContingency(float danaSisaContingency)
	{
		int tahapSekarang = PlayerPrefs.GetInt("counterTahap");
        gameInstance.ListTahap[tahapSekarang].DanaSisaContingency = danaSisaContingency;
	}

	public void WriteDanaSisaProyek(float danaSisaProyek)
	{
		int tahapSekarang = PlayerPrefs.GetInt("counterTahap");
        gameInstance.ListTahap[tahapSekarang].DanaTersisa = danaSisaProyek;
        
	}

	public float GetDanaTahapPekerjaan()
	{
		int tahapSekarang = PlayerPrefs.GetInt("counterTahap");
		
		return gameInstance.ListTahap[tahapSekarang].DanaTahap;
	}

	public void WriteRisikoTerjadi(int[] angkaRisikoTerjadi, RisikoObject[] objectRisiko, ArrayList ListRisikoTerpilih, float[] DampakWaktu, 
		float[] ListDampakDana, float estimasi, float DanaResponRisiko, float DanaPerbaikan, bool[] MMkah, bool[] K3kah)
	{
		int tahapSekarang = PlayerPrefs.GetInt("counterTahap");
        float danaTersedia = gameInstance.ListTahap[tahapSekarang].DanaTersedia;
        gameInstance.ListTahap[tahapSekarang].DanaPerbaikan = DanaPerbaikan;
        gameInstance.ListTahap[tahapSekarang].DanaResponRisiko = DanaResponRisiko;
        gameInstance.ListTahap[tahapSekarang].WaktuTerjadi = (int) estimasi;
        float danasisaContingency = danaTersedia - (DanaPerbaikan + DanaResponRisiko);
        gameInstance.ListTahap[tahapSekarang].DanaSisaContingency = danasisaContingency;
        gameInstance.ListTahap[tahapSekarang].DanaTersisa = gameInstance.ListTahap[tahapSekarang].DanaTersisa + danasisaContingency;
        Debug.Log("Ini Saya kalo saya muncul dua kali berarti error" + gameInstance.ListTahap[tahapSekarang].DanaTersisa);
            
        gameInstance.ListTahap[tahapSekarang].ListRisikoTerjadi.Clear();//Memastikan tidak ada risiko yang terjadi di tahap tersebut sebelumnya

        for (int i = 0; i < angkaRisikoTerjadi.Length; i++)
        {
            HappendRisk newRiskHappen = new HappendRisk(objectRisiko[angkaRisikoTerjadi[i]],ListDampakDana[i],DampakWaktu[i],MMkah[i],K3kah[i]);
            gameInstance.ListTahap[tahapSekarang].ListRisikoTerjadi.Add(newRiskHappen);
        }
        WriteRisikoTerpilih(ListRisikoTerpilih);
        SaveJSON();

        #region Koding Lama
        //        string idTahap;
        //		switch (tahapSekarang)
        //		{
        //		case 0:
        //			idTahap = "tahapSatu";

        //			break;
        //		case 1:
        //			idTahap = "tahapDua";
        //			break;
        //		case 2:
        //			idTahap = "tahapTiga";
        //			break;
        //		case 3:
        //			idTahap = "tahapEmpat";

        //			break;
        //		case 4:
        //			idTahap = "tahapLima";

        //			break;
        //		case 5:
        //			idTahap = "tahapEnam";

        //			break;
        //		case 6:
        //			idTahap = "tahapTujuh";

        //			break;
        //		case 7:
        //			idTahap = "tahapDelapan";

        //			break;
        //		default:
        //			idTahap = "tahapSelesai";

        //			break;

        //		}
        //		string urlFile = Application.dataPath + @"/Resources/DataGame.xml";
        //		XmlTextReader reader = new XmlTextReader(urlFile);

        //		XmlDocument xmlDoc = new XmlDocument();
        //		float nilaiDanaSisa = 0f;


        //		xmlDoc.Load(reader); //Assuming reader is your XmlReader 
        //		XmlNode tahapNode = xmlDoc.GetElementById(idTahap);
        //		float danaTersedia = 0f;
        //		//The data digging begins here
        //		XmlNodeList detailTahap = tahapNode.ChildNodes;
        //		foreach (XmlNode detail in detailTahap) 
        //		{
        //			if (detail.Name == "DanaTersedia") 
        //			{

        //				danaTersedia = float.Parse(detail.InnerText);
        //			}

        //			if (detail.Name == "DanaPerbaikan") 
        //			{

        //				detail.InnerText = DanaPerbaikan;
        //			}
        //			if (detail.Name == "DanaResponRisiko") 
        //			{

        //				detail.InnerText = DanaResponRisiko;
        //			}
        //			if (detail.Name == "WaktuTejadi") 
        //			{

        //				detail.InnerText = estimasi.ToString();
        //			}
        //			if (detail.Name == "DanaSisaContingency") 
        //			{
        //				float nilaiDanaPerbaikan = float.Parse (DanaPerbaikan.Replace(".",""));
        //				float nilaiDanaResponRisiko = float.Parse (DanaResponRisiko.Replace(".",""));
        //				nilaiDanaSisa = danaTersedia - (nilaiDanaPerbaikan +nilaiDanaResponRisiko);
        //				detail.InnerText = nilaiDanaSisa.ToString("N0");
        //			}
        //			if (detail.Name == "DanaTersisa") 
        //			{
        //				float nilaiRABProyekTersisa;
        //				nilaiRABProyekTersisa = float.Parse(detail.InnerText);
        //				nilaiRABProyekTersisa += nilaiDanaSisa;
        //				detail.InnerText = nilaiRABProyekTersisa.ToString("N0");
        //			}
        //			if (detail.Name == "ListRiskTerjadi") {
        //				XmlNode root = detail;
        //				detail.RemoveAll ();
        //				for (int i = 0; i < angkaRisikoTerjadi.Length; i++) 
        //				{
        //					using (XmlWriter writer = root.CreateNavigator ().AppendChild ()) 
        //					{
        //						writer.WriteStartElement("RisikoTerjadi");
        //						writer.WriteElementString("NamaRisiko", objectRisiko[angkaRisikoTerjadi[i]].namaRisiko.text.ToString());
        //						writer.WriteElementString("IDRisiko", objectRisiko[angkaRisikoTerjadi[i]].NamaKartu.ToString());
        //						writer.WriteElementString("DampakDanaTerjadi", ListDampakDana[i].ToString ("N0"));
        //						writer.WriteElementString("DampakWaktuTerjadi", DampakWaktu[i].ToString());
        //						writer.WriteElementString("ManajemenMutu", MMkah[i].ToString());
        //						writer.WriteElementString("K3", K3kah[i].ToString());
        //    //						writer.WriteElementString ("DanaResponRisikoTerjadi", DanaResponRisikoTerjadi[i].toString ("N0"));
        ////						writer.WriteElementString("WaktuTerjadiRisiko", "afdafdss");
        //						writer.WriteElementString("ResponRisiko", objectRisiko[angkaRisikoTerjadi[i]].StatusRespon.ToString());
        //						writer.WriteEndElement();
        //					}
        //				}
        //				break;
        //			} 
        //		}

        //		reader.Close();
        //		xmlDoc.Save(urlFile);
        //		LoadData();
        #endregion

    }
    public List<HappendRisk> GetHappendRisk(int tahapSekarang)
    {
        return gameInstance.ListTahap[tahapSekarang].ListRisikoTerjadi;
    }

    public List<OverviewRisk> GetOverviewRisk(int tahapSekarang)
    {
        return gameInstance.ListTahap[tahapSekarang].ListRisikoOverview;
    }

    public void WriteRisikoTerpilih(ArrayList ListRisikoTerpilih)
    {
        int tahapSekarang = PlayerPrefs.GetInt("counterTahap");
        gameInstance.ListTahap[tahapSekarang].ListRisikoTerpilih.Clear();
        for (int i = 0; i < ListRisikoTerpilih.Count; i++)
        {
            RisikoTerpilih newRiskChoosen = new RisikoTerpilih((RisikoObject) ListRisikoTerpilih[i]);
            gameInstance.ListTahap[tahapSekarang].ListRisikoTerpilih.Add(newRiskChoosen);
        }
    }

    public void WriteRisikoOverview()
    {
        int tahapSekarang = PlayerPrefs.GetInt("counterTahap");
        gameInstance.ListTahap[tahapSekarang].ListRisikoOverview.Clear();
        for (int i = 0; i < RisikoController.instance.objectRisiko.Length; i++)
        {
            OverviewRisk overviewRisk = new OverviewRisk(RisikoController.instance.objectRisiko[i]);
            gameInstance.ListTahap[tahapSekarang].ListRisikoOverview.Add(overviewRisk);
        }
    }

}
