using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RadialMenu : MonoBehaviour {

    public RadialButton buttonPrefab;
    public Text label;
    public RadialButton selected;
    GameObject player;


    public void Start() {
        Cursor.visible = (Cursor.visible == false) ? true : false;
        Cursor.lockState = CursorLockMode.None;
        player = GameObject.Find("RigidBodyFPSController");
        player.GetComponent<UserExtendedFlycam>().lockRotation = true;
    }

	public void SpawnButtons (Interactable obj) {
        StartCoroutine(AnimateButtons(obj));
	}

    IEnumerator AnimateButtons(Interactable obj) {
  for (int i = 0; i < obj.options.Length; i++)
        {
            RadialButton newButton = Instantiate(buttonPrefab) as RadialButton;
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / obj.options.Length) * i;
            float xpos = Mathf.Sin(theta);
            float ypos = Mathf.Cos(theta);
            newButton.transform.localPosition = new Vector3(xpos, ypos, 0f) * 100f;
            newButton.circle.color = obj.options[i].color;
            newButton.icon.sprite = obj.options[i].sprite;
            newButton.title = obj.options[i].title;
            newButton.myMenu = this;
            newButton.Anim();
            newButton.item = obj.options[i].prefab;
            yield return new WaitForSeconds(0.08f);
        }
    }


    void Update() {
        if (Input.GetMouseButtonUp(0)) {
            if (selected) {
               
                GameObject obj = Instantiate(selected.item);
                obj.transform.SetParent(player.transform, false);
                obj.transform.localPosition = new Vector3(0f, 0f, 5f);
                Debug.Log(selected.title + " was selected");
            }
            Cursor.visible = (Cursor.visible == false) ? true : false;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<UserExtendedFlycam>().lockRotation = false;
            Destroy(gameObject);
        }
    }
}
