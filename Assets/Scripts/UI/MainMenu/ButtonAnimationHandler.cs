using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimationHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
	//Animator anim;
	//AudioSource source;
	//// Start is called before the first frame update
	//void Start()
	//{
	//    anim = GetComponent<Animator>();
	//    source = GetComponent<AudioSource>();
	//}

	public void OnSelect(BaseEventData eventData)
	{
		eventData.selectedObject.GetComponent<Animator>()?.SetTrigger("MoveForward");
		//anim.SetTrigger("MoveForward");
		//source.Play();
	}

	public void OnDeselect(BaseEventData eventData)
	{
		eventData.selectedObject.GetComponent<Animator>()?.SetTrigger("MoveBack");
		//anim.SetTrigger("MoveBack");
	}
}
