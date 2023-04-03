using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;
    [SerializeField] ElevatorScript elevator;

    public bool Interact(InteractionManager interactor) {
        elevator.GoUp();
        return true;
    }

    public GameObject getGameObject() {
        return this.gameObject;
    }
}
