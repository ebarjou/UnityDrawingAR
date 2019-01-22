using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	// Use this for initialization
	public static AudioManager instance = null;
	public AudioSource m_soundStream;

	void Awake() {
		if(instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else
		{
			Destroy (gameObject);
		}
}

	// Update is called once per frame
	void Update () {
	}

	public void PlaySound(AudioClip soundClipToPlay) {
		m_soundStream.clip = soundClipToPlay;
		m_soundStream.Play ();
	}
}
