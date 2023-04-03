using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;

    void Start()
    {
        SpawnPlayer();
    }

    public PlayerManager SpawnPlayer() {
        PlayerManager _p = Instantiate(playerPrefab, transform.position, playerPrefab.transform.rotation).GetComponent<PlayerManager>();
        return _p;
    }

    public PlayerManager SpawnPlayer(Vector3 position) {
        PlayerManager _p = Instantiate(playerPrefab, position, playerPrefab.transform.rotation).GetComponent<PlayerManager>();
        return _p;  
    }

}
