using UnityEngine;
using System.Collections;

public class PlayAreaSize : MonoBehaviour {

    [SerializeField, Tooltip("Drag in gameObjects to set the play area's boundries")]
    private Transform top, bottom, left, right;
    
    public float GetTop()
    {
        return top.position.y;
    }

    public float GetBottom()
    {
        return bottom.position.y;
    }

    public float GetLeft()
    {
        return left.position.x;
    }

    public float GetRight()
    {
        return right.position.x;
    }
}

namespace WorldBounds
{
    struct Get
    {
        public static float top { get { return GameObject.FindObjectOfType<PlayAreaSize>().GetTop(); } }
        public static float bottom { get { return GameObject.FindObjectOfType<PlayAreaSize>().GetBottom(); } }
        public static float right { get { return GameObject.FindObjectOfType<PlayAreaSize>().GetRight(); } }
        public static float left { get { return GameObject.FindObjectOfType<PlayAreaSize>().GetLeft(); } }
        public static float width
        {
            get
            {
                PlayAreaSize p = GameObject.FindObjectOfType<PlayAreaSize>();
                return p.GetRight() - p.GetLeft();
            }
        }
        public static float height
        {
            get
            {
                PlayAreaSize p = GameObject.FindObjectOfType<PlayAreaSize>();
                return p.GetTop() - p.GetBottom();
            }
        }
        /*public static float top()
        {
            return GameObject.FindObjectOfType<PlayAreaSize>().GetTop();
        }

        public static float bottom()
        {
            return GameObject.FindObjectOfType<PlayAreaSize>().GetBottom();
        }

        public static float left()
        {
            return GameObject.FindObjectOfType<PlayAreaSize>().GetLeft();
        }

        public static float right()
        {
            return GameObject.FindObjectOfType<PlayAreaSize>().GetRight();
        }

        public static float width()
        {
            PlayAreaSize p = GameObject.FindObjectOfType<PlayAreaSize>();
            return p.GetRight() - p.GetLeft();
        }

        public static float height()
        {
            PlayAreaSize p = GameObject.FindObjectOfType<PlayAreaSize>();
            return p.GetTop() - p.GetBottom();
        }*/
    }
}
