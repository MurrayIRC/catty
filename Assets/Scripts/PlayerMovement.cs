using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private bool isMouseDown = false;
    [SerializeField] private bool wasMouseDown = false;

    [SerializeField] private Vector2 dragStartPos = Vector2.zero;

    void Awake() {

    }

    void Update() {
        if (isMouseDown && !wasMouseDown) {
            // Initial press. Store the starting mouse position.
            dragStartPos = Input.mousePosition;
        } else if (isMouseDown && wasMouseDown) {
            // Drag state. Track the mouse's movement here.
        } else if (!isMouseDown && wasMouseDown) {
            // We've let go of the mouse. Calculate the shot here.
        }

        wasMouseDown = isMouseDown;
        isMouseDown = Input.GetMouseButtonDown(0);
    }
}
