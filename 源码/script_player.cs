using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_player : MonoBehaviour
{
    public List<int> pai_wall = new List<int>();
    public GameObject GB_pai_manager;
    public bool is_add_the_14th_pai = false;
    public bool ting = false;
    public bool Rii_cii = false;
    protected int s_win = -2;
    //public script_player_pai pai;
    // public script_game_manager instance;
    //public GameObject test;

    private void Awake()
    {
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<script_pai_manager>().home_name = transform.name;//利用transform通过child或parent访问，
                                                                                                                    //需要用GetComponent<>()声明类名
        deal_pai_from_GM();//发牌
        

        //gameObject.Find(game_manager).GetComponent<script_game_manager>.for_test_num();
        //instance.for_test_num();
        // test.GetComponent<script_game_manager>().for_test_num();
    }


    public virtual void deal_pai_from_GM()//发牌
    {
      

    }
    public virtual void when_get_a_pai()
    {

        //  pai_wall.Sort();
        transform.GetChild(0).GetChild(0).GetChild(0).GetChild(13).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<script_pai_manager>().renovate_all();//pai_manager类更新并驱动pai类更新  为每张牌赋值并命名
        is_add_the_14th_pai = true;

        //next_turn();
    }//摸牌 打牌函数在pai类
    public virtual void next_turn()//摸切轮转
    {
        transform.GetChild(0).GetChild(0).GetChild(0).GetChild(13).gameObject.SetActive(false);
    }


    public virtual void auto_del()//自动摸切
    {
        //delay_del();
       // Invoke("delay_del", 1f);
        Invoke("delay_del", 0.1f);
    }
    public void delay_del()
    {   
        transform.GetChild(0).GetChild(0).GetChild(0).GetChild(13).GetComponent<script_player_pai>().del_a_pai();
    }
    public virtual void comfire_rii_cii()
    {
        Rii_cii = true;
    }

    public virtual void ron()
    {

    }
    public virtual void tsu_mo()
    {

    }
    public virtual void cancle()
    {

    }

    public virtual void hide_button()
    {

    }
    /*public void pai_list_to_GameObject()
    {
        for (int i = 1; i < pai_wall.Count + 1; i++)
        {
           if(pai.name == "pai_" + i)
           {
                pai.key = pai_wall[i - 1];
           }
    }  } */
}
