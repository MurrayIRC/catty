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
        } else if () {

        } else {

        }

        wasMouseDown = isMouseDown;
        isMouseDown = Input.GetMouseButtonDown(0);
    }
}
