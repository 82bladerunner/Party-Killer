using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickedObject : MonoBehaviour, IPointerClickHandler
{
    private void Start()
    {
        AddPhysics2DRaycaster();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

        if(eventData.pointerCurrentRaycast.gameObject.tag == "Killer") {
            GameController.KillerFound = true; 
            Debug.Log("Killer found!");
            }
        else if(eventData.pointerCurrentRaycast.gameObject.tag == "NonKiller"){
            GameController.guessCounter++;
            Debug.Log("WRONG!! BITCHHH!!!" + GameController.guessCounter + "/3");
        }
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }
}

