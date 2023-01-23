using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Select;
using Inputs;
public class PotionMovement : MonoBehaviour
{
    [SerializeField] private AudioClip [] clinkSounds; // sounds
    public AudioClip clip;       //

    [SerializeField] private GameObject closeIcon, touchMoveIcon; // UI

    [SerializeField] private InputData _inputData;
    [SerializeField] private float _dur,_rotationSpeed;
    [SerializeField] private Transform _potionTransform;
   
    private Vector3 _oldPos;
    private Quaternion _oldRot;
    private void OnEnable()
    {
        SelectManager.Instance.onSelectPotion += SelectMove;
        SelectManager.Instance.onCancelPotion += CancelMove;
    }
    private void OnDisable()
    {
        SelectManager.Instance.onCancelPotion -= CancelMove;
        SelectManager.Instance.onSelectPotion -= SelectMove;
    }
    private void Update()
    {
        RotatePotion(SelectManager.Instance.GetSelectedObject());
    }
    private void SelectMove(GameObject gameObject)
    {
        AudioSource.PlayClipAtPoint(clinkSounds[Random.Range(0, clinkSounds.Length)],Camera.main.transform.position);
        touchMoveIcon.SetActive(true);
        closeIcon.SetActive(true);
        _oldRot = gameObject.transform.parent.rotation;
        _oldPos = gameObject.transform.parent.position;
        gameObject.transform.parent.DOMove(_potionTransform.position, _dur);
        gameObject.transform.parent.DORotate(_potionTransform.rotation.eulerAngles, _dur);
    }
    private void CancelMove(GameObject gameObject)
    {
        if(gameObject != null)
        {
            AudioSource.PlayClipAtPoint(clinkSounds[Random.Range(0, clinkSounds.Length)], Camera.main.transform.position);
            closeIcon.SetActive(false);
            gameObject.transform.parent.DOMove(_oldPos, _dur);
            gameObject.transform.parent.DORotate(_oldRot.eulerAngles, _dur);
        }
    }
    // rotation and clamping
    private void RotatePotion(GameObject gameObject)
    {
        if(gameObject != null && _inputData.DeltaPosition.y != 0)
        {
            touchMoveIcon.SetActive(false);
            if(!(gameObject.transform.parent.rotation.eulerAngles.x < 330 && gameObject.transform.parent.rotation.eulerAngles.x > 180) && _inputData.DeltaPosition.y < 0)
            {
                
                Quaternion rot = Quaternion.Euler(new Vector3(gameObject.transform.parent.rotation.eulerAngles.x + (_inputData.DeltaPosition.normalized.y * _rotationSpeed) ,gameObject.transform.parent.rotation.y, gameObject.transform.parent.rotation.z));
                gameObject.transform.parent.rotation = rot;
                return;
            }
            if(!(gameObject.transform.parent.rotation.eulerAngles.x > 45 && gameObject.transform.parent.rotation.eulerAngles.x < 180) && _inputData.DeltaPosition.y > 0)
            {
                Quaternion rot = Quaternion.Euler(new Vector3(gameObject.transform.parent.rotation.eulerAngles.x + (_inputData.DeltaPosition.normalized.y * _rotationSpeed), gameObject.transform.parent.rotation.y, gameObject.transform.parent.rotation.z));
                gameObject.transform.parent.rotation = rot;

            }
            //Mathf.Clamp(gameObject.transform.parent.rotation.x, -30, 45);
        }
    }
}
