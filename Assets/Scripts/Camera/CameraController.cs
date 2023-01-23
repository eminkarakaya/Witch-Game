using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Select;
namespace CameraController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _dur;
        private Vector3 _oldCamPos;
        [SerializeField] private Transform zoomTransform;
        private void OnEnable()
        {
            SelectManager.Instance.onSelectPotion += ZoomIn;
            SelectManager.Instance.onCancelPotion += ZoomOut;
        }
        private void OnDisable()
        {
            SelectManager.Instance.onSelectPotion -= ZoomIn;
            SelectManager.Instance.onCancelPotion -= ZoomOut;
        }
        public void ZoomIn(GameObject gameObject)
        {
            _oldCamPos = transform.position;
            transform.DOMove(zoomTransform.position, _dur);
        }
        public void ZoomOut(GameObject gameObject)
        {
            transform.DOMove(_oldCamPos, _dur);

        }
    }
}