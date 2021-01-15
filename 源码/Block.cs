using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*using UnityEngine;*/

public class Block
{
    /*连续或相同的牌称为一块*/
    private readonly int loc = 0;//手牌中的位置
    private int len = 1;//块的长度
    private bool type = true; //真 表示该块数量为3n，是完整型
                              //假 表示为不完整型

    public int Loc => loc;
    public int Len { get => len; set => len = value; }
    public bool Type { get => type; set => type = value; }

    public Block(int _loc) => loc = _loc;

   
    
    public bool block_complete(Opponent op, int que_tou_loc = -1)//参数2表示雀头在手牌的位置。筛选完整性lv2
    {
 
        List<Group> group = new List<Group> { new Group(Loc) };
        
        for(int i = Loc; i < Loc + Len - 1; i++)
        {
            if(op.link(i) == 1)//当关系是连续的时候，即不相同
            {
                group.Last().Len = i + 1 - group.Last().Loc;
                group.Add(new Group(i + 1));
            }
        }

        group.Last().Len = Loc + Len - group.Last().Loc;

        int group_num = group.Count;
        bool[] block_card = new bool[Len];//true 牌属于顺子 false 牌属于刻子

        for (int i = 0; i < Len; i++)
        {
            block_card[i] = true;
        }
        if(que_tou_loc != -1)//有雀头
        {
            group[que_tou_loc].Compirmed += 2;//雀头有两张，此处用于固定雀头组，两张牌
            block_card[group[que_tou_loc].Loc - Loc] = false;
            block_card[group[que_tou_loc].Loc - Loc + 1] = false;
        }
        for(int i = 0; i < group_num; i++)
        {
            switch(group[i].Len - group[i].Compirmed)//该组未固定的牌数
            {
                case 0:
                    continue;
                case 1://只剩一张牌，只能构成顺子
                    if(group_num > i + 2)//还剩至少2组
                    {
                        group[i + 1].Compirmed++;
                        group[i + 2].Compirmed++;
                        continue;
                    }
                    break;
                case 2:
                    if(group_num > i + 2)
                    {
                        group[i + 1].Compirmed += 2;
                        group[i + 2].Compirmed += 2;
                        continue;
                    }
                    break;
                case 3://此处构成刻子,由于不对后面的组产生影响，无需comfired
                    block_card[group[i].Loc - Loc] = false;
                    block_card[group[i].Loc - Loc + 1] = false;
                    block_card[group[i].Loc - Loc + 2] = false;
                    continue;
                case 4://1刻子1顺子
                    if(group_num > i + 2)
                    {
                        group[i + 1].Compirmed++;
                        group[i + 2].Compirmed++;
                        block_card[group[i].Loc - Loc] = false;
                        block_card[group[i].Loc - Loc + 1] = false;
                        block_card[group[i].Loc - Loc + 2] = false;
                        continue;
                    }
                    break;
                default: break;

            }
           
            type = false;//该块是不完整型
            return false;
        }
        return true;
    }
   public bool block_qu_dui(Opponent op)//判断牌组是否为雀头完整型 （3n+2张牌，先去对，在判断剩下的是否组成面子）
    {
        int temp_group_num = 0;
        for(int i = Loc; i < Loc + Len - 1; i++)
        {
            if(op.link(i) == 1)
            {
                {
                    temp_group_num++;
                }
            }
            else if(block_complete(op, temp_group_num))//即相同，构成雀头, 固定雀头，检测剩下的是否为完整型
            {
                return true;
            }
        }
        return false;
    }

    public bool block_ergodic(Opponent op, bool mode)//mode为true 全不完整型 雀头完整缺1    为false 面子不完整型 面子完整缺1
    {
        int first_pai;//插入在最前面的牌的key
        int last_pai;//插入在最后面的牌的key

        /*计算first_pai和last_pai*/
        if (op.tile[Loc] / 4 == 0 || op.tile[Loc] / 4 == 9 || op.tile[Loc] / 4 == 18)//首张牌为 1m 1p 1s
        {
            first_pai = op.tile[Loc];
        }
        else
        {
            first_pai = op.tile[Loc] - 4;//比首张牌的上种牌
        }

        if (op.tile[Loc + Len - 1] / 4 == 8 || op.tile[Loc + Len - 1] / 4 == 17 || op.tile[Loc + Len - 1] / 4 == 26 || op.tile[Loc + Len - 1] / 4 >= 27)//首张牌为 9m 9p 9s 字牌
        {
            last_pai = op.tile[Loc + Len - 1];
        }
        else
        {
            last_pai = op.tile[Loc + Len - 1] + 4;//比末张牌的下种牌
        }

        int temp_tile = first_pai;//寄存准备插入的牌的key
        Opponent temp_op = new Opponent();
        Block temp_block = new Block(0) { Len = Len + 1 };//loc = 0   len = len + 1   加入新牌后的块
        
        for (int i = 0; i < (last_pai - first_pai) / 4 + 1; i++) 
        { 
            for(int j = Loc; j < Loc + Len; j++)
            {
                temp_op.tile.Add(op.tile[j]);//赋值原有的牌到tile
            }
            temp_op.tile.Add(temp_tile);//被遍历的牌
            temp_op.tile.Sort();

            if(mode && temp_block.block_qu_dui(temp_op) || !mode && temp_block.block_complete(temp_op))
            {
                op.chong.Add(temp_tile);
            }
            temp_op.tile.Clear();
            temp_tile += 4;//下一种牌
        }

        if(op.chong != null && op.chong.Count > 0)
        {
            return true;
        }
        return false;

    }
}