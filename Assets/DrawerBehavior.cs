using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerBehavior : MonoBehaviour {
	float timer;
	Vector3 lastDrawPosition;
    Vector3 position;
	public PrimitiveType brush_shape = PrimitiveType.Sphere;
	public Color brush_color = Color.red;
	public float brush_size = 0.25f;
    public bool draw = false;
    public GameObject brush_model;

    public AudioSource brush_sound;
    public AudioManager audio_m;

	private double drawStep = 0.01;

	// Use this for initialization
	void Start () {
		timer = 0;
		lastDrawPosition = gameObject.transform.position;
	}

    // Update is called once per frame
    void FixedUpdate() {
        position = gameObject.transform.position;
        brush_model.transform.position = position;
        if (timer > 0.2)
        {
            if (draw)
            {
                GameObject curve_elt = GameObject.CreatePrimitive(brush_shape);
                curve_elt.transform.position = position;
                if (brush_shape == PrimitiveType.Cylinder || brush_shape == PrimitiveType.Cube)
                {
                    curve_elt.transform.localScale = new Vector3(brush_size, Vector3.Distance(position, lastDrawPosition) / 2, brush_size);
                    Vector3 pos = Vector3.Lerp(position, lastDrawPosition, (float)0.5);
                    curve_elt.transform.position = pos;
                    curve_elt.transform.up = (position - lastDrawPosition);
                }
                else
                {
                    curve_elt.transform.localScale *= brush_size;
                }
                MeshRenderer mr = curve_elt.GetComponent<MeshRenderer>();
                Material newmaterial = new Material(Shader.Find("Standard"));
                newmaterial.color = brush_color;
                mr.material = newmaterial;
                audio_m.PlaySound(brush_sound.clip);
            }
            timer = 0;
            lastDrawPosition = position;
        }
        timer += Time.deltaTime;
    }
}
