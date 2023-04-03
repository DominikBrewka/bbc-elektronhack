using UnityEngine;

public class ManagerObject : MonoBehaviour {
    public static GameStateManager gameStateManger;

    public virtual void Start() {
        Register();
    }

    protected void Register() {
        gameStateManger.RegisterManager(this);
    }
}