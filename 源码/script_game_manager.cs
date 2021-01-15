using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
 万  1-9m            36张  0~35（Key）
 饼  1-9p            36张  36~71
 索  1-9S            36张  72~107
 字  东南西北白发中  28张 108~135
 */
public class script_game_manager : MonoBehaviour
{
    public List<int> pai_montain = new List<int>();//牌山
    public readonly int pai_num = 136;//牌数量
    private int pai_point = 0;
    public int remaining_pai = 136;
    private bool stop_get_a_pai = false;
   // public static script_game_manager _instance;

    public GameObject East;
    public GameObject South;
    public GameObject West;
    public GameObject North;

   

    public List<int> East_pai_wall = new List<int>();
    public List<int> South_pai_wall = new List<int>();
    public List<int> West_pai_wall = new List<int>();
    public List<int> North_pai_wall = new List<int>();


    public bool East_turn = false;
    public bool South_turn = false;
    public bool West_turn = false;
    public bool North_turn = false;

    public static int test_num = 141;

    public List<int> East_chong;
    public List<int> South_chong;
    public List<int> West_chong;
    public List<int> North_chong;

    public List<int> East_she_pai;
    public List<int> South_she_pai;
    public List<int> West_she_pai;
    public List<int> North_she_pai;

    //public SpriteRenderer sr;
    private void Awake()
    {
        
        // _instance = this;
        game_initialization();
       // sr = GetComponent<SpriteRenderer>();
        //test_common_win_game_initialization();
        game_init_deal_pai();//发牌
      
        East_turn = true;
       
    }
    private void Update()
    {
        if (false == game_over_no_pai())
        {
             gaming();
        }
         else
         {
             print("牌无力");
         }
       
    }
    /*牌山初始化*/
    private void game_initialization()
    {
        for(int i = 0; i < pai_num; i++)
        {
            pai_montain.Add(i);
        }
       
        /*洗牌*/
        int index = 0;
        for(int i = 0; i < pai_montain.Count; i++)
        {
            index = Random.Range(0, pai_montain.Count - 1);

            pai_montain[i] += pai_montain[index];
            pai_montain[index] = pai_montain[i] - pai_montain[index]; 
            pai_montain[i] = pai_montain[i] - pai_montain[index]; 
        }
    }

