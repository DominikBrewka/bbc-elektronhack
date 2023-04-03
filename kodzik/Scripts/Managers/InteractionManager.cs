using UnityEngine;

public class InteractionManager : ManagerObject
{
    [SerializeField] Transform _interactionPoint;
    [SerializeField] float _interactionRange = 1.5f;
    [SerializeField] LayerMask _interactableMask;

    public KeyCode interactionKey = KeyCode.E;

    public UIManager playerUI;
    IInteractable target;

    public override void Start()
    {
        base.Start();
        playerUI = (UIManager)gameStateManger.GetManager<UIManager>();
    }

    void Update()
    {
        bool raycast = Physics.Raycast(_interactionPoint.position, _interactionPoint.forward, out var hitData, _interactionRange, _interactableMask);
        if (raycast){
            if (target == null) target = hitData.collider.gameObject.GetComponent<IInteractable>();
            playerUI.SetCrosshairState(UIManager.ElementState.active);
            playerUI.SetInteractionText(target.InteractionPrompt);

            if (Input.GetKeyDown(interactionKey)) { 
                target.Interact(this);
            }
        } else {
            target = null;
            playerUI.SetCrosshairState(UIManager.ElementState.regular);
            playerUI.SetInteractionText(" ");
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(_interactionPoint.position, _interactionPoint.TransformDirection(Vector3.forward * _interactionRange));
    }
}
