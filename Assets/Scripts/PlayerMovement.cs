using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    [Header("Component References")]
    [SerializeField] private CircleCollider2D col;
    [SerializeField] private Rigidbody2D rb;

    [Header("Throw Values")]
    [SerializeField] private float extraThrowForce = 100f;

    private enum DragState {
        IDLE = 0,
        PRESSED,
        DRAGGING,
        RELEASED,
        MAX,
    }
    private DragState dragState = DragState.IDLE;

    private bool isMouseDown = false;
    private bool wasMouseDown = false;

    private Vector2 dragStartPos = Vector2.zero;
    private Vector2 dragEndPos = Vector2.zero;

    private void Awake() {
        if (col == null) {
            col = GetComponent<CircleCollider2D>();
        }

        if (rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Update() {
        if (isMouseDown && !wasMouseDown) {
            dragState = DragState.PRESSED;

            // Initial press. Store the starting mouse position.
            dragStartPos = GetCurrentMouseWorldPos();
        } else if (isMouseDown && wasMouseDown) {
            dragState = DragState.DRAGGING;

            // Drag state. Track the mouse's movement here.
            Vector2 currentDragPos = GetCurrentMouseWorldPos();

            // Adjust the rotation to face the throw direction.
            Vector2 normDir = (currentDragPos - dragStartPos).normalized;
            rb.rotation = (Mathf.Atan2(normDir.y, normDir.x) * Mathf.Rad2Deg) + 90f;
        } else if (!isMouseDown && wasMouseDown) {
            dragState = DragState.RELEASED;

            // We've let go of the mouse. Calculate the shot here.
            dragEndPos = GetCurrentMouseWorldPos();
            Vector2 throwVec = -(dragEndPos - dragStartPos);

            rb.AddForce(throwVec * extraThrowForce * Time.deltaTime, ForceMode2D.Impulse);
        } else {
            dragState = DragState.IDLE;
        }

        wasMouseDown = isMouseDown;
        isMouseDown = Input.GetMouseButton(0);
    }

    private Vector3 GetCurrentMouseWorldPos() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnGUI() {
        // Make a background box
        GUI.Box(new Rect(10, 10, 100, 90), "Player Values");

        GUI.TextField(new Rect(20, 40, 90, 20), dragState.ToString());
    }
}