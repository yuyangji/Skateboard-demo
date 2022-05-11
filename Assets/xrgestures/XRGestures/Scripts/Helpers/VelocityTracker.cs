using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class VelocityTracker : MonoBehaviour
{
    [SerializeField] Transform refT;
    [SerializeField] float rate;


    [SerializeField] Vector3 Velocity;
    [SerializeField] Vector3 Velocity2;


    [ReadOnly]
    [SerializeField] Vector3 Direction;
    private void Awake()
    {
        StartCoroutine(VelocityUpdate());
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
            var _vel2 =refT.InverseTransformDirection(Direction) / rate;
            
            Velocity = _vel.RoundTo3();
            Velocity2 = _vel2.RoundTo3();
             prevPos = transform.position;
           
            yield return new WaitForSeconds(rate);
        }
    }


}
