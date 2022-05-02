using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PetLarUtils : MonoBehaviour
{
    public static Vector2 GetDirection(Vector2 position, Vector2 targetPosition)
    {
        return (targetPosition - position).normalized;
    }

    /// <summary>
    /// return a random vector2 based on vector2.zero and the value sent.
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Vector2 GetRandomPosition(Vector2 min, Vector2 max)
    {
        return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }

    public static void LoopTimerEvent(Action action, float time, String name)
    {
        TimerEvent.Create(() =>
        {
            action();

            LoopTimerEvent(action, time, name);

        }, time, name);
    }
    public class Complex<ComplexType> where ComplexType : MonoBehaviour
    {
        public delegate bool ComplexDelegate(ComplexType reference);

        public static ComplexType ClosestObject(Transform origin, ComplexType[] targets)
        {

            ComplexType closestSpot = null;
            foreach (ComplexType item in targets)
            {
                if (closestSpot == null)
                {
                    closestSpot = item;
                }
                else if (Vector2.Distance(origin.position, item.transform.position) < Vector2.Distance(origin.position, closestSpot.transform.position))
                {
                    closestSpot = item;
                }
            }
            return closestSpot;
        }
        public static ComplexType ClosestObject(Transform origin, ComplexType[] targets, ComplexDelegate extra_condition)
        {
            ComplexType closestSpot = default(ComplexType);
            foreach (ComplexType item in targets)
            {
                if (extra_condition(item))
                    if (closestSpot == null)
                    {
                        closestSpot = item;
                    }
                    else if (Vector2.Distance(origin.position, item.transform.position) < Vector2.Distance(origin.position, closestSpot.transform.position))
                    {
                        closestSpot = item;
                    }
            }
            return closestSpot;
        }

    }

}
