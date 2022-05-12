using NaughtyAttributes;
using System.Collections;
using UnityEngine;
public class VelocityTracker : MonoBehaviour
{
    [SerializeField] Transform refT;
    [Range(0.1f, 1f)]
    [SerializeField] float rate;


    [SerializeField] Vector3 AngularVelocity;
    [SerializeField] float magnitude;


    [ReadOnly]
    [SerializeField] Vector3 Direction;
    private void Awake()
    {
        // StartCoroutine(VelocityUpdate());
        StartCoroutine(AngularVelocityUpdate());
    }
    Vector3 GetAngularVelocity(Quaternion foreLastFrameRotation, Quaternion lastFrameRotation, float deltaTime)
    {
        var q = lastFrameRotation * Quaternion.Inverse(foreLastFrameRotation);
        // no rotation?
        // You may want to increase this closer to 1 if you want to handle very small rotations.
        // Beware, if it is too close to one your answer will be Nan
        if (Mathf.Abs(q.w) > 1023.5f / 1024.0f)
        {
            return new Vector3(0, 0, 0);
        }

        float gain;
        // handle negatives, we could just flip it but this is faster
        if (q.w < 0.0f)
        {
            var angle = Mathf.Acos(-q.w);
            gain = -2.0f * angle / (Mathf.Sin(angle) * deltaTime);
        }
        else
        {
            var angle = Mathf.Acos(q.w);
            gain = 2.0f * angle / (Mathf.Sin(angle) * deltaTime);
        }
        return new Vector3(q.x * gain, q.y * gain, q.z * gain);
    }
    IEnumerator AngularVelocityUpdate()
    {
        Quaternion prevRotation = transform.rotation;
        WaitForSeconds waitForSeconds = new WaitForSeconds(rate);
        while (true)
        {
            AngularVelocity = GetAngularVelocity(prevRotation, transform.rotation, rate).normalized;
            AngularVelocity = transform.InverseTransformDirection(AngularVelocity);
            magnitude = AngularVelocity.magnitude;
            // AngularVelocity = refT.InverseTransformDirection(AngularVelocity);
            prevRotation = transform.rotation;
            yield return waitForSeconds;
        }

    }


    IEnumerator VelocityUpdate()
    {

        Vector3 prevPos = transform.position;
        yield return new WaitForSeconds(rate);
        while (true)
        {
            Direction = (transform.position - prevPos);
            // refT.worldToLocalMatrix.MultiplyVector (Direction)
            var _vel = refT.worldToLocalMatrix.MultiplyVector(Direction) / rate;
            var _vel2 = refT.InverseTransformDirection(Direction) / rate;

            prevPos = transform.position;

            yield return new WaitForSeconds(rate);
        }
    }


}
