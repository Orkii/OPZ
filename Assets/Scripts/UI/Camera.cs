using UnityEngine;

public class Camera : MonoBehaviour {

    [SerializeField]
    public GameObject player;
    void Update() {
        if (player != null) {
            transform.position =
                new Vector3(
                    player.transform.position.x,
                    player.transform.position.y,
                    -5);
        }
    }
}
