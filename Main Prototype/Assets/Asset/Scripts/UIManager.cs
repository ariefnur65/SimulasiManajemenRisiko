using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private GameManager gameManager;
    public GameObject UIPanel, MainMenuUI, MenuPengaturanUI, MenuInfoProyek, HUDGame, MenuTahapPekerjaan;
    public GameObject MenuDaftarRisiko, MenuAnalisisRisiko, MenuInfoPekerjaan, MenuKesimpulan, MenuTentang;

    private string[] namaTahapPekerjaan = { "Bore Pile", "Footing Abutmen", "Footing Pilar", "Wall Abutmen", "Pilar", "Pier Head", "Gidrder", "Plat Jembatan", "Finishing" };
    //HelpButton
    public GameObject HelpOverview, HelpRespon;
    //BtnResetMainMenu,  BtnKembaliKemainmenu
    public Button BtnResetMenuRisiko, BtnKembaliKemainmenu;

    //Menu Info Pekerjaan
    public Text textJudulInfoPekerjaan;

    //Popup Risiko terjadi
    public GameObject PopUpRisikoTerjadi;

    //Menu Kesimpulan Akhir
    public GameObject MenuKesimpulanAkhir;
    public TahapPekerjaan[] ArrayTahapPekerjaan;
    public Text TotalRisikoTerjadi, TotalRAB, TotalRAP, TotalMM, TotalK3, EstimasiWaktu, DurasiWaktu;
    public Text TotalContingency, TotalSisa;

    /*Kesimpulan Akhir
    1. Buka dari list, isi semua nilai tahap (Butuh array class tahappekerjaan)
    2. Ketika klik nama pekerjaan bisa langsung ke kesimpulan tahap pekerjaan tersebut
    3. GameObject BackTOKA di ONkan
    4. Method BackTOKA adalah ssama seperti method buka KA dari list
        */
    //Rev 20 Juli
    public GameObject MenuOverview, EstimasiHari, TopPoint, ContainerToggle;
	public Text EstimasiOverview, JudulOverview, K3Overview, MMOverview, OverviewContingency;
	public Image TabelDaftarRisiko, ListRisiko;
    public GameObject ParentOfListRisikoDaftar;
    public Toggle btnToggleKesimpulan, btnToggleOverview;
    public GameObject btnLanjut, btnMulai, btnBack, btnKembali;
    public GameObject TahapPekerjaanRisk, MainMenuRisk;
    private UnityAction methodForToggle;

	//Menu Bantuan
	public GameObject MenuBantuan, PopUpBantuan, panelKosongPrefab, KartuRisiko;
	private GameObject MenuClone, panelKosong;
	public GameObject KetInfoProyek, KetTahap, KetInfoPekerjaan, KetKartu, KetAnalisis, KetRespon, KetOverview, KetInGame, KetKesimpulan, KetKesimpulanAkhir;
	public Image backgroundBantuan;
    public GameObject buttonCloseHUD, buttonCloseBiasa;

    //Special for Kesimpulan
    public GameObject TahapWithIncrease, TahapWithoutIncrease;
    public GameObject MainMenuWithIncrease;
    public int TahapKesimpulanMuncul;
    //Special for Overview Kesimpulan
    public Scrollbar ScrollbarOverview;
    public GameObject TahapWithoutIncreaseOverview;
    public int TahapMuncul;

    public GameObject judulGame;
    public GameObject PanelpopUp, popUpReset;
    public BuildingManager buildingManager;

    

    //InGameoBject
    public Text namaTahap;
    public Text danaTahap;
    public Text countUpHari;
    public Text EstimateHari;
    public GameObject warningSign;
    public Text progressGame;
    public GameObject IsiProgressGame;
    
    //public RiskManager rm;

	//audio / sound manager
	public AudioClip AudioMainMenu;
	public AudioClip AudioLainMainMenu;
	public AudioClip clickSFX;

    //Time Float
    public bool timePlayed = true;
    public int timeStat;
    public float fast, fastest, normal, slow, slowest;
    public Sprite play, pause;
    public Image ImgPlayPause;
    public Button btnFast, btnSlow, btnPauseStart;
    public GameObject timeStatList;
    //Button Menu Tahap 
    public Button[] buttonTahap;
    public Button btnLanjutTahapPekerjaan;
    public Button btnKesimpulanAkhir;

    private int GUIOVCounter = 0;
    //singleton
    public static UIManager instance;
	public UnityAction methodForPopUpRestart,methodForPopUpMM, methodForKeluarPopUP;


    public GameObject ButtonRisiko;
    // Use this for initialization
    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        buildingManager = FindObjectOfType<BuildingManager>();
        GUIMMOn();
        methodForToggle = new UnityAction(DoNothing);
    }
    //Fitur Singleton
    private void Awake()
    {   //UnityAction for PopUp
        methodForPopUpRestart += ResetGame;
		methodForPopUpMM += KembaliKeMM;
        methodForKeluarPopUP += Application.Quit;
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update() {
        if (gameManager.getStateOfTheGame().Equals(true))
        {
            progressGame.text = buildingManager.GetProgress().ToString() + "%";
            float progressPercentage = buildingManager.persenPembangunan;
            float isiProgress = progressPercentage * 7.02f;
            var theBarRectTransform = IsiProgressGame.transform as RectTransform;

            theBarRectTransform.sizeDelta = new Vector2(isiProgress, theBarRectTransform.sizeDelta.y);

        }


        /*
        (x/y) * (100/1) = (702/1)
	    1. Min: 0
	    2. Max 702


         */
    }

    public void POPRestartGame()
    {
    
        AlertPopUP.instance.ShowAlert("Restart Simulasi", "Apakah anda akan kembali ke Menu Daftar Risiko? Semua progress pekerjaan ini akan hilang", methodForPopUpRestart);
        //jika timenya lagi played maka jadi pause
        timePlayed = false;
        Time.timeScale = 0.0f;
        ImgPlayPause.sprite = play;
    }


	public void POPReturnToMenu()
	{

		AlertPopUP.instance.ShowAlert("Kembali Ke Main Menu", "Apakah anda akan kembali ke Main Menu? Semua progress pekerjaan ini akan hilang", methodForPopUpMM);
		//jika timenya lagi played maka jadi pause
		timePlayed = false;
		Time.timeScale = 0.0f;
		ImgPlayPause.sprite = play;

	}

	public void KembaliKeMM(){
        PopUpRisikoTerjadi.SetActive(false);
        OnRisikoOK();

        //Reset Status waktu		
        Time.timeScale = 1f;
		BuildingManager.instance.CameraAnimationFalse();
		BuildingManager.instance.cameraAnimation.SetBool("Done", true);
        BuildingManager.instance.theBridge.transform.position = new Vector3(-1f, 0.6f, 6);
        AnimationManager.instance.StopAnimation();
        SoundManager.instance.efxSource.Stop();

        BuildingManager.instance.cameraAnimation.Play("Idle");
		timePlayed = true;
		timeStat = 0;
		ImgPlayPause.sprite = pause;
		//building manager
		BuildingManager.instance.deactivated = true;    


		//Reset Camera
		CameraControl.instance.ResetPosition();
		//Reset Building
		BuildingManager.instance.BangunSemua();
		//Generate Risk RisikoCOntroller true;
		BuildingManager.instance.generaterisk = true;
		//waktu tempuh di reset
		BuildingManager.instance.waktuTempuh = 0.0f;
        BuildingManager.instance.persenPembangunan = 0.0f;

        BuildingManager.instance.intro = 0;
		//HUD OFF
		HUDGame.SetActive(false);
        //Set Play OFF
        gameManager.SetPlayOff();
        //List Risiko Muncul
        GUIMMOn();	
	}

    public void ResetGame()
    {
        //Menghilangkan pop up risiko
        PopUpRisikoTerjadi.SetActive(false);
        OnRisikoOK();
        //Reset Status waktu
        BuildingManager.instance.CameraAnimationFalse();
		BuildingManager.instance.cameraAnimation.SetBool("Done", true);
		BuildingManager.instance.cameraAnimation.Play("Idle");
        BuildingManager.instance.theBridge.transform.position = new Vector3(-1f,0.6f,6);
        AnimationManager.instance.StopAnimation();
        SoundManager.instance.efxSource.Stop();

        timePlayed = true;
        timeStat = 0;
        Time.timeScale = 1f;
        ImgPlayPause.sprite = pause;
        //building manager
        BuildingManager.instance.deactivated = true;    


        //Reset Camera
        CameraControl.instance.ResetPosition();
        //Reset Building
        BuildingManager.instance.BangunSemua();
        //Generate Risk RisikoCOntroller true;
        BuildingManager.instance.generaterisk = true;
        //waktu tempuh di reset
        BuildingManager.instance.waktuTempuh = 0.0f;
        BuildingManager.instance.persenPembangunan = 0.0f;

        BuildingManager.instance.intro = 0;
        //HUD OFF
        HUDGame.SetActive(false);
        //Set Play OFF
        gameManager.SetPlayOff();
        //List Risiko Muncul
        GUIDaftarRisikoON();

    }
    public void SetNameTahap(string namaTahap)
    {
        this.namaTahap.text = namaTahap;
    }
    public void SetDanaTahap(string DanaTahap)
    {
        this.danaTahap.text = DanaTahap;
    }
    public void SetEstHari(string estHari)
    {
        this.EstimateHari.text = estHari;        
    }
    public void SetCountUpHari(string realHari)
    {
        //this.EstimateHari.text = estHari;
        if (float.Parse(realHari) < 0)
            realHari = "0";
        this.countUpHari.text = realHari;
    }
    public void GUIOverviewRisikoON()
    {
        MenuKesimpulan.SetActive(false);
        MenuDaftarRisiko.SetActive(true);
		MenuOverview.SetActive (true);
		EstimasiOverview.text = RisikoController.instance.textEstimasiTahap.text;
		TopPoint.transform.localPosition = new Vector2 (0, 208);
		EstimasiHari.SetActive (false);
		TabelDaftarRisiko.transform.localPosition = new Vector3 (0, -45, 0);
		TabelDaftarRisiko.rectTransform.sizeDelta = new Vector2 (880, 300);
		ListRisiko.rectTransform.localPosition = new Vector3 (0, -180, 0);
		ListRisiko.rectTransform.sizeDelta = new Vector2 (880, 250);
        JudulOverview.text = "OVERVIEW RISIKO";
		SoundManager.instance.PlaySingle (clickSFX);
        RisikoController.instance.CekRespon();
        Debug.Log("Nilai Cashflow: "+InputDanaController.instance.CashFlowOverview.text);

        HelpOverview.SetActive(true);
        HelpRespon.SetActive(false);

        K3Overview.text = ": " + RisikoController.instance.K3.ToString ();
		MMOverview.text = ": " + RisikoController.instance.MM.ToString ();
        ScrollbarOverview.value = 1f;


    }


    public void GUIDaftarRisikoONFromOverview()
    {
        if (JudulOverview.text == "OVERVIEW RISIKO")
        {

            TopPoint.transform.localPosition = new Vector2(0, 150);
            ListRisiko.rectTransform.localPosition = new Vector3(0, -227, 0);
            ListRisiko.rectTransform.sizeDelta = new Vector2(880, 345);
        }
        JudulOverview.text = "RESPON RISIKO";

        SoundManager.instance.PlaySingle(clickSFX);
        GUIOVCounter = 0;
        RisikoController.instance.AnalisisOff();

        UIPanel.SetActive(true);
        MenuAnalisisRisiko.SetActive(false);

        MenuDaftarRisiko.SetActive(true);
        EstimasiHari.SetActive(true);
        TabelDaftarRisiko.transform.localPosition = new Vector3(0, -2, 0);
        TabelDaftarRisiko.rectTransform.sizeDelta = new Vector2(880, 400);

        MenuTahapPekerjaan.SetActive(false);
        MenuInfoPekerjaan.SetActive(false);
        MenuInfoProyek.SetActive(false);
        MenuOverview.SetActive(false);

        judulGame.SetActive(false);
        MainMenuUI.SetActive(false);
        MenuPengaturanUI.SetActive(false);
        PanelpopUp.SetActive(false);
        MenuKesimpulan.SetActive(false);
        //popUpReset.SetActive(false);

        HUDGame.SetActive(false);
    }

    public void GUIDaftarRisikoON()
    {
		if (JudulOverview.text == "OVERVIEW RISIKO") {			
			Debug.Log ("Change Top Point");
			TopPoint.transform.localPosition = new Vector2 (0, 150);
			ListRisiko.rectTransform.localPosition = new Vector3 (0, -227, 0);
			ListRisiko.rectTransform.sizeDelta = new Vector2 (880, 345);
		}
		JudulOverview.text = "RESPON RISIKO";

		SoundManager.instance.PlaySingle (clickSFX);
        GUIOVCounter = 0;
        RisikoController.instance.AnalisisOff();
        RisikoController.instance.AmbilSorted();

        UIPanel.SetActive(true);
        MenuAnalisisRisiko.SetActive(false);

        MenuDaftarRisiko.SetActive(true);
		EstimasiHari.SetActive (true);
		TabelDaftarRisiko.transform.localPosition = new Vector3 (0, -2, 0);
		TabelDaftarRisiko.rectTransform.sizeDelta = new Vector2 (880, 400);

        MenuTahapPekerjaan.SetActive(false);
		MenuInfoPekerjaan.SetActive (false);
        MenuInfoProyek.SetActive(false);
		MenuOverview.SetActive (false);

        judulGame.SetActive(false);
        MainMenuUI.SetActive(false);
        MenuPengaturanUI.SetActive(false);
        PanelpopUp.SetActive(false);
		MenuKesimpulan.SetActive (false);
        //popUpReset.SetActive(false);
        HelpOverview.SetActive(false);
        HelpRespon.SetActive(true);


        TahapWithoutIncreaseOverview.SetActive(false); 
        MainMenuRisk.SetActive(false);
        TahapPekerjaanRisk.SetActive(false);

        HUDGame.SetActive(false);
    }
    public void GUITahapPekerjaanON()
    {

        btnKembali.SetActive(true);
        btnLanjut.SetActive(true);
        ContainerToggle.gameObject.SetActive(false);
        CekTahapPekerjaan();
        InputDanaController.instance.EndInputProyek ();
		timeStat = 0;
		SoundManager.instance.PlaySingle (clickSFX);
        RisikoController.instance.ResetRespon();
        RisikoController.instance.AnalisisOff();
        RisikoController.instance.ClearPilihanRisiko();
        RisikoController.instance.ResetRespon();

        MenuTahapPekerjaan.SetActive(true);
        MenuDaftarRisiko.SetActive(false);
        MenuAnalisisRisiko.SetActive(false);
		MenuInfoPekerjaan.SetActive (false);
		MenuOverview.SetActive (false);

        MenuInfoProyek.SetActive(false);
        MenuKesimpulanAkhir.SetActive(false);
        judulGame.SetActive(false);
        MainMenuUI.SetActive(false);
        MenuPengaturanUI.SetActive(false);
        PanelpopUp.SetActive(false);
		MenuKesimpulan.SetActive (false);
        //popUpReset.SetActive(false);

        HUDGame.SetActive(false);
    }

    public void LoadDataKu()
    {

    }

	public void GUIInfoPekerjaanON(){
		SoundManager.instance.PlaySingle (clickSFX);
        InputDanaController.instance.CheckInputPekerjaan();
        RisikoController.instance.ResetRespon();
        RisikoController.instance.AnalisisOff();
        RisikoController.instance.ResetAnalisis();
        MenuTahapPekerjaan.SetActive(false);
		MenuDaftarRisiko.SetActive(false);
		MenuAnalisisRisiko.SetActive(false);
		MenuInfoPekerjaan.SetActive (true);
		MenuInfoProyek.SetActive(false);
		MenuOverview.SetActive (false);
        textJudulInfoPekerjaan.text = "INFO "+namaTahapPekerjaan[PlayerPrefs.GetInt("counterTahap")].ToUpper() ;

        judulGame.SetActive(false);
		MainMenuUI.SetActive(false);
		MenuPengaturanUI.SetActive(false);
		PanelpopUp.SetActive(false); 
		MenuKesimpulan.SetActive (false);
		//popUpReset.SetActive(false);

		HUDGame.SetActive(false);
	}
    public void GUIAanalisisON()
    {

		SoundManager.instance.PlaySingle (clickSFX);
        RisikoController.instance.AnalisisON();
        RisikoController.instance.ClearPilihanRisiko();
        RisikoController.instance.ResetRespon();
        MenuAnalisisRisiko.SetActive(true);
        MenuTahapPekerjaan.SetActive(false);
        MenuDaftarRisiko.SetActive(false);
		MenuOverview.SetActive (false);
        MenuInfoProyek.SetActive(false);
		MenuInfoPekerjaan.SetActive (false);

        judulGame.SetActive(false);
        MainMenuUI.SetActive(false);
        MenuPengaturanUI.SetActive(false);
        PanelpopUp.SetActive(false);
		MenuKesimpulan.SetActive (false);
        //popUpReset.SetActive(false);

        HUDGame.SetActive(false);
    }
    public void GUIInfoProyekON()
    {
		SoundManager.instance.PlaySingle (clickSFX);
		if (SoundManager.instance.musicSource.clip != AudioLainMainMenu) {
			SoundManager.instance.PlayMusic (AudioLainMainMenu);
		}
		InputDanaController.instance.CheckRABProyek ();
        
        MenuDaftarRisiko.SetActive(false);
        	
        MenuInfoProyek.SetActive(true);
		MenuInfoPekerjaan.SetActive (false);
        MenuTahapPekerjaan.SetActive(false);
		MenuOverview.SetActive (false);

        judulGame.SetActive(false);
        MainMenuUI.SetActive(false);
        MenuPengaturanUI.SetActive(false);
        PanelpopUp.SetActive(false);
		MenuKesimpulan.SetActive (false);
        //popUpReset.SetActive(false);

        HUDGame.SetActive(false);

    }

    public void GUITentang()
    {
        SoundManager.instance.PlaySingle(clickSFX);
       

        MenuDaftarRisiko.SetActive(false);
        MenuTentang.SetActive(true);
        MenuInfoProyek.SetActive(false);
        MenuInfoPekerjaan.SetActive(false);
        MenuTahapPekerjaan.SetActive(false);
        MenuOverview.SetActive(false);

        judulGame.SetActive(false);
        MainMenuUI.SetActive(false);
        MenuPengaturanUI.SetActive(false);
        PanelpopUp.SetActive(false);
        MenuKesimpulan.SetActive(false);
        //popUpReset.SetActive(false);

        HUDGame.SetActive(false);

    }
	#region menu bantuan
	public void TutupBantuan(){
		PopUpBantuan.SetActive (false);
		if (backgroundBantuan.color == Color.white) {
			backgroundBantuan.color = Color.clear;
		}
	}
    public void TutupBantuanHUD()
    {
        PopUpBantuan.SetActive(false);
        if (backgroundBantuan.color == Color.white)
        {
            backgroundBantuan.color = Color.clear;
        }
        buildingManager.intro = 1;
        PlayerPrefs.SetInt("HUDTutor", 1);
    }
    public void AllKeteranganOff(){
		KetInfoProyek.SetActive (false);
		KetTahap.SetActive (false);
		KetInfoPekerjaan.SetActive (false);
		KetKartu.SetActive (false);
		KetAnalisis.SetActive (false);
		KetRespon.SetActive (false);
		KetOverview.SetActive (false);
		KetInGame.SetActive (false);
		KetKesimpulan.SetActive (false);
        buttonCloseBiasa.SetActive(true);
        KetKesimpulanAkhir.SetActive(false);
    }

    public void BantuanMenuInfoProyek(){
		PopUpBantuan.SetActive (true);
		AllKeteranganOff ();
		KetInfoProyek.SetActive (true);
        SoundManager.instance.PlaySingle(clickSFX);

    }

    public void BantuanMenuTahap(){
		PopUpBantuan.SetActive (true);
		AllKeteranganOff ();
		KetTahap.SetActive (true);
        SoundManager.instance.PlaySingle(clickSFX);

    }

    public void BantuanMenuInfoPekerjaan(){
		PopUpBantuan.SetActive (true);
		AllKeteranganOff ();
		KetInfoPekerjaan.SetActive (true);
        SoundManager.instance.PlaySingle(clickSFX);

    }

    public void BantuanKartu(){
		PopUpBantuan.SetActive (true);
		AllKeteranganOff ();
		KetKartu.SetActive (true);
        SoundManager.instance.PlaySingle(clickSFX);

    }

    public void BantuanMenuAnalisis(){
		PopUpBantuan.SetActive (true);
		AllKeteranganOff ();
		KetAnalisis.SetActive (true);
        SoundManager.instance.PlaySingle(clickSFX);

    }

    public void BantuanMenuRespon(){
		PopUpBantuan.SetActive (true);
		AllKeteranganOff ();
		KetRespon.SetActive (true);
        SoundManager.instance.PlaySingle(clickSFX);


    }

    public void BantuanMenuOverview(){
		PopUpBantuan.SetActive (true);
		AllKeteranganOff ();
		KetOverview.SetActive (true);
        SoundManager.instance.PlaySingle(clickSFX);

    }

    public void BantuanInGame(){
		PopUpBantuan.SetActive (true);
		AllKeteranganOff ();
		KetInGame.SetActive (true);
        buttonCloseBiasa.SetActive(false);
        buttonCloseHUD.SetActive(true);

    }

    public void BantuanKesimpulan(){
		PopUpBantuan.SetActive (true);
		AllKeteranganOff ();
		KetKesimpulan.SetActive (true);
        SoundManager.instance.PlaySingle(clickSFX);

    }

    public void BantuanKesimpulanAkhir()
    {
        PopUpBantuan.SetActive(true);
        AllKeteranganOff();
        KetKesimpulanAkhir.SetActive(true);
        SoundManager.instance.PlaySingle(clickSFX);

    }
    #endregion

    public void GUIKesimpulanInListTahap(int tahap)
    {
        
        TahapKesimpulanMuncul = tahap;
        ContainerToggle.gameObject.SetActive(true);
        btnToggleKesimpulan.isOn = true;
        SoundManager.instance.PlaySingle(clickSFX);


        KesimpulanController.instance.GenerateKesimpulan(DataManager.instance.gameInstance.ListTahap[tahap].ListRisikoTerjadi, tahap);
        //Ambil data dari game instance
        RisikoController.instance.LoadOverview(tahap);
        TahapMuncul = tahap;
        //Isi contingency dll
        EstimasiOverview.text = DataManager.instance.gameInstance.ListTahap[tahap].WaktuEstimasi.ToString();
        InputDanaController.instance.CashFlowOverview.text = DataManager.instance.gameInstance.ListTahap[tahap].DanaTahap.ToString("N0", new CultureInfo("id-ID"));
        InputDanaController.instance.RABOverview.text = DataManager.instance.gameInstance.ListTahap[tahap].RABTahap.ToString("N0", new CultureInfo("id-ID"));
        InputDanaController.instance.RAPOverview.text = DataManager.instance.gameInstance.ListTahap[tahap].RAPTahap.ToString("N0", new CultureInfo("id-ID"));
        OverviewContingency.text = DataManager.instance.gameInstance.ListTahap[tahap].DanaTersedia.ToString("N0", new CultureInfo("id-ID"));

        //isi Menu Kesimpulan
        MenuKesimpulanAkhir.SetActive(false);

        //atur Button mana yang ON
        TahapWithIncrease.SetActive(false);
        TahapWithoutIncrease.SetActive(true);

        MainMenuWithIncrease.SetActive(false);

        MenuAnalisisRisiko.SetActive(false);

        MenuDaftarRisiko.SetActive(false);
        MenuInfoPekerjaan.SetActive(false);

        UIPanel.SetActive(true);
        judulGame.SetActive(false);
        MainMenuUI.SetActive(false);
        MenuPengaturanUI.SetActive(false);
        PanelpopUp.SetActive(false);
        //popUpReset.SetActive(false);
        MenuKesimpulan.SetActive(true);
        MenuInfoProyek.SetActive(false);
        MenuTahapPekerjaan.SetActive(false);
        MenuDaftarRisiko.SetActive(false);
		MenuOverview.SetActive (false);
        HUDGame.SetActive(false);
    }



    public void GUIKesimpulanON()
    {
        ContainerToggle.gameObject.SetActive(true);
        SoundManager.instance.PlaySingle(clickSFX);

        if (SoundManager.instance.musicSource.clip != AudioLainMainMenu || !SoundManager.instance.musicSource.isPlaying)
        {
            SoundManager.instance.PlayMusic(AudioLainMainMenu);
        }		 /*int[] angkaRisikoTerjadi, RisikoObject[] objectRisiko, float[] DampakWaktu, 
		float[] ListDampakDana, float estimasi, string DanaResponRisiko, string DanaPerbaikan

		*/
        //Piliih button mana yang ON
        TahapWithIncrease.SetActive(true);
        TahapWithoutIncrease.SetActive(false);

        MainMenuWithIncrease.SetActive(true);

        MenuAnalisisRisiko.SetActive(false);

        MenuDaftarRisiko.SetActive(false);
        MenuInfoPekerjaan.SetActive(false);
        MenuOverview.SetActive(false);
        UIPanel.SetActive(true);
        judulGame.SetActive(false);
        MainMenuUI.SetActive(false);
        MenuPengaturanUI.SetActive(false);
        PanelpopUp.SetActive(false);
        //popUpReset.SetActive(false);
        MenuKesimpulan.SetActive(true);
        MenuInfoProyek.SetActive(false);
        MenuTahapPekerjaan.SetActive(false);
        MenuDaftarRisiko.SetActive(false);

        HUDGame.SetActive(false);

    }

    public void GUIMMOn()
    {
        AllKeteranganOff();
        PopUpBantuan.SetActive(false);
        MenuBantuan.SetActive (false);
        MenuTentang.SetActive(false);
        btnKembali.SetActive(true);
        btnLanjut.SetActive(true);
        ContainerToggle.gameObject.SetActive(false);
        MenuKesimpulanAkhir.SetActive(false);

        timeStat = 0;
		//SoundManager.instance.PlaySingle (clickSFX);
		if (SoundManager.instance.musicSource.clip != AudioMainMenu) {
			SoundManager.instance.PlayMusic (AudioMainMenu);
		}
        MenuAnalisisRisiko.SetActive(false);

        MenuDaftarRisiko.SetActive(false);
		MenuInfoPekerjaan.SetActive (false);

        UIPanel.SetActive(true);
        judulGame.SetActive(true);
        MainMenuUI.SetActive(true);
        MenuPengaturanUI.SetActive(false);
        PanelpopUp.SetActive(false);
        //popUpReset.SetActive(false);
		MenuKesimpulan.SetActive(false);
        MenuInfoProyek.SetActive(false);
        MenuTahapPekerjaan.SetActive(false);
        MenuDaftarRisiko.SetActive(false);
		MenuOverview.SetActive (false);
        HUDGame.SetActive(false);
    }

    public void GUIMenuPengaturanOn()
    {
		SoundManager.instance.PlaySingle (clickSFX);
        UIPanel.SetActive(true);
        MainMenuUI.SetActive(false);
        MenuPengaturanUI.SetActive(true);
        PanelpopUp.SetActive(false);
		MenuKesimpulan.SetActive (false);
        //popUpReset.SetActive(false);

        MenuInfoProyek.SetActive(false);

    }

    public void GUIOff()
    {
		SoundManager.instance.PlaySingle (clickSFX);
		SoundManager.instance.musicSource.Stop ();
        MenuDaftarRisiko.SetActive(false);
        MenuAnalisisRisiko.SetActive(false);

        MenuTentang.SetActive(false);
        UIPanel.SetActive(false);
        MainMenuUI.SetActive(false);
        MenuPengaturanUI.SetActive(false);
        PanelpopUp.SetActive(false);
        //popUpReset.SetActive(false);
        MenuDaftarRisiko.SetActive(false);

        MenuInfoProyek.SetActive(false);
        MenuTahapPekerjaan.SetActive(false);
		MenuInfoPekerjaan.SetActive (false);
		MenuKesimpulan.SetActive (false);
		MenuOverview.SetActive (false);
    }

    public void PopUpResetOn()
    {
        SoundManager.instance.PlaySingle(clickSFX);

        UIPanel.SetActive(true);
        MainMenuUI.SetActive(false);
        MenuPengaturanUI.SetActive(true);
        PanelpopUp.SetActive(true);
        //popUpReset.SetActive(true);

    }

    public void GUIBantuanON()
    {
        MainMenuUI.SetActive(false);
        judulGame.SetActive(false);
        MenuBantuan.SetActive(true);
        SoundManager.instance.PlaySingle(clickSFX);

    }

    public void RestartMMON()
    {

    }
    public void ExitGame()
    {
        AlertPopUP.instance.ShowAlert("Keluar dari Simulasi", "Apakah anda yakin akan keluar dari simulasi?", methodForKeluarPopUP);

    }
    public void playGame()
    {
        CekTimeStat();
        DataManager.instance.WriteRisikoOverview();
        SoundManager.instance.PlaySingle (clickSFX);
        gameManager.SetPlayOn();
        AnimationManager.instance.PlayAnimation();
        GUIOff();
		this.countUpHari.text = "0";
        BuildingManager.instance.cameraAnimation.SetBool("Done", false);
        /**Generate Risiko
		rm.GenerateJumlahRisiko ();
		rm.GenerateWaktuRisiko ();*/
        /**if (cameraAnimation.GetCurrentAnimatorStateInfo (0).IsName("CameraTahap1")){
			gameManager.SetPlayOn ();
			//ButtonRisiko.SetActive(true);
			HUDGame.SetActive (true);
			RisikoController.instance.HitungDanaSisa ();
		}*/
	}
    public void StopGame()
	{
        Time.timeScale = 1f;
        AnimationManager.instance.StopAnimation();
        gameManager.SetPlayOff ();
		ButtonRisiko.SetActive (false);
		GUIKesimpulanON ();
        //Masukan Data ke XML
        DataManager.instance.WriteRisikoTerjadi(RisikoController.instance.angkaRisiko, RisikoController.instance.objectRisiko, RisikoController.instance.listRisikoTerpilih,
            RisikoController.instance.DampakWaktu, RisikoController.instance.ListDampakDana, RisikoController.instance.estimasi,
            float.Parse(RisikoController.instance.textTotalDana.text.Replace(".", "")), float.Parse(KesimpulanController.instance.textTotal.text.Replace(".", "")), RisikoController.instance.MMkah, RisikoController.instance.K3kah);

    }

    public void SetListenerKesimpulanButton(Button btnTahap, int intTahap)
    {
        btnTahap.GetComponent<Button>().onClick.RemoveAllListeners();
        btnTahap.onClick.AddListener(() => GUIKesimpulanInListTahap(intTahap));
        btnTahap.onClick.AddListener(() => IsiListenerToggleListTahap());

    }

    public void SetListenerLockedButton(Button btnTahap)
    {
        btnTahap.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void SetListenerForTahapInfoAccess(Button btnTahap, int intTahap)
    {
        btnTahap.GetComponent<Button>().onClick.RemoveAllListeners();
        btnTahap.onClick.AddListener(() => GUIInfoPekerjaanON());
        btnTahap.onClick.AddListener(() => DataManager.instance.LoadDataTahap());
    }


    public void CekTahapPekerjaan()
    {
        int tahapPekerjaan = PlayerPrefs.GetInt("counterTahap");
        //Cek PlayerPref udah tahap berapa
        //Jika sudah tahu maka 
        if (tahapPekerjaan >= 9)
        {
            btnLanjutTahapPekerjaan.interactable = false;
            btnKesimpulanAkhir.interactable = true;
        }
        else
        {
            btnLanjutTahapPekerjaan.interactable = true;
            btnKesimpulanAkhir.interactable = false;
        }

        for (int urut = 0; urut < buttonTahap.Length; urut++)
        {
            if (urut > tahapPekerjaan)
            {
                //masih di locked
                //Button setelah nilai akan di disable interactablenya, dan child 2nya di enable, child 1 nya di disable
                buttonTahap[urut].interactable = false;
                buttonTahap[urut].transition = Button.Transition.Animation;

                buttonTahap[urut].transform.GetChild(2).gameObject.SetActive(true);
                buttonTahap[urut].transform.GetChild(1).gameObject.SetActive(false);

                buttonTahap[urut].transform.GetChild(3).gameObject.SetActive(false);//Penanda
                buttonTahap[urut].transform.GetChild(4).gameObject.SetActive(false);//BGPenanda
                SetListenerLockedButton(buttonTahap[urut]);
            }
            else if (urut < tahapPekerjaan)
            {
                //unlocked
                //Button sebelum nilai akan di enable interactablenya, dan child 1 nya di enablie, child 2nya di disable
                buttonTahap[urut].interactable = true;
                buttonTahap[urut].transition = Button.Transition.ColorTint;
                buttonTahap[urut].transform.GetChild(2).gameObject.SetActive(false);
                buttonTahap[urut].transform.GetChild(1).gameObject.SetActive(true);

                buttonTahap[urut].transform.GetChild(3).gameObject.SetActive(false);//Penanda
                buttonTahap[urut].transform.GetChild(4).gameObject.SetActive(false);//BGPenanda
                SetListenerKesimpulanButton(buttonTahap[urut], urut);

            }
            else
            {
                //bisa di click
                //Button sama nilai akan di disable interactablenya, dan child 1 nya di disable, child 2nya di disable
                buttonTahap[urut].interactable = true;
                buttonTahap[urut].transition = Button.Transition.Animation;

                buttonTahap[urut].transform.GetChild(2).gameObject.SetActive(false);
                buttonTahap[urut].transform.GetChild(1).gameObject.SetActive(false);
                buttonTahap[urut].transform.GetChild(3).gameObject.SetActive(true);//Penanda
                buttonTahap[urut].transform.GetChild(4).gameObject.SetActive(true);//BGPenanda
                SetListenerForTahapInfoAccess(buttonTahap[urut], urut);

            }
        }
    }
    public void FasterTime()
    {
        timeStat += 1;

        CekTimeStat();
        ImgPlayPause.sprite = pause;

    }
    public void CekTimeStat()
    {
        if (timeStat > 2)
        {
            timeStat = 2;
        }

        if (timeStat < -2)
        {
            timeStat = -2;
        }
        if (timeStat == 2)
        {
            Time.timeScale = fastest;
        }
        else if (timeStat == 1)
        {
            Time.timeScale = fast;

        }
        else if (timeStat == 0)
        {
            Time.timeScale = normal;
        }
        else if (timeStat == -2)
        {
            Time.timeScale = slowest;
        }
        else if (timeStat == -1)
        {
            Time.timeScale = slow;

        }
        for (int i = 0; i < 5; ++i)
        {
            if (i == timeStat + 2)
            {
                //Yang terpilih
                timeStatList.transform.GetChild(timeStat + 2).GetComponent<Image>().enabled = true;
                timeStatList.transform.GetChild(timeStat + 2).GetComponent<Image>().transform.GetChild(0).GetComponent<Text>().color = new Color32(71,103,171,255) ;
            }
            else
            {
                timeStatList.transform.GetChild(i).GetComponent<Image>().enabled = false;
                timeStatList.transform.GetChild(i).GetComponent<Image>().transform.GetChild(0).GetComponent<Text>().color = new Color(1,1,1);

            }

         

        }
    }
    public void SlowerTime()
    {
        timeStat -= 1;
        CekTimeStat();
        ImgPlayPause.sprite = pause;



    }
    public void PauseResumeTime()
    {
        if (timePlayed)
        {
            //jika timenya lagi played maka jadi pause
            timePlayed = false;
            Time.timeScale = 0.0f;
            ImgPlayPause.sprite = play;

        }
        else
        {
            //jika timenya lagi pause maka jadi true timeScale di cek sesuai timestat
            timePlayed = true;
            CekTimeStat();
            ImgPlayPause.sprite = pause;

        }
    }
    public void OnRisikoOK()
    {
        PauseResumeTime();
        btnFast.interactable = true;
        btnPauseStart.interactable = true;
        btnSlow.interactable = true;
    }
    public void OnRisikoTerjadi()
    {
        PauseResumeTime();
        btnFast.interactable = false;
        btnPauseStart.interactable = false;
        btnSlow.interactable = false;
    }

    public void DoNothing() { }

    public void SetListenerKembaliKeKesimpulan()
    {
        UnityAction unityAction = new UnityAction(GUIKesimpulanAkhir);
        TahapWithoutIncrease.GetComponent<Button>().onClick.RemoveAllListeners();
        TahapWithoutIncreaseOverview.GetComponent<Button>().onClick.RemoveAllListeners();

        TahapWithoutIncrease.GetComponent<Button>().onClick.AddListener(unityAction);
        TahapWithoutIncreaseOverview.GetComponent<Button>().onClick.AddListener(unityAction);
    }

    public void SetListenerKembaliKeDaftar()
    {
        UnityAction unityAction = new UnityAction(GUITahapPekerjaanON);
        TahapWithoutIncrease.GetComponent<Button>().onClick.RemoveAllListeners();
        TahapWithoutIncreaseOverview.GetComponent<Button>().onClick.RemoveAllListeners();

        TahapWithoutIncrease.GetComponent<Button>().onClick.AddListener(unityAction);
        TahapWithoutIncreaseOverview.GetComponent<Button>().onClick.AddListener(unityAction);
    }

   
    public void GUIKesimpulanAkhir()
    {
        /*Menu Kesimpulan Akhir
        public GameObject MenuKesimpulanAkhir;
        public TahapPekerjaan[] ArrayTahapPekerjaan;
        public GameObject BackTOKA;
        public Text TotalRisikoTerjadi, TotalRAB, TotalRAP, TotalMM, TotalK3, EstimasiWaktu, DurasiWaktu;
        public Text TotalContingency, TotalSisa;*/
		SoundManager.instance.PlaySingle(clickSFX);
        SetListenerKembaliKeKesimpulan();
        TotalRAB.text = DataManager.instance.gameInstance.DanaTotalProyek.ToString("N0", new CultureInfo("id-ID"));
        TotalRAP.text = DataManager.instance.gameInstance.CountTotalRAP().ToString("N0", new CultureInfo("id-ID"));
        TotalMM.text = ": " + DataManager.instance.gameInstance.CountMMTahap().ToString() + " / 90";
        TotalK3.text = ": " + DataManager.instance.gameInstance.CountK3Tahap().ToString() + " / 90";

        EstimasiWaktu.text = DataManager.instance.gameInstance.CountWaktuEstimasi().ToString();
        DurasiWaktu.text = DataManager.instance.gameInstance.CountWaktuDurasi().ToString();

        TotalContingency.text = DataManager.instance.gameInstance.CountTotalContingency().ToString("N0", new CultureInfo("id-ID"));
        TotalSisa.text = DataManager.instance.gameInstance.ListTahap[8].DanaTersisa.ToString("N0", new CultureInfo("id-ID"));

        //Masukan Data Tahap
        for (int i = 0; i < ArrayTahapPekerjaan.Length; i++)
        {
            ArrayTahapPekerjaan[i].IsiKesimpulanTahap(DataManager.instance.gameInstance.ListTahap[i]);
        }

        MenuKesimpulanAkhir.SetActive(true);
        MenuTahapPekerjaan.SetActive(false);
        MenuKesimpulan.SetActive(false);
        ContainerToggle.SetActive(false);
        MenuTentang.SetActive(false);

        //SoundManager.instance.PlaySingle (clickSFX);
		if (SoundManager.instance.musicSource.clip != AudioLainMainMenu || !SoundManager.instance.musicSource.isPlaying)
		{
			SoundManager.instance.PlayMusic(AudioLainMainMenu);
		}
    }

    public void IsiListenerToggleListTahap()
    {
        btnToggleKesimpulan.onValueChanged.RemoveAllListeners();
        btnToggleOverview.onValueChanged.RemoveAllListeners();
        
        btnToggleKesimpulan.onValueChanged.AddListener((value) => {
            OnTogglePerbandinganChangeInListTahap(value);
        }  
       );
        btnToggleOverview.onValueChanged.AddListener((value) => {
            OnTogglePerbandinganChangeInListTahap(value);
        }
       );
    }

    public void IsiListenerToggle()
    {
        btnToggleKesimpulan.onValueChanged.RemoveAllListeners();
        btnToggleOverview.onValueChanged.RemoveAllListeners();

        btnToggleKesimpulan.onValueChanged.AddListener((value) => {
            OnTogglePerbandinganChange(value);
        }
       );
        btnToggleOverview.onValueChanged.AddListener((value) => {
            OnTogglePerbandinganChange(value);
        }
       );
    }
    public void OnTogglePerbandinganChangeInListTahap(bool value)
    {


        if (btnToggleKesimpulan.isOn)
        {
            TahapPekerjaanRisk.SetActive(false);
            MainMenuRisk.SetActive(false);
            

            GUIKesimpulanInListTahap(TahapKesimpulanMuncul);
            btnKembali.SetActive(true);
            btnLanjut.SetActive(true);
        }
        else if (btnToggleOverview.isOn)
        {
            TahapPekerjaanRisk.SetActive(false);
            MainMenuRisk.SetActive(false);
            TahapWithoutIncreaseOverview.SetActive(true);

            TopPoint.transform.localPosition = new Vector2(0, 150);
            ListRisiko.rectTransform.localPosition = new Vector3(0, -227, 0);
            ListRisiko.rectTransform.sizeDelta = new Vector2(880, 345);


            TabelDaftarRisiko.transform.localPosition = new Vector3(0, -2, 0);
            TabelDaftarRisiko.rectTransform.sizeDelta = new Vector2(880, 400);


            GUIOverviewRisikoON();

            btnKembali.SetActive(false);
            btnLanjut.SetActive(false);
            btnBack.SetActive(false);
            btnLanjut.SetActive(false);
            RisikoController.instance.LoadOverview(TahapMuncul);



        }
    }

    public void OnTogglePerbandinganChange(bool value)
    {
        

        if (btnToggleKesimpulan.isOn)
        {
            TahapPekerjaanRisk.SetActive(false);
            MainMenuRisk.SetActive(false);
            GUIKesimpulanON();
            btnKembali.SetActive(true);
            btnLanjut.SetActive(true);
        }
        else if (btnToggleOverview.isOn)
        {
            TahapPekerjaanRisk.SetActive(true);
            MainMenuRisk.SetActive(true);

            TahapWithoutIncreaseOverview.SetActive(false);

            TopPoint.transform.localPosition = new Vector2(0, 150);
            ListRisiko.rectTransform.localPosition = new Vector3(0, -227, 0);
            ListRisiko.rectTransform.sizeDelta = new Vector2(880, 345);


            TabelDaftarRisiko.transform.localPosition = new Vector3(0, -2, 0);
            TabelDaftarRisiko.rectTransform.sizeDelta = new Vector2(880, 400);


            GUIOverviewRisikoON();
            
            btnKembali.SetActive(false);
            btnLanjut.SetActive(false);
            btnBack.SetActive(false);
            btnLanjut.SetActive(false);

        }
    }

    #region AlertMethod
    public void PPResetData()
    {
        AlertPopUP.instance.ShowAlert("Reset Progress", "Apa anda yakin akan menghapus data anda?", DataManager.instance.methodForPopUp);
    }
    #endregion

}
