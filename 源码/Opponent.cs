using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/*     507 550 551行注释   在block中定义全局变量op 会堆栈溢出
            35行注释 会堆栈溢出
*/

/*第一次行，第二次不行，  可能是使用了未再次初始化的值    block   errblock*/
/*
1万0~3  /4=0  9万32~35  /4=   8
1饼36~39  /4=9  9饼68~71  /4 = 17
1索72~35  /4=18  9索104~107  /4=26
1m                 0
9m                 8
1p                 9
9p                17
1s                18
9s                26
东108~111         27
南112~115         28 
西116~119         29
北120~123         30
白124~127         31
发128~131         32
中132~135         33
*/
public class Opponent
{

    
    //Opponent temp_op = new Opponent();
    /*int存放的是key*/
    public List<int> tile = new List<int>(14);//手牌，为了减少代码量而定义
    public List<int> fulu = new List<int>(4);//副露
    public List<int> she = new List<int>(30);//舍牌
    public List<int> chong = new List<int>(13);//能胡的牌
    public bool ting = false;//是否听牌

    /*国士无双*/
    private int que_pai_sort = 0;//至少缺的牌的种类号
    private readonly int[] koshi_ting_sort = new int[13] { 0, 8, 9, 17, 18, 26, 27, 28, 29, 30, 31, 32, 33 };//胡牌需要的种类号


    /*七对*/
    //private int dan_sort = 34;//未成对 单张牌的种类


    /*一般型胡牌检测用变量*/
    public int blockErr = 0;//不完整块数
    public List<Block> block = new List<Block>(6) { new Block(0) };//第一块，最多6块
                                                                   //public int block_last = 0;//最后一块的地址


    /* public class Tile
    {

    }
    public class Fulu
    {

    }*/
    /*
     1万0~3  /4=0  9万32~35  /4=   8
     1饼36~39  /4=9  9饼68~71  /4 = 17
     1索72~35  /4=18  9索104~107  /4=26
        1m                 0
        9m                 8
        1p                 9
        9p                17
        1s                18
        9s                26
        东108~111         27
        南112~115         28 
        西116~119         29
        北120~123         30
        白124~127         31
        发128~131         32
        中132~135         33
     */

    

    public bool check_ting()
    {
        //Todo tile一般型赋值
        tile = GameObject.Find("East").GetComponent<script_player>().pai_wall;
      
        
        ting = false;
        chong.Clear();
        
        //if (check_koshi() || check_qidui() || check_common_win())
        if(check_koshi())
        {
            ting = true;
            return true;
        }
        else if(check_qidui())
        {
            ting = true;
            return true;
        }
        else if (check_common_win())
        {
            ting = true;
            return true;
        }
        return false;

    }
    /*
   1万0~3  /4=0  9万32~35  /4=   8
   1饼36~39  /4=9  9饼68~71  /4 = 17
   1索72~35  /4=18  9索104~107  /4=26
      1m                 0
      9m                 8
      1p                 9
      9p                17
      1s                18
      9s                26
      东108~111         27
      南112~115         28 
      西116~119         29
      北120~123         30
      白124~127         31
      发128~131         32
      中132~135         33
   */

