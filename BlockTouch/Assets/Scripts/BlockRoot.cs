using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRoot : MonoBehaviour
{
    public GameObject blockPrefab = null;
    public BlockControl[,] blocks;

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
}
