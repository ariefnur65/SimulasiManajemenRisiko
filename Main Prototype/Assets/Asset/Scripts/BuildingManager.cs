using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour {
    public float[] waktuPengerjaan;

    public float waktuTempuh;
    public float persenPembangunan;
    public float objectPerPersen;
    public int counterProgress = 0;
    public float stopWatch;
    public float modifier;
    public float timeModifier;
    public static BuildingManager instance;

    public Camera Camera;

    public int counterTahap;
    public int counterBagian;

    public UIManager uiManager;
	public RisikoController rc;

    public GameObject theBridge;
    public GameObject[] borePileAbutmen1;
    public GameObject[] borePileAbutmen2;

    public GameObject[] footingAbutmen1;
    public GameObject[] footingAbutmen2;

    public GameObject[] abutmen1;
    public GameObject[] abutmen2;

    public GameObject[] borePilePilar1;
    public GameObject[] borePilePilar2;

    public GameObject[] footingPilar1;
    public GameObject[] footingPilar2;

    public GameObject[] pilar1;
    public GameObject[] pilarHead1;

    public GameObject[] pilar2;
    public GameObject[] pilarHead2;

    public GameObject[] grider1;
    public GameObject[] grider2;
    public GameObject[] griderTengah;

    public GameObject[] platJembatan;
    public GameObject[] finishing;
    public GameObject Jalan;
    

	public GameObject[] pekerjaanBorePile;
	public GameObject[] pekerjaanFootingAbutmen;
	public GameObject[] pekerjaanFootingPilar;
	public GameObject[] pekerjaanWallAbutmen;
	public GameObject[] pekerjaanPilar;
	public GameObject[] pekerjaanPilarHead;
	public GameObject[] pekerjaanGirder;
	public GameObject[] pekerjaanPlatJembatan;
	public GameObject[] pekerjaanFinishing;

	private GameManager gameManager;

    public bool deactivated = true;
	public bool generaterisk = true;

	public Animator cameraAnimation;
    public int intro = 0;
	public KesimpulanController kc;

	public AudioClip bgm1, bgm2, buildsfx1, buildsfx2;
    //public ArrayList objectYangAkanDiBangun;
    //public GameObject[] tahapPondasi;
    //public GameObject[] tahapFootingAbutmen;
    //public GameObject[] tahapFootingPilar;
    //public GameObject[] tahapWallAbutmen;
    //public GameObject[] tahapPilar;
    //public GameObject[] tahapPierHead;
    //public GameObject[] tahapGirder;
    //public GameObject[] tahapPlatJembatan;
    //public GameObject[] tahapFinishing;


    //Diperlukan sebuah rata2 waktu untuk object di aktifasi
    //:second/object
    //: jumlahbangunan yang harus di bangun/waktupengerjaan
    // Ketika terjadi bencana maka waktu pengerjaan tambah active GameObjectny
    // Membuat pembuatan jadi pertahap
    /*
	1. Pekerjaan Pondasi
	2. Pekerjaan Footing Abutmen
	3. Pekerjaan Footing Pilar
	4. Pekerjaan Wall Abutmen
	5. Pekerjaan Pilar
	6. Pekerjaan pier Head
	7. Pekerjaan Girder
	8. Pekerjaan Plat Jembatan
	9. Pekerjaan Finishing

        ======================Algoritma Pembangunan berdasarkan waktu=====================

        1. Persentase pembangunan = Waktu total/ waktu yang di tempuh
        2. Object per Persen =   sebanyak komponen yang akan di bangun  di bagi dengan 100% 
        3. pertambahan waktu yang di tempuh setiap frame, tidak setiap detik
        
    */
    //SingleTon
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Use this for initialization  
    void Start () {
		counterTahap = PlayerPrefs.GetInt   ("counterTahap");
        Camera = FindObjectOfType<Camera>();
        //objectYangAkanDiBangun = new ArrayList();
        int jumlahBagian = borePileAbutmen2.Length + borePileAbutmen1.Length;
        //Yang akan di bangun
        //Stop Watch
        //objectPerPersen = waktuPengerjaan[counterTahap] / (jumlahBagian);
        //stopWatch = objectPerPersen;
		deactivated = true;
        gameManager = FindObjectOfType<GameManager>();
    }
	// Update is called once per frame
	void Update () {
        
		if (gameManager.getStateOfTheGame().Equals(true))
        {
			
			//Generate Risiko
			if (generaterisk == true) {
				SoundManager.instance.RandomizeMusic (bgm1, bgm2);
				rc.DampakDanaGame = 0;
				rc.DampakHariGame = 0;
				rc.GenerateJumlahRisiko ();
				rc.GenerateWaktuRisiko ();
				rc.HitungTotalProbabilitas ();
				rc.GenerateRisiko ();
				generaterisk = false;
			}

			rc.CekRisikoTerjadi ();
			counterTahap = PlayerPrefs.GetInt   ("counterTahap");


            switch (counterTahap)
            {
			case 0:
				#region Pekerjaan Pondasi Bore Pile

			//nonaktif object
			while (deactivated)
            {
                    RisikoController.instance.HitungDanaSisa();
                    cameraAnimation.Play ("CameraIntro");
                    Jalan.SetActive(false);

                    foreach (GameObject gameObject in pekerjaanBorePile)
				{
					gameObject.SetActive(true);
				}
				foreach (GameObject gameObject in abutmen1)
				{
					gameObject.SetActive(false);
				}

				foreach (GameObject gameObject in abutmen2)
				{
					gameObject.SetActive(false);
				}

				foreach (GameObject gameObject in footingPilar1)
				{
					gameObject.SetActive(false);
				}

				foreach (GameObject gameObject in footingPilar2)
				{
					gameObject.SetActive(false);
				}

				foreach (GameObject gameObject in grider1)
				{
					gameObject.SetActive(false);
				}

				foreach (GameObject gameObject in grider2)
				{
					gameObject.SetActive(false);
				}

				foreach (GameObject gameObject in griderTengah)
				{
					gameObject.SetActive(false);
				}

				foreach (GameObject gameObject in platJembatan)
				{
					gameObject.SetActive(false);
				}

				foreach (GameObject gameObject in borePileAbutmen1)
				{
					gameObject.SetActive(false);
				}

				foreach (GameObject gameObject in borePileAbutmen2)
				{
					gameObject.SetActive(false);
				}

				foreach (GameObject gameObject in borePilePilar1)
				{
					gameObject.SetActive(false);
				}

				foreach (GameObject gameObject in borePilePilar2)
				{
					gameObject.SetActive(false);
				}
				foreach (GameObject gameObject in finishing)
				{
					gameObject.SetActive(false);
				}
				foreach (GameObject gameObject in platJembatan)
				{
					gameObject.SetActive(false);
				}
				//Tambahan
				foreach (GameObject gameObject in pilar1)
				{
					gameObject.SetActive(false);
				}
				foreach (GameObject gameObject in pilar2)
				{
					gameObject.SetActive(false);
				}
				foreach (GameObject gameObject in pilarHead1)
				{
					gameObject.SetActive(false);
				}
				foreach (GameObject gameObject in pilarHead2)
				{
					gameObject.SetActive(false);
				}
				foreach (GameObject gameObject in footingAbutmen1)
				{
					gameObject.SetActive(false);
				}
				foreach (GameObject gameObject in footingAbutmen2)
				{
					gameObject.SetActive(false);
				}
				deactivated = false;
			}

                //kondisi nunggu kamera pindah
                if (cameraAnimation.GetCurrentAnimatorStateInfo(0).IsName("CameraIntro") && cameraAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && intro == 0)
                {
					CameraAnimationFalse();
                    cameraAnimation.SetBool("Abutmen1", true);
                    if (PlayerPrefs.HasKey("HUDTutor") || PlayerPrefs.GetInt("HUDTutor") == 1)
                    {
                        // Jika sudah menampilkan hud tutor mmaka lanjut ke pembangunan
                        intro = 1;
                    }
                    else
                    {
                        //jika belum tampil, ditampilkan dulu
                        UIManager.instance.BantuanInGame();
                    }
                    //cameraAnimation.Play("CameraIdle");
                }
                    
                if (intro == 1)
                {
                        

                    //Debug.Log("length = " +uiManager.cameraAnimation.GetCurrentAnimatorStateInfo (0).length);
                    //Debug.Log("normalized time = " +uiManager.cameraAnimation.GetCurrentAnimatorStateInfo (0).normalizedTime);
                    //Camera.orthographic = true;
                    //Camera.orthographicSize = 15;
                    foreach (GameObject gameObject in pekerjaanBorePile)
                    {
                        gameObject.SetActive(false);
                    }
                    //RisikoController.instance.HitungDanaSisa ();
                    uiManager.HUDGame.SetActive(true);



                    if (persenPembangunan <= 100) //Jika belum selesai
                {						
					if (theBridge.transform.position.y < 7.5)
                    {
                        theBridge.transform.Translate(new Vector3(0, 0.5f, 0));
                    }

                       
                        waktuTempuh += Time.deltaTime;
                    //Object Off
                    //Jika objectnya sudah di disable semua maka tidak di eksekusi
                        

                    //Pembangunan persentase
                    persenPembangunan = (waktuTempuh / waktuPengerjaan[counterTahap]) * 100;
                    float countUpWaktu;
                    //countUpWaktu = waktuPengerjaan[counterTahap] - waktuTempuh;

					countUpWaktu = waktuTempuh;
                    UIManager.instance.SetCountUpHari(Mathf.Floor(countUpWaktu).ToString());
                    //OBject per persen
                    int jumlahBagian = borePileAbutmen2.Length + borePileAbutmen1.Length + borePilePilar1.Length + borePilePilar2.Length;
                    objectPerPersen = 100 / jumlahBagian;
                    float bagian = persenPembangunan / objectPerPersen;
                    //Kalkulasi object apa yang muncul di setiap persen
                    if (persenPembangunan >= (counterBagian + 1) * objectPerPersen)
                    {
						if (counterBagian < borePileAbutmen1.Length)
                        {
							CameraAnimationFalse();
							cameraAnimation.SetBool("Abutmen1", true);
                            BangunBangunan(counterBagian, borePileAbutmen1);
                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
                        else if (counterBagian >= (borePileAbutmen1.Length + borePileAbutmen2.Length + borePilePilar1.Length))
                        {
							CameraAnimationFalse();
                            cameraAnimation.SetBool("Pilar2", true);
							BangunBangunan(counterBagian - (borePileAbutmen1.Length + borePileAbutmen2.Length + borePilePilar1.Length), borePilePilar2);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }

                                }
                        else if (counterBagian >= (borePileAbutmen1.Length + borePileAbutmen2.Length))
                        {
							CameraAnimationFalse();
                            cameraAnimation.SetBool("Pilar1", true);
                            BangunBangunan(counterBagian - (borePileAbutmen1.Length + borePileAbutmen2.Length), borePilePilar1);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
                        else if (counterBagian >= borePileAbutmen1.Length)
                        {
							CameraAnimationFalse();
                            cameraAnimation.SetBool("Abutmen2", true);
							BangunBangunan(counterBagian - borePileAbutmen1.Length, borePileAbutmen2);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
                    }
                    counterBagian = (int)Mathf.Floor(bagian);


                }
                else
                {
					if (theBridge.transform.position.y > 0.5)
                    {
                        theBridge.transform.Translate(new Vector3(0, -0.1f, 0));
                        CameraAnimationFalse();
                        cameraAnimation.SetBool("T1End", true);
                        UIManager.instance.timeStat = 0;
                        UIManager.instance.CekTimeStat();
                        UIManager.instance.btnFast.interactable = false;
                        UIManager.instance.btnPauseStart.interactable = false;
                        UIManager.instance.btnSlow.interactable = false;
                        UIManager.instance.BtnResetMenuRisiko.interactable = false;
                        UIManager.instance.BtnKembaliKemainmenu.interactable = false;

                            }

                            else {
                                if (theBridge.transform.position.y > 0.5)
                                {
                                    
                                    theBridge.transform.Translate(new Vector3(0, -0.1f, 0));
                                    
                                }

                                else {
                                    UIManager.instance.btnFast.interactable = true;
                                    UIManager.instance.btnPauseStart.interactable = true;
                                    UIManager.instance.btnSlow.interactable = true;

                                    UIManager.instance.BtnResetMenuRisiko.interactable = true;
                                    UIManager.instance.BtnKembaliKemainmenu.interactable = true;
                                    persenPembangunan = 0;
							        waktuTempuh = 0;							
							        deactivated = true;
							        CameraAnimationFalse();
							        cameraAnimation.SetBool("Done", true);
							        SoundManager.instance.efxSource.Stop();
							        kc.GenerateKesimpulan(rc.jumlahRisiko);
							        uiManager.StopGame();
                                }

					}
                    
                        
                }
			}
            break;
            #endregion
			case 1:
				#region Pekerjaan Footing Abutmen

				//nonaktif object
				while (deactivated)
				{
					RisikoController.instance.HitungDanaSisa();
					cameraAnimation.Play ("CameraIntro");
					#region Tahap 1 Done
					foreach (GameObject gameObject in borePileAbutmen1)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePileAbutmen2)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePilePilar1)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePilePilar2)
					{
						gameObject.SetActive(true);
					}
                        #endregion


                        Jalan.SetActive(false);

                        foreach (GameObject gameObject in pekerjaanBorePile)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanFootingAbutmen)
					{
						gameObject.SetActive(true);
					}
					foreach (GameObject gameObject in abutmen1)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in abutmen2)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in footingPilar1)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in footingPilar2)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in grider1)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in grider2)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in griderTengah)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in platJembatan)
					{
						gameObject.SetActive(false);
					}


					foreach (GameObject gameObject in finishing)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in platJembatan)
					{
						gameObject.SetActive(false);
					}
					//Tambahan
					foreach (GameObject gameObject in pilar1)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilar2)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilarHead1)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilarHead2)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in footingAbutmen1)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in footingAbutmen2)
					{
						gameObject.SetActive(false);
					}
					deactivated = false;
				}

				//kondisi nunggu kamera pindah
				if (cameraAnimation.GetCurrentAnimatorStateInfo(0).IsName("CameraIntro") && cameraAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && intro == 0)
				{
					CameraAnimationFalse();
					cameraAnimation.SetBool("T2Abutmen1", true);
					intro = 1;
					//cameraAnimation.Play("CameraIdle");
				}
				if (intro == 1)
				{
					foreach (GameObject gameObject in pekerjaanFootingAbutmen)
					{
						gameObject.SetActive(false);
					}
					//RisikoController.instance.HitungDanaSisa ();
					uiManager.HUDGame.SetActive(true);



					if (persenPembangunan <= 100) //Jika belum selesai
					{						
						/*if (theBridge.transform.position.y < 7.5)
						{
							theBridge.transform.Translate(new Vector3(0, 0.5f, 0));
						}*/

						waktuTempuh += Time.deltaTime;
						//Object Off
						//Jika objectnya sudah di disable semua maka tidak di eksekusi


						//Pembangunan persentase
						persenPembangunan = (waktuTempuh / waktuPengerjaan[counterTahap]) * 100;
						float countUpWaktu;
						//countUpWaktu = waktuPengerjaan[counterTahap] - waktuTempuh;
						countUpWaktu = waktuTempuh;
						UIManager.instance.SetCountUpHari(Mathf.Floor(countUpWaktu).ToString());
						//OBject per persen
						int jumlahBagian = footingAbutmen1.Length + footingAbutmen2.Length;
						objectPerPersen = 100 / jumlahBagian;
						float bagian = persenPembangunan / objectPerPersen;
						//Kalkulasi object apa yang muncul di setiap persen
						if (persenPembangunan >= (counterBagian + 1) * objectPerPersen)
						{
							if (counterBagian < footingAbutmen1.Length)
							{
								CameraAnimationFalse();
								cameraAnimation.SetBool("T2Abutmen1", true);
								BangunBangunan(counterBagian, footingAbutmen1);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
							else if (counterBagian >= footingAbutmen1.Length)
							{
								CameraAnimationFalse();
								cameraAnimation.SetBool("T2Abutmen2", true);
								BangunBangunan(counterBagian - footingAbutmen1.Length, footingAbutmen2);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
						}
						counterBagian = (int)Mathf.Floor(bagian);


					}
					else
					{
						persenPembangunan = 0;
						waktuTempuh = 0;							
						deactivated = true;
						CameraAnimationFalse();
						cameraAnimation.SetBool("Done", true);
						SoundManager.instance.efxSource.Stop();
						kc.GenerateKesimpulan(rc.jumlahRisiko);
						uiManager.StopGame();

					}
				}
				break;                    
				#endregion
			case 2:
				#region Pekerjaan Footing Pilar

				//nonaktif object
				while (deactivated)
				{
					RisikoController.instance.HitungDanaSisa();
					cameraAnimation.Play ("CameraIntro");
					#region Tahap 1 Done dan 2
					foreach (GameObject gameObject in borePileAbutmen1)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePileAbutmen2)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePilePilar1)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePilePilar2)
					{
						gameObject.SetActive(true);
					}
					//Tahap 2

					foreach (GameObject gameObject in footingAbutmen1)
					{
						gameObject.SetActive(true);
					}
					foreach (GameObject gameObject in footingAbutmen2)
					{
						gameObject.SetActive(true);
					}

                        #endregion


                        Jalan.SetActive(false);

                        foreach (GameObject gameObject in pekerjaanBorePile)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanFootingAbutmen)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanFootingPilar)
					{
						gameObject.SetActive(true);
					}



					foreach (GameObject gameObject in abutmen1)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in abutmen2)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in footingPilar1)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in footingPilar2)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in grider1)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in grider2)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in griderTengah)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in platJembatan)
					{
						gameObject.SetActive(false);
					}


					foreach (GameObject gameObject in finishing)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in platJembatan)
					{
						gameObject.SetActive(false);
					}
					//Tambahan
					foreach (GameObject gameObject in pilar1)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilar2)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilarHead1)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilarHead2)
					{
						gameObject.SetActive(false);
					}

					deactivated = false;
				}

				//kondisi nunggu kamera pindah
				if (cameraAnimation.GetCurrentAnimatorStateInfo(0).IsName("CameraIntro") && cameraAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && intro == 0)
				{
					CameraAnimationFalse();
					cameraAnimation.SetBool("T3FootPilar1", true);
					intro = 1;
					//cameraAnimation.Play("CameraIdle");
				}
				if (intro == 1)
				{
					foreach (GameObject gameObject in pekerjaanFootingPilar)
					{
						gameObject.SetActive(false);
					}
					//RisikoController.instance.HitungDanaSisa ();
					uiManager.HUDGame.SetActive(true);



					if (persenPembangunan <= 100) //Jika belum selesai
					{						
						/*if (theBridge.transform.position.y < 7.5)
						{
							theBridge.transform.Translate(new Vector3(0, 0.5f, 0));
						}*/

						waktuTempuh += Time.deltaTime;
						//Object Off
						//Jika objectnya sudah di disable semua maka tidak di eksekusi


						//Pembangunan persentase
						persenPembangunan = (waktuTempuh / waktuPengerjaan[counterTahap]) * 100;
						float countUpWaktu;
						//countUpWaktu = waktuPengerjaan[counterTahap] - waktuTempuh;
						countUpWaktu = waktuTempuh;
						UIManager.instance.SetCountUpHari(Mathf.Floor(countUpWaktu).ToString());
						//OBject per persen
						int jumlahBagian = footingPilar1.Length + footingPilar2.Length;
						objectPerPersen = 100 / jumlahBagian;
						float bagian = persenPembangunan / objectPerPersen;
						//Kalkulasi object apa yang muncul di setiap persen
						if (persenPembangunan >= (counterBagian + 1) * objectPerPersen)
						{
							if (counterBagian < footingPilar1.Length)
							{
								CameraAnimationFalse();
								cameraAnimation.SetBool("T3FootPilar1", true);
								BangunBangunan(counterBagian, footingPilar1);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
							else if (counterBagian >= footingPilar1.Length)
							{
								CameraAnimationFalse();
								cameraAnimation.SetBool("T3FootPilar2", true);
								BangunBangunan(counterBagian - footingAbutmen1.Length, footingPilar2);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
						}
						counterBagian = (int)Mathf.Floor(bagian);


					}
					else
					{
						persenPembangunan = 0;
						waktuTempuh = 0;							
						deactivated = true;
						CameraAnimationFalse();
						cameraAnimation.SetBool("Done", true);
						SoundManager.instance.efxSource.Stop();
						kc.GenerateKesimpulan(rc.jumlahRisiko);
						uiManager.StopGame();

					}
				}
				break;
				#endregion
			case 3:
				#region Pekerjaan Wall Abutmen

				//nonaktif object
				while (deactivated)
				{
					RisikoController.instance.HitungDanaSisa();
					cameraAnimation.Play ("CameraIntro");
					#region Tahap 1 Done dan 2 dan 3
					foreach (GameObject gameObject in borePileAbutmen1)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePileAbutmen2)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePilePilar1)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePilePilar2)
					{
						gameObject.SetActive(true);
					}
					//Tahap 2

					foreach (GameObject gameObject in footingAbutmen1)
					{
						gameObject.SetActive(true);
					}
					foreach (GameObject gameObject in footingAbutmen2)
					{
						gameObject.SetActive(true);
					}

					//Tahap 3
					foreach (GameObject gameObject in footingPilar1)
					{
						gameObject.SetActive(true);
					}
					foreach (GameObject gameObject in footingPilar2)
					{
						gameObject.SetActive(true);
					}

                        #endregion


                        Jalan.SetActive(false);
					foreach (GameObject gameObject in pekerjaanBorePile)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanFootingAbutmen)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanFootingPilar)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanWallAbutmen)
					{
						gameObject.SetActive(true);
					}


					foreach (GameObject gameObject in abutmen1)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in abutmen2)
					{
						gameObject.SetActive(false);
					}


					foreach (GameObject gameObject in grider1)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in grider2)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in griderTengah)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in platJembatan)
					{
						gameObject.SetActive(false);
					}


					foreach (GameObject gameObject in finishing)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in platJembatan)
					{
						gameObject.SetActive(false);
					}
					//Tambahan
					foreach (GameObject gameObject in pilar1)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilar2)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilarHead1)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilarHead2)
					{
						gameObject.SetActive(false);
					}

					deactivated = false;
				}

				//kondisi nunggu kamera pindah
				if (cameraAnimation.GetCurrentAnimatorStateInfo(0).IsName("CameraIntro") && cameraAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && intro == 0)
				{
					CameraAnimationFalse();
					cameraAnimation.SetBool("T4WallAbutmen1", true);
					intro = 1;
					//cameraAnimation.Play("CameraIdle");
				}
				if (intro == 1)
				{
					foreach (GameObject gameObject in pekerjaanWallAbutmen)
					{
						gameObject.SetActive(false);
					}
					//RisikoController.instance.HitungDanaSisa ();
					uiManager.HUDGame.SetActive(true);



					if (persenPembangunan <= 100) //Jika belum selesai
					{						
						/*if (theBridge.transform.position.y < 7.5)
						{
							theBridge.transform.Translate(new Vector3(0, 0.5f, 0));
						}*/

						waktuTempuh += Time.deltaTime;
						//Object Off
						//Jika objectnya sudah di disable semua maka tidak di eksekusi


						//Pembangunan persentase
						persenPembangunan = (waktuTempuh / waktuPengerjaan[counterTahap]) * 100;
						float countUpWaktu;
						//countUpWaktu = waktuPengerjaan[counterTahap] - waktuTempuh;
						countUpWaktu = waktuTempuh;
						UIManager.instance.SetCountUpHari(Mathf.Floor(countUpWaktu).ToString());
						//OBject per persen
						int jumlahBagian = abutmen1.Length + abutmen2.Length;
						objectPerPersen = 100 / jumlahBagian;
						float bagian = persenPembangunan / objectPerPersen;
						//Kalkulasi object apa yang muncul di setiap persen
						if (persenPembangunan >= (counterBagian + 1) * objectPerPersen)
						{
							if (counterBagian < abutmen1.Length)
							{
								CameraAnimationFalse();
								cameraAnimation.SetBool("T4WallAbutmen1", true);
								BangunBangunan(counterBagian, abutmen1);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
							else if (counterBagian >= abutmen1.Length)
							{
								CameraAnimationFalse();
								cameraAnimation.SetBool("T4WallAbutmen2", true);
								BangunBangunan(counterBagian - abutmen1.Length, abutmen2);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                } 
						}
						counterBagian = (int)Mathf.Floor(bagian);


					}
					else
					{
						persenPembangunan = 0;
						waktuTempuh = 0;							
						deactivated = true;
						CameraAnimationFalse();
						cameraAnimation.SetBool("Done", true);
						SoundManager.instance.efxSource.Stop();
						kc.GenerateKesimpulan(rc.jumlahRisiko);
						uiManager.StopGame();

					}
				}

				break;
				#endregion
			case 4:
				#region Pekerjaan Pilar
                
				//nonaktif object
				while (deactivated)
				{
					RisikoController.instance.HitungDanaSisa();
					cameraAnimation.Play ("CameraIntro");
					#region Tahap 1 Done dan 2 dan 3, 4
					foreach (GameObject gameObject in borePileAbutmen1)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePileAbutmen2)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePilePilar1)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePilePilar2)
					{
						gameObject.SetActive(true);
					}
					//Tahap 2

					foreach (GameObject gameObject in footingAbutmen1)
					{
						gameObject.SetActive(true);
					}
					foreach (GameObject gameObject in footingAbutmen2)
					{
						gameObject.SetActive(true);
					}

					//Tahap 3
					foreach (GameObject gameObject in footingPilar1)
					{
						gameObject.SetActive(true);
					}
					foreach (GameObject gameObject in footingPilar2)
					{
						gameObject.SetActive(true);
					}

					//Tahap 4
					foreach (GameObject gameObject in abutmen1)
					{
						gameObject.SetActive(true);
					}
					foreach (GameObject gameObject in abutmen2)
					{
						gameObject.SetActive(true);
					}


					#endregion
                    Jalan.SetActive(false);
					foreach (GameObject gameObject in pekerjaanBorePile)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanFootingAbutmen)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanFootingPilar)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanWallAbutmen)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanPilar)
					{
						gameObject.SetActive(true);
					}



					foreach (GameObject gameObject in grider1)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in grider2)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in griderTengah)
					{
						gameObject.SetActive(false);
					}

					foreach (GameObject gameObject in platJembatan)
					{
						gameObject.SetActive(false);
					}


					foreach (GameObject gameObject in finishing)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in platJembatan)
					{
						gameObject.SetActive(false);
					}
					//Tambahan
					foreach (GameObject gameObject in pilar1)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilar2)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilarHead1)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilarHead2)
					{
						gameObject.SetActive(false);
					}

					deactivated = false;
				}

				//kondisi nunggu kamera pindah
				if (cameraAnimation.GetCurrentAnimatorStateInfo(0).IsName("CameraIntro") && cameraAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && intro == 0)
				{
					CameraAnimationFalse();
					cameraAnimation.SetBool("T5Pilar1", true);
					intro = 1;
					//cameraAnimation.Play("CameraIdle");
				}
				if (intro == 1)
				{
					foreach (GameObject gameObject in pekerjaanPilar)
					{
						gameObject.SetActive(false);
					}
					//RisikoController.instance.HitungDanaSisa ();
					uiManager.HUDGame.SetActive(true);



					if (persenPembangunan <= 100) //Jika belum selesai
					{						
						

						waktuTempuh += Time.deltaTime;
						//Object Off
						//Jika objectnya sudah di disable semua maka tidak di eksekusi


						//Pembangunan persentase
						persenPembangunan = (waktuTempuh / waktuPengerjaan[counterTahap]) * 100;
						float countUpWaktu;
						//countUpWaktu = waktuPengerjaan[counterTahap] - waktuTempuh;
						countUpWaktu = waktuTempuh;
						UIManager.instance.SetCountUpHari(Mathf.Floor(countUpWaktu).ToString());
						//OBject per persen
						int jumlahBagian = pilar1.Length + pilar2.Length;
						objectPerPersen = 100 / jumlahBagian;
						float bagian = persenPembangunan / objectPerPersen;
						//Kalkulasi object apa yang muncul di setiap persen
						if (persenPembangunan >= (counterBagian + 1) * objectPerPersen)
						{
							if (counterBagian < pilar1.Length)
							{
								CameraAnimationFalse();
								cameraAnimation.SetBool("T5Pilar1", true);
								BangunBangunan(counterBagian, pilar1);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
							else if (counterBagian >= pilar1.Length)
							{
								CameraAnimationFalse();
								cameraAnimation.SetBool("T5Pilar2", true);
								BangunBangunan(counterBagian - pilar1.Length, pilar2);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
						}
						counterBagian = (int)Mathf.Floor(bagian);


					}
					else
					{
						persenPembangunan = 0;
						waktuTempuh = 0;							
						deactivated = true;
						CameraAnimationFalse();
						cameraAnimation.SetBool("Done", true);
						SoundManager.instance.efxSource.Stop();
						kc.GenerateKesimpulan(rc.jumlahRisiko);
						uiManager.StopGame();

					}
				}

				break;

				#endregion
             case 5:
				#region Pekerjaan PilarHead

				//nonaktif object
				while (deactivated)
				{
					RisikoController.instance.HitungDanaSisa();
					cameraAnimation.Play ("CameraIntro");
					#region Tahap 1 Done dan 2 dan 3, 4, 5
					foreach (GameObject gameObject in borePileAbutmen1)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePileAbutmen2)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePilePilar1)
					{
						gameObject.SetActive(true);
					}

					foreach (GameObject gameObject in borePilePilar2)
					{
						gameObject.SetActive(true);
					}
					//Tahap 2

					foreach (GameObject gameObject in footingAbutmen1)
					{
						gameObject.SetActive(true);
					}
					foreach (GameObject gameObject in footingAbutmen2)
					{
						gameObject.SetActive(true);
					}

					//Tahap 3
					foreach (GameObject gameObject in footingPilar1)
					{
						gameObject.SetActive(true);
					}
					foreach (GameObject gameObject in footingPilar2)
					{
						gameObject.SetActive(true);
					}

					//Tahap 4
					foreach (GameObject gameObject in abutmen1)
					{
						gameObject.SetActive(true);
					}
					foreach (GameObject gameObject in abutmen2)
					{
						gameObject.SetActive(true);
					}

					//Tahap 5
					foreach (GameObject gameObject in pilar1)
					{
						gameObject.SetActive(true);
					}
					foreach (GameObject gameObject in pilar2)
					{
						gameObject.SetActive(true);
					}
					#endregion
                    Jalan.SetActive(false);
					foreach (GameObject gameObject in pekerjaanBorePile)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanFootingAbutmen)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanFootingPilar)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanWallAbutmen)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanPilar)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pekerjaanPilarHead)
					{
						gameObject.SetActive(true);
					}
                    
					foreach (GameObject gameObject in grider1)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in grider2)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in griderTengah)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in platJembatan)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in finishing)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in platJembatan)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilarHead1)
					{
						gameObject.SetActive(false);
					}
					foreach (GameObject gameObject in pilarHead2)
					{
						gameObject.SetActive(false);
					}

					deactivated = false;
				}

				//kondisi nunggu kamera pindah
				if (cameraAnimation.GetCurrentAnimatorStateInfo(0).IsName("CameraIntro") && cameraAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && intro == 0)
				{
					CameraAnimationFalse();
					cameraAnimation.SetBool("T6PilarHead1", true);
					intro = 1;
					//cameraAnimation.Play("CameraIdle");
				}
				if (intro == 1)
				{
					foreach (GameObject gameObject in pekerjaanPilarHead)
					{
						gameObject.SetActive(false);
					}
					//RisikoController.instance.HitungDanaSisa ();
					uiManager.HUDGame.SetActive(true);



					if (persenPembangunan <= 100) //Jika belum selesai
					{	
						waktuTempuh += Time.deltaTime;
						//Object Off
						//Jika objectnya sudah di disable semua maka tidak di eksekusi


						//Pembangunan persentase
						persenPembangunan = (waktuTempuh / waktuPengerjaan[counterTahap]) * 100;
						float countUpWaktu;
						//countUpWaktu = waktuPengerjaan[counterTahap] - waktuTempuh;
						countUpWaktu = waktuTempuh;
						UIManager.instance.SetCountUpHari(Mathf.Floor(countUpWaktu).ToString());
						//OBject per persen
						int jumlahBagian = pilarHead1.Length + pilarHead2.Length;
						objectPerPersen = 100 / jumlahBagian;
						float bagian = persenPembangunan / objectPerPersen;
						//Kalkulasi object apa yang muncul di setiap persen
						if (persenPembangunan >= (counterBagian + 1) * objectPerPersen)
						{
							if (counterBagian < pilarHead1.Length)
							{
								CameraAnimationFalse();
								cameraAnimation.SetBool("T6PilarHead1", true);
								BangunBangunan(counterBagian, pilarHead1);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
							else if (counterBagian >= pilarHead1.Length)
							{
								CameraAnimationFalse();
								cameraAnimation.SetBool("T6PilarHead2", true);
								BangunBangunan(counterBagian - pilarHead1.Length, pilarHead2);

                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
						}
						counterBagian = (int)Mathf.Floor(bagian);


					}
					else
					{
					persenPembangunan = 0;
					waktuTempuh = 0;							
					deactivated = true;
					CameraAnimationFalse();
					cameraAnimation.SetBool("Done", true);
					SoundManager.instance.efxSource.Stop();
					kc.GenerateKesimpulan(rc.jumlahRisiko);
					uiManager.StopGame();

					}
				}

				break;

                #endregion
                case 6:
                #region Pekerjaan Girder

                    //nonaktif object
                    while (deactivated)
                    {
                        Jalan.SetActive(false);
                        RisikoController.instance.HitungDanaSisa();
                        cameraAnimation.Play("CameraIntro");
                        //Tahap 1
                        foreach (GameObject gameObject in borePileAbutmen1)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in borePileAbutmen2)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in borePilePilar1)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in borePilePilar2)
                        {
                            gameObject.SetActive(true);
                        }
                        //Tahap 2

                        foreach (GameObject gameObject in footingAbutmen1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in footingAbutmen2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 3
                        foreach (GameObject gameObject in footingPilar1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in footingPilar2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 4
                        foreach (GameObject gameObject in abutmen1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in abutmen2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 5
                        foreach (GameObject gameObject in pilar1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in pilar2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 6
                        foreach (GameObject gameObject in pilarHead1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in pilarHead2)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in pekerjaanBorePile)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanFootingAbutmen)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanFootingPilar)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanWallAbutmen)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanPilar)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanPilarHead)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanGirder)
                        {
                            gameObject.SetActive(true);
                        }


                        foreach (GameObject gameObject in grider1)
                        {
                            gameObject.SetActive(false);
                        }

                        foreach (GameObject gameObject in grider2)
                        {
                            gameObject.SetActive(false);
                        }

                        foreach (GameObject gameObject in griderTengah)
                        {
                            gameObject.SetActive(false);
                        }

                        foreach (GameObject gameObject in platJembatan)
                        {
                            gameObject.SetActive(false);
                        }


                        foreach (GameObject gameObject in finishing)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in platJembatan)
                        {
                            gameObject.SetActive(false);
                        }

                        deactivated = false;
                    }

                    //kondisi nunggu kamera pindah
                    if (cameraAnimation.GetCurrentAnimatorStateInfo(0).IsName("CameraIntro") && cameraAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && intro == 0)
                    {
                        CameraAnimationFalse();
                        //camera tahap
                        cameraAnimation.SetBool("T7Girder1", true);
                        intro = 1;
                        //cameraAnimation.Play("CameraIdle");
                    }
                    if (intro == 1)
                    {
                        foreach (GameObject gameObject in pekerjaanGirder)
                        {
                            gameObject.SetActive(false);
                        }
                        //RisikoController.instance.HitungDanaSisa ();
                        uiManager.HUDGame.SetActive(true);



                        if (persenPembangunan <= 100) //Jika belum selesai
                        {
                            waktuTempuh += Time.deltaTime;
                            //Object Off
                            //Jika objectnya sudah di disable semua maka tidak di eksekusi


                            //Pembangunan persentase
                            persenPembangunan = (waktuTempuh / waktuPengerjaan[counterTahap]) * 100;
                            float countUpWaktu;
                            //countUpWaktu = waktuPengerjaan[counterTahap] - waktuTempuh;
                            countUpWaktu = waktuTempuh;
                            UIManager.instance.SetCountUpHari(Mathf.Floor(countUpWaktu).ToString());
                            //OBject per persen
                            int jumlahBagian = grider1.Length + grider2.Length + griderTengah.Length;
                            objectPerPersen = 100 / jumlahBagian;
                            float bagian = persenPembangunan / objectPerPersen;
                            //Kalkulasi object apa yang muncul di setiap persen
                            if (persenPembangunan >= (counterBagian + 1) * objectPerPersen)
                            {
                                if (counterBagian < grider1.Length)
                                {
                                    CameraAnimationFalse();
                                    cameraAnimation.SetBool("T7Girder1", true);
                                    BangunBangunan(counterBagian, grider1);
                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
                                else if (counterBagian < grider1.Length + griderTengah.Length)
                                {
                                    CameraAnimationFalse();
                                    cameraAnimation.SetBool("T7GirderTengah", true);
                                    BangunBangunan(counterBagian - grider1.Length, griderTengah);
                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
                                else if (counterBagian >= grider1.Length + griderTengah.Length)
                                {
                                    CameraAnimationFalse();
                                    cameraAnimation.SetBool("T7Girder2", true);
                                    BangunBangunan(counterBagian - (grider1.Length + griderTengah.Length), grider2);
                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }
                            }
                            counterBagian = (int)Mathf.Floor(bagian);


                        }
                        else
                        {
                            persenPembangunan = 0;
                            waktuTempuh = 0;
                            deactivated = true;
                            CameraAnimationFalse();
                            cameraAnimation.SetBool("Done", true);
                            SoundManager.instance.efxSource.Stop();
                            kc.GenerateKesimpulan(rc.jumlahRisiko);
                            uiManager.StopGame();

                        }
                    }

                    break;
                #endregion
                case 7:
                #region Pekerjaan Plat Jembatan

                    //nonaktif object
                    while (deactivated)
                    {
                        RisikoController.instance.HitungDanaSisa();
                        cameraAnimation.Play("CameraIntro");
                        Jalan.SetActive(false);
                        //Tahap 1
                        foreach (GameObject gameObject in borePileAbutmen1)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in borePileAbutmen2)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in borePilePilar1)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in borePilePilar2)
                        {
                            gameObject.SetActive(true);
                        }
                        //Tahap 2

                        foreach (GameObject gameObject in footingAbutmen1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in footingAbutmen2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 3
                        foreach (GameObject gameObject in footingPilar1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in footingPilar2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 4
                        foreach (GameObject gameObject in abutmen1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in abutmen2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 5
                        foreach (GameObject gameObject in pilar1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in pilar2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 6
                        foreach (GameObject gameObject in pilarHead1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in pilarHead2)
                        {
                            gameObject.SetActive(true);
                        }
                        //Tahap 7
                        foreach (GameObject gameObject in grider1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in grider2)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in griderTengah)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in pekerjaanBorePile)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanFootingAbutmen)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanFootingPilar)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanWallAbutmen)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanPilar)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanPilarHead)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanGirder)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanPlatJembatan)
                        {
                            gameObject.SetActive(true);
                        }


                        foreach (GameObject gameObject in platJembatan)
                        {
                            gameObject.SetActive(false);
                        }

                        foreach (GameObject gameObject in finishing)
                        {
                            gameObject.SetActive(false);
                        }

                        deactivated = false;
                    }

                    //kondisi nunggu kamera pindah
                    if (cameraAnimation.GetCurrentAnimatorStateInfo(0).IsName("CameraIntro") && cameraAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && intro == 0)
                    {
                        CameraAnimationFalse();
                        //camera tahap
                        cameraAnimation.SetBool("T8PlatJembatan", true);
                        intro = 1;
                        //cameraAnimation.Play("CameraIdle");
                    }
                    if (intro == 1)
                    {
                        foreach (GameObject gameObject in pekerjaanPlatJembatan)
                        {
                            gameObject.SetActive(false);
                        }
                        //RisikoController.instance.HitungDanaSisa ();
                        uiManager.HUDGame.SetActive(true);



                        if (persenPembangunan <= 100) //Jika belum selesai
                        {
                            waktuTempuh += Time.deltaTime;
                            //Object Off
                            //Jika objectnya sudah di disable semua maka tidak di eksekusi


                            //Pembangunan persentase
                            persenPembangunan = (waktuTempuh / waktuPengerjaan[counterTahap]) * 100;
                            float countUpWaktu;
                            //countUpWaktu = waktuPengerjaan[counterTahap] - waktuTempuh;
                            countUpWaktu = waktuTempuh;
                            UIManager.instance.SetCountUpHari(Mathf.Floor(countUpWaktu).ToString());
                            //OBject per persen
                            int jumlahBagian = platJembatan.Length;
                            objectPerPersen = 100 / jumlahBagian;
                            float bagian = persenPembangunan / objectPerPersen;
                            //Kalkulasi object apa yang muncul di setiap persen
                            if (persenPembangunan >= (counterBagian + 1) * objectPerPersen)
                            {
                                if (counterBagian < platJembatan.Length)
                                {
                                    CameraAnimationFalse();
                                    cameraAnimation.SetBool("T8PlatJembatan", true);
                                    BangunBangunan(counterBagian, platJembatan);
                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }


                            }
                            counterBagian = (int)Mathf.Floor(bagian);
                        }
                        else
                        {
                            persenPembangunan = 0;
                            waktuTempuh = 0;
                            deactivated = true;
                            CameraAnimationFalse();
                            cameraAnimation.SetBool("Done", true);
                            SoundManager.instance.efxSource.Stop();
                            kc.GenerateKesimpulan(rc.jumlahRisiko);
                            uiManager.StopGame();
                        }
                    }
                    break;
                #endregion
                case 8:
                #region Pekerjaan Finishing

                    //nonaktif object
                    while (deactivated)
                    {
                        RisikoController.instance.HitungDanaSisa();
                        cameraAnimation.Play("CameraIntro");
                        //Tahap 1
                        foreach (GameObject gameObject in borePileAbutmen1)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in borePileAbutmen2)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in borePilePilar1)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in borePilePilar2)
                        {
                            gameObject.SetActive(true);
                        }
                        //Tahap 2

                        foreach (GameObject gameObject in footingAbutmen1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in footingAbutmen2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 3
                        foreach (GameObject gameObject in footingPilar1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in footingPilar2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 4
                        foreach (GameObject gameObject in abutmen1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in abutmen2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 5
                        foreach (GameObject gameObject in pilar1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in pilar2)
                        {
                            gameObject.SetActive(true);
                        }

                        //Tahap 6
                        foreach (GameObject gameObject in pilarHead1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in pilarHead2)
                        {
                            gameObject.SetActive(true);
                        }
                        //Tahap 7
                        foreach (GameObject gameObject in grider1)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in grider2)
                        {
                            gameObject.SetActive(true);
                        }
                        foreach (GameObject gameObject in griderTengah)
                        {
                            gameObject.SetActive(true);
                        }
                        //Tahap 8
                        foreach (GameObject gameObject in platJembatan)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in pekerjaanBorePile)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanFootingAbutmen)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanFootingPilar)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanWallAbutmen)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanPilar)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanPilarHead)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanGirder)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanPlatJembatan)
                        {
                            gameObject.SetActive(false);
                        }
                        foreach (GameObject gameObject in pekerjaanFinishing)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in finishing)
                        {
                            gameObject.SetActive(false);
                        }
                        Jalan.SetActive(false);
                        deactivated = false;
                    }

                    //kondisi nunggu kamera pindah
                    if (cameraAnimation.GetCurrentAnimatorStateInfo(0).IsName("CameraIntro") && cameraAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && intro == 0)
                    {
                        CameraAnimationFalse();
                        //camera tahap
                        cameraAnimation.SetBool("T9Finishing", true);
                        intro = 1;
                        //cameraAnimation.Play("CameraIdle");
                    }
                    if (intro == 1)
                    {
                        foreach (GameObject gameObject in pekerjaanFinishing)
                        {
                            gameObject.SetActive(false);
                        }
                        //RisikoController.instance.HitungDanaSisa ();
                        uiManager.HUDGame.SetActive(true);



                        if (persenPembangunan <= 100) //Jika belum selesai
                        {
                            waktuTempuh += Time.deltaTime;
                            //Object Off
                            //Jika objectnya sudah di disable semua maka tidak di eksekusi


                            //Pembangunan persentase
                            persenPembangunan = (waktuTempuh / waktuPengerjaan[counterTahap]) * 100;
                            float countUpWaktu;
                            //countUpWaktu = waktuPengerjaan[counterTahap] - waktuTempuh;
                            countUpWaktu = waktuTempuh;
                            UIManager.instance.SetCountUpHari(Mathf.Floor(countUpWaktu).ToString());
                            //OBject per persen
                            int jumlahBagian = finishing.Length;
                            objectPerPersen = 100 / jumlahBagian;
                            float bagian = persenPembangunan / objectPerPersen;
                            //Kalkulasi object apa yang muncul di setiap persen
                            if (persenPembangunan >= (counterBagian + 1) * objectPerPersen)
                            {
                                if (counterBagian < finishing.Length)
                                {
                                    CameraAnimationFalse();
                                    cameraAnimation.SetBool("T9Finishing", true);
                                    BangunBangunan(counterBagian, finishing);
                                    if (rc.warningSign.activeSelf)
                                    {
                                        SoundManager.instance.PlaySingle(rc.warningSfx);
                                    }
                                    else
                                    {
                                        SoundManager.instance.RandomizeSfx(buildsfx1, buildsfx2);
                                    }
                                }

                            }
                            counterBagian = (int)Mathf.Floor(bagian);
                        }
                        else
                        {
                            Jalan.SetActive(true);

                            persenPembangunan = 0;
                            waktuTempuh = 0;
                            deactivated = true;
                            finishing[0].SetActive(false);
                            CameraAnimationFalse();
                            cameraAnimation.SetBool("Done", true);
                            SoundManager.instance.efxSource.Stop();
                            kc.GenerateKesimpulan(rc.jumlahRisiko);
                            uiManager.StopGame();
                        }
                    }
                    break;
                #endregion


                default:
                    counterTahap = 0;
                break;
            }
        }
        else
        {
			generaterisk = true;
			intro = 0;
        }

    }

    public void BangunSemua()
    {
        Jalan.SetActive(true);


        for (int i = 0; i < abutmen1.Length; i++)
        {
            if (i == abutmen1.Length - 1 )
            {
                abutmen1[i].SetActive(true);
            }
            else
            {
                abutmen1[i].SetActive(false);

            }

        }

        for (int i = 0; i < abutmen2.Length; i++)
        {
			if (i == abutmen2.Length - 1)
            {
                abutmen2[i].SetActive(true);
            }
            else
            {
                abutmen2[i].SetActive(false);

            }

        }

        //foreach (GameObject gameObject in abutmen1)
        //{
        //    gameObject.SetActive(false);
        //}
        //foreach (GameObject gameObject in abutmen2)
        //{
        //    gameObject.SetActive(false);
        //}

        for (int i = 0; i < footingPilar1.Length; i++)
        {
			if (i == footingPilar1.Length - 1)
            {
                footingPilar1[i].SetActive(true);
            }
            else
            {
                footingPilar1[i].SetActive(false);

            }

        }

        for (int i = 0; i < footingPilar2.Length; i++)
        {
			if (i == footingPilar2.Length - 1)
            {
                footingPilar2[i].SetActive(true);
            }
            else
            {
                footingPilar2[i].SetActive(false);

            }

        }

        //foreach (GameObject gameObject in footingPilar1)
        //{
        //    gameObject.SetActive(false);
        //}
        //foreach (GameObject gameObject in footingPilar2)
        //{
        //    gameObject.SetActive(false);
        //}

        for (int i = 0; i < grider1.Length; i++)
        {
			if (i == grider1.Length- 1)
            {
                grider1[i].SetActive(true);
            }
            else
            {
                grider1[i].SetActive(false);

            }

        }

        for (int i = 0; i < grider2.Length; i++)
        {
			if (i == grider2.Length- 1)
            {
                grider2[i].SetActive(true);
            }
            else
            {
                grider2[i].SetActive(false);

            }

        }

        //foreach (GameObject gameObject in grider1)
        //{
        //    gameObject.SetActive(false);
        //}

        //foreach (GameObject gameObject in grider2)
        //{
        //    gameObject.SetActive(false);
        //}

        for (int i = 0; i < griderTengah.Length; i++)
        {
			if (i == griderTengah.Length- 1)
            {
                griderTengah[i].SetActive(true);
            }
            else
            {
                griderTengah[i].SetActive(false);

            }

        }

        for (int i = 0; i < platJembatan.Length; i++)
        {
			if (i == platJembatan.Length- 1)
            {
                platJembatan[i].SetActive(true);
            }
            else
            {
                platJembatan[i].SetActive(false);

            }

        }

        //foreach (GameObject gameObject in griderTengah)
        //{
        //    gameObject.SetActive(false);
        //}

        //foreach (GameObject gameObject in platJembatan)
        //{
        //    gameObject.SetActive(false);
        //}
        for (int i = 0; i < borePileAbutmen2.Length; i++)
        {
			if (i == borePileAbutmen2.Length- 1)
            {
                borePileAbutmen2[i].SetActive(true);
            }
            else
            {
                borePileAbutmen2[i].SetActive(false);

            }

        }

        for (int i = 0; i < borePileAbutmen1.Length; i++)
        {
			if (i == borePileAbutmen1.Length- 1)
            {
                borePileAbutmen1[i].SetActive(true);
            }
            else
            {
                borePileAbutmen1[i].SetActive(false);

            }

        }

        //foreach (GameObject gameObject in borePileAbutmen1)
        //{
        //    gameObject.SetActive(false);
        //}

        //foreach (GameObject gameObject in borePileAbutmen2)
        //{
        //    gameObject.SetActive(false);
        //}

        for (int i = 0; i < borePilePilar1.Length; i++)
        {
			if (i == borePilePilar1.Length- 1)
            {
                borePilePilar1[i].SetActive(true);
            }
            else
            {
                borePilePilar1[i].SetActive(false);

            }

        }

        for (int i = 0; i < borePilePilar2.Length; i++)
        {
			if (i == borePilePilar2.Length- 1)
            {
                borePilePilar2[i].SetActive(true);
            }
            else
            {
                borePilePilar2[i].SetActive(false);

            }

        }

        //foreach (GameObject gameObject in borePilePilar1)
        //{
        //    gameObject.SetActive(false);
        //}

        //foreach (GameObject gameObject in borePilePilar2)
        //{
        //    gameObject.SetActive(false);
        //}

        for (int i = 0; i < platJembatan.Length; i++)
        {
			if (i == platJembatan.Length- 1)
            {
                platJembatan[i].SetActive(true);
            }
            else
            {
                platJembatan[i].SetActive(false);

            }

        }

        for (int i = 0; i < finishing.Length; i++)
        {
			if (i == finishing.Length- 1)
            {
                finishing[i].SetActive(true);
            }
            else
            {
                finishing[i].SetActive(false);

            }

        }

        //foreach (GameObject gameObject in finishing)
        //{
        //    gameObject.SetActive(false);
        //}
        //foreach (GameObject gameObject in platJembatan)
        //{
        //    gameObject.SetActive(false);
        //}

        for (int i = 0; i < pilar2.Length; i++)
        {
			if (i == pilar2.Length- 1)
            {
                pilar2[i].SetActive(true);
            }
            else
            {
                pilar2[i].SetActive(false);

            }

        }

        for (int i = 0; i < pilar1.Length; i++)
        {
			if (i == pilar1.Length- 1)
            {
                pilar1[i].SetActive(true);
            }
            else
            {
                pilar1[i].SetActive(false);

            }

        }

        //Tambahan
        //foreach (GameObject gameObject in pilar1)
        //{
        //    gameObject.SetActive(false);
        //}
        //foreach (GameObject gameObject in pilar2)
        //{
        //    gameObject.SetActive(false);
        //}

        for (int i = 0; i < pilarHead1.Length; i++)
        {
			if (i == pilarHead1.Length- 1)
            {
                pilarHead1[i].SetActive(true);
            }
            else
            {
                pilarHead1[i].SetActive(false);

            }

        }

        for (int i = 0; i < pilarHead2.Length; i++)
        {
			if (i == pilarHead2.Length- 1)
            {
                pilarHead2[i].SetActive(true);
            }
            else
            {
                pilarHead2[i].SetActive(false);

            }

        }

        //foreach (GameObject gameObject in pilarHead1)
        //{
        //    gameObject.SetActive(false);
        //}
        //foreach (GameObject gameObject in pilarHead2)
        //{
        //    gameObject.SetActive(false);
        //}

        for (int i = 0; i < footingAbutmen1.Length; i++)
        {
			if (i == footingAbutmen1.Length- 1)
            {
                footingAbutmen1[i].SetActive(true);
            }
            else
            {
                footingAbutmen1[i].SetActive(false);

            }

        }

        for (int i = 0; i < footingAbutmen2.Length; i++)
        {
			if (i == footingAbutmen2.Length- 1)
            {
                footingAbutmen2[i].SetActive(true);
            }
            else
            {
                footingAbutmen2[i].SetActive(false);

            }

        }

        //foreach (GameObject gameObject in footingAbutmen1)
        //{
        //    gameObject.SetActive(false);
        //}
        //foreach (GameObject gameObject in footingAbutmen2)
        //{
        //    gameObject.SetActive(false);
        //}
    }
    private void BangunBangunan(int i, GameObject[] bangunanDiBangun) {



        if (i == 0)
        {
            i = 0;

            bangunanDiBangun[i].SetActive(true);
            for (int o = bangunanDiBangun.Length - 1; o > i; o--)
            {
                bangunanDiBangun[o].SetActive(false);
            }
        }
        else if (i < bangunanDiBangun.Length)
        {


            for (int o = i - 1; o > 0; o--)
            {
                bangunanDiBangun[o].SetActive(false);
            }
            bangunanDiBangun[i].SetActive(true);
            for (int o = bangunanDiBangun.Length - 1; o > i; o--)
            {
                bangunanDiBangun[o].SetActive(false);
            }
        }
        else if (i >= bangunanDiBangun.Length)
        {


            for (int o = i - 1; o > 0; o--)
            {

                bangunanDiBangun[o].SetActive(false);
            }
            i = bangunanDiBangun.Length - 1;
            bangunanDiBangun[i].SetActive(true);
        }

        else
        {

        }
    }
    private void HancurkanBangunan(GameObject[] bangunanYangDiHancurkan)
    {
        foreach (GameObject bangunan in bangunanYangDiHancurkan)
        {
            bangunan.SetActive(false);
        }
    }
    //Membuat suatu interface agar dapat menambah waktu pengerjaan akitbat risiko terjadi
    //Set Multipy waktu pengerjaan
	public void IncreaseWaktuPengerjaan(float DampakWaktu)
	{
		int counterTahap = PlayerPrefs.GetInt   ("counterTahap");

		waktuPengerjaan[counterTahap] += DampakWaktu;

		persenPembangunan = (waktuTempuh / waktuPengerjaan[counterTahap]) * 100;
		float bagian = persenPembangunan / objectPerPersen;
		counterBagian = (int)Mathf.Floor(bagian);

		switch (counterTahap) 
		{
		case 0:
            #region Tahap1
            if (counterBagian < borePileAbutmen1.Length)
			{
			    BangunBangunan(counterBagian, borePileAbutmen1);
			    HancurkanBangunan(borePileAbutmen2);
			}
			else if (counterBagian >= (borePileAbutmen1.Length + borePileAbutmen2.Length + borePilePilar1.Length))
			{
				BangunBangunan(counterBagian - (borePileAbutmen1.Length + borePileAbutmen2.Length + borePilePilar1.Length), borePilePilar2);

			}
			else if (counterBagian >= (borePileAbutmen1.Length + borePileAbutmen2.Length))
			{
				BangunBangunan(counterBagian - (borePileAbutmen1.Length + borePileAbutmen2.Length), borePilePilar1);
				HancurkanBangunan(borePilePilar2);

			}
			else if (counterBagian >= borePileAbutmen1.Length)
			{

				HancurkanBangunan(borePilePilar1);

				BangunBangunan(counterBagian - borePileAbutmen1.Length, borePileAbutmen2);

			}
			break;
                #endregion
        case 1:
            #region Tahap2
            if (counterBagian < footingAbutmen1.Length)
            {
                BangunBangunan(counterBagian, footingAbutmen1);
                HancurkanBangunan(footingAbutmen2);

            }
            else if (counterBagian >= footingAbutmen1.Length)
            {
                BangunBangunan(counterBagian - footingAbutmen1.Length, footingAbutmen2);
            }
            break;
        #endregion

        case 2:
            #region Tahap3
            if (counterBagian < footingPilar1.Length)
            {
                BangunBangunan(counterBagian, footingPilar1);
                HancurkanBangunan(footingPilar2);
            }
            else if (counterBagian >= footingPilar2.Length)
            {
                BangunBangunan(counterBagian - footingPilar1.Length, footingPilar2);

            }

            break;
            #endregion

        case 3:
             #region Tahap4

            if (counterBagian < abutmen1.Length)
            {
                BangunBangunan(counterBagian, abutmen1);
                HancurkanBangunan(abutmen2);
            }
            else if (counterBagian >= abutmen1.Length)
            {
                BangunBangunan(counterBagian - abutmen1.Length, abutmen2);

            }
                break;
            #endregion

        case 4:
            #region Tahap5
            if (counterBagian < pilar1.Length)
			{
				BangunBangunan(counterBagian, pilar1);
				HancurkanBangunan(pilar2);
			}
			else if (counterBagian >= pilar1.Length)
			{
				BangunBangunan(counterBagian-pilar1.Length, pilar2);

			}
			break;
            #endregion 
        case 5:
            #region Tahap6
            if (counterBagian < pilarHead1.Length)
            {
                BangunBangunan(counterBagian, pilarHead1);
                HancurkanBangunan(pilarHead2);
            }
            else if (counterBagian >= pilarHead1.Length)
            {
                BangunBangunan(counterBagian - pilarHead1.Length, pilarHead2);

            }
            break;
            #endregion
        case 6:
            #region Tahap7

                if (counterBagian < grider1.Length)
                {
                    HancurkanBangunan(griderTengah);
                    BangunBangunan(counterBagian, grider1);
                   
                }
                else if (counterBagian < grider1.Length + griderTengah.Length)
                {
                    HancurkanBangunan(grider2);

                    BangunBangunan(counterBagian - grider1.Length, griderTengah);
                    
                }
                else if (counterBagian >= grider1.Length + griderTengah.Length)
                {
                    
                    BangunBangunan(counterBagian - (grider1.Length + griderTengah.Length), grider2);
                   
                }
                #endregion
                break;
		case 7:
            #region Tahap8

                if (counterBagian < platJembatan.Length)
                {
                    
                    BangunBangunan(counterBagian, platJembatan);
                }
                #endregion
                break;
		case 8:
            #region Tahap9
            if (counterBagian < finishing.Length)
            {
                BangunBangunan(counterBagian, finishing);
            }
            #endregion
                break;

		}

	}

    //untuk mengambil persen pembangunan
    public int  GetProgress()
    {
        return (int) Mathf.Floor(persenPembangunan);
    }

	public void CameraAnimationFalse(){
		cameraAnimation.SetBool("Done", false);
		cameraAnimation.SetBool("Abutmen1", false);
		cameraAnimation.SetBool("Abutmen2", false);
		cameraAnimation.SetBool("Pilar1", false);
		cameraAnimation.SetBool("Pilar2", false);
        cameraAnimation.SetBool("T1End", false); 

        cameraAnimation.SetBool("T2Abutmen1", false);
		cameraAnimation.SetBool("T2Abutmen2", false);
		cameraAnimation.SetBool("T3FootPilar1", false);
		cameraAnimation.SetBool("T3FootPilar2", false);
		cameraAnimation.SetBool("T4WallAbutmen1", false);
		cameraAnimation.SetBool("T4WallAbutmen2", false);
		cameraAnimation.SetBool("T5Pilar1", false);
		cameraAnimation.SetBool("T5Pilar2", false);
		cameraAnimation.SetBool("T6PilarHead1", false);
		cameraAnimation.SetBool("T6PilarHead2", false);

        cameraAnimation.SetBool("T7Girder1", false);
        cameraAnimation.SetBool("T7GirderTengah", false);
        cameraAnimation.SetBool("T7Girder2", false);

        cameraAnimation.SetBool("T8PlatJembatan", false);
        cameraAnimation.SetBool("T9Finishing", false);


     

    }
}
 