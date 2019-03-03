using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshowscript : MonoBehaviour {

	Image myImageComponent;
	public Sprite gambar1; //Drag your first sprite here in inspector.
	public Sprite gambar2; //Drag your second sprite here in inspector.
	public Sprite gambar3;
	public Sprite gambar4;
	public Sprite gambar5;
	public Sprite gambar6;
	public Sprite gambar7;
	public Sprite gambar8;
	public int x;

	// Use this for initialization
	void Start () {
		myImageComponent = GetComponent<Image>(); //Our image component is the one attached to this gameObject.

        x = 0;
        SetNextImage();

    }
	
	public void SetNextImage() //method to set our first image
	{
		x = x + 1;
		switch (x)
		{
		case 1:
			myImageComponent.sprite = gambar1;
			break;
		case 2:
			myImageComponent.sprite = gambar2;
			break;
		case 3:
			myImageComponent.sprite = gambar3;
			break;
		case 4:
			myImageComponent.sprite = gambar4;
			break;
		case 5:
			myImageComponent.sprite = gambar5;
			break;
		case 6:
			myImageComponent.sprite = gambar6;
			break;
		case 7:
			myImageComponent.sprite = gambar7;
			break;
		case 8:
			myImageComponent.sprite = gambar8;
			break;
		default:
			x = 1;
			myImageComponent.sprite = gambar1;
			break;
		}
	}

	public void SetPrevImage(){
		x = x - 1;
		switch (x)
		{
		case 1:
			myImageComponent.sprite = gambar1;
			break;
		case 2:
			myImageComponent.sprite = gambar2;
			break;
		case 3:
			myImageComponent.sprite = gambar3;
			break;
		case 4:
			myImageComponent.sprite = gambar4;
			break;
		case 5:
			myImageComponent.sprite = gambar5;
			break;
		case 6:
			myImageComponent.sprite = gambar6;
			break;
		case 7:
			myImageComponent.sprite = gambar7;
			break;
		case 8:
			myImageComponent.sprite = gambar8;
			break;
		default:
			x = 8;
			myImageComponent.sprite = gambar8;
			break;
		}
	}
}

