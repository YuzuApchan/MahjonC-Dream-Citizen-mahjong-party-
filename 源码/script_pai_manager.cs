using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
public class script_pai_manager : MonoBehaviour
{
    /*lsit(a) 参数a表示初始长度*/
    public string home_name = "test_home_name";

    // bool ting = false;
    public Opponent op = new Opponent();
    public List<int> chong;


    public Text East_ting; 

    private string text_ting_pai = "";
    private void Awake()
    {
       
        pai_list_to_key();
       

    }

    public void pai_list_to_key()
    {
        
        for (int i = 0; i < GameObject.Find(home_name).GetComponent<script_player>().pai_wall.Count; i++)
        {
            transform.GetChild(i).GetComponent<script_player_pai>().key = GameObject.Find(home_name).GetComponent<script_player>().pai_wall[i];
            transform.GetChild(i).GetComponent<script_player_pai>().pai_location = i + 1;
        }
    }

    public void renovate_all()
    {
       
        pai_list_to_key();//更新牌key
        for (int i = 0; i < GameObject.Find(home_name).GetComponent<script_player>().pai_wall.Count; i++)
        {
            transform.GetChild(i).GetComponent<script_player_pai>().renovate();//更新牌信息
        }
       
    }
    public void show_all_pai_info()
    {
        string pai_name_list = "牌为：";
        for (int i = 0; i < GameObject.Find(home_name).GetComponent<script_player>().pai_wall.Count; i++)
        {
           
            pai_name_list += transform.GetChild(i).GetComponent<script_player_pai>().pai_name + " ";
        }
        print(pai_name_list);
        
    }
    public bool check_ting()
    {
        if (op.check_ting())
        {
            chong = op.chong;
            transform.parent.parent.parent.parent.parent.GetComponent<script_game_manager>().East_chong = chong;
            //GameObject.Find("game_manager").GetComponent<script_game_manager>().East_chong = chong;
            print("听牌了");
            print_ting();
            /*for(int i = 0; i < op.chong.Count; i++)
            {
                print(op.chong[i]);
            }
            print("铳牌数量:" + op.chong.Count);
            */
            return true;
        }
        else
        {
            chong = op.chong;
            //transform.parent.parent.parent.parent.parent.GetComponent<script_game_manager>().East_chong = chong;
            //GameObject.Find("game_manager").GetComponent<script_game_manager>().East_chong = chong;
            print("未听牌");
            return false;
        }
        
    }
    public void print_ting()
    {
        text_ting_pai = "";

        int ting_pai_sort;
        int ting_pai_num;
        string ting_pai_name = "";
        foreach(int key in chong)
        {
            ting_pai_sort = key / 36;
            ting_pai_num = (key % 36 / 4) + 1;

            switch (ting_pai_sort)
            {
                case 0: //万
                    ting_pai_name = ting_pai_num + "万 ";
                    break;
                case 1: //饼
                    ting_pai_name = ting_pai_num + "饼 ";
                    break;
                case 2: //索
                    ting_pai_name = ting_pai_num + "索 ";

                    break;
                default: //字
                    switch (ting_pai_num)
                    {
                        case 1: ting_pai_name = "东 "; break;
                        case 2: ting_pai_name = "南 "; break;
                        case 3: ting_pai_name = "西 "; break;
                        case 4: ting_pai_name = "北 "; break;
                        case 5: ting_pai_name = "白 "; break;
                        case 6: ting_pai_name = "发 "; break;
                        case 7: ting_pai_name = "中 "; break;
                        default: break;
                    }
                    break;
            }

            text_ting_pai += ting_pai_name;
        }
        East_ting.text = text_ting_pai;
    }
}