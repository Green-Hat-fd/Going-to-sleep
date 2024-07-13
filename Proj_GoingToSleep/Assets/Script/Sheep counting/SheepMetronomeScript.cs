using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SheepMetronomeScript : MonoBehaviour
{
    [SerializeField] Slider sheepSl;
    [SerializeField] Vector2 speedRange = new Vector2(3f, 6.5f);
    [Range(0, 90)]
    [SerializeField] float angle = 45;
    [SerializeField] Vector2 min_max_AreaSize;

    [Space(20)]
    [SerializeField] RectTransform rotTr;
    float speed;
    bool isStarted;

    float endToGoValue,
          targetNum;

    [Space(20)]
    [SerializeField] AudioSource dingSfx;
    [SerializeField] AudioSource correctSfx, errorSfx;
    [SerializeField] bool debug_start;
    [Space(20)]
    [SerializeField] Slider slCenter, slMin, slMax;
    
    
    
    void Awake()
    {
        endToGoValue = Random.Range(0, 2) >= 1
                        ? 1
                        : -1;

        speed = (speedRange.y - speedRange.x) * 0.5f + speedRange.x;


        GameManager.inst.inputManager.Player.Press.started += PlayerPress_started;
    }


    private void PlayerPress_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(isStarted
           &&
           (sheepSl.value >= slMin.value && sheepSl.value <= slMax.value))
        {
            correctSfx.Play();
        }
        else
        {
            errorSfx.Play();
        }


        ChangeTarget();
    }

    void Update()
    {
        if (isStarted)
        {
            if (sheepSl.value >= 1 || sheepSl.value <= -1)
                SwapEndToGoNum();

            //Moves constantly the value
            sheepSl.value = Mathf.MoveTowards(sheepSl.value, endToGoValue, speed * Time.deltaTime);
        }


        rotTr.rotation = Quaternion.Euler(sheepSl.value * angle * Vector3.forward);



        //if (GameManager.inst.inputManager.Player.Press.triggered)
        //{

        //}


        
        isStarted = debug_start;
    }
    
    

    void SwapEndToGoNum()
    {
        //Swaps the target num (-1 <=> +1)
        endToGoValue *= -1;

        //Chooses another random speed
        speed = Random.Range(speedRange.x, speedRange.y);


        //Plays the "ding" sound
        dingSfx.PlayOneShot(dingSfx.clip);
    }

    void ChangeTarget()
    {
        //
        targetNum = Random.Range(min_max_AreaSize.x / 2, min_max_AreaSize.y / 2);
        float targetPos = Random.Range(-1 + targetNum, 1 - targetNum);

        slCenter.value = targetPos;
        slMin.value = targetPos - targetNum;
        slMax.value = targetPos + targetNum;

        print($"Uscito in {targetPos} di sez. \"{targetNum}\"\t(nella linea, [{targetPos-targetNum} ; {targetPos+targetNum}])");
    }



    #region EXTRA - Changing the Inspector

    private void OnValidate()
    {
        
    }

    #endregion


    #region EXTRA - Gizmos

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Quaternion quatAngle = Quaternion.Euler(angle * Vector3.forward),
                   negQuatAngle = Quaternion.Euler(-angle * Vector3.forward);

        Handles.color = Color.blue;
        Handles.Slider(rotTr.position,
                       quatAngle * Vector3.up * 5.5f,
                       HandleUtility.GetHandleSize(rotTr.position) * 0.5f,
                       Handles.ArrowHandleCap,
                       1);
        Handles.Slider(rotTr.position,
                       negQuatAngle * Vector3.up * 5.5f,
                       HandleUtility.GetHandleSize(rotTr.position) * 0.5f,
                       Handles.ArrowHandleCap,
                       1);
    }

#endif

    #endregion
}
