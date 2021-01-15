using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_player_pai : MonoBehaviour
{
    public int key = 136;
    public int pai_sort = 4;//0 万   1 饼   2 索   3 字、
    public int pai_num = 10;// 字1-7 东南西北白发中
    public string pai_name = "test";
    public int pai_location = 15;
    public string home = "home_test";

    protected SpriteRenderer sr;//图片信息

    //public script_pai_manager home;
    //public script_player Home; 
    private void Awake()//awake调用须有控制 否则home.count为0   player脚本中setactive
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("back");
        if ("pai_gride" == transform.parent.name)
        {
            home = transform.parent.GetComponent<script_pai_manager>().home_name;
        }
        //print(home);

        //pai_list_to_key();//读取key     //2020年12月1日20:30:51 改为在manager中读减少计算量
        calculate_pai_sort_num();//计算牌类型和数字
        calculate_pai_name();//计算牌名字
        
        //print(Home.pai_wall.Count);
        //print(GameObject.Find("East").GetComponent<script_player>().pai_wall.Count);

        // home = GetComponent<script_pai_manager>();

        // print(transform.parent.GetComponent<script_pai_manager>().home_name);
        
       
    }
    public void renovate()
    {
      //  pai_list_to_key();//读取key
        calculate_pai_sort_num();//计算牌类型和数字
        calculate_pai_name();//计算牌名字
        show_pai();//更新牌图片
    }
   /* private void Update()
    {
       // renovate();
    }*/
    private void OnMouseDown()
    {
       // pai_list_to_key();
        //print(key);
       // print("第" + pai_location + "张牌");
       // calculate_pai_sort_num();//计算牌类型和数字
        //calculate_pai_name();//计算牌名字
       // print_pai_name();

        del_a_pai();
    }

    private void pai_list_to_key()
    {
        //for(int i = 1; i < GameObject.Find("East").GetComponent<script_player>().pai_wall.Count + 1; i++)
        //for(int i = 1; i < GameObject.Find(home).GetComponent<script_player>().pai_wall.Count + 1; i++)
        
        for (int i = 1; i < GameObject.Find(transform.parent.GetComponent<script_pai_manager>().home_name).GetComponent<script_player>().pai_wall.Count + 1; i++)
        {
            if (transform.name == "pai_" + i)
            {
                
                key = GameObject.Find(transform.parent.GetComponent<script_pai_manager>().home_name).GetComponent<script_player>().pai_wall[i - 1];
                pai_location = i;
               // key = GameObject.Find("East").GetComponent<script_player>().pai_wall[i - 1];
                //key = GameObject.Find(home).GetComponent<script_player>().pai_wall[i - 1];
            }
        }
    }

    private void calculate_pai_sort_num()
    {
        pai_sort = key / 36;
        pai_num = (key % 36 / 4) + 1; 
    }

    protected void calculate_pai_name()
    {
       /*
             万  1-9m            36张  0~35（Key）
             饼  1-9p            36张  36~71
             索  1-9S            36张  72~107
             字  东南西北白发中  28张 108~135
       */
       switch(pai_sort)
       {
            case 0: //万
                         pai_name = "m" + pai_num;
                break;
            case 1: //饼
                         pai_name = "p" + pai_num;
                break;
            case 2: //索
                         pai_name = "s" + pai_num;
                
                break;
            default: //字
                         switch(pai_num)
                         {
                            case 1: pai_name = "east"; break;
                            case 2: pai_name = "south"; break;
                            case 3: pai_name = "west"; break;
                            case 4: pai_name = "north"; break;
                            case 5: pai_name = "bai"; break;
                            case 6: pai_name = "fa"; break;
                            case 7: pai_name = "zhong"; break;

                           /* case 1: pai_name = "东"; break;
                            case 2: pai_name = "南"; break;
                            case 3: pai_name = "西"; break;
                            case 4: pai_name = "北"; break;
                            case 5: pai_name = "白"; break;
                            case 6: pai_name = "发"; break;
                            case 7: pai_name = "中"; break;*/
                    default:break;
                         }
                break;
       }
    }

    private void print_pai_name()
    {
        print(pai_name);
    }
    public void print_pai_info()
    {
        print(pai_name + " " + key + " " + pai_location);
    }
    public void del_a_pai()//打牌
    {
        if(GameObject.Find(home).GetComponent<script_player>().is_add_the_14th_pai)
        {
            GameObject.Find(home).GetComponent<script_player>().is_add_the_14th_pai = false;        //禁止再打出牌
            GameObject.Find("game_manager").GetComponent<script_game_manager>().when_del_a_pai(key, home);//舍牌添加, 放炮检测
            GameObject.Find(home).GetComponent<script_player>().pai_wall.RemoveAt(pai_location - 1);//移除选择打掉的牌 
                                                                                                     //Remove(i)移除为i的元素 RemoveAt(i)移除下标为i的元素

            GameObject.Find(home).GetComponent<script_player>().pai_wall.Sort();                    //打出牌后自动排序
            transform.parent.GetComponent<script_pai_manager>().renovate_all();                     //更新所有牌信息
            GameObject.Find(home).GetComponent<script_player>().next_turn();                        //摸切轮转，隐藏第14张牌
                                                                                                    //transform.parent.GetComponent<script_pai_manager>().check_ting();

        }
    }

    public void show_pai()
    {
        if ("East" == home || "home_test" == home)//测试默认东家，故只改变东家牌图片
        {
            sr.sprite = Resources.Load<Sprite>(pai_name);
        }
    }
}