    private void test_common_win_game_initialization()
    {
        /*初始化牌山*/
        for (int i = 0; i < pai_num; i++)
        {
            pai_montain.Add(i);
        }

        /*洗牌*/
        int index = 0;
        for (int i = 0; i < pai_montain.Count; i++)
        {
            index = Random.Range(14, pai_montain.Count - 1);

            pai_montain[i] += pai_montain[index];
            pai_montain[index] = pai_montain[i] - pai_montain[index];
            pai_montain[i] = pai_montain[i] - pai_montain[index];
        }
       
    }
    public void deal_pai(List<int> pai_wall, GameObject player)
    {
        for (int i = 0; i < 13; i++)
        {
            pai_wall.Add(pai_montain[pai_point]);
            pai_point++;
            remaining_pai--;
        }
        /*用于演示*/
        //test_koshi_win();
        //test_qi_dui();
        //test_common_win_ping_he();
        //test_common_win_kan();
        //test_common_win_liang_mian_que_tou();
        //test_common_win_que_tou_mian_zi();
        //test_common_win_9_lian();

        player.SetActive(true);//gameobject激活， gameobject对应cs执行
    }
    private void test_common_win_ping_he()
    {
        East_pai_wall[0] = 4;
        East_pai_wall[1] = 8;
        East_pai_wall[2] = 22;
        East_pai_wall[3] = 22;
        East_pai_wall[4] = 22;
        East_pai_wall[5] = 33;
        East_pai_wall[6] = 33;
        East_pai_wall[7] = 33;
        East_pai_wall[8] = 44;
        East_pai_wall[9] = 44;
        East_pai_wall[10] = 44;
        East_pai_wall[11] = 55;
        East_pai_wall[12] = 55;
    }
    private void test_common_win_9_lian()
    {
        East_pai_wall[0] = 0;
        East_pai_wall[1] = 0;
        East_pai_wall[2] = 0;
        East_pai_wall[3] = 4;
        East_pai_wall[4] = 8;
        East_pai_wall[5] = 12;
        East_pai_wall[6] = 16;
        East_pai_wall[7] = 20;
        East_pai_wall[8] = 24;
        East_pai_wall[9] = 28;
        East_pai_wall[10] = 32;
        East_pai_wall[11] = 32;
        East_pai_wall[12] = 32;
    }
    private void test_common_win_kan()
    {
        East_pai_wall[0] = 0;
        East_pai_wall[1] = 8;
        East_pai_wall[2] = 22;
        East_pai_wall[3] = 22;
        East_pai_wall[4] = 22;
        East_pai_wall[5] = 33;
        East_pai_wall[6] = 33;
        East_pai_wall[7] = 33;
        East_pai_wall[8] = 44;
        East_pai_wall[9] = 44;
        East_pai_wall[10] = 44;
        East_pai_wall[11] = 55;
        East_pai_wall[12] = 55;
    }
    private void test_common_win_liang_mian_que_tou()
    {
        East_pai_wall[0] = 0;
        East_pai_wall[1] = 4;
        East_pai_wall[2] = 8;
        East_pai_wall[3] = 12;
        East_pai_wall[4] = 55;
        East_pai_wall[5] = 33;
        East_pai_wall[6] = 33;
        East_pai_wall[7] = 33;
        East_pai_wall[8] = 44;
        East_pai_wall[9] = 44;
        East_pai_wall[10] = 44;
        East_pai_wall[11] = 55;
        East_pai_wall[12] = 55;
    }
    private void test_common_win_que_tou_mian_zi()
    {
        East_pai_wall[0] = 12;
        East_pai_wall[1] = 12;
        East_pai_wall[2] = 12;
        East_pai_wall[3] = 16;
        East_pai_wall[4] = 55;
        East_pai_wall[5] = 33;
        East_pai_wall[6] = 33;
        East_pai_wall[7] = 33;
        East_pai_wall[8] = 44;
        East_pai_wall[9] = 44;
        East_pai_wall[10] = 44;
        East_pai_wall[11] = 55;
        East_pai_wall[12] = 55;
    }
    private void test_koshi_win()
    {

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
        
        East_pai_wall[0] = 132;
        East_pai_wall[1] = 0;
        East_pai_wall[2] = 32;
        East_pai_wall[3] = 36;
        East_pai_wall[4] = 68;
        East_pai_wall[5] = 72;
        East_pai_wall[6] = 104;
        East_pai_wall[7] = 108;
        East_pai_wall[8] = 112;
        East_pai_wall[9] = 116;
        East_pai_wall[10] = 120;
        East_pai_wall[11] = 124;
        East_pai_wall[12] = 128;
        
    }
    private void test_qi_dui()
    {
        East_pai_wall[0] = 0;
        East_pai_wall[1] = 0;
        East_pai_wall[2] = 32;
        East_pai_wall[3] = 32;
        East_pai_wall[4] = 68;
        East_pai_wall[5] = 68;
        East_pai_wall[6] = 104;
        East_pai_wall[7] = 104;
        East_pai_wall[8] = 112;
        East_pai_wall[9] = 112;
        East_pai_wall[10] = 120;
        East_pai_wall[11] = 120;
        East_pai_wall[12] = 128;
    }

    /*游戏初始化 发牌给四家*/
    public void game_init_deal_pai()
    {
        deal_pai(East_pai_wall, East);//To East
        deal_pai(South_pai_wall, South);
        deal_pai(West_pai_wall, West);
        deal_pai(North_pai_wall, North);
    }
    

    


    public void gaming()
    {
        {
            if (true == East_turn && false == stop_get_a_pai)
            {
                add_a_pai(East);
            }
            else if (South_turn && false == stop_get_a_pai)
            {
                add_a_pai(South);
            }
            else if (West_turn && false == stop_get_a_pai)
            {
                add_a_pai(West);
            }
            else if (North_turn && false == stop_get_a_pai)
            {
                add_a_pai(North);  
            }
            else
            {
               
            }
        }
        
    }
   
    private void print_East_chong()
    {
        for (int i = 0; i < East_chong.Count; i++)
        {
            print(East_chong[i]);
        }
    }
   
