using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
//using System;
using UnityEngine;
using UnityEngine.UI;

public class RisikoController : MonoBehaviour {
    
    //SingleTon
    public static RisikoController instance;

    public RisikoObject[] objectRisiko;
    //Kartu Risiko
    public Image kartuRisiko;
    public Text hargaMitigate;
    public Text hargaAvoid;
    public Text KartuTextDampakDana;

    public Text KartuTextDampakDanaInGame;

    public Sprite[] ArrayGambarKartu;
    public Text textTotalDana;
    public float totalDana;    
    //  Judul Tahap
    public Text textJudulTahap;
    //public Text textJudulOverview;
    public int jumlahK3, jumlahMM;

    public GameObject PrepKartuRisiko;
    //Estimasi Tahap
    public Text textEstimasiTahap;
    public Text textEstimasiOverview;

    //public Text textEstimasiOverview;

    //Perhitungan Dana tersedia
    public float danaTersedia; //Awal
    public float danaSisa; //akhir
    public float danaRespon; //Dana hitung

    public Button btnLanjutOverview;

    //risk manager yanuaratp
    public BuildingManager BuildProcess;
    public int jumlahRisiko;
	public int[] waktuRisikoTerjadi;
	public bool[] sudahTerjadi;
	public int[] angkaRisiko;
    public float totalProbabilitas;
    public float probRisiko;
    public float dampakdana;
    public float dampakwaktu;
    public string respon;
	public GameObject warningSign;
	public AudioClip warningSfx;
	public GameObject popuprisiko;
    public Text keteranganrisiko;
    public Text EfekUang;
    public Text EfekHari;
	public float DampakDanaGame, DampakHariGame;
	public Text TextDampakDana;
	public Text TextDampakHari;

	//Buat MM dan K3
	public RisikoObject RisikoMuncul;
	public GameObject ContainerKartu;
	public Toggle BtnMM, BtnK3, BtnMMInGame, BtnK3InGame;
	public int K3, MM;

	//buat kesimpulan
	public float[] DampakWaktu;
	public float estimasi;
	public float[] ListDampakDana;
	public float[] ResponDanaRisiko;
	public bool[] MMkah;
	public bool[] K3kah;

	//public string[] namaRisikoTerjadi;
	//public float[] danaRisikoTerjadi;
   

    //Menyimpan nilai dari object yang sudah di pilih
    public ArrayList listRisikoTerpilih = new ArrayList();
    public ArrayList listGambarRisiko = new ArrayList();
    public ArrayList listGambarRisikoSorted = new ArrayList();
    public Text TextDanaTersedia;
    //PopUpKartu
    public GameObject PopUpKartu;
    public bool StateKartuTerlihat;
    //Analisis Object
    #region Menu Analisis
    //Algoritma nya gini
    /*1. Data dari xml di masukan kedalam array object Risiko Aanalisis; V
     *2. Nilai PxI di tentukan pada menu Aanalisis V
     *3. Dilakukan Sortir dengan memamsukan array risikoanalisis ke risiko sortedanalisis 
     *4. Urutkan SortedAnalisis mengisi array objectRisiko sudah dalam bentuk urutan
     *5. BackGround di list objectRisiko di Update sesuai Urutan
     *6. Gui yang lain juga di update     */
    public RisikoObject[] risikoAnalisis;
        public RisikoObject[] sortedAnalisis;
        public Text EstimasiHariAanalisis;
        public Text NamaPekerjaanAanalisis;
	public int nilai_tertinggi = 0;
    public Sprite[] RisikoBGSprite;

    #endregion

    
    public void UpdateNilaiKartuMM(){
        if (StateKartuTerlihat)
        { }
        else {
            
            RisikoMuncul.MMkah = BtnMM.isOn;

            RisikoMuncul.CheckTipeRespond();
        }
	}

	public void UpdateNilaiKartuK3(){

        if (StateKartuTerlihat)
        {
        }
        else
        {


            RisikoMuncul.K3Kah = BtnK3.isOn;
            RisikoMuncul.CheckTipeRespond();
        }

	}

    public void ResetAnalisis()
    {
        foreach (RisikoObject objectAnalisis in risikoAnalisis)
        {
            objectAnalisis.ResetNilaiAnalisis();
        }
    }

