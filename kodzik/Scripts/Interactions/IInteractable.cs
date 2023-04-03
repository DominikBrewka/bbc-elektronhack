using UnityEngine;

public interface IInteractable
{

    public GameObject getGameObject();
    public string InteractionPrompt { get; }
    public bool Interact(InteractionManager interactor);
}
