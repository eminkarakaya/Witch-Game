using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Select;
namespace cam.CamController
{
    public class CameraController : Singleton<CameraController>
    {
        [SerializeField] private float _dur;
        [SerializeField] private Vector3[] _rotates;
        private Vector3 _oldCamPos;
        public Transform potionPos,bookPos;
        [SerializeField] private Transform potionWaitTransform;
        private void OnEnable()
        {
            SelectManager.Instance.onSelectPotion += ZoomIn;
            SelectManager.Instance.onCancelPotion += ZoomOut;
            StageManager.Instance.onSwipeLeft += Rotate;
            StageManager.Instance.onSwipeRight += Rotate;
        }
        private void OnDisable()
        {
            SelectManager.Instance.onSelectPotion -= ZoomIn;
            SelectManager.Instance.onCancelPotion -= ZoomOut;
            StageManager.Instance.onSwipeLeft -= Rotate;
            StageManager.Instance.onSwipeRight -= Rotate;
        }
        public void ZoomIn(GameObject gameObject)
        {
            //_oldCamPos = transform.position;
            //transform.DOMove(_zoomTransform.position, _dur);
        }
        public void ZoomOut(GameObject gameObject)
        {
            //transform.DOMove(_oldCamPos, _dur);
        }
        public void Rotate()
        {
            if(StageManager.Instance.index  == StageManager.CAULDRONINDEX)
            {
                if(SelectManager.Instance.GetSelectedEmptyObject() != null)
                {
                    SelectManager.Instance.GetSelectedEmptyObject().transform.SetParent(potionWaitTransform);
                    SelectManager.Instance.GetSelectedEmptyObject().transform.localPosition = Vector3.zero;
                }
                transform.DORotate(_rotates[StageManager.Instance.index], _dur).OnComplete(()=> { 
                    
                });
            }
            else
            {
                transform.DORotate(_rotates[StageManager.Instance.index], _dur).OnComplete(()=> { 
            
                    if(SelectManager.Instance.GetSelectedEmptyObject() != null)
                    {
                        SelectManager.Instance.GetSelectedEmptyObject().transform.SetParent(this.transform);
                    }
                });
            }
            
        }
        public void PosAndRotate(Vector3 pos,Vector3 rot,float dur,System.Action onComplate = null)
        {
            transform.DOMove(pos, dur);
            transform.DORotate(rot, dur).OnComplete(()=>onComplate?.Invoke());
        }

    }
}