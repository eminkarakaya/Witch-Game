using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
public class QuestSelectManager : Singleton<QuestSelectManager>
{
    [SerializeField] private InputData _inputData;
    public GameObject selectedObject;
    public GameObject _selectedEmptyObject;
    private void Update()
    {
        SelectBasePotion();
        if(selectedObject!= null)
        {
            PotionMovement.Instance.RotatePotion(selectedObject);
        }
    }
    private void SelectBasePotion()
    {

        if (_inputData.IsClick)
        {
            RaycastHit hit = CastRay();
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Quest quest))
                {
                    if (quest == QuestManager.Instance.GetCurrentQuest())
                    {
                        quest.Trigger();
                    }
                }
            }
        }
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
