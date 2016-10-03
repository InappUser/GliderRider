using UnityEngine;
using System.Collections;


public class Interactable : MonoBehaviour {

    [System.Serializable]
    public class Action {
        public Color color;
        public Sprite sprite;
        public string title;
        public GameObject prefab;
    }

    public string title;
public Action[] options;



    public void Start() {
        if (title == "" || title == null)
        {
            title = gameObject.name;
        }
    }

void OnMouseDown() {
        RadialMenuSpawner.ins.SpawnMenu(this);

}


}
