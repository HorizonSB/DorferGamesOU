using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
    public static Vector3 GetFollowPointVector(Transform startPoint, Transform endPoint, float timer)
    {
        float height = 0.3f;

        Vector3 middlePoint = endPoint.position / 2;
        middlePoint = new Vector3(middlePoint.x, middlePoint.y + height, middlePoint.z);

        Vector3 middleOfFirstSegment = Vector3.Lerp(startPoint.position, middlePoint, timer);
        Vector3 middleOfSecondSegment = Vector3.Lerp(middlePoint, endPoint.position, timer);
        Vector3 middleOfFirstAndSecondSegments = Vector3.Lerp(middleOfFirstSegment, middleOfSecondSegment, timer);

        return middleOfFirstAndSecondSegments;
    }
}