    public bool check_koshi(/*List<int> tile*/)
    {

        bool que = false;//判断国士离胡牌是否有缺的牌
        bool duo = false;//判断手牌中是否有多的幺九牌

        //int que_pai_sort = 0;//缺的牌的种类号


        for (int i = 0; i < tile.Count; i++)
        {
            if (check_19(tile[i]) == false)  //存在非19牌
            {
                return false;               //必不能胡国士
            }
        }

        for (int i = 1; i < tile.Count; i++)//判断13张幺九牌的拥有情况
        {

            if (tile[i - 1] == tile[i])//和上一张是否相同
            {
                if (duo == false) //还没有多的牌
                {
                    duo = true;//相同
                }
                else
                {
                    return false;//2种多的牌 无法听国士
                }
            }
            
            else if (check_koshi_continuity(tile[i - 1], tile[i])) //19连续为真
            {
                if (check_koshi_que_more_than_a_sort(tile[i - 1], tile[i]) == false)//不连续的牌中，只少1种牌，而非少多种
                {
                    if (que == false)//还没有缺的牌
                    {
                        que = true;//缺一种19
                        //que_pai_sort = check_koshi_continuity(tile[i - 1], tile[i]);   //使用了新函数，无需再赋值
                    }
                    else
                    {
                        return false;//缺2种19 无法听国士
                    }
                }
            }

        }
        if (duo == true)//若有多出来的牌
        {
            if (que == true)//若有缺
            {
                chong.Add(que_pai_sort);//听缺张
            }
            else
            {
                chong.Add(33);//听中，有多张必非十三面，而前面都是连续的幺九牌，所以是听中
            }
        }
        else
        {
            for (int i = 0; i < koshi_ting_sort.Length; i++)
            {
                chong.Add(koshi_ting_sort[i] * 4);//十三面听
            }
        }
        return true;
    }
    /*
  1万0~3  /4=0  9万32~35  /4=   8
  1饼36~39  /4=9  9饼68~71  /4 = 17
  1索72~35  /4=18  9索104~107  /4=26
     1m                 0
     9m                 8
     1p                 9
     9p                17
     1s                18
     9s                26
     东108~111         27
     南112~115         28 
     西116~119         29
     北120~123         30
     白124~127         31
     发128~131         32
     中132~135         33
  */
    public bool check_19(int pai_key)
    {
        int pai_sort = pai_key / 4;
        /*if (pai_sort == 0 || pai_sort == 8 || pai_sort == 9 || pai_sort == 17 || pai_sort == 18 || pai_sort == 26 || pai_key >= 27 && pai_key <= 33)
        {
            return true;
        }
        else
        {
            return false;
        }*/

        foreach (int i in koshi_ting_sort)
        //for(int i = 0; i < 13; i++)
        {
            if (pai_sort == i)
            {
                return true;
            }
        }
        return false;
    }
    /*将que_pai_sort变为全局变量，不使用return返回*/
    /* public int check_koshi_continuity(int pai_last, int pai_now)//检查手牌19是否是连续的，连续返回0，不连续返回至少缺的牌
     {
         int key_pai_last = pai_last / 4;
         int key_pai_now = pai_now / 4;
         if (0 == key_pai_last || 9 == key_pai_last || 18 == key_pai_last)//1m 1p 1s特殊
         {
             if (key_pai_now - key_pai_last == 8)//1m 1p 1s 与下一种19 sort差8
             {
                 return 0;
             }
             else
             {
                 return key_pai_last + 8;
             }
         }
         else
         {
             if (key_pai_now - key_pai_last == 1)//9m 9p 9s 字牌  则差1
             {
                 return 0;
             }
             else
             {
                 return key_pai_last + 1;
             }
         }
     }*/


    /*
1万0~3  /4=0  9万32~35  /4=   8
1饼36~39  /4=9  9饼68~71  /4 = 17
1索72~35  /4=18  9索104~107  /4=26
 1m                 0
 9m                 8
 1p                 9
 9p                17
 1s                18
 9s                26
 东108~111         27
 南112~115         28 
 西116~119         29
 北120~123         30
 白124~127         31
 发128~131         32
 中132~135         33
*/

