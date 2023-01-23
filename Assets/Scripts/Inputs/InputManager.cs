using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inputs
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputData _data;
        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _data.IsClick = true;
                }
                else if(touch.phase == TouchPhase.Moved)
                {
                    _data.DeltaPosition = touch.deltaPosition;
                }
                else
                {
                    _data.IsClick = false;
                    _data.DeltaPosition = Vector3.zero;
                }

            }
            
        }
    }

}