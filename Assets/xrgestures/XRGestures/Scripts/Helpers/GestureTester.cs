using NaughtyAttributes;
using UnityEngine;
namespace XR_Gestures
{
    public class GestureTester : MonoBehaviour
    {
        [SerializeField] XRAvatar avatar;
        [BoxGroup("")]

        [Expandable]
        [SerializeField] Gesture gesture;



        private void Start()
        {
            gesture.Initialise(avatar);
        }


        private void Update()
        {
            if (gesture == null)
            {
                return;
            }

            var state = gesture.Run();
            if (state == State.Performed)
            {
                Debug.Log($"Gesture Test : performed {gesture}");
            }

        }
    }

}