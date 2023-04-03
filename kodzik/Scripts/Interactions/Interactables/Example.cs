using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    public bool Interact(InteractionManager interactor) {
        print("Interacted");
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        return true;
    }

    public GameObject getGameObject() {
        return this.gameObject;
    }
}
