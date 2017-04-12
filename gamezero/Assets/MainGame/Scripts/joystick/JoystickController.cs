using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler {

	public Image bgImage;
	public Image joystickImage;
	public Image hitBoxImage;
	private Color jsColor;
	private Color bgColor;
	private Vector2 OnDownPos;
	private Vector3 direct;
	private object synclock = new object();
	private bool active = false;

	// Use this for initializataion
	void Start () {
		OnDownPos = Vector2.zero;
		bgColor = bgImage.color;
		jsColor = joystickImage.color;
		bgImage.color = Color.clear;
		joystickImage.color = Color.clear;
		direct = Vector3.zero;
	}

	//returns new Vector3 object
	public Vector3 getDirection(){
		Vector3 ret3 = Vector3.zero;

		lock (synclock) {
			ret3 = new Vector3 (this.direct.x, this.direct.y, this.direct.z);
		}
		return ret3;
	}

	public virtual void OnDrag(PointerEventData ped){
		Vector2 pos = Vector2.zero;
		if (this.active) {
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(hitBoxImage.rectTransform,
				   ped.position,
				   ped.pressEventCamera,
				   out pos)) {
				if (hitBoxImage.rectTransform.rect.Contains (pos)) {
					Vector3 direction = new Vector3 (); //vector relative to centre of image
					direction.x = pos.x - OnDownPos.x; 
					direction.y = pos.y - OnDownPos.y;
					float width = bgImage.rectTransform.rect.width / 2;

					//direction max magnitude = 1
					if (direction.magnitude > width) {
						direction.Normalize ();
					} else {
						direction.x = direction.x / width;
						direction.y = direction.y / width;
					}
					
					//modify speed
					lock (synclock) {
						this.direct.x = direction.x;
						this.direct.z = direction.y;
						this.direct.y = 0;
					}
					//update joystick image
					direction.x = direction.x * width + bgImage.rectTransform.position.x;
					direction.y = direction.y * width + bgImage.rectTransform.position.y;
					this.joystickImage.rectTransform.position = direction;
				} else { //drag out of hitbox range
					this.OnPointerUp (ped);
				}
			}
		}

	}
	public virtual void OnPointerDown(PointerEventData ped){
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(hitBoxImage.rectTransform,
			    ped.position,
			    ped.pressEventCamera,
				out OnDownPos)) 
		{
			this.active = true;
			//move bgImage and joystickImage to onDownPos
			joystickImage.transform.position = new Vector3 (OnDownPos.x, OnDownPos.y);
			bgImage.transform.position = new Vector3 (OnDownPos.x, OnDownPos.y);

			//make Images visible
			bgImage.color = bgColor;
			joystickImage.color = jsColor;

		}
	}
	public virtual void OnPointerUp(PointerEventData ped){
		//reset speed
		lock (synclock) {
			this.direct = Vector3.zero;
		}
		this.active = false;
		bgImage.color = Color.clear;
		joystickImage.color = Color.clear;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
