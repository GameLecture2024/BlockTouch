using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks 
{
    public static float COLLISION_SIZE = 1.0f;
    public static float VANISH_TIME = 3.0f;

    public struct iPosition
    {
        public int x;
        public int y;
    }

    public enum STEP
    {
        NONE = -1,
        IDLE = 0,
        GRABBED,
        RELEASED,
        SLIDE,
        VACANT,
        RESPAWN,
        FALL,
        LONG_SLIDE,
        NUM,
    }
    public enum COLOR
    {
        NONE = -1,
        PINK = 0,
        BLUE,
        YELLOW,
        GREEN,
        MAGENTA,
        ORANGE,
        GRAY,
        NUM,
        FIRST = PINK,
        LAST = ORANGE,
        NORMAL_COLOR_NUM = GRAY,
    };

    public enum DIR
    {
        NONE = -1,
        RIGHT,
        LEFT,
        UP,
        DOWN,
        NUM, // 방향이 몇 종류이 있는지
    };

    public static int BLOCK_NUM_X = 9;
    public static int BLOCK_NUM_Y = 9;
}
