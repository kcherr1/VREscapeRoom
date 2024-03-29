using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EventTest : MonoBehaviour {
    public TextMeshPro txt;
    public Door door;

    public void HoverEntered(HoverEnterEventArgs e) => Display("hover entered", e);
    public void HoverExited(HoverExitEventArgs e) => Display("hover exited", e);

    public void SelectEntered(SelectEnterEventArgs e) => Display("select entered", e);
    public void SelectExited(SelectExitEventArgs e) => Display("select exited", e);

    public void FocusEntered(FocusEnterEventArgs e) => Display("focus entered", e);
    public void FocusExited(FocusExitEventArgs e) => Display("focus exited", e);

    public void Activated(ActivateEventArgs e) => Display("activated", e);
    public void Deactivated(DeactivateEventArgs e) => Display("deactivated", e);

    private void Display(string text, BaseInteractionEventArgs e) {
        if (text == "activated") {
            door.ChangeState(DoorStateType.OPENING);
        }
        txt.text = text;
        print($"{text}: '{e}'");
    }
}
