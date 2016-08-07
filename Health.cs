using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Health : MonoBehaviour {
	
	public Slider healthBarSlider;  //reference for slider
	
	//Check if player enters/stays on the fire
	void OnTriggerStay(Collider other){
		//if player triggers fire object and health is greater than 0
		if(other.gameObject.name=="Health" && healthBarSlider.value>0){
			healthBarSlider.value -=.011f;  //reduce health
		}
	}
}