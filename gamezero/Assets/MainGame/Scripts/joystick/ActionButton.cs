using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler {

	public Image btnImage;
	public Image actionhitBoxImage;
	public GameObject player;
	private Complete.TankShooting playerActionObject;
	private Color btnColor;
	private Vector2 OnDownPos;
    public bool shootbuttondown = false;
    public bool shootbuttonup = true;


    // Use this for initializataion
    void Start () {
		OnDownPos = Vector2.zero;
		//btnColor = btnImage.color;
		//btnImage.color = Color.clear;
}

	public void onRelease(){
		//actions here

		if (playerActionObject == null) {
			//playerActionObject = player.GetComponent<PlayerAction> ();
            //playerActionObject = player.GetComponent<Complete.TankShooting>();
        }
        //playerActionObject.action ();
        //playerActionObject.Fire();

        //Debug.Log("release" + Time.time.ToString());
	}

	public void onDown(){
        //actions here

        //Debug.Log("down" + Time.time.ToString());

    }


	public virtual void OnPointerDown(PointerEventData ped){
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(actionhitBoxImage.rectTransform,
				ped.position,
				ped.pressEventCamera,
				out OnDownPos)) 
		{
			//move btnImage to onDownPos
			//btnImage.transform.position = actionhitBoxImage.transform.position + new Vector3 (OnDownPos.x, OnDownPos.y);
			//make Image visible
			//btnImage.color = btnColor;
            this.shootbuttonup = false;
            this.shootbuttondown = true;
			this.onDown ();
		}
	}
	public virtual void OnPointerUp(PointerEventData ped){
		//btnImage.color = Color.clear;
        this.shootbuttonup = true;
        this.shootbuttondown = false;
        this.onRelease ();
	}
	// Update is called once per frame
	void Update () {

	}
}
