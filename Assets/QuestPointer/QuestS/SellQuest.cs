using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
public class SellQuest : Quest
{
    [SerializeField] private InputData _inputData;
    EmptyPotion emptyPotion;
    private float yPos = 3.103001f;
    bool isSelected;
    
    private void OnEnable()
    {
        emptyPotion = GetComponent<EmptyPotion>();
    }
    private void OnDisable()
    {
        
    }
    private void OnDestroy()
    {
        if(QuestManager.Instance != null)
        QuestManager.Instance.RemoveQuest(this);
    }
    private void Update()
    {
        if (_inputData.IsClick)
        {
            RaycastHit hit = CastRay();
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out EmptyPotion emptyPotion))
                {
                    isSelected = true;
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
            if (isSelected && QuestSelectManager.Instance._selectedEmptyObject != null)
            {
                QuestSelectManager.Instance._selectedEmptyObject.transform.GetChild(0).GetComponent<EmptyPotion>().Back();
                Debug.Log("back");
            }
            isSelected = false;
        }
        if (emptyPotion.isFinish)
            QuestManager.Instance.RemoveQuest(this);
    }
    public void Drag(RaycastHit hit)
    {
        if (StageManager.Instance.index != 1)
            return;
        GameObject selected = QuestSelectManager.Instance._selectedEmptyObject;
        if (selected == null)
            return;
        Vector3 position = new Vector3(_inputData.TouchPosition.x, _inputData.TouchPosition.y, Camera.main.WorldToScreenPoint(selected.transform.position).z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(position);
        selected.transform.position = new Vector3(worldPos.x, yPos, worldPos.z);
    }
    public override void Trigger()
    {
        
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
