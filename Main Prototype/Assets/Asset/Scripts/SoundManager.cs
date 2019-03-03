/**using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    //Sound Manager
    //Music BG sesuai dengan toggle
    //SFX sesuai dengan toggle
    //Menyimpan keadaan toggle setiap di rubah
    //Menyesuaikan keadaan toggle dengan data yang disimpan
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour 
{
	public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
	public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
	public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
	public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
	public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
	public Sprite OnMusic, OffMusic, OnSfx, OffSfx;
	public Button BtnMusic, BtnSfx;
	public Toggle ToggleMusic, ToggleSfx;
	public int SFXStat, MusicStat;

	void Awake ()
	{
		//Check if there is already an instance of SoundManager
		if (instance == null)
			//if not, set it to this.
			instance = this;
		//If instance already exists:
		else if (instance != this)
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		
		if (!PlayerPrefs.HasKey ("SFXStat")) 
		{
			PlayerPrefs.SetInt ("SFXStat",1);
		}
	

		if (!PlayerPrefs.HasKey ("MusicStat")) 
		{
			PlayerPrefs.SetInt ("MusicStat",1);
		}

		SFXStat = PlayerPrefs.GetInt ("SFXStat");
		MusicStat = PlayerPrefs.GetInt ("MusicStat");
		ToggleSfx.interactable = false;
		ToggleMusic.interactable = false;
		if (SFXStat == 1) {
			//sfx jalan
			efxSource.mute = false;
			BtnSfx.image.sprite = OnSfx;
			ToggleSfx.isOn = true;
		} else {
			//sfx mute
			efxSource.mute = true;
			BtnSfx.image.sprite = OffSfx;
			ToggleSfx.isOn = false;
		}

		if (MusicStat == 1) {
			//music jalan
			musicSource.mute = false;
			BtnMusic.image.sprite = OnMusic;
			ToggleMusic.isOn = true;
		} else {
			//music mute
			musicSource.mute = true;
			BtnMusic.image.sprite = OffMusic;
			ToggleMusic.isOn = false;

		}
		ToggleSfx.interactable = true;
		ToggleMusic.interactable = true;

	}
	//Used to play single sound clips.

	public void PlayMusic(AudioClip clip)
	{
		musicSource.clip = clip;

		musicSource.Play ();
	}

	public void RandomizeMusic (params AudioClip[] clips)
	{
		//Generate a random number between 0 and the length of our array of clips passed in.
		int randomIndex = Random.Range(0, clips.Length);

		//Choose a random pitch to play back our clip at between our high and low pitch ranges.
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		//Set the pitch of the audio source to the randomly chosen pitch.
		musicSource.pitch = randomPitch;

		//Set the clip to the clip at our randomly chosen index.
		musicSource.clip = clips[randomIndex];

		//Play the clip.
		musicSource.Play();
	}

    public void PlayClickSFX()
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = UIManager.instance.clickSFX;

        //Play the clip.
        efxSource.Play();
    }
	public void PlaySingle(AudioClip clip)
	{
		//Set the clip of our efxSource audio source to the clip passed in as a parameter.
		efxSource.clip = clip;

		//Play the clip.
		efxSource.Play ();
	}


	//RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
	public void RandomizeSfx (params AudioClip[] clips)
	{
		//Generate a random number between 0 and the length of our array of clips passed in.
		int randomIndex = Random.Range(0, clips.Length);

		//Choose a random pitch to play back our clip at between our high and low pitch ranges.
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		//Set the pitch of the audio source to the randomly chosen pitch.
		efxSource.pitch = randomPitch;

		//Set the clip to the clip at our randomly chosen index.
		efxSource.clip = clips[randomIndex];

		//Play the clip.
		efxSource.Play();
	}






	public void MuteMusic(){
		if (ToggleMusic.isOn == false) {
			BtnMusic.image.sprite = OffMusic;
			//ToggleMusic.isOn = false;
			musicSource.mute = true;

			PlayerPrefs.SetInt ("MusicStat",0);
            MusicStat = 0;

		} else {
			BtnMusic.image.sprite = OnMusic;
			//ToggleMusic.isOn = true;
			musicSource.mute = false;

			PlayerPrefs.SetInt ("MusicStat",1);
            MusicStat = 1;


        }
    }

	public void MuteSfx(){
		if (ToggleSfx.isOn == false) {
			BtnSfx.image.sprite = OffSfx;
			//ToggleSfx.isOn = false;
			PlayerPrefs.SetInt ("SFXStat",0);
			efxSource.mute = true;
            SFXStat = 0;


        } else {
			BtnSfx.image.sprite = OnSfx;
			//ToggleSfx.isOn = true;
			PlayerPrefs.SetInt ("SFXStat",1);
            SFXStat = 1;
            efxSource.mute = false;

        }
    }


	public void EventMusic(){
		ToggleMusic.isOn = !ToggleMusic.isOn;
	}
	public void EventSFX(){
		ToggleSfx.isOn = !ToggleSfx.isOn;

	}

}
