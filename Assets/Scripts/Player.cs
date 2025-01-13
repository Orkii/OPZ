using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Creature {

    private CharacterController characterController;
    [SerializeField]
    public float speed = 1;

    [SerializeField]
    InputActionReference movementInput;


    Vector2 move;
    
    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update() {

        move = movementInput.action.ReadValue<Vector2>();
        Debug.Log("move = " + move);
        characterController.Move(move * speed * Time.deltaTime);
    }
}