    public void game_over_ron(string home_name)
    {
        
        stop_get_a_pai = true;
        print(home_name + "胡了");
        win();
    }
    public void game_over_tsu_mo(string home_name)
    {
        stop_get_a_pai = true;
        print(home_name + "自摸");
        win();
    }
    private void add_a_pai(GameObject player)
    {
        remaining_pai--;
        player.GetComponent<script_player>().pai_wall.Add(pai_montain[pai_point++]);
        player.GetComponent<script_player>().when_get_a_pai();
    }
    public  void for_test_num()
    {
        print(test_num);
    }

    public void when_del_a_pai(int key, string home)
    {
        switch (home)
        {
            case "East": 
                East_she_pai.Add(key);
                transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<script_player_pai>().key = key;
                transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<script_player_pai>().renovate();
                break;
            case "South":
                South_she_pai.Add(key);
                transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<script_player_pai>().key = key;
                transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<script_player_pai>().renovate();
                break;
            case "West":
                West_she_pai.Add(key);
                transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<script_player_pai>().key = key;
                transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<script_player_pai>().renovate();
                break;
            case "North": 
                North_she_pai.Add(key);
                transform.GetChild(1).GetChild(0).GetChild(0).GetChild(3).GetComponent<script_player_pai>().key = key;
                transform.GetChild(1).GetChild(0).GetChild(0).GetChild(3).GetComponent<script_player_pai>().renovate();
                break;
            default:break;
        }
        check_win(key, home);
    }
    public int check_win(int key, string home)
    {
        foreach(int chong in East_chong)
         {
            if(key / 4 == chong / 4)
            {
                print("东家胡了");
                if(home == "East")
                {
                    print("自摸");
                    stop_get_a_pai = true;
                    transform.GetChild(0).GetChild(0).GetComponent<script_player_East>().Button_tsu_mo.SetActive(true);
                    transform.GetChild(0).GetChild(0).GetComponent<script_player_East>().Button_cancle.SetActive(true);
                    return -1;
                }
                else
                {
                    print(home + "点炮");
                    stop_get_a_pai = true;
                    transform.GetChild(0).GetChild(0).GetComponent<script_player_East>().Button_ron.SetActive(true);
                    transform.GetChild(0).GetChild(0).GetComponent<script_player_East>().Button_cancle.SetActive(true);
                    return 1;
                }
            }
         }
        foreach (int chong in South_chong)
        {
            if (key / 4 == chong / 4)
            {
                print("南家胡了");
                if (home == "South")
                {
                    print("自摸");
                    return -1;
                }
                else
                {
                    print(home + "点炮");
                    return 1;
                }
            }
        }
        foreach (int chong in West_chong)
        {
            if (key / 4 == chong / 4)
            {
                print("西家胡了");
                if (home == "West")
                {
                    print("自摸");
                    return -1;
                }
                else
                {
                    print(home + "点炮");
                    return 1;
                }
            }
        }
        foreach (int chong in North_chong)
        {
            if (key / 4 == chong / 4)
            {
                print("北家胡了");
                if (home == "North")
                {
                    print("自摸");
                    return -1;
                }
                else
                {
                    print(home + "点炮");
                    return 1;
                }
            }
          
        }
        return 0;
        /*   switch (home)
           {
               case "East":
                   foreach (int chong in East_chong)
                   {
                       if (key == chong)
                       {
                           print("东家胡了");
                       }
                   }; break;
               case "South":
                   foreach (int chong in South_chong)
                   {
                       if (key == chong)
                       {
                           print("南家胡了");
                       }
                   }; break;
               case "West":
                   foreach (int chong in West_chong)
                   {
                       if (key == chong)
                       {
                           print("西家胡了");
                       }
                   }; break;
               case "North":
                   foreach (int chong in North_chong)
                   {
                       if (key == chong)
                       {
                           print("北家胡了");
                       }
                   }; break;
               default: break;
           }*/

    }

    /*立直bug补救  修复bug需先完成向听检测*/
    public void Check_Rii_cii()
    {
        stop_get_a_pai = true;
    }

    public void win()
    {
        print("游戏结束");
        //Application.Quit();
        load_scene_after_play();


    }

    public bool game_over_no_pai()
    {


        if (0 == remaining_pai)
        {
            Time.timeScale = 0;
            print("流局");
            // Application.Quit();
            load_scene_after_play();
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void game_continue()
    {
        stop_get_a_pai = false;
    }
    public void load_scene_after_play()
    {
        SceneManager.LoadScene(5);
    }
}
