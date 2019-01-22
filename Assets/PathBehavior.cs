using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBehavior : MonoBehaviour {
    public GameObject target;
    LinkedList<Vector3> path;
    bool play;
	// Use this for initialization
	void Start () {
        path = new LinkedList<Vector3>();
        play = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (play)
        {
            if(path.Count == 0)
            {
                play = false;
            } else
            {
                gameObject.transform.position = path.First.Value;
                path.RemoveFirst();
            }
        }
        else
        {
            path.AddLast(target.transform.position);
            if(path.Count > 60 * 5)
            {
                path.RemoveFirst();
            }
        }
        if (Input.GetKey(KeyCode.P))
        {
            play = true;
        }
    }

}
