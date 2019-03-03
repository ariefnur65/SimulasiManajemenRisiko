using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class texttest : MonoBehaviour {

	public Text Accept, Mitigate, Avoid;
	//public Image panelAccept, panelMitigate, panelAvoid;
	// Use this for initialization
	void Start () {
		Accept.text = "Bojo Galak\nSayang \nVia Vallen\nArief\nAyod\nGondang";
		Mitigate.text = "OAOE\nOAOE\nOOOOOOOOOOOOOOO\nArief\nAyod\nGondang";
		Avoid.text = "Arief\nAyod\nGondang";

		/**float tinggi1 = panelAccept.rectTransform.rect.height;
		float tinggi1 = Accept.preferredHeight;
		//Vector3 panel1 = panelAccept.lossyScale;
		//Debug.Log (panel1);
		panelMitigate.rectTransform.anchoredPosition = new Vector3 (0, -tinggi1 -70, 0);
		//float panel2 = panelMitigate.lossyScale.y;
		float tinggi2 = tinggi1 + Mitigate.preferredHeight;
		//panelAvoid.localPosition = new Vector3 (0, -panel1 - panel2 - 20, 0);
		panelAvoid.rectTransform.anchoredPosition = new Vector3 (0, -tinggi2 -140, 0);
		*/
	}
}
