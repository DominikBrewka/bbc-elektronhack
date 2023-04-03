using System.Collections.Generic;

public class GameStateManager : ManagerObject
{
    // This class controls the game's state and ensures that important game events happen in the right order.
    // There should ever be only one GameStateManager at a time, first initialized at the splash screen.
    //! GameStateManager is an essential class that should never be restarted, destroyed, or stopped.
    //! GameStateManager should be the first manager initialized in the game.

    public List<ManagerObject> managers;

    void Awake() {
        DontDestroyOnLoad(this);
        gameStateManger = this;
    }

    public override void Start() {
        base.Start();
    }

    public void RegisterManager(ManagerObject manager) {
        gameStateManger = this;

        print("Registered a new Manager! " + manager);
        managers.Add(manager);
    }

    public ManagerObject GetManager<T>() {
        // Find a Manager in managers
        ManagerObject found = null;
        foreach (var item in managers) {
            if (item.GetType() == typeof(T)) {
                found = item;
            }
        }

        return found;
    }
}
