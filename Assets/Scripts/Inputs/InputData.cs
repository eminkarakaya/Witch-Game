using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    [CreateAssetMenu(menuName ="Input/InputData")]
    public class InputData : ScriptableObject
    {
        [SerializeField] private Vector2 _touchPos;
        [SerializeField] private bool _isClick;
        [SerializeField] private bool _isEnd;
        [SerializeField] private bool _isMove;
        [SerializeField] private Vector2 _deltaPosition;
        [SerializeField] private bool _swipeLeft, _swipeRight, _swipeUp, _swipeDown;
        [SerializeField] private float swipeDistance;
        public bool IsClick { get => _isClick; set {_isClick = value;}}
        public bool IsMove { get => _isMove; set { _isMove = value;}}
        public bool IsEnd { get => _isEnd; set { _isEnd = value;}}
        public Vector3 DeltaPosition{ get => _deltaPosition; set{_deltaPosition = value;}}
        public Vector2 TouchPosition{ get => _touchPos; set{ _touchPos = value;}}
        public bool SwipeLeft { get => _swipeLeft; set { _swipeLeft = value; } }
        public bool SwipeRight { get => _swipeRight; set { _swipeRight = value; } }
        public bool SwipeUp { get => _swipeUp; set { _swipeUp = value; } }
        public bool SwipeDown { get => _swipeDown; set { _swipeDown = value; } }
    }
}
