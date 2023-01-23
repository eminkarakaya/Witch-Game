using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    [SerializeField] private Material prefabMaterial;
    public LineRenderer lineRenderer = null;
    private Vector3 targetPosition = Vector3.zero;

    private Coroutine pourCoroutine = null;
    private ParticleSystem splashParticle = null;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();
        lineRenderer.material = Instantiate(prefabMaterial);
    }
    private void Start()
    {
        MoveToPosition(0, transform.position);
        AnimateToPosition(1, targetPosition);
    }
    public void Begin(Potion potion)
    {
        StartCoroutine(UpdateParticle());
        pourCoroutine = StartCoroutine(BeginPour(potion));
    }
    public void End()
    {
        StopCoroutine(pourCoroutine);
        pourCoroutine = StartCoroutine(EndPour());
    }
    private IEnumerator EndPour()
    {
        while(!HasReachedPosition(0,targetPosition))
        {
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);
            yield return null;
        }
        Destroy(gameObject);
    }

    private IEnumerator BeginPour(Potion potion)
    {
        while(gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();
            Bottle bottle = FindBottle();
            if(bottle != null)
            {
                bottle.FillBottle( potion);
            }
            MoveToPosition(0, transform.position);
            MoveToPosition(1, targetPosition);
            yield return null;

        }
    }
    private Bottle FindBottle()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, 2.0f);
        /*Vector3 endPoint = Vector3.zero;*/ /*hit.collider ? hit.point : ray.GetPoint(2.0f);*/
        if (hit.collider)
        {
            if (hit.collider.TryGetComponent(out Bottle bottle))
            {
                return bottle;
            }
            else
                return null;
        }
        else
            return null;
    }
    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, 2.0f);
        Vector3 endPoint = Vector3.zero; /*hit.collider ? hit.point : ray.GetPoint(2.0f);*/

        if(hit.collider)
        {
            endPoint = hit.point;
        }
        else
            endPoint = ray.GetPoint(2.0f);
        return endPoint;
    }

    private void MoveToPosition(int index,Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
    }
    private void AnimateToPosition(int index,Vector3 targetPosition)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f);
        lineRenderer.SetPosition(index, newPosition);
    }
    private bool HasReachedPosition(int index,Vector3 targetPosition)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);
        return currentPosition == targetPosition;
    }
    private IEnumerator UpdateParticle()
    {
        while(gameObject.activeSelf)
        {
            splashParticle.gameObject.transform.position = targetPosition;

            bool isHitting = HasReachedPosition(1, targetPosition);
            splashParticle.gameObject.SetActive(isHitting);
            yield return null;
        }
    }
}
