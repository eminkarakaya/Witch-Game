using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
using DG.Tweening;
using Select;
using UnityEngine.EventSystems;

public class EmptyPotion : MonoBehaviour
{
    [SerializeField] private InputData _inputData;
    RaycastHit hit;
    [SerializeField] private LayerMask mask;
    public bool isFinish;
    //private void OnEnable()
    //{
    //    SelectManager.Instance.onSellPotion += CheckTable;
    //}
    //private void OnDisable()
    //{
    //    SelectManager.Instance.onSellPotion -= CheckTable;

    //}
    bool outlineGarbage,outlineTable;
    Bottle bottle;
    Collider _collider = null;
    private void Start()
    {
        bottle = GetComponent<Bottle>();
    }
    private void Update()
    {
        Ray();
        CheckTable();
    }
    private void Ray()
    {
        hit = CastRayChar();
        if (isFinish) return;
        if (hit.collider)
        {
            _collider = hit.collider;
        }

        if (hit.collider && _collider.tag == "Garbage")
        {
            if (!outlineGarbage)
            {
                Outline outline = _collider.gameObject.AddComponent<Outline>();
                outlineGarbage = true;
                return;
            }

        }
        else
        {
            if (_collider == null) return;  
            else if(_collider.tag =="Garbage")
            {
                outlineGarbage = false;
                Destroy(_collider.gameObject.GetComponent<Outline>());
            }
        }

        if (hit.collider && _collider.tag == "Table")
        {
            if (CustomerManager.Instance.CheckCustomerOrder(bottle.colorType))
            {
                if (!outlineTable)
                {
                    Outline outline = _collider.gameObject.AddComponent<Outline>();
                    outline.OutlineColor = Color.green;
                    outlineTable = true;
                    return;
                }

            }
            else
            {
                if (!outlineTable)
                {
                    Outline outline = _collider.gameObject.AddComponent<Outline>();
                    outline.OutlineColor = Color.red;
                    outlineTable = true;
                    return;
                }
            }
        }
        else
        {

            if (_collider == null) return;
            else if (_collider.tag == "Table")
            {
                outlineTable = false;
                Destroy(_collider.gameObject.GetComponent<Outline>());
            }
        }

    }
    private void CheckTable()
    {
        if (isFinish)
            return;

        hit = CastRayChar();
        if (_inputData.IsEnd )
        {
            if (hit.collider && hit.collider.tag =="Garbage")
            {
                SelectManager.Instance.NullSelectedEmptyPotion();
                Destroy(transform.parent.gameObject);        
            }
            else if (hit.collider && CustomerManager.Instance.CheckCustomerOrder(bottle.colorType))
            {
                Table table = hit.collider.GetComponent<Table>();
                table.orderObjects.Add(transform.parent.gameObject);
                transform.parent.DOMove(table.potionTransforms[table.IncreaseAndGetIndex()].position, .1f);
                transform.parent.SetParent(null);
                isFinish = true;
                bottle.TogglePotionImage(false);
                bottle.enabled = false;
                Destroy(hit.collider.GetComponent<Outline>());
                SelectManager.Instance.NullSelectedEmptyPotion();
                CustomerManager.Instance.OrderComplate(bottle.colorType);
            }
            //else
            //{
            //    Back();
            //}
        }
    }
    public void Back()
    {
        if (isFinish) 
            return;
        hit = CastRayChar();
        if(hit.collider && hit.collider.tag == "Table" && CustomerManager.Instance.CheckCustomerOrder(bottle.colorType))
        {

        }
        else
            transform.parent.DOMove(Camera.main.GetComponent<CameraController.CameraController>().potionPos.position, .2f);
    }
    
    private RaycastHit CastRayChar()
    {
        Vector3 charPos = gameObject.transform.parent.position;
        Vector3 dir = Vector3.down;
        RaycastHit hit;
        Physics.Raycast(charPos, dir, out hit, 100,mask);
        return hit;
    }
    
}
