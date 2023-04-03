using dbrewka.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : ManagerObject
{
    public Shop shopScript;
    [SerializeField] private Image crosshair;
    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private float crosshairTransitionTime = .1f;
    public KeyValueList<ElementState, CrosshairState> crosshairStates;

    public override void Start()
    {
        base.Start();
    }

    public void SetCrosshairState(ElementState state)
    {
        CrosshairState _cs = crosshairStates.FindValueWithKey(state);
        crosshair.color = Color.Lerp(crosshair.color, _cs.color, Time.deltaTime * (1 / crosshairTransitionTime));
        crosshair.rectTransform.localScale = Vector3.Lerp(crosshair.rectTransform.localScale, _cs.size * Vector3.one, Time.deltaTime * (1 / crosshairTransitionTime));
    }

    public void SetInteractionText(string text)
    {
        interactionText.text = text;
    }

    public enum ElementState
    {
        active,
        regular,
        disabled,
    }

    //shop
    public void showShop()
    {
        shopScript.TurnOn();
    }
}