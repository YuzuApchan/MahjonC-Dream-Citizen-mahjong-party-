using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_player_North : script_player
{
    public override void deal_pai_from_GM()
    {
        base.deal_pai_from_GM();
        pai_wall = GameObject.Find("game_manager").GetComponent<script_game_manager>().North_pai_wall;
        pai_wall.Sort();
        GB_pai_manager.SetActive(true);//发牌完毕，启动pai_manager以  为string（位置名）赋值   同时启动pai  为GameObject（牌）赋key值  
    }

    public override void when_get_a_pai()
    {
        base.when_get_a_pai();
        GameObject.Find("game_manager").GetComponent<script_game_manager>().North_turn = false;
        auto_del();
    }

    public override void next_turn()
    {
        base.next_turn();
       
        GameObject.Find("game_manager").GetComponent<script_game_manager>().East_turn = true;
    }
}