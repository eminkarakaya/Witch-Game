using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;

namespace Select
{
    public class SelectManager : Singleton<SelectManager>
    {
        [SerializeField] private float yPos;
        [SerializeField] private InputData _inputData;
        [SerializeField] private GameObject _selectedBasePotion,_selectedEmptyPotion;
        public delegate void OnSelectPotion(GameObject gameObject);
        public delegate void OnCancelPotion(GameObject gameObject);
        public delegate void OnSellPotion();
        public OnSelectPotion onSelectPotion;
        public OnCancelPotion onCancelPotion;
        public OnSellPotion onSellPotion;
        //public LayerMask mask;
        [SerializeField] bool isSelected = false;
        private void Update()
        {
            SelectBasePotion();
        }
        private void SelectBasePotion()
        {
            if (_inputData.IsClick)
            {
                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                    if(hit.collider.TryGetComponent(out Book book))
                    {
                        book.OpenBook();
                    }
                    if (hit.collider.TryGetComponent(out PotionBase potion))
                    {
                        if (_selectedBasePotion != null)
                            return;
                        _selectedBasePotion = hit.collider.transform.parent.gameObject;
                        onSelectPotion?.Invoke(_selectedBasePotion);
                    }
                    else if (hit.collider.TryGetComponent(out EmptyPotion emptyPotion))
                    {
                        isSelected = true;
                    }
                    else if (hit.collider.TryGetComponent(out Chest chest))
                    {
                        if(_selectedEmptyPotion ==null)
                        {
                            _selectedEmptyPotion = chest.CreatePotion();
                        }
                    }
                }
            }
            if(isSelected)
            {
                RaycastHit hit = CastRay();
                Drag(hit);
            }
            if (_inputData.IsEnd)
            {
                if(isSelected && _selectedEmptyPotion != null)
                    _selectedEmptyPotion.transform.GetChild(0).GetComponent<EmptyPotion>().Back();
                isSelected = false;
            }
        }
        public void Drag(RaycastHit hit)
        {
            if (StageManager.Instance.index != 1)
                return;
            if (_selectedEmptyPotion == null)
                return;
            Vector3 position = new Vector3(_inputData.TouchPosition.x, _inputData.TouchPosition.y, Camera.main.WorldToScreenPoint(_selectedEmptyPotion.transform.position).z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(position);
            _selectedEmptyPotion.transform.position = new Vector3(worldPos.x, yPos, worldPos.z);
        }
        public void CancelPotion()
        {
            onCancelPotion?.Invoke(_selectedBasePotion);
            _selectedBasePotion = null;

        }
        public GameObject GetSelectedObject()
        {
            return _selectedBasePotion;
        }
        public void NullSelectedEmptyPotion()
        {
            _selectedEmptyPotion = null;
        }
        public void SetEmptyPotion(GameObject gameObject)
        {
            _selectedEmptyPotion = gameObject;
        }
        public void SetSelectedObject(GameObject gameObject)
        {
            _selectedBasePotion = gameObject;
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