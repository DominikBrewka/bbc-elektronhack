using TMPro;
using UnityEngine;

public class PlayerManager : ManagerObject
{
    public Vector3 START_POS = new Vector3(0, 5, 0);
    public bool DEBUG_MODE = true;
    public bool AUTO_BHOP = false;
	public bool canOpenDoor = true;
	public TMP_Text canOpenDoorText;
	public bool cursorLock= false;

	public override void Start() {
		canOpenDoorText = GameObject.Find("CANOPEN").GetComponent<TMP_Text>();
		canOpenDoorText.text = "Mozesz otworzyc drzwi";
        base.Start();
    }

	void Update() {
		
		if(Input.GetMouseButtonDown(0)) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		if (cursorLock) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}




        if (!DEBUG_MODE) return;

        // restart pos
        if (Input.GetKeyDown(KeyCode.Backspace)) { transform.position = START_POS; }
	}
}