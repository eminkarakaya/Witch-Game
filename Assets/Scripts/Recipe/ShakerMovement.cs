using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Inputs;

public class ShakerMovement : Singleton<ShakerMovement>
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private InputData _inputData;
    [SerializeField] private float minY, currentY;
    [SerializeField] private GameObject _closeIcon,_touchIcon;
    [SerializeField] private float offset;
    [SerializeField] private Shaker _currentShaker;
    
    [SerializeField] bool isShake,isMove;
    public Shaker CurrentShaker { get; set; }    

    void Update()
    {
        Shake();
    }
    public bool IsCurrentShaker()
    {
        return _currentShaker != null;
    }
    private void Shake()
    {
        if(isMove) return;
        if(!Cauldron.Instance.CheckShake())  return;
        if(_currentShaker == null) return;
        if(_inputData.IsMove)
            currentY += Mathf.Abs (_inputData.DeltaPosition.y);
        if(_inputData.IsEnd)
            currentY = 0;
        
        if(currentY > minY)
        {
            if(!isShake)
            {
                _currentShaker.SetParentSalt(_currentShaker.transform);
                isShake = true;
                // _currentShaker.rb.DOMoveY()
                _currentShaker.transform.parent.DOMoveY(_currentShaker.transform.position.y + offset,.2f).OnComplete(()=>
                {
                    _currentShaker.transform.parent.DOMoveY(_currentShaker.transform.position.y + -offset,.2f).OnComplete(()=>
                    {
                        _currentShaker.CreatePotionItemType();
                        isShake = false;
                        AudioSource.PlayClipAtPoint(_clip,Camera.main.transform.position);
                        _currentShaker.CreateDustPrefab();
                        Cauldron.Instance.AddRecipe(_currentShaker.itemType);    
                    });
                });
            }
        }
    }

    public void MoveShaker(GameObject gameObject)
    {
        if(!Cauldron.Instance.CheckShake())  return;
        if(_currentShaker == null) return;
        isMove = true;
        _currentShaker.parentSalt.transform.SetParent(_currentShaker.transform);
        gameObject.transform.DOMove(Camera.main.GetComponent<cam.CamController.CameraController>().potionPos.position + new Vector3(0,2,0), .3f).OnComplete(()=>
        {
            isMove = false;
            _currentShaker.parentSalt.transform.SetParent(null);
        });
        gameObject.transform.DORotate(new Vector3(0,0,-180),.3f);
        _closeIcon.SetActive(true);
        _touchIcon.SetActive(true);
        
    }
    public void PutItBack()
    {
        _currentShaker.parentSalt.transform.SetParent(_currentShaker.transform);
        _currentShaker. transform.parent.DOMove(_currentShaker.pos, .3f);
        _currentShaker. transform.parent.DORotate(_currentShaker.rot,.3f).OnComplete(()=>
        {
            _currentShaker.parentSalt.transform.SetParent(null);
        });
        _closeIcon.SetActive(false);
        _touchIcon.SetActive(false);
        isShake = false;
        SetCurrentShaker(null);
    }
    public void SetCurrentShaker(Shaker obj)
    {
        currentY = 0;
        _currentShaker = obj;
    }
}
