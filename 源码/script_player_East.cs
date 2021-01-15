using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_player_East : script_player
{
    public GameObject Button_rii_cii;
    public GameObject Button_ron;
    public GameObject Button_tsu_mo;
    public GameObject Button_cancle;


    private int can_tsu_mo;//-1表示自摸

    //private bool is_del_rii_ci_pai = false;
    public override void deal_pai_from_GM()
    {
        base.deal_pai_from_GM();
        pai_wall = GameObject.Find("game_manager").GetComponent<script_game_manager>().East_pai_wall;
        pai_wall.Sort();
        ting = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<script_pai_manager>().check_ting();
        GB_pai_manager.SetActive(true);//发牌完毕，启动pai_manager以  为string（位置名）赋值   同时启动pai  为GameObject（牌）赋key值  
    }
    //todo sort位置改变引起立直检测bug
    public override void when_get_a_pai()
    {
        base.when_get_a_pai();
        GameObject.Find("game_manager").GetComponent<script_game_manager>().East_turn = false;
        can_tsu_mo = transform.parent.parent.GetComponent<script_game_manager>().check_win(pai_wall[pai_wall.Count - 1], "East");//自摸检测
        if (true == Rii_cii && -1 != can_tsu_mo)
        {
            auto_del();
        }
        else if (true == ting)
        {
            
            
            if(false == Rii_cii)
            {
                // GameObject.Find("Rii_cii").SetActive(true);
                Button_rii_cii.SetActive(true);

            }
            else if(-1 == can_tsu_mo)
            {

            }
            else /*if (true == Rii_cii)*/
            {
                auto_del();
            }
        }
        else
        {
             //GameObject.Find("Rii_cii").SetActive(false);
            Button_rii_cii.SetActive(false);
        }
        
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<script_pai_manager>().show_all_pai_info();
    }
    public override void hide_button() /*隐藏按钮*/
    {
        base.hide_button();
        Button_rii_cii.SetActive(false);
        Button_ron.SetActive(false);
        Button_tsu_mo.SetActive(false);
        Button_cancle.SetActive(false);
    }
    public override void next_turn()
    {
        base.next_turn();
        ting = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<script_pai_manager>().check_ting();

        /*立直bug补救  修复bug需先完成向听检测*/
        if (true == ting)
        {
            if (false == Rii_cii)
            {
                // GameObject.Find("Rii_cii").SetActive(true);
                Button_rii_cii.SetActive(true);
                Button_cancle.SetActive(true);
                GameObject.Find("game_manager").GetComponent<script_game_manager>().Check_Rii_cii();
            }
            else /*if (true == Rii_cii)*/
            {
                auto_del();
            }
        }

        else
        {
            /*隐藏按钮*/
            hide_button();
        }

        GameObject.Find("game_manager").GetComponent<script_game_manager>().South_turn = true;
    }

    public override void comfire_rii_cii()
    {
        base.comfire_rii_cii();
        Button_rii_cii.SetActive(false);
        transform.parent.parent.GetComponent<script_game_manager>().game_continue();
        /*if (true == is_del_rii_ci_pai)
        { 
              //auto_del();

        }*/
    }
    //todo the two
    public override void ron()
    {
        base.ron();
        hide_button();
        transform.parent.parent.GetComponent<script_game_manager>().game_over_ron("East");
    }
    public override void tsu_mo()
    {
        base.tsu_mo();
        hide_button();
        transform.parent.parent.GetComponent<script_game_manager>().game_over_tsu_mo("East");
    }
    public override void cancle()
    {
        base.cancle();
        hide_button();
        transform.parent.parent.GetComponent<script_game_manager>().game_continue();
        if(-1 == can_tsu_mo && true == Rii_cii)
        {
            auto_del();
            
            transform.parent.parent.GetComponent<script_game_manager>().game_continue();
            hide_button();
        }
    }

}