    public void AmbilSorted()
    {
        SortingObject();
        //mentransfer nilai sorted ke array objectRisiko
        for(int i =0; i< objectRisiko.Length; i++)
        {
            objectRisiko[i].analisisOn = false;
            objectRisiko[i].namaRisiko.text = sortedAnalisis[i].namaRisiko.text;
            objectRisiko[i].NamaKartu = sortedAnalisis[i].NamaKartu;
            objectRisiko[i].dampakDana = sortedAnalisis[i].dampakDana;
            objectRisiko[i].dampakWaktu = sortedAnalisis[i].dampakWaktu;
            objectRisiko[i].Probabilitas = sortedAnalisis[i].Probabilitas;
            objectRisiko[i].tipeRespon = sortedAnalisis[i].tipeRespon;
            objectRisiko[i].HargaAvoid = sortedAnalisis[i].HargaAvoid;
            objectRisiko[i].HargaMitigate = sortedAnalisis[i].HargaMitigate;
            objectRisiko[i].NilaiDampak = sortedAnalisis[i].NilaiDampak;
            objectRisiko[i].NilaiProbabilitas = sortedAnalisis[i].NilaiProbabilitas;
			objectRisiko[i].MMkah = sortedAnalisis[i].MMkah;
			objectRisiko[i].K3Kah = sortedAnalisis[i].K3Kah;
            objectRisiko[i].PxI = sortedAnalisis[i].PxI;
            if (i < 3)
            {   if (i % 2 == 0) //genap
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[0]; }

                else if (i % 2 == 1) //ganjil
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[1]; }
            }
            else if (i < 6)
            {
                if (i % 2 == 0) //genap
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[2]; }
                else if (i % 2 == 1) //ganjil
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[3]; }
            }
            else
            {

                if (i % 2 == 0) //genap
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[4]; }
                else if (i % 2 == 1) //ganjil
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[5]; }
            }

            objectRisiko[i].CheckRisikoOn();
           

        }

        //Sorting kartu risiko
        listGambarRisikoSorted.Clear();
        for (int x = 0; x < objectRisiko.Count(); x++)
        {
            foreach (Sprite GambarKartu in listGambarRisiko)
            {
                if (GambarKartu.name.Equals(objectRisiko[x].NamaKartu))
                {
                    listGambarRisikoSorted.Add(GambarKartu);
                    break;
                }
            }
        }
    }
    private void SortingObject()
    {
        //Tempat sorting
		//sortedAnalisis = risikoAnalisis;
		sortedAnalisis = risikoAnalisis.OrderByDescending(go => go.PxI).ToArray();
    }
    public void AnalisisON()
    {
        foreach (RisikoObject objectAnalisis in risikoAnalisis)
        {
            objectAnalisis.AnalisisOn();
        }
    }
    public void AnalisisOff()
    {
        foreach (RisikoObject objectAnalisis in risikoAnalisis)
        {
            objectAnalisis.AnalisisOff();
        }
    }
    private void OnEnable()
    {
        for (int i = 0; i < objectRisiko.Length; i++)
        {
            objectRisiko[i].CekUrutanBG(i % 2);

        }
    }
    /**public void SetDanaTersedia(string isiText, float floatText)
    {
		
        TextDanaTersedia.text = isiText;
        danaTersedia = floatText * 1000000;

    }*/
    public void ClearPilihanRisiko()
    {

        foreach (RisikoObject objectRisk in objectRisiko)
        {
            objectRisk.btnRisikoOn.isOn = false;
            objectRisk.CheckRisikoOn();

        }
    }
	public void SetDanaTersedia(float dana)
	{
		danaTersedia = dana;
		TextDanaTersedia.text = danaTersedia.ToString("N0", new CultureInfo("id-ID"));

	}
    public void SetJudul(string isiJudul)
    {
        textJudulTahap.text = isiJudul;
        NamaPekerjaanAanalisis.text = isiJudul;
        //textJudulOverview.text = isiJudul;
    }
    public void SetWaktuEst(string isiWaktu)    
    {
        textEstimasiTahap.text = isiWaktu;
        EstimasiHariAanalisis.text = isiWaktu;
        //textEstimasiOverview.text = isiWaktu;
    }
    //Fitur singleton
    private void Awake()
    {

        foreach (Sprite GambarKartu in ArrayGambarKartu)
        {
            listGambarRisiko.Add(GambarKartu);
        }

        if (instance == null)
        {
            instance = this;
        }
    }
    public void RevealKartu(string namaKartu, float hargaMitigate, float hargaAvoid)
    {
        this.hargaMitigate.text = hargaMitigate.ToString("N0", new CultureInfo("id-ID"));
        this.hargaAvoid.text = hargaAvoid.ToString("N0", new CultureInfo("id-ID"));



        foreach (Sprite GambarKartu in listGambarRisiko)
        {
            if (GambarKartu.name.Equals(namaKartu))
            {
                kartuRisiko.sprite = GambarKartu;
                PrepKartuRisiko.gameObject.SetActive(true);
                return;
            }
            
            
        }
    }

	public void CountainerOnRisikoON()
	{
		ContainerKartu.GetComponent<Image>().color = new Color32(255,255,255,255);
		BtnK3.interactable = true;
		BtnK3.isOn = RisikoMuncul.K3Kah;
		BtnMM.interactable = true;
		BtnMM.isOn = RisikoMuncul.MMkah;
		
	}
    public void ContainerOnlyOFF()
    {
        ContainerKartu.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
        BtnK3.onValueChanged.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        BtnMM.onValueChanged.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);


        BtnK3.interactable = false;
        BtnK3.isOn = false;
        BtnMM.interactable = false;
        BtnMM.isOn = false;

        BtnK3.onValueChanged.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.EditorAndRuntime);
        BtnMM.onValueChanged.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.EditorAndRuntime);

    }

    public void ContainerOffNoValueChange()
    {
        ContainerKartu.GetComponent<Image>().color = new Color32(150, 150, 150, 255);

        BtnK3.interactable = false;
        BtnMM.interactable = false;
    }

	public void CountainerOnRisikoOFF()
	{
		ContainerKartu.GetComponent<Image>().color = new Color32(150,150,150,255);

		BtnK3.interactable = false;
		BtnK3.isOn = false;
		UpdateNilaiKartuK3 ();
		BtnMM.interactable = false;
		BtnMM.isOn = false;
		UpdateNilaiKartuMM ();

	}

        public void RevealKartu(string namaKartu, float hargaMitigate, float hargaAvoid, RisikoTerpilih chosenRisk)
    {
        //digunakan jika menu kesimpulan di panggil dari menu list pekerjaan
        this.hargaMitigate.text = hargaMitigate.ToString("N0", new CultureInfo("id-ID"));
        this.hargaAvoid.text = hargaAvoid.ToString("N0", new CultureInfo("id-ID"));
        BtnK3.interactable = false;
        BtnMM.interactable = false;
        StateKartuTerlihat = true;
        this.KartuTextDampakDana.text = chosenRisk.DampakDana.ToString("N0", new CultureInfo("id-ID"));

        if (chosenRisk.ResponRisiko != "")
        {
            //jika risiko di respon
            ContainerKartu.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            BtnK3.isOn = chosenRisk.K3kah;
            BtnMM.isOn = chosenRisk.MMkah;

        }
        else
        {
            //jika risiko tidak di respon
            ContainerKartu.GetComponent<Image>().color = new Color32(150, 150, 150, 255);

            BtnK3.interactable = false;
            BtnK3.isOn = false;
            BtnMM.interactable = false;
            BtnMM.isOn = false;
        }
        StateKartuTerlihat = false;

        //Tidak perlu di cek karena di panggil hanya ketika sudah kesimpulan


        foreach (Sprite GambarKartu in listGambarRisiko)
        {
            if (GambarKartu.name.Equals(namaKartu))
            {
                kartuRisiko.sprite = GambarKartu;
                PrepKartuRisiko.gameObject.SetActive(true);
                return;
            }
        }
    }

    public void RevealKartu(string namaKartu, float hargaMitigate, float hargaAvoid, HappendRisk happendRisk)
    {
        //digunakan jika menu kesimpulan di panggil dari menu list pekerjaan
        this.hargaMitigate.text = hargaMitigate.ToString("N0", new CultureInfo("id-ID"));
        this.hargaAvoid.text = hargaAvoid.ToString("N0", new CultureInfo("id-ID"));
        StateKartuTerlihat = false;
        this.KartuTextDampakDana.text = happendRisk.DampakDana.ToString("N0", new CultureInfo("id-ID"));

        if (happendRisk.ResponRisiko != "")
        {
            //jika risiko di respon
            ContainerKartu.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            BtnK3.interactable = true;
            BtnK3.isOn = happendRisk.K3Kah;
            BtnMM.interactable = true;
            BtnMM.isOn = happendRisk.MMKah;

        }
        else
        {
            //jika risiko tidak di respon
            ContainerKartu.GetComponent<Image>().color = new Color32(150, 150, 150, 255);

            BtnK3.interactable = false;
            BtnK3.isOn = false;
            BtnMM.interactable = false;
            BtnMM.isOn = false;
        }

        //Tidak perlu di cek karena di panggil hanya ketika sudah kesimpulan
        BtnK3.interactable = false;
        BtnMM.interactable = false;


        foreach (Sprite GambarKartu in listGambarRisiko)
        {
            if (GambarKartu.name.Equals(namaKartu))
            {
                kartuRisiko.sprite = GambarKartu;
                PrepKartuRisiko.gameObject.SetActive(true);
                return;
            }
        }
    }


    public void RevealKartu(string namaKartu, float hargaMitigate, float hargaAvoid, RisikoObject ObjectMuncul)
	{
		this.hargaMitigate.text = hargaMitigate.ToString("N0", new CultureInfo("id-ID"));
		this.hargaAvoid.text = hargaAvoid.ToString("N0", new CultureInfo("id-ID"));
        StateKartuTerlihat = false;

        RisikoMuncul = ObjectMuncul;
        this.KartuTextDampakDana.text = RisikoMuncul.dampakDana.ToString("N0", new CultureInfo("id-ID"));



        if (RisikoMuncul.btnRisikoOn.isOn) {
			//jika risiko di respon
			CountainerOnRisikoON();


		} else {
			//jika risiko tidak di respon
			CountainerOnRisikoOFF();

		}



        if (ObjectMuncul.StatusRespon.ToLower() == "avoid")
        {
            RisikoController.instance.BtnK3.isOn = false;
            RisikoController.instance.BtnMM.isOn = false;

            RisikoController.instance.BtnK3.interactable = false;
            RisikoController.instance.BtnMM.interactable = false;
        }
        else if (ObjectMuncul.StatusRespon.ToLower() == "mitigate" || ObjectMuncul.StatusRespon.ToLower() == "accept")
        {

            RisikoController.instance.BtnK3.interactable = true;
            RisikoController.instance.BtnMM.interactable = true;
        }
        else
        {
            RisikoController.instance.BtnK3.interactable = false;
            RisikoController.instance.BtnMM.interactable = false;
        }



        if (RisikoMuncul.summaryRespon.gameObject.activeInHierarchy || RisikoMuncul.analisisOn == true)
        {//Sudah overview
            BtnK3.interactable = false;
            BtnMM.interactable = false;
        }

        foreach (Sprite GambarKartu in listGambarRisiko)
		{
			if (GambarKartu.name.Equals(namaKartu))
			{
				kartuRisiko.sprite = GambarKartu;
				PrepKartuRisiko.gameObject.SetActive(true);
				return;
			}


		}
	}
    public void NextRisiko()
    {
        //int tahap = PlayerPrefs.GetInt("counterTahap");
        //mengganti sprite kartu risiko dengan gambar kartu setelah gambar tersebut 
        //1. menentukan gmbar kartu muntuk mendapatkan urutnya
        //2. urutan +1 gmbar kartu
        //3. ganti gambar karturisiko
        /* New algorithm for kartu risiko
         * Jika masih pada tahap analisis maka menggunakan algoritma lama, jika sudah pada tahap respon maka menggunakan algoritma baru  
         * 
         1. Cari kartu risiko yang muncul itu nomor berapa di object risiko yang sudah di sortir
         2. 
         */
        int gambarMuncul = 0;

        if (UIManager.instance.MenuDaftarRisiko.activeInHierarchy)
        {//Jika daftar risiko aktif maka pakai algoritma baru
            //Algoritma baru
            gambarMuncul = listGambarRisikoSorted.IndexOf(kartuRisiko.sprite);
            gambarMuncul++;
          
            if (gambarMuncul <= 0)
                gambarMuncul = 0;
            else if (gambarMuncul >= listGambarRisikoSorted.Count)
                gambarMuncul = listGambarRisikoSorted.Count - 1;
            SetHargaKartu(objectRisiko, gambarMuncul);

            Debug.Log(listGambarRisikoSorted[gambarMuncul].ToString());
            kartuRisiko.sprite = (Sprite)listGambarRisikoSorted[gambarMuncul];


        }
        else if (UIManager.instance.MenuAnalisisRisiko.activeInHierarchy)
        {//Jika daftar Analisis aktif maka pakai algoritma baru
            gambarMuncul = listGambarRisiko.IndexOf(kartuRisiko.sprite);
            gambarMuncul++;
            
            if (gambarMuncul <= 0)
                gambarMuncul = 0;
            else if (gambarMuncul >= listGambarRisiko.Count)
                gambarMuncul = listGambarRisiko.Count - 1;

            SetHargaKartu(risikoAnalisis, gambarMuncul);
            Debug.Log(listGambarRisiko[gambarMuncul].ToString());
            kartuRisiko.sprite = (Sprite)listGambarRisiko[gambarMuncul];

        }
        else
        {
            Debug.Log("Kartu harusnya tidak muncul");
        }


    }
    public void KurangDanaSisa(float amountKurang)
    {
        Debug.Log("kurang" + amountKurang);
        danaSisa -= amountKurang;
        Debug.Log("sisa" + danaSisa);

        UIManager.instance.SetDanaTahap(danaSisa.ToString("N0", new CultureInfo("id-ID")));

    }
    public void HitungDanaSisa()
    {
        danaSisa = danaTersedia - danaRespon;
     
        UIManager.instance.SetDanaTahap(danaSisa.ToString("N0", new CultureInfo("id-ID")));
    }
    public void PrevRisiko()
    {
        //int tahap = PlayerPrefs.GetInt("counterTahap");
        //mengganti sprite kartu risiko dengan gambar kartu setelah gambar tersebut 
        //1. menentukan gmbar kartu muntuk mendapatkan urutnya
        //2. urutan +1 gmbar kartu
        //3. ganti gambar karturisiko
        int gambarMuncul=0;
        if (UIManager.instance.MenuDaftarRisiko.activeInHierarchy)
        {//Jika daftar risiko aktif maka pakai algoritma baru
            //Algoritma baru
            gambarMuncul = listGambarRisikoSorted.IndexOf(kartuRisiko.sprite);
            gambarMuncul--;
            
            if (gambarMuncul <= 0)
                gambarMuncul = 0;
            else if (gambarMuncul >= listGambarRisikoSorted.Count)
                gambarMuncul = listGambarRisikoSorted.Count - 1;

            SetHargaKartu(objectRisiko, gambarMuncul);

            Debug.Log(listGambarRisikoSorted[gambarMuncul].ToString());
            kartuRisiko.sprite = (Sprite)listGambarRisikoSorted[gambarMuncul];


        }
        else if (UIManager.instance.MenuAnalisisRisiko.activeInHierarchy)
        {//Jika daftar Analisis aktif maka pakai algoritma baru

            gambarMuncul = listGambarRisiko.IndexOf(kartuRisiko.sprite);
            gambarMuncul--;
            
            if (gambarMuncul <= 0)
                gambarMuncul = 0;
            else if (gambarMuncul >= listGambarRisiko.Count)
                gambarMuncul = listGambarRisiko.Count - 1;

            SetHargaKartu(risikoAnalisis, gambarMuncul);
            Debug.Log(listGambarRisiko[gambarMuncul].ToString());
            kartuRisiko.sprite = (Sprite)listGambarRisiko[gambarMuncul];

        }
        else
        {
            Debug.Log("Kartu harusnya tidak muncul");
            gambarMuncul = 0;
        }
    }

    public void SetHargaKartu(RisikoObject[] objectRiskON, int objectON)
    {
        if (objectRiskON[objectON].tipeRespon.ToLower() == "both")
        {
            this.hargaMitigate.text = objectRiskON[objectON].HargaMitigate.ToString("N0", new CultureInfo("id-ID"));
            this.hargaAvoid.text = objectRiskON[objectON].HargaAvoid.ToString("N0", new CultureInfo("id-ID"));
        }
        else if (objectRiskON[objectON].tipeRespon.ToLower() == "mitigate")
        {
            this.hargaMitigate.text = objectRiskON[objectON].HargaMitigate.ToString("N0", new CultureInfo("id-ID"));
            this.hargaAvoid.text = "";
        }
        else if (objectRiskON[objectON].tipeRespon.ToLower() == "avoid")
        {
            this.hargaMitigate.text = "";
            this.hargaAvoid.text = objectRiskON[objectON].HargaAvoid.ToString("N0", new CultureInfo("id-ID"));
        }
        

    }
    //Menghitund total dana
    public void HitungTotalDana()
    {
        
        totalDana = 0;
        foreach (RisikoObject risiko in objectRisiko)
        {
            if (risiko.btnRisikoOn.isOn)
                totalDana += risiko.danaRespon;
        }
        danaRespon = totalDana;
        if (danaTersedia <= totalDana)
        {
            btnLanjutOverview.interactable = false;
        }
        else
        {
            btnLanjutOverview.interactable = true;
        }
        textTotalDana.text = totalDana.ToString("N0", new CultureInfo("id-ID"));
    }
    public void CekRisikoTerpilih()
    {
        listRisikoTerpilih.Clear();
        foreach (RisikoObject risiko in objectRisiko)
        {
            if (risiko.btnRisikoOn.isOn)
            {
                listRisikoTerpilih.Add(risiko);
            }
        }
    }
    public void CancelOverview()
    {
        for (int i = 0; i < objectRisiko.Length; i++)
        {
            objectRisiko[i].BackOverview();
            
        }
    }
    public void CekRespon()
    {
		K3 = 0;
		MM = 0;
		PrepKartuRisiko.gameObject.SetActive(false);

        for(int i =0; i<objectRisiko.Length; i ++)  
        {
            if (objectRisiko[i].K3Kah)
            {
                K3++;

                Debug.Log("Sebelumnya K3nya: " + objectRisiko[i].K3Kah + "ke " + K3);

            }
            if (objectRisiko[i].MMkah)
            {
                MM++;

                Debug.Log("Sebelumnya MMnya: " + objectRisiko[i].MMkah + "ke " + MM);
            }
            objectRisiko[i].GetOverview();
            objectRisiko[i].CekUrutanBG(i%2);
			if (objectRisiko [i].K3Kah) {
                Debug.Log("Nama Risiko nya : " + objectRisiko[i].namaRisiko.text);

                Debug.Log("Sesudahnya K3nya: " + objectRisiko[i].K3Kah + "ke " + K3);

            }
            if (objectRisiko [i].MMkah) {
                Debug.Log("Nama Risiko nya : " + objectRisiko[i].namaRisiko.text);

                Debug.Log("Sesudahnya MMnya: " + objectRisiko[i].MMkah + "ke " + MM);
            }
            objectRisiko[i].InteractOFF();
        }
    }
    public void ResetRespon()
    {
        foreach (RisikoObject risiko in objectRisiko)
        {
            risiko.ResetOverview();
        }

        PrepKartuRisiko.gameObject.SetActive(false);
    }

    public void LoadOverview(int tahap)
    {
        //Nilai yang ada di gameinstance di isikan satu persatu di object risiko
        textJudulTahap.text = DataManager.instance.gameInstance.ListTahap[tahap].NamaTahap;
        textEstimasiOverview.text = DataManager.instance.gameInstance.ListTahap[tahap].WaktuEstimasi.ToString();
        List<OverviewRisk> listOverviewObject = DataManager.instance.GetOverviewRisk(tahap);
        jumlahK3 = 0; jumlahMM = 0;
        for (int i = 0; i < listOverviewObject.Count; i++)
        {
            objectRisiko[i].analisisOn = false;
            objectRisiko[i].AnalisisOffOnlyForOverview();

            objectRisiko[i].namaRisiko.text = listOverviewObject[i].NamaRisiko;
            objectRisiko[i].NamaKartu = listOverviewObject[i].IDRisiko;
            objectRisiko[i].dampakWaktu = listOverviewObject[i].DampakWaktu;
            objectRisiko[i].dampakDana = listOverviewObject[i].DampakDana;
            objectRisiko[i].Probabilitas = listOverviewObject[i].Probabilitas;
            objectRisiko[i].tipeRespon = listOverviewObject[i].TipeRespon;
            float hargaAvoid = (listOverviewObject[i].Avoid / 150f) * PlayerPrefs.GetFloat("danaAwal");
            float hargaMitigate = (listOverviewObject[i].Mitigate / 150f) * PlayerPrefs.GetFloat("danaAwal");
            objectRisiko[i].HargaAvoid = hargaAvoid;
            objectRisiko[i].HargaMitigate = hargaMitigate;
            objectRisiko[i].MMkah = listOverviewObject[i].MMkah;
            objectRisiko[i].K3Kah = listOverviewObject[i].K3kah;


            objectRisiko[i].StatusRespon = listOverviewObject[i].ResponRisiko;

            objectRisiko[i].TextDanaRespon.text = listOverviewObject[i].HargaResponRisiko.ToString("N0", new CultureInfo("id-ID"));
            objectRisiko[i].btnRisikoOn.isOn = listOverviewObject[i].terpilih;

            objectRisiko[i].CekStatusRespon();
          
            if (i < 3)
            {
                if (i % 2 == 0) //genap
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[0]; }

                else if (i % 2 == 1) //ganjil
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[1]; }
            }
            else if (i < 6)
            {
                if (i % 2 == 0) //genap
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[2]; }
                else if (i % 2 == 1) //ganjil
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[3]; }
            }
            else
            {

                if (i % 2 == 0) //genap
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[4]; }
                else if (i % 2 == 1) //ganjil
                { objectRisiko[i].gameObject.GetComponent<Image>().sprite = RisikoBGSprite[5]; }
            }
            if (objectRisiko[i].MMkah)
            {
                jumlahMM++;
                Debug.Log("Nama Risiko nya : " + objectRisiko[i].namaRisiko.text);
                Debug.Log("Ini loh harusnya MMnya: " + objectRisiko[i].MMkah + "ke " + jumlahMM);
            }
            if (objectRisiko[i].MMkah)
            {
                jumlahK3++;
                Debug.Log("Nama Risiko nya : " + objectRisiko[i].namaRisiko.text);

                Debug.Log("ini loh harusnya K3nya: " + objectRisiko[i].K3Kah + "ke " + jumlahK3);

            }

        }
        for (int i = 0; i < listOverviewObject.Count; i++)
        {
            objectRisiko[i].AnalisisOffOnlyForOverview();

            if (objectRisiko[i].K3Kah)
            {
                K3++;

                Debug.Log("Saya ini adalah K3nya: " + objectRisiko[i].K3Kah + "ke " + K3);

            }
            if (objectRisiko[i].MMkah)
            {
                MM++;

                Debug.Log("Saya ini adalah MMnya: " + objectRisiko[i].MMkah + "ke " + MM);
            }
        }
        CekRespon();

    }
    //risk manager yanuaratp
    #region Risk Manager Yanuar ATP
    public void GenerateJumlahRisiko()
    {


        jumlahRisiko = Random.Range(1, 4);
		//jumlahRisiko = 2;
        sudahTerjadi = new bool[jumlahRisiko];
		DampakWaktu = new float[jumlahRisiko];
		ListDampakDana = new float[jumlahRisiko];
		angkaRisiko = new int[jumlahRisiko];
		MMkah = new bool[jumlahRisiko];
		K3kah = new bool[jumlahRisiko];
		//namaRisikoTerjadi = new string[jumlahRisiko];
		//danaRisikoTerjadi = new float[jumlahRisiko];
		TextDampakDana.text = "0";
		TextDampakHari.text = "0";
        for (int i = 0; i < jumlahRisiko; i++)
        {
            sudahTerjadi[i] = false;
        }
        Debug.Log(jumlahRisiko);
    }

    public void GenerateWaktuRisiko()
    {
        waktuRisikoTerjadi = new int[jumlahRisiko];
        for (int i = 0; i < jumlahRisiko; i++)
        {
            waktuRisikoTerjadi[i] = Random.Range(7, 100);
			if (i != 0) {
				int[] dist = new int[i];
				for (int j = 0; j < i; j++) {
					dist[j] = waktuRisikoTerjadi [i] - waktuRisikoTerjadi [j];
					if (dist [j] > -15 && dist [j] < 15) {
						waktuRisikoTerjadi [i] = Random.Range (7, 100);
						j = -1;
						Debug.Log ("Jarak kurang dari 15");
					} else {
						Debug.Log ("Jarak sudah lebih dari 15");
					}
						
				}
			}
            Debug.Log(waktuRisikoTerjadi[i]);
        }
    }

    public void CekRisikoTerjadi()
    {
        int progress = BuildProcess.GetProgress();
        for (int i = 0; i < jumlahRisiko; i++)
        {
			if (waktuRisikoTerjadi [i] - 5 == progress && sudahTerjadi [i] == false) {
				warningSign.SetActive (true);

            }
            if (waktuRisikoTerjadi [i] == progress && sudahTerjadi [i] == false) {
				warningSign.SetActive (false);
				CekResponRisiko (angkaRisiko[i], i);
				sudahTerjadi [i] = true;
			}
        }
    }

    /**public void CetakNamaRisiko(){
		for (int i = 0; i < objectRisiko.Length; i++)
		{
			//Debug.Log (objectRisiko [i].namaRisiko.text);

		}
	}*/


    public void NaikTurunProbabilitas(int i)
    {
        //Cek Nilai Probabilitas
        //probabilitas 0,7(P = 4)
        if (objectRisiko[i].Probabilitas == 0.7f)
        {
            if (objectRisiko[i].NilaiProbabilitas == 4)
            {
                objectRisiko[i].Probabilitas -= 0.1f;
                //Debug.Log ("P = 4, Kurang 0.1 probabilitas");
            }
            else
            {
                objectRisiko[i].Probabilitas += 0.1f;
                //Debug.Log("P != 4, Tambah 0.1 probabilitas");
            }
        }
        //probabilitas 0,5 (P = 3)
        else if (objectRisiko[i].Probabilitas == 0.5f)
        {
            if (objectRisiko[i].NilaiProbabilitas == 3)
            {
                objectRisiko[i].Probabilitas -= 0.1f;
                //Debug.Log ("P = 3, Kurang 0.1 probabilitas");
            }
            else
            {
                objectRisiko[i].Probabilitas += 0.1f;
                //Debug.Log("P != 3, Tambah 0.1 probabilitas");
            }
        }
        //probabilitas 0,3 (P = 2)
        else if (objectRisiko[i].Probabilitas == 0.3f)
        {
            if (objectRisiko[i].NilaiProbabilitas == 2)
            {
                objectRisiko[i].Probabilitas -= 0.1f;
                //Debug.Log ("P = 2, Kurang 0.1 probabilitas");
            }
            else
            {
                objectRisiko[i].Probabilitas += 0.1f;
                //Debug.Log("P != 2, Tambah 0.1 probabilitas");
            }
        }
        //probabilitas 0,1 (P = 1)
        else if (objectRisiko[i].Probabilitas == 0.1f)
        {
            if (objectRisiko[i].NilaiProbabilitas == 1)
            {
                objectRisiko[i].Probabilitas -= 0.1f;
                //Debug.Log ("P = 1, Kurang 0.1 probabilitas");
            }
            else
            {
                objectRisiko[i].Probabilitas += 0.1f;
                //Debug.Log("P != 1, Tambah 0.1 probabilitas");
            }
        }

        //Cek Nilai Dampak
        float angkaDampakDana = objectRisiko[i].dampakDana * 150 / PlayerPrefs.GetFloat("danaAwal");

        //dampakdana 4 dan 5
        if (angkaDampakDana == 4 || angkaDampakDana == 5)
        {
            //Debug.Log ("Cek Nilai Dampak 4 dan 5");
            if (objectRisiko[i].dampakWaktu == "Low")
            {
                //I = 2
                if (objectRisiko[i].NilaiDampak == 2)
                {
                    objectRisiko[i].Probabilitas -= 0.1f;
                    //Debug.Log ("I = 2, Kurang 0.1 probabilitas");
                }
                else
                {
                    objectRisiko[i].Probabilitas += 0.1f;
                    //Debug.Log("I != 2, Tambah 0.1 probabilitas");
                }
            }
            else if (objectRisiko[i].dampakWaktu == "Moderate")
            {
                //I = 2.5 (2 atau 3)
                if (objectRisiko[i].NilaiDampak == 2 || objectRisiko[i].NilaiDampak == 3)
                {
                    objectRisiko[i].Probabilitas -= 0.1f;
                    //Debug.Log ("I = 2 atau 3, Kurang 0.1 probabilitas");
                }
                else
                {
                    objectRisiko[i].Probabilitas += 0.1f;
                    //Debug.Log("I != 2 atau 3, Tambah 0.1 probabilitas");
                }
            }
            else if (objectRisiko[i].dampakWaktu == "High")
            {
                //I = 3
                if (objectRisiko[i].NilaiDampak == 3)
                {
                    objectRisiko[i].Probabilitas -= 0.1f;
                    //Debug.Log ("I = 3, Kurang 0.1 probabilitas");
                }
                else
                {
                    objectRisiko[i].Probabilitas += 0.1f;
                    //Debug.Log("I != 3, Tambah 0.1 probabilitas");
                }
            }
        }
        //dampakdana 8 dan 10
        else if (angkaDampakDana == 8 || angkaDampakDana == 10)
        {
            //Debug.Log ("Cek Nilai Dampak 8 dan 10");
            if (objectRisiko[i].dampakWaktu == "Low")
            {
                //I = 2.5 (2 atau 3)
                if (objectRisiko[i].NilaiDampak == 2 || objectRisiko[i].NilaiDampak == 3)
                {
                    objectRisiko[i].Probabilitas -= 0.1f;
                    //Debug.Log ("I = 2 atau 3, Kurang 0.1 probabilitas");
                }
                else
                {
                    objectRisiko[i].Probabilitas += 0.1f;
                    //Debug.Log("I != 2 atau 3, Tambah 0.1 probabilitas");
                }
            }
            else if (objectRisiko[i].dampakWaktu == "Moderate")
            {
                //I = 3
                if (objectRisiko[i].NilaiDampak == 3)
                {
                    objectRisiko[i].Probabilitas -= 0.1f;
                    //Debug.Log ("I = 3, Kurang 0.1 probabilitas");
                }
                else
                {
                    objectRisiko[i].Probabilitas += 0.1f;
                    //Debug.Log("I != 3, Tambah 0.1 probabilitas");
                }
            }
            else if (objectRisiko[i].dampakWaktu == "High")
            {
                //I = 3.5 (3 atau 4)
                if (objectRisiko[i].NilaiDampak == 3 || objectRisiko[i].NilaiDampak == 4)
                {
                    objectRisiko[i].Probabilitas -= 0.1f;
                    //Debug.Log ("I = 3 atau 4, Kurang 0.1 probabilitas");
                }
                else
                {
                    objectRisiko[i].Probabilitas += 0.1f;
                    //Debug.Log("I != 3 atau 4, Tambah 0.1 probabilitas");
                }
            }
        }
        //dampakdana 16, 18, 20
        else if (angkaDampakDana == 16 || angkaDampakDana == 18 || angkaDampakDana == 20)
        {
            //Debug.Log ("Cek Nilai Dampak 16, 18, dan 20");
            if (objectRisiko[i].dampakWaktu == "Low")
            {
                //I = 3
                if (objectRisiko[i].NilaiDampak == 3)
                {
                    objectRisiko[i].Probabilitas -= 0.1f;
                    //Debug.Log ("I = 3, Kurang 0.1 probabilitas");
                }
                else
                {
                    objectRisiko[i].Probabilitas += 0.1f;
                    //Debug.Log("I != 3, Tambah 0.1 probabilitas");
                }
            }
            else if (objectRisiko[i].dampakWaktu == "Moderate")
            {
                //I = 3.5 (3 atau 4)
                if (objectRisiko[i].NilaiDampak == 3 || objectRisiko[i].NilaiDampak == 4)
                {
                    objectRisiko[i].Probabilitas -= 0.1f;
                    //Debug.Log ("I = 3 atau 4, Kurang 0.1 probabilitas");
                }
                else
                {
                    objectRisiko[i].Probabilitas += 0.1f;
                    //Debug.Log("I != 3 atau 4, Tambah 0.1 probabilitas");
                }
            }
            else if (objectRisiko[i].dampakWaktu == "High")
            {
                //I = 4
                if (objectRisiko[i].NilaiDampak == 4)
                {
                    objectRisiko[i].Probabilitas -= 0.1f;
                    //Debug.Log ("I = 4, Kurang 0.1 probabilitas");
                }
                else
                {
                    objectRisiko[i].Probabilitas += 0.1f;
                    //Debug.Log("I != 4, Tambah 0.1 probabilitas");
                }
            }
        }

        //Cek K3
        if (objectRisiko[i].K3Kah == true)
        {
            //Debug.Log("Kurang 0.1 probabilitas, K3 Terpilih");
            objectRisiko[i].Probabilitas -= 0.1f;
        }
        //Cek MM
        if (objectRisiko[i].MMkah == true)
        {
            //Debug.Log("Kurang 0.1 probabilitas, MM terpilih");
            objectRisiko[i].Probabilitas -= 0.1f;
        }

        //Debug.Log (i + ". Risiko = " + objectRisiko [i].namaRisiko.text + ", Dampak Dana : " + angkaDampakDana + ", Probabilitas = " + objectRisiko [i].Probabilitas);
        if (objectRisiko[i].Probabilitas < 0f)
        {
            objectRisiko[i].Probabilitas = 0f;
        }
    }
    public void HitungTotalProbabilitas()
    {
        totalProbabilitas = 0;
        //int panjang = objectRisiko.Length + 1;
        for (int i = 0; i < 10; i++)
        {
            //Debug.Log ("Probabilitas Risiko Sebelum = " + objectRisiko [i].Probabilitas);
            NaikTurunProbabilitas(i);
            totalProbabilitas += objectRisiko[i].Probabilitas;
            //Debug.Log (i + ". Risiko = " + objectRisiko [i].namaRisiko.text + ", Probabilitas = " + objectRisiko [i].Probabilitas + ", Total Prob = " + totalProbabilitas);
        }

    }
    public void GenerateRisiko()
    {
        //Debug.Log("Generating risiko..");
        for (int i = 0; i < jumlahRisiko; i++)
        {
            probRisiko = 0;
            //Debug.Log ("Total  probabilitas = " + totalProbabilitas);
            probRisiko = Random.Range(0f, totalProbabilitas);
            Debug.Log("Probabilitas Risiko = " + probRisiko);
            float CurrentTotal = 0;
            for (int j = 0; j < 10; j++)
            {
                //NaikTurunProbabilitas(j);
                CurrentTotal += objectRisiko[j].Probabilitas;
                Debug.Log(j + ". Risiko = " + objectRisiko[j].namaRisiko.text + ", Probabilitas = " + objectRisiko[j].Probabilitas + ", Current Total = " + CurrentTotal);
                if (probRisiko <= CurrentTotal)
                {
                    angkaRisiko[i] = j;
                    int x = i + 1;
                    Debug.Log("Risiko " + x + " adalah " + objectRisiko[j].namaRisiko.text + " dengan probabilitas " + probRisiko + " dan terjadi pada " + waktuRisikoTerjadi[i] + "%");
                    break;
                }
            }
        }

        /*ArrayList listRisiko = new ArrayList();
        for (int i = 0; i < 10; i++){
            float j = objectRisiko[i].Probabilitas * 10.0f;
            int k = Mathf.RoundToInt(j);
            for(int l = 0; l < k; l++)
            {
                listRisiko.Add(i);
            }
        }
        Debug.Log("isi list risiko : " + listRisiko.Count);
        for(int i = 0; i < jumlahRisiko; i++)
        {
            probRisiko = 0;
            probRisiko = Random.Range(0, listRisiko.Count);
            Debug.Log("Probabilitas yang muncul : " + probRisiko);
            int indexRisk = Mathf.RoundToInt(probRisiko);
            Debug.Log("Nomor Risiko " + listRisiko[indexRisk]);
            angkaRisiko[i] = int.Parse(string.Format("{0}", listRisiko[indexRisk]));
            int j = angkaRisiko[i];
            int x = i + 1;
            Debug.Log("Risiko " + x + " adalah " + objectRisiko[j].namaRisiko.text + " dengan probabilitas " + probRisiko + " dan terjadi pada " + waktuRisikoTerjadi[i] + "%");
        }*/
    }

    public void CekResponRisiko(int i, int x)
    {
        dampakdana = 0;
        respon = objectRisiko[i].StatusRespon;
        if (respon == "accept" || respon == "")
        {
            //dampakdana
            if (respon == "")
            {
                respon = "Kosong";
            }
            dampakdana = objectRisiko[i].dampakDana;

            //dampakwaktu
            if (objectRisiko[i].dampakWaktu == "High")
            {
                dampakwaktu = Random.Range(11, 20);
            }
            else if (objectRisiko[i].dampakWaktu == "Moderate")
            {
                dampakwaktu = Random.Range(5, 10);
            }
            else
            {
                dampakwaktu = Random.Range(1, 4);
            }

            //Debug.Log ("Respon terhadap risiko adalah " + respon + ", dan anda harus membayar sebesar Rp " + dampakdana + ", serta perpanjangan waktu " + dampakwaktu + " Hari.");
        }
        else if (respon == "mitigate")
        {
            //dampakdana
            dampakdana = objectRisiko[i].dampakDana - objectRisiko[i].HargaMitigate;
            if (dampakdana <= 0)
            {
                dampakdana = 0;
            }
            //dampakwaktu
            if (objectRisiko[i].dampakWaktu == "High")
            {
                dampakwaktu = Random.Range(11, 20) / 2;
            }
            else if (objectRisiko[i].dampakWaktu == "Moderate")
            {
                dampakwaktu = Random.Range(5, 10) / 2;
            }
            else
            {
                dampakwaktu = Random.Range(1, 4) / 2;
            }
            //Debug.Log ("Respon terhadap risiko adalah " + respon + ", dan anda harus membayar sebesar Rp " + dampakdana + ", serta perpanjangan waktu " + dampakwaktu + " Hari.");
        }
        else
        {
            dampakdana = 0;

            dampakwaktu = 0;
        }
        DampakWaktu[x] = dampakwaktu;
        ListDampakDana[x] = dampakdana;
        MMkah[x] = objectRisiko[i].MMkah;
        K3kah[x] = objectRisiko[i].K3Kah;
        string dana = dampakdana.ToString("N0", new CultureInfo("id-ID"));
        //AlertPopUP.instance.ShowAlert("Risiko Terjadi", "Risiko yang muncul adalah " + objectRisiko[i].namaRisiko.text + "Respon terhadap risiko adalah " + respon + ", dan anda harus membayar sebesar Rp " + dana + ", serta perpanjangan waktu " + dampakwaktu + " Hari.",);

        popuprisiko.SetActive(true);

        keteranganrisiko.text = "Risiko yang muncul adalah " + objectRisiko[i].namaRisiko.text + ". Respon terhadap risiko adalah " + respon + ", dan anda harus membayar sebesar Rp. " + dana + ", serta perpanjangan waktu pengerjaan " + dampakwaktu + " Hari.";
        RevealPopUpRisikoOnHappend(objectRisiko[i]);

        EfekUang.text = "- " + dana;
        EfekHari.text = "+ " + dampakwaktu;


        DampakDanaGame += dampakdana;
        TextDampakDana.text = DampakDanaGame.ToString("N0", new CultureInfo("id-ID"));

        DampakHariGame += dampakwaktu;
        TextDampakHari.text = DampakHariGame.ToString();

        estimasi = float.Parse(UIManager.instance.EstimateHari.text);
        estimasi += dampakwaktu;
        UIManager.instance.SetEstHari(estimasi.ToString());
        KurangDanaSisa(dampakdana);
        BuildProcess.IncreaseWaktuPengerjaan(dampakwaktu);
        UIManager.instance.OnRisikoTerjadi();
    }

    public void RevealPopUpRisikoOnHappend(RisikoObject objectRisikoTerjadi)
    {

        /*
         urutan child:
         0: Harga Accept
         1: Harga Mitigate
         2: Harga Avoid 

         */
        //Ganti Gambar dari popup kartu ke gambar object RIsiko terjadi
        foreach (Sprite GambarKartu in listGambarRisiko)
        {
            if (GambarKartu.name.Equals(objectRisikoTerjadi.NamaKartu))
            {
                //int indexKartu = listGambarRisiko.IndexOf(GambarKartu);
                //SetHargaKartu(objectRisiko, indexKartu);

                PopUpKartu.GetComponent<Image>().sprite = GambarKartu;
                break;
            }


        }


		//Mengecek btnMM dan btnK3

		BtnK3InGame.isOn = objectRisikoTerjadi.K3Kah;
		BtnMMInGame.isOn = objectRisikoTerjadi.MMkah;

        //Mengisi Harga di text Harga
        if (objectRisikoTerjadi.tipeRespon.ToLower().Equals("both"))
        {
            PopUpKartu.transform.GetChild(1).GetComponent<Text>().text = objectRisikoTerjadi.HargaMitigate.ToString("N0", new CultureInfo("id-ID"));
            PopUpKartu.transform.GetChild(2).GetComponent<Text>().text = objectRisikoTerjadi.HargaAvoid.ToString("N0", new CultureInfo("id-ID"));
            Debug.Log("Both");
        }
        else if (objectRisikoTerjadi.tipeRespon.ToLower().Equals("avoid"))
        {
            PopUpKartu.transform.GetChild(1).GetComponent<Text>().text = "";

            PopUpKartu.transform.GetChild(2).GetComponent<Text>().text = objectRisikoTerjadi.HargaAvoid.ToString("N0", new CultureInfo("id-ID"));
            Debug.Log("avoid");
        }
        else if (objectRisikoTerjadi.tipeRespon.ToLower().Equals("mitigate"))
        {
            PopUpKartu.transform.GetChild(1).GetComponent<Text>().text = objectRisikoTerjadi.HargaMitigate.ToString("N0", new CultureInfo("id-ID"));

            PopUpKartu.transform.GetChild(2).GetComponent<Text>().text = "";
            Debug.Log("mitigate");

        }
        KartuTextDampakDanaInGame.text = objectRisikoTerjadi.dampakDana.ToString("N0", new CultureInfo("id-ID"));
    }

    #endregion
    #region MyRegion
   
    #endregion
}
