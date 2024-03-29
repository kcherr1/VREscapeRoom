using UnityEngine;

public enum DoorStateType {
    CLOSED,
    OPENING,
    OPEN,
}

public class Door : MonoBehaviour {
    public DoorStateType State { get; private set; }
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        State = DoorStateType.CLOSED;
    }

    void Update() {
        switch (State) {
            case DoorStateType.CLOSED:
                break;

            case DoorStateType.OPENING:
                animator.Play("Open");
                SoundManager.Instance.Play(SoundType.SOLVED);
                ChangeState(DoorStateType.OPEN);
                break;

            case DoorStateType.OPEN:
                break;
        }
    }

    public void ChangeState(DoorStateType newState) {
        State = newState;
    }
}
