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

            //TODO
            //killer rengarenk olsun
            //oyun bitti yazısı
            //skor hesabı

            Debug.Log("Killer found!");
            AudioManager.Instance.PlaySFX("CrowdYay");

            }
        else if(eventData.pointerCurrentRaycast.gameObject.tag == "NonKiller"){
            GameController.guessCounter--;
            Debug.Log("WRONG!!!" + GameController.guessCounter + " guesses left.");
            AudioManager.Instance.PlaySFX("Wrong");
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

