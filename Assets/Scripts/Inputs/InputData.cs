using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    [CreateAssetMenu(menuName ="Input/InputData")]
    public class InputData : ScriptableObject
    {
        [SerializeField] private bool _isClick;
        [SerializeField] private Vector2 _deltaPosition;
        public bool IsClick { get => _isClick; set {
                 _isClick = value;
            }
        }
        public Vector3 DeltaPosition
        {
            get => _deltaPosition; set
            {
                _deltaPosition = value;
            }
        }
    }
}