    public bool check_koshi_continuity(int pai_last, int pai_now)//检查手牌19是否是连续的，不是则为que_pai_sort赋值，值为至少缺的牌
    {
        int key_pai_last = pai_last / 4;
        int key_pai_now = pai_now / 4;
        if (0 == key_pai_last || 9 == key_pai_last || 18 == key_pai_last)//1m 1p 1s特殊
        {
            if (key_pai_now - key_pai_last == 8)//1m 1p 1s 与下一种19 sort差8
            {
                return true;
            }
            else
            {
                que_pai_sort = key_pai_last + 8;
                return false;
            }
        }
        else
        {
            if (key_pai_now - key_pai_last == 1)//9m 9p 9s 字牌  则差1
            {
                return true;
            }
            else
            {
                que_pai_sort = key_pai_last + 1;
                return false;
            }
        }
    }
    public bool check_koshi_que_more_than_a_sort(int pai_last, int pai_now)//是否缺超过2种19
    {
        int key_pai_last = pai_last / 4;
        int key_pai_now = pai_now / 4;
        if (0 == key_pai_last || 9 == key_pai_last || 18 == key_pai_last || 8 == key_pai_last || 17 == key_pai_last)//1m 1p 1s 9m 9p 特殊
        {
            if (key_pai_now - key_pai_last == 9)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (key_pai_now - key_pai_last == 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
    /*protected int calculate_que_pai_sort(int pai_last, int pai_now)//计算缺的牌的种类
    {
        int key_pai_last = pai_last / 4;
        int key_pai_now = pai_now / 4;
        if (0 == key_pai_last || 9 == key_pai_last || 18 == key_pai_last)//1m 1p 1s特殊
        {
            return key_pai_last + 8;
        }
        else
        {
            return key_pai_last + 1;
        }
    }*/
    /*
    public class group
    {
        private readonly int loc = 0;
        private int len = 1;
        private int comfirmed = 0;
        //  public int Loc => loc;
        public int Loc => loc;
        public int Len { get => len; set => len = value; }
        public int Compirmed { get => comfirmed; set => comfirmed = value; }


        public group(int _loc) => loc = _loc;
    }*/
    /*public class Block
    {
        private readonly int loc = 0;//手牌中的位置
        private int len = 1;//块的长度
        private bool type = true; //真 表示该块数量为3n，是完整型
                                  //假 表示为不完整型

        public int Loc => loc;
        public int Len { get => len; set => len = value; }
        public bool Type { get => type; set => type = value; }

        public Block(int _loc) => loc = _loc;

    }*/
    public bool check_qidui(/*List<int> tile*/)
    {
        bool dan = false; //是否有单牌
        int dan_index = -1; //单张牌的序号
        int each_add = 2;//在检测到单张牌之前，i每次加2,检测到一张单牌时，下一次只能加1

        for (int i = 1; i < tile.Count; i += each_add)
        {
            if (check_same(tile[i - 1], tile[i]) == false)//两张牌不同
            {
                if (dan == false)
                {
                    each_add = 1;
                    dan = true;
                    dan_index = i - 1;
                    /*if(check_same(tile[i], tile[i + 1]) == false)
                    {

                    }*/
                }
                else
                {
                    return false;//2张单张 必不听七对
                }
            }
            else
            {
                each_add = 2;
            }
        }
        if (dan == false)
        {
            dan_index = 12;//没有检测到单牌，单牌就是最后一个
        }
        chong.Add(tile[dan_index]);
        //ting = true;
        return true;
    }
    public bool check_same(int pai_last, int pai_now)
    {
        int key_pai_last = pai_last / 4;
        int key_pai_now = pai_now / 4;

        if (key_pai_last == key_pai_now)
        {
            return true;
        }
        return false;
    }

    public int link(int index) //返回与下一张牌的距离（sort）
    {
        if (index == 13 || tile[index] > 107)
        {
            return 100;
        }
        else
        {
            return tile[index + 1] / 4 - tile[index] / 4;
        }
    }

    public bool check_common_win()
    {
        block = new List<Block>(6) { new Block(0) };
        blockErr = 0;//bug
        Opponent temp_op = new Opponent();

        for (int i = 0; i < tile.Count - 1; i++)
        {
            if (link(i) > 1)//当该牌和下一张牌的关系不是连续或是相同时
            {
                //bug
                if (block.Count == 6)//当有6个块 而 将还有更多块 时，无听（最多6个块，而已有6个块时无听）
                {
                    return false;
                }
                block[block.Count - 1].Len = i - block[block.Count - 1].Loc + 1;//记录本块的长度
                block[block.Count - 1].Type = !type_trans_int_to_bool(block[block.Count - 1].Len % 3); //当长度可被三整除时，返回true，即可能为面子完整型
                if (block[block.Count - 1].Type == false)//非完整型时，errblock加一
                {
                    ++blockErr;
                }

                if (blockErr == 4)//当有4个非完整型时
                {
                    return false;
                }

                block.Add(new Block(i + 1));//为下一张牌添加新块，i+1是他在手牌的index

            }
        }

        block[block.Count - 1].Len = tile.Count - block[block.Count - 1].Loc;//最后一块长度
        block[block.Count - 1].Type = !type_trans_int_to_bool(block[block.Count - 1].Len % 3);
        if (block[block.Count - 1].Type == false)//非完整型时，block加一
        {
            ++blockErr;
        }
        if (blockErr == 4)//当有4个非完整型时，无听
        {
            return false;
        }
        foreach (Block each in block)//对可能的面子完整型进行判断
        {
            if (true == each.Type)
            {
                if (false == each.block_complete(this))//可能的面子完整型不构成完整型
                {
                    return false;//无听
                }
            }
        }
        switch (blockErr)
        {
            case 1://有一块不完整，必是全不完整型 3n+4
                foreach (Block each_err in block)
                {
                    if (each_err.Type == false)//是不完整型
                    {
                        if (true == each_err.block_ergodic(this, true))
                        {
                            //chong = each_err.temp_op.chong;
                            break;
                        }
                        else
                        {
                            return false;//无听
                        }
                    }
                }
                break;

            case 2://有2块不完整型 面子不完整型3n+2 雀头完整型3n+2
                int err_block_1 = 0;

                while (err_block_1 < block.Count)
                {
                    if (false == block[err_block_1].Type)
                    {
                        break;//当序号为err_block_1的块是不完整型，跳出循环，记录序号
                    }

                    err_block_1++;
                }

                int err_block_2 = err_block_1 + 1;

                while (err_block_2 < block.Count)
                {
                    if (false == block[err_block_2].Type)
                    {
                        break;
                    }

                    err_block_2++;
                }
                if (block.Count == err_block_2)
                {
                    return false;
                }

                if (block[err_block_1].block_ergodic(this, false) && block[err_block_2].block_qu_dui(this)
                    || block[err_block_2].block_ergodic(this, false) && block[err_block_1].block_qu_dui(this))
                {
                    //chong = block[err_block_1].temp_op.chong;
                    //chong.AddRange(block[err_block_1].temp_op.chong);
                    break;
                }
                else
                {
                    return false;
                }

            case 3://3块不完整型， 两块半不完整型3n+1 即一个坎张   一块雀头完整型3n+2
                int err_block = 0;
                int tile_chong = -1;
                while (err_block < block.Count)
                {
                    if (false == block[err_block].Type)
                    {
                        if (1 == block[err_block].Len % 3)
                        {
                            break;//记录第一个半不完整型序号
                        }
                    }
                    err_block++;
                }

                tile_chong = tile[block[err_block].Loc + block[err_block].Len - 1] + 4;//-1为最后一张牌的key 再+4为听的牌
                

                if (tile_chong / 4 != tile[block[err_block + 1].Loc] / 4 - 1)//与可能的坎张另一边不连续
                {
                    return false;
                }

                foreach (Block que_tou_block in block)
                {
                    if (false == que_tou_block.Type)
                    {
                        if (2 == que_tou_block.Len % 3)/*3n+2非完整型*/
                        {
                            if (false == que_tou_block.block_qu_dui(this))//没有雀头 即不是雀头完整型
                            {
                                return false;//无听
                            }
                        }
                    }
                }

                
                Block temp_block = new Block(0) { Len = block[err_block].Len + 1 + block[err_block + 1].Len };//新块长度为坎张两块+1（听的牌）

                /*合并两个半不完整型*/
                int i = 0;
                while (i < block[err_block].Len)
                {
                    temp_op.tile.Add(tile[block[err_block].Loc + i]);
                    i++;
                }

                temp_op.tile.Add(tile_chong);

                while (i < temp_block.Len)
                {
                    temp_op.tile.Add(tile[block[err_block].Loc + i]);
                    i++;
                }

                if (true == temp_block.block_complete(temp_op))
                {
                    chong.Add(tile_chong);
                }
                break;
        }

        ting = true;
        return true;
    }
    public bool type_trans_int_to_bool(int num)
    {
        if (num == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
