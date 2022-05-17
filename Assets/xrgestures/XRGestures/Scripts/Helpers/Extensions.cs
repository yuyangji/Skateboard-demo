using UnityEngine;

static class Extensions
{
    /// <summary>
    /// Vector3 extensions - XR_Gestures.
    /// </summary>

    public static Vector3 RoundTo3(this Vector3 vector3)
    {


        return new Vector3(
            (float) System.Math.Round(vector3.x, 3),
          (float) System.Math.Round(vector3.y, 3),
           (float) System.Math.Round(vector3.z, 3));
    }

    public static Vector3 ReplaceY(this Vector3 _vector3, float y)
    {
        return new Vector3(_vector3.x, y, _vector3.z);
    }

    public static Vector3 ReplaceXY(this Vector3 _vector3, float x, float y)
    {
        return new Vector3(x, y, _vector3.z);
    }

    public static Vector3 AddXY(this Vector3 _vector3, float x, float y)
    {
        return new Vector3(_vector3.x + x, _vector3.y + y, _vector3.z);
    }

    public static Vector3 AddXZ(this Vector3 _vector3, float x, float z)
    {
        return new Vector3(_vector3.x + x, _vector3.y, _vector3.z + z);
    }
    public static Vector3 AddXYZ(this Vector3 _vector3, float x, float y, float z)
    {
        return new Vector3(_vector3.x + x, _vector3.y + y, _vector3.z + z);
    }

    public static Vector3 AddY(this Vector3 _vector3, float y)
    {
        return new Vector3(_vector3.x, _vector3.y + y, _vector3.z);
    }

    public static Vector3 SubY(this Vector3 _vector3, float y)
    {
        return new Vector3(_vector3.x, _vector3.y - y, _vector3.z);
    }

}