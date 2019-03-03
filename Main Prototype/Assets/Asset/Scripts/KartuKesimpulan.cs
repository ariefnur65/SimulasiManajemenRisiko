using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
//using System;
using UnityEngine;
using UnityEngine.UI;

public class KartuKesimpulan : MonoBehaviour {

    //keperluan kartu
    public RisikoController rc;
    public string namaKartu;
    public float hargaMitigate, hargaAvoid;
    public Button next, prev;
    public RisikoObject objectMuncul;
    public HappendRisk risikoTerjadi;
    public RisikoTerpilih risikoTerpilih;
    public Button btnInformasi;
    public bool happendKah;
    public void ChooseMethod()
    {
        if (happendKah)
        {        //digunakan jika menu kesimpulan di panggil dari menu list pekerjaan

            TampilKartuWithHappend();
        }
        else
        {
            TampilKartu();
        }
    }

    public void TampilKartuWithTerpilih()
    {        //digunakan jika menu kesimpulan di panggil dari menu list pekerjaan

        rc.RevealKartu(namaKartu, hargaMitigate, hargaAvoid, risikoTerpilih);
        next.gameObject.SetActive(false);
        prev.gameObject.SetActive(false);
        rc.BtnK3.interactable = false;
        rc.BtnMM.interactable = false;
    }

    public void TampilKartuWithHappend()
    {        //digunakan jika menu kesimpulan di panggil dari menu list pekerjaan

        rc.RevealKartu(namaKartu, hargaMitigate, hargaAvoid, risikoTerjadi);
        next.gameObject.SetActive(false);
        prev.gameObject.SetActive(false);
        rc.BtnK3.interactable = false;
        rc.BtnMM.interactable = false;
    }
	public void TampilKartu(){
		//Debug.Log ("Nama Kartu : " + namaKartu + ", Mitigate : " + hargaMitigate + ", Avoid : " + hargaAvoid);
		rc.RevealKartu (namaKartu, hargaMitigate, hargaAvoid, objectMuncul);
		next.gameObject.SetActive (false);
		prev.gameObject.SetActive (false);
		rc.BtnK3.interactable = false;
		rc.BtnMM.interactable = false;

	}
	
}
