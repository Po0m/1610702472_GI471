using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W8_TouchInPut : MonoBehaviour
{
    public RectTransform joyStickBG;
    public RectTransform joyStickCenter;
    public float radiusLimit = 5.0f;


    private bool isRightScreenActive;
    private bool isLeftScreenActive;

    private int fingerIdActiveScreenRight;
    private int fingerIdActiveScreenLeft;

    private Vector2 leftAxis;
    private Vector2 rightAxis;

    private Vector2 originPosLeft;
    private Vector2 originPosRight;

    private void Update()
    {
        joyStickBG.gameObject.SetActive(true);
        joyStickCenter.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        W8_TouchManager.Instance.OnTouchBegan += OnTouchBegan;
        W8_TouchManager.Instance.OnTouchMoved += OnTouchMoved;
        W8_TouchManager.Instance.OnTouchCanceled += OnTouchCanceled;
        W8_TouchManager.Instance.OnTouchEnded += OnTouchEnded;
    }

    private void OnDisable()
    {
        if (W8_TouchManager.Instance == null)
        {
            return;
        }

        W8_TouchManager.Instance.OnTouchBegan += OnTouchBegan;
        W8_TouchManager.Instance.OnTouchMoved += OnTouchMoved;
        W8_TouchManager.Instance.OnTouchCanceled += OnTouchCanceled;
        W8_TouchManager.Instance.OnTouchEnded += OnTouchEnded;

    }

    public Vector2 GetLeftScreenAxis()
    {
        leftAxis.x = Mathf.Clamp(leftAxis.x, -1.0f, 1.0f);
        leftAxis.y = Mathf.Clamp(leftAxis.y, -1.0f, 1.0f);
        return leftAxis;
    }

    public Vector2 GetRightScreenAxis()
    {
        rightAxis.x = Mathf.Clamp(rightAxis.x, -1.0f, 1.0f);
        rightAxis.y = Mathf.Clamp(rightAxis.y, -1.0f, 1.0f);
        return rightAxis;
    }

    private void ActiveScreenSide(TouchData touchData)
    {
        float haftScreenSize = Screen.width / 2;

        if (touchData.position.x > haftScreenSize && isRightScreenActive == false)
        {
            isRightScreenActive = true;
            fingerIdActiveScreenRight = touchData.fingerId;
            originPosRight = touchData.position;
        }

        if (touchData.position.x < haftScreenSize && isLeftScreenActive == false)
        {
            isLeftScreenActive = true;
            fingerIdActiveScreenLeft = touchData.fingerId;
            originPosLeft = touchData.position;

            ActiveJoyStick(originPosLeft);
        }
    }

    private void DeactiveScreenSide(TouchData touchData)
    {
        if(touchData.fingerId == fingerIdActiveScreenRight)
        {
            isRightScreenActive = false;
            rightAxis = Vector2.zero;
        }

        if(touchData.fingerId == fingerIdActiveScreenLeft)
        {
            isRightScreenActive = false;
            leftAxis = Vector2.zero;

            DeactiveJoyStick();
        }
    }

    private void UpdateTouchMoveSide(TouchData touchData)
    {
        float maxMoveRatioWithScreen = Screen.width * 0.05f;

        if(touchData.fingerId == fingerIdActiveScreenLeft && isLeftScreenActive)
        {
            Vector2 currenMovePos = touchData.position;
            float distance = Vector2.Distance(currenMovePos, originPosLeft);

            float weight = distance / maxMoveRatioWithScreen;
            leftAxis = (currenMovePos - originPosLeft).normalized * weight;

            UpdateJoyStick(touchData.position, originPosLeft);

        }

        if(touchData.fingerId == fingerIdActiveScreenRight && isRightScreenActive)
        {
            Vector2 currentMovePos = touchData.position;

            rightAxis = (currentMovePos - originPosRight).normalized;

            originPosRight = currentMovePos;
        }
    }

    private void ActiveJoyStick(Vector2 position)
    {
        joyStickBG.gameObject.SetActive(true);
        joyStickCenter.gameObject.SetActive(true);

        joyStickBG.anchoredPosition = position;
    }

    private void DeactiveJoyStick()
    {
        joyStickBG.gameObject.SetActive(false);
        joyStickCenter.gameObject.SetActive(false);


    }

    private void UpdateJoyStick(Vector2 followPos, Vector2 originPos)
    {
        Vector2 direction = followPos - originPos;
        direction = Vector2.ClampMagnitude(direction, radiusLimit);
        joyStickCenter.anchoredPosition = originPos + direction;
    }



    public void OnTouchBegan(TouchData touchData)
    {
        ActiveScreenSide(touchData);
    }

    public void OnTouchMoved(TouchData touchData)
    {
        UpdateTouchMoveSide(touchData);
    }

    public void OnTouchCanceled(TouchData touchData)
    {
        DeactiveScreenSide(touchData);
    }

    public void OnTouchEnded(TouchData touchData)
    {
        DeactiveScreenSide(touchData);
    }


}
