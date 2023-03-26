using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGround : MonoBehaviour
{
    [SerializeField] private float saveFrequency = 1f;
    public Vector2 SafeGroundLocation { get; private set; } = Vector2.zero;

    private Coroutine safeGroundCoroutine;

    private GroundCheck groundCheck;

    private void Start(){
        safeGroundCoroutine = StartCoroutine(SaveGroundLocation());
        SafeGroundLocation = transform.position;
        groundCheck = GetComponent<GroundCheck>();
    }

    private IEnumerator SaveGroundLocation(){
        float elapsedTime = 0f;
        while(elapsedTime < saveFrequency){
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if(groundCheck.IsGrounded()){
            SafeGroundLocation = transform.position;
        }

        safeGroundCoroutine = StartCoroutine(SaveGroundLocation());
    }

    public void WarpToSafeGround(){
        transform.position = SafeGroundLocation;
    }
}
