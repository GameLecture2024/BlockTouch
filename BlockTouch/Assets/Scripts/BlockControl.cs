using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    public Blocks.COLOR color = (Blocks.COLOR)0;
    public BlockRoot block_root = null;
    public Blocks.iPosition i_pos;

    private void Start()
    {
        SetColor(this.color);
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
        
    }
}
