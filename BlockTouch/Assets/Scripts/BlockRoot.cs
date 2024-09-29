using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRoot : MonoBehaviour
{
    public GameObject blockPrefab = null;
    public BlockControl[,] blocks;

    private GameObject mainCam = null;
    private BlockControl grabbedBlock = null;

    private void Start()
    {
        mainCam = Camera.main.gameObject;
    }

    // 마우스 좌표와 겹치는지 확인
    // 잡을 수 있는 상태의 블록을 잡는다

    private void Update()
    {
        Vector3 mousePos;
        unprojectMousePosition(out mousePos, Input.mousePosition);

        Vector2 mousePositionXY = new Vector2(mousePos.x, mousePos.y);

        if(grabbedBlock == null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                foreach(BlockControl block in blocks)
                {
                    if(!block.IsGrabbable())
                    {
                        continue;
                    }
                    if(!block.IsContainedPosition(mousePositionXY))
                    {
                        continue;
                    }
                    grabbedBlock = block;
                    grabbedBlock.BeginGrab();
                    break;
                }
            }
        }
        else
        {
            if(!Input.GetMouseButton(0))
            {
                grabbedBlock.EndGrab();
                grabbedBlock = null;
            }
        }
    }

    public void InitialSetUp()
    {
        this.blocks = new BlockControl[Blocks.BLOCK_NUM_X, Blocks.BLOCK_NUM_Y];
        int colorIndex = 0;

        for(int y = 0; y < Blocks.BLOCK_NUM_Y; y++)
        {
            for(int x =0; x < Blocks.BLOCK_NUM_X; x++)
            {
                GameObject obj = Instantiate(blockPrefab) as GameObject;

                BlockControl block = obj.GetComponent<BlockControl>();

                blocks[x, y] = block;

                block.i_pos.x = x;
                block.i_pos.y = y;

                block.block_root = this;

                Vector3 position = BlockRoot.CalcBlockPosition(block.i_pos);
                block.transform.position = position;
                block.SetColor((Blocks.COLOR)colorIndex);
                block.name = "block(" + block.i_pos.x.ToString() + "," + block.i_pos.y.ToString() + ")";

                colorIndex = Random.Range(0, (int)Blocks.COLOR.NORMAL_COLOR_NUM);
            }
        }
    }

    public static Vector3 CalcBlockPosition(Blocks.iPosition i_pos)
    {
        Vector3 position = new Vector3(-(Blocks.BLOCK_NUM_X / 2.0f - 0.5f), -(Blocks.BLOCK_NUM_Y / 2.0f - 0.5f), 0.0f);

        position.x += (float)i_pos.x * Blocks.COLLISION_SIZE;
        position.y += (float)i_pos.y * Blocks.COLLISION_SIZE;

        return position;
    }

    public bool unprojectMousePosition(out Vector3 worldPosition, Vector3 mousePosition)
    {
        bool ret;

        Plane plane = new Plane(Vector3.back, new Vector3(0f, 0f, -Blocks.COLLISION_SIZE / 2.0f));

        Ray ray = mainCam.GetComponent<Camera>().ScreenPointToRay(mousePosition);

        float depth;

        if(plane.Raycast(ray, out depth))
        {
            worldPosition = ray.origin + ray.direction * depth;
            ret = true;
        }
        else
        {
            worldPosition = Vector3.zero;
            ret = false;
        }
        
        return ret;
    }
}
