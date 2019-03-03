using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RisikoObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Toggle btnRisikoOn;
    public Text namaRisiko;
    public string tipeRespon;//Both, Avoid, Mitigate
    public float HargaAvoid;
    public float HargaMitigate;
    public string dampakWaktu;
    public float dampakDana;
    public string StatusRespon;
    public Scrollbar responBar;
    public float danaRespon;//Dana Yang di alokasikan 
    public Text Rp;
    public Text TextDanaRespon;
    public string NamaKartu;
    private GameObject kartuRisiko;
    public Image SliderTanggapan;
    public GameObject TanggapanSlidetoActive;
    public float Probabilitas;
    public Image summaryRespon;//Summary utk kesimpulan dan overview
    public Text textSummaryRespon;//Untuk Kesimpulan dan overview
    public Sprite[] sumImage;//List Image Summary Avoid0 mitigate1 accept2
    public Sprite[] BGRisiko; //Ganjil0Genap1Selected not ganjil2genap3selected
    public bool genapKah; //true genap, false ganjil
    public GameObject btnAvoid, btnMitigate, btnAccept;


	//variable untuk Kartu MM dan K3
	public bool MMkah, K3Kah;
	/*
	Algoritma Pendeteksi ON OFF Kartu MM dan K3
	Spec: Write: Mengubah nilai boolean MM dan K3 di risiko object, Ketika MM dan K3 ON maka effeknya masing2 adalah harga respon akan naik 1% dari harga respon avoid.
		Read: Ketika reveal kartu maka menyesuaikan nilai boolean MM dan K3 dari risiko tersebut
		1. Ketika Reveal Kartu Maka diperlukan parameter tambahan yaitu instance dari object tersebut, digunakan untuk mengetahui nilai boolean MM dan K3,
		serta reference untuk mengubah nilai MM dan K3 dari object Risiko tersebut
		
		2. Ketika Object Risiko di aktifkan maka container kartu baru keliatan (Alphanya baru 255, dan interactablenya ON), tika risiko tidak di respon maka Alphanya 100 dan interactabelnya off
		
		3. Ketika kartu di tekan dan ON maka mengubah nilai MM dan K3 pada object Risiko sesuai kartu Risiko yang lagi muncul dan mengubah nilai harga respon
		
		
	*/

    //Membuat delegate
    //public delegate void UpdateRespon(); 
    //public static event UpdateRespon OnUpdateRespon; //event ketika delegate terjadi
    //color of not selected
    Color notSelected = new Color32(210, 210, 210, 255);
    //color of selected
    Color Selected = new Color32(170, 170, 170, 255);
    Color NormalColor = new Color32(255,255,255,255);
    Color OverviewNotSelected = new Color32(120, 120, 120, 255);


    public bool analisisOn = true;

    #region  Khusus Analisis
    //For Analisis
    public int NilaiProbabilitas, NilaiDampak, PxI;
    public Text textNilaiProbabilitas, textNilaiDampak, textPxI;
    public int nomorUrut = 0;
    //Gameobject untuk Analisis
    public GameObject InterfaceDampak, InterfaceProbabilitas, InterfacePxI;
    public GameObject Analisis, Respon;

    public void AnalisisOffOnlyForOverview()
    {
        IntefaceAnalisisOff();

        if (btnRisikoOn.isOn)
        {

            gameObject.GetComponent<Image>().color = Selected;

            Rp.color = Color.white;
            TextDanaRespon.color = Color.white;
            CheckTipeRespond();
            if (!summaryRespon.IsActive())
            {
                TanggapanSlidetoActive.SetActive(true);

            }
            else
            {
                TanggapanSlidetoActive.SetActive(false);
            }
        }
        else
        {
            gameObject.GetComponent<Image>().color = notSelected;
            danaRespon = 0;
            TextDanaRespon.text = danaRespon.ToString("N0", new CultureInfo("id-ID"));
            Rp.color = Color.gray;
            StatusRespon = "";
            TextDanaRespon.color = Color.gray;
            TanggapanSlidetoActive.SetActive(false);
            RisikoController.instance.ContainerOffNoValueChange();


            RisikoController.instance.listRisikoTerpilih.Remove(NamaKartu);
        }
        RisikoController.instance.HitungTotalDana();
        if (StatusRespon.ToLower() == "avoid")
        {

            RisikoController.instance.BtnK3.isOn = false;
            RisikoController.instance.BtnMM.isOn = false;
            RisikoController.instance.BtnK3.interactable = false;
            RisikoController.instance.BtnMM.interactable = false;
        }
    }

    public void AnalisisOff()
    {
        analisisOn = false;
        CheckRisikoOn();
    }

    public void AnalisisOn()
    {
        analisisOn = true;
        CheckRisikoOn();
    }
    public void IntefaceAnalisisOff()
    {
        Analisis.SetActive(false);
        Respon.SetActive(true);
        InterfaceDampak.SetActive(false);
        InterfaceProbabilitas.SetActive(false);
        InterfacePxI.SetActive(false);
        btnRisikoOn.interactable = true;
    }

    public void IntefaceAnalasisOn()
    {
        btnRisikoOn.interactable = false;
        Analisis.SetActive(true);
        Respon.SetActive(false);
        InterfaceDampak.SetActive(true);
        InterfaceProbabilitas.SetActive(true);
        InterfacePxI.SetActive(true);
    }

    public void ResetNilaiAnalisis()
    {
        NilaiDampak = 1;
        NilaiProbabilitas = 1;
        textNilaiProbabilitas.text = NilaiProbabilitas.ToString();
        textNilaiDampak.text = NilaiDampak.ToString();
        HitungPxI();
    }

    public void HitungPxI()
    {
        PxI = NilaiProbabilitas * NilaiDampak;
        textPxI.text =  PxI.ToString();
    }

    public void NextProbabilitas()
    {
        NilaiProbabilitas++;
        if (NilaiProbabilitas > 5)
        {
            NilaiProbabilitas = 5;
        }
        else if (NilaiProbabilitas <= 1)
        {
            NilaiProbabilitas = 1;
        }
		if (NilaiProbabilitas > 0 && NilaiProbabilitas <= 5)
		{
			HitungPxI();
		}
        textNilaiProbabilitas.text = NilaiProbabilitas.ToString();

    }

    public void PrevProbabilitas()
    {
        NilaiProbabilitas--;
        if (NilaiProbabilitas > 5)
        {
            NilaiProbabilitas = 5;
        }
        else if (NilaiProbabilitas <= 1)
        {
            NilaiProbabilitas = 1;
        }
		if (NilaiProbabilitas > 0 && NilaiProbabilitas <= 5)
		{
			HitungPxI();
		}
        textNilaiProbabilitas.text = NilaiProbabilitas.ToString();

    }

    public void NextDampak()
    {
        NilaiDampak++;
        if (NilaiDampak > 5)
        {
            NilaiDampak = 5;
        }
        else if (NilaiDampak <= 1)
        {
            NilaiDampak = 1;
		}
		if (NilaiDampak > 0 && NilaiDampak <= 5)
		{
			HitungPxI();
			textNilaiDampak.text = NilaiDampak.ToString();
		}
	}

    public void PrevDampak()
    {
        NilaiDampak--;
        
        if (NilaiDampak > 5)
        {
            NilaiDampak = 5;
        }
        else if (NilaiDampak <= 1)
        {
            NilaiDampak = 1;
        }
		if (NilaiDampak > 0 && NilaiDampak <= 5)
		{
			HitungPxI();
			textNilaiDampak.text = NilaiDampak.ToString();
		}
    }
    #endregion

    public void Start()
    {
    }

	public void CekDanaKartuK3MM()
	{
		if (K3Kah) {
			if (tipeRespon == "Mitigate") {
				danaRespon += 0.01f * HargaMitigate;
			} else {
				danaRespon += 0.01f * HargaAvoid;
			}

		}


		if (MMkah) {
			if (tipeRespon == "Mitigate") {
				danaRespon += 0.01f * HargaMitigate;
			} else {
				danaRespon += 0.01f * HargaAvoid;
			}
		}
	}

    public void RevealKartu()
    {
		RisikoController.instance.RevealKartu(NamaKartu, HargaMitigate, HargaAvoid, this);

    }
    public void CheckRisikoOn()
    {//Ganjil0Genap1Selected ||  ganjil2genap3notselected
      
        if (analisisOn)
        {
            IntefaceAnalasisOn();
            if (genapKah)
            {
                gameObject.GetComponent<Image>().sprite = BGRisiko[1];

            }
            else
            {
                gameObject.GetComponent<Image>().sprite = BGRisiko[0];

            }
            gameObject.GetComponent<Image>().color = NormalColor;

        }
        else
        { //ketika analsisi Off
            IntefaceAnalisisOff();

            if (btnRisikoOn.isOn)
            {

                gameObject.GetComponent<Image>().color = Selected;
                
                Rp.color = Color.white;
                TextDanaRespon.color = Color.white;
                CheckTipeRespond();
                RisikoController.instance.RevealKartu(NamaKartu, HargaMitigate, HargaAvoid, this);
                if (!summaryRespon.IsActive())
                {
                    TanggapanSlidetoActive.SetActive(true);

                }
                else {
                    TanggapanSlidetoActive.SetActive(false); 
                }
            }
            else
            {
                gameObject.GetComponent<Image>().color = notSelected;
                danaRespon = 0;
                TextDanaRespon.text = danaRespon.ToString("N0", new CultureInfo("id-ID"));
                Rp.color = Color.gray;
                StatusRespon = "";
                TextDanaRespon.color = Color.gray;
                TanggapanSlidetoActive.SetActive(false);
                RisikoController.instance.ContainerOnlyOFF();
                

                RisikoController.instance.listRisikoTerpilih.Remove(NamaKartu);
            }
            RisikoController.instance.HitungTotalDana();
        }
        if (StatusRespon.ToLower() == "avoid")
        {

            RisikoController.instance.BtnK3.isOn = false;
            RisikoController.instance.BtnMM.isOn = false;
            RisikoController.instance.BtnK3.interactable = false;
            RisikoController.instance.BtnMM.interactable = false;
        }
    }
    public void CheckTipeRespond()
    {
        //if (OnUpdateRespon != null)
        //    OnUpdateRespon();

        if (btnRisikoOn.isOn)
        {
            if (tipeRespon.ToLower().Equals("both"))
            {

                if (responBar.value == 0)
                { //Accept
                  //Dana
                    danaRespon = 0;
                    CekDanaKartuK3MM();
                    //Warna
                    SliderTanggapan.color = Color.red;
                    StatusRespon = "accept";
                }
                else if (responBar.value == 0.5)
                { //Mitigate
                    danaRespon = HargaMitigate;
                    CekDanaKartuK3MM();
                    SliderTanggapan.color = Color.yellow;
                    StatusRespon = "mitigate";

                }
                else if (responBar.value == 1)
                { //Avoid
                    danaRespon = HargaAvoid;
                    CekDanaKartuK3MM();
                    SliderTanggapan.color = Color.green;
                    StatusRespon = "avoid";
                }

                TextDanaRespon.text = danaRespon.ToString("N0", new CultureInfo("id-ID"));
                btnAvoid.SetActive(true);//Avoid
                btnMitigate.SetActive(true);//Mitigate
                btnAccept.SetActive(true);//Accept

            }
            else if (tipeRespon.ToLower().Equals("avoid"))
            {

                if (responBar.value < 0.33)
                { //Accept
                    StatusRespon = "accept";

                    //Dana
                    danaRespon = 0;
                    CekDanaKartuK3MM();
                    //Warna
                    SliderTanggapan.color = Color.red;
                }
                else if (responBar.value < 0.67)
                {
                    StatusRespon = "avoid";

                    responBar.value = 1f;
                }
                else if (responBar.value == 1)
                { //Mitigate
                    StatusRespon = "avoid";

                    danaRespon = HargaAvoid;
                    CekDanaKartuK3MM();
                    SliderTanggapan.color = Color.green;
                }

                TextDanaRespon.text = danaRespon.ToString("N0", new CultureInfo("id-ID"));
                btnAvoid.SetActive(true);//Avoid
                btnMitigate.SetActive(false);//Mitigate
                btnAccept.gameObject.SetActive(true);//Accept

            }
            else if (tipeRespon.ToLower().Equals("mitigate"))
            {
                //3step
                //0-0.33 accept
                //0.34-0.66 mitigate
                //0.67-1 avoid

                if (responBar.value == 0)
                { //Accept
                    StatusRespon = "accept";
                    //Dana
                    danaRespon = 0;
                    CekDanaKartuK3MM();
                    //Warna
                    SliderTanggapan.color = Color.red;
                }
                else if (responBar.value == 0.5)
                { //Mitigate
                    StatusRespon = "mitigate";

                    danaRespon = HargaMitigate;
                    CekDanaKartuK3MM();
                    SliderTanggapan.color = Color.yellow;
                }
                else if (responBar.value > 0.67)
                {
                    StatusRespon = "mitigate";

                    responBar.value = 0.5f;
                }
                TextDanaRespon.text = danaRespon.ToString("N0", new CultureInfo("id-ID"));
                btnAvoid.SetActive(false);//Avoid
                btnMitigate.SetActive(true);//Mitigate
                btnAccept.SetActive(true);//Accept
            }
            else
            {
                Debug.Log("tipe Respon salah");
            }
            RisikoController.instance.HitungTotalDana();

        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //gameObject.GetComponent<Image>().color = Selected;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //gameObject.GetComponent<Image>().color = notSelected;

    }
    public void InteractOFF()
    {
        btnRisikoOn.interactable = false;
    }
    public void InteractON()
    {
        btnRisikoOn.interactable = true;

    }
    public void ResetOverview()
    {

        InteractON();
        responBar.value = 0.0f;
        danaRespon = 0;
        SliderTanggapan.color = Color.red;
        TextDanaRespon.text = danaRespon.ToString("N0", new CultureInfo("id-ID"));
        StatusRespon = "";
        //menetukan Isi Respon
        summaryRespon.gameObject.SetActive(false);

        btnRisikoOn.isOn = false;

        //Disable SliderTanggapan
        TanggapanSlidetoActive.SetActive(false);
        //disable button


    }
    public void BackOverview()
    {
        //Summary di OFF
        summaryRespon.gameObject.SetActive(false);
        //slider tanggapanON
        if (btnRisikoOn.isOn)
        {
            TanggapanSlidetoActive.SetActive(true);
            gameObject.GetComponent<Image>().color = Selected;

        }
        else
        {
            gameObject.GetComponent<Image>().color = notSelected;

        }

        InteractON();
    }
    public void GetOverview()
    {



        //menetukan Isi Respon
        summaryRespon.gameObject.SetActive(true);
        summaryRespon.GetComponent<Image>().enabled = true;

        if (btnRisikoOn.isOn)
        {
            switch (StatusRespon.ToLower())
            {
                case "avoid":
                    summaryRespon.sprite = sumImage[0];
                    textSummaryRespon.text = "Avoid";
                    break;

                case "mitigate":
                    summaryRespon.sprite = sumImage[1];
                    textSummaryRespon.text = "Mitigate";
                    break;

                case "accept":
                    summaryRespon.sprite = sumImage[2];
                    textSummaryRespon.text = "Accept";
                    break;

                default:
                    summaryRespon.sprite = sumImage[2];
                    textSummaryRespon.text = "Accept";
                    StatusRespon = "accept";
                    break;
            }
        }
        else
        {//Tidak di select
            textSummaryRespon.text = "Tidak di Respon";
            summaryRespon.enabled = false;
            gameObject.GetComponent<Image>().color = OverviewNotSelected;
        }
        //Disable SliderTanggapan
        TanggapanSlidetoActive.SetActive(false);
         
    }

    public void CekStatusRespon()
    {
        if (StatusRespon.ToLower() == "avoid")
        {
            responBar.value = 1f;

        }
        else if (StatusRespon.ToLower() == "mitigate")
        {
            responBar.value = 0.5f;

        }
        else if (StatusRespon.ToLower() == "accept")
        {
            responBar.value = 0f;

        }
    }
    public void CekUrutanBG(int sisaMod)
    {//Ganjil0Genap1Selected ||  ganjil2genap3notselected
        if (sisaMod == 0)//genap
        {
            genapKah = true;
            //if (btnRisikoOn.isOn)
            //{
            //    gameObject.GetComponent<Image>().sprite = BGRisiko[1];
            //}
            //else
            //{
            //    gameObject.GetComponent<Image>().sprite = BGRisiko[3];
            //}
        }
        else if (sisaMod == 1)//ganjil
        {
            genapKah = false;
            //if (btnRisikoOn.isOn)
            //{
            //    gameObject.GetComponent<Image>().sprite = BGRisiko[0];
            //}
            //else
            //{
            //    gameObject.GetComponent<Image>().sprite = BGRisiko[2];
            //}
        }
    }
    //Sebuah object risiko bisa :
    /*
     Diambil harga Respon
     mengupdate harga Respon
     nilainya bisa di tampilkan GUIRespon

    Jika Slider di ganti akan mengganti statusRespon

    jika toggle On maka slider ON dan RP. jadi putih, text dana jadi putih
    jika tidak maka dana Risiko 0
     */


}
