using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    public Blocks.COLOR color = (Blocks.COLOR)0;
    public BlockRoot block_root = null;
    public Blocks.iPosition i_pos;

    public Blocks.STEP step = Blocks.STEP.NONE;          // 지금 상태
    public Blocks.STEP nextStep = Blocks.STEP.NONE;      // 다음 상태
    private Vector3 positionOffsetInitial = Vector3.zero;// 교체 전 위치
    public Vector3 positionOffset = Vector3.zero;        // 교체 후 위치

    private void Start()
    {
        SetColor(this.color);
        nextStep = Blocks.STEP.IDLE;
    }

    public void SetColor(Blocks.COLOR _color)
    {
        color = _color;
        Color color_value = new();

        switch (color)
        {
            case Blocks.COLOR.PINK:
                color_value = new Color(1.0f, 0.5f, 0.5f);
                break;
            case Blocks.COLOR.BLUE:
                color_value = Color.blue;
                break;
            case Blocks.COLOR.YELLOW:
                color_value = Color.yellow;
                break;
            case Blocks.COLOR.GREEN:
                color_value = Color.green;
                break;
            case Blocks.COLOR.MAGENTA:
                color_value = Color.magenta;
                break;
            case Blocks.COLOR.ORANGE:
                color_value = new Color(1.0f, 0.46f, 0.0f);
                break;
            default:
                break;
        }
        GetComponent<Renderer>().material.color = color_value;
    }

    private void Update()
    {
        Vector3 mousePos;
        block_root.unprojectMousePosition(out mousePos, Input.mousePosition);

        Vector2 mousePositionXY = new Vector2(mousePos.x, mousePos.y);

        while(nextStep != Blocks.STEP.NONE)
        {
            step = nextStep;
            nextStep = Blocks.STEP.NONE;

            switch (step)
            {
                case Blocks.STEP.IDLE:
                    positionOffset = Vector3.zero;
                    transform.localScale = Vector3.one * 1.0f;
                    break;
                case Blocks.STEP.GRABBED:
                    transform.localScale = Vector3.one * 1.2f;
                    break;
                case Blocks.STEP.RELEASED:
                    positionOffset = Vector3.zero;
                    transform.localScale = Vector3.one * 1.0f;
                    break;
            }
        }

        Vector3 position = BlockRoot.CalcBlockPosition(i_pos) + positionOffset;

        transform.position = position;
    }

    public void BeginGrab()
    {
        nextStep = Blocks.STEP.GRABBED;
    }

    public void EndGrab()
    {
        nextStep = Blocks.STEP.IDLE;
    }

    public bool IsGrabbable()
    {
        bool isGrab = false;
        switch (step)
        {
            case Blocks.STEP.IDLE:
                isGrab = true;
                break;
        }
        return isGrab;

    }
    public bool IsContainedPosition(Vector2 pos)
    {
        bool ret = false;
        Vector3 center = transform.position;
        float h = Blocks.COLLISION_SIZE / 2.0f;

        // block이 교체하고자 하는 대상과 충돌하면 false을 return
        do
        {
            if(pos.x < center.x -h || center.x + h < pos.x)
            {
                break;
            }

            if(pos.y < center.y - h || center.y + h < pos.y)
            {
                break;
            }

            ret = true;
        }
        while (false);

        return ret;
    }
}