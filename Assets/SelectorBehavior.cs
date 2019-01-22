using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorBehavior : MonoBehaviour {
	private string[] Entries = new string[]{"Brush shape", "Brush color", "Brush size", "Background"};
	private string[][] SubEntries = {new string[]{"Sphere", "Cube", "Cylinder"},
									new string[]{"Red", "Green", "Blue"},
									new string[]{"0.1", "0.25", "0.5"},
									new string[]{"Plain Box", "Cam Feed"}};
	private int[] SubMenuEntry;
	private int MenuEntry;
	private int NbEntries;
	public Text UItext;
	private bool isClicked;

	public DrawerBehavior db;
    private bool drawing;

    public AudioManager audio_m;
    public AudioSource entry_sound;
    public AudioSource subentry_sound;

    public GameObject background;

	// Use this for initialization
	void Start () {
		MenuEntry = 0;
		NbEntries = Entries.Length;
		SubMenuEntry = new int[SubEntries.Length];
		isClicked = false;
        drawing = false;
		buildMenu();
	}

	void buildMenu(){
		string content = "";
		for(int i = 0; i < NbEntries; ++i){
			content += (i==MenuEntry?" > ":"   ") + Entries[i] + " : " + SubEntries[i][SubMenuEntry[i]] + "\n";
		}
		UItext.text = content;
	}

	// Update is called once per frame
	void Update() {

		if (Input.GetMouseButton(0))
        {
            db.draw = true;
        } else
        {
            db.draw = false;
        }

        if (Input.GetMouseButton(1))
        {
            if(!isClicked)
                SubMenuEntry[MenuEntry] = (SubMenuEntry[MenuEntry]+1)%SubEntries[MenuEntry].Length;
			buildMenu();
			isClicked = true;
			updateBrush(MenuEntry,SubMenuEntry[MenuEntry]);
			audio_m.PlaySound(subentry_sound.clip);
        }

		if (Input.GetMouseButton(3) || Input.mouseScrollDelta.y > 0)
        {
			if(!isClicked && MenuEntry > 0)
				MenuEntry--;
			buildMenu();
			isClicked = true;
			audio_m.PlaySound(entry_sound.clip);
        }

		if (Input.GetMouseButton(4) || Input.mouseScrollDelta.y < 0)
        {
			if(!isClicked && MenuEntry < NbEntries-1)
				MenuEntry++;
			buildMenu();
			isClicked = true;
			audio_m.PlaySound(entry_sound.clip);
        }

		if(!Input.GetMouseButton(1) && !Input.GetMouseButton(3) && !Input.GetMouseButton(4)){
			isClicked = false;
		}
	}

void updateBrush(int entry, int subentry)
{
	switch (entry)
	{
			case 0:
				switch(subentry)
				{
					case 0:
					db.brush_shape = PrimitiveType.Sphere;
					break;
					case 1:
					db.brush_shape = PrimitiveType.Cube;
					break;
					case 2:
					db.brush_shape = PrimitiveType.Cylinder;
					break;
				}
			break;
			case 1:
				switch(subentry)
				{
					case 0:
					db.brush_color = Color.red;
					break;
					case 1:
					db.brush_color = Color.green;
					break;
					case 2:
					db.brush_color = Color.blue;
					break;
				}
			break;
			case 2:
				switch(subentry)
				{
					case 0:
					db.brush_size = 0.1f;
					break;
					case 1:
					db.brush_size = 0.25f;
					break;
					case 2:
					db.brush_size = 0.5f;
					break;
				}
				break;
            case 3:
                switch(subentry)
                {
                case 0:
                background.SetActive(true);
                break;
                case 1:
                background.SetActive(false);
                break;
                }
			break;
	}
}
}
