using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;

namespace Select
{
    public class SelectManager : Singleton<SelectManager>
    {
        [SerializeField] private InputData _inputData;
        [SerializeField] private GameObject _selectedObject;
        public delegate void OnSelectPotion(GameObject gameObject);
        public delegate void OnCancelPotion(GameObject gameObject);
        public OnSelectPotion onSelectPotion;
        public OnSelectPotion onCancelPotion;
        //public LayerMask mask;
        private void Update()
        {
            SelectPotion();
        }
        private void SelectPotion()
        {
            if (_inputData.IsClick)
            {
                if (_selectedObject != null)
                    return;
                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {

                    if (!hit.collider.TryGetComponent(out Potion potion))
                    {
                        return;
                    }
                    
                    _selectedObject = hit.collider.transform.parent.gameObject;
                    onSelectPotion?.Invoke(_selectedObject);
                }
            }
        }
        public void CancelPotion()
        {
            onCancelPotion?.Invoke(_selectedObject);
            _selectedObject = null;

        }
        public GameObject GetSelectedObject()
        {
            return _selectedObject;
        }
        private RaycastHit CastRay()
        {
            Vector3 screenMousePosFar = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.farClipPlane
            );
            Vector3 screenMousePosNear = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.nearClipPlane
            );
            Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
            Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
            RaycastHit hit;
            Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, Mathf.Infinity);

            return hit;
        }
    }
}