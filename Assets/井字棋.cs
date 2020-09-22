using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 井字棋 : MonoBehaviour
{
    private int turn = 1;
    /*记录谁的回合：turn=1为圈，turn==2为×*/
    int[][] board = new int[3][] { new int[3], new int[3], new int[3] };

    /*初始调用reset函数达到同样的效果*/
    void Start() {
        reset();
    }

    /*重新开局初始化*/
    void reset() {
        turn = 1;
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                board[i][j] = 0;
            }
        }
    }
 
    /*每一帧都检查是否满足结束游戏的条件*/
    int check() {
        /*横向获胜*/
        for (int i = 0; i < 3; i++) {
            if (board[i][0] != 0 && board[i][0] == board[i][1] && board[i][1] == board[i][2]) {
                return board[i][0];
            }
        }
        /*纵向获胜*/
        for (int i = 0; i < 3; i++) {
            if (board[0][i] != 0 && board[0][i] == board[1][i] && board[1][i] == board[2][i]) {
                return board[0][i];
            }
        }
        /*对角线获胜*/
        if (board[1][1] != 0 &&
            board[0][0]== board[1][1] && board[2][2] == board[1][1] ||
            board[0][2] == board[1][1] && board[2][0] == board[1][1]) {
            return board[1][1];
        }
        /*对局未结束*/
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                if (board[i][j] == 0) return 0;
            }
        }
        /*平局*/
        return 3;
    }

    
    void OnGUI() {
        int m = Screen.width / 2;
        GUI.Box(new Rect(m - 150, 130, 300, 420), "井字棋");
        /*如果reset被点击则重开*/
        if (GUI.Button(new Rect(m - 50, 500, 100, 35), new GUIContent("Reset", "点击重新开始"))) reset();
        GUI.Label(new Rect(m - 50, 535, 100, 35), GUI.tooltip);
        /*检查对局状态*/
        int res = check();
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
            	/*显示已有棋子*/
                if (board[i][j] == 1) {
                    GUI.Button(new Rect(m - 150 + i * 100, 200 + j * 100, 100, 100), "O");
                }
                else if (board[i][j] == 2) {
                    GUI.Button(new Rect(m - 150 + i * 100, 200 + j * 100, 100, 100), "X");
                }   
                /*显示本局下的棋子*/
                if(GUI.Button(new Rect(m - 150 + i * 100, 200 + j * 100, 100, 100), "")) { 
                    if (res == 0) {
                        if (turn == 1) {
                            board[i][j] = turn;
                            turn = 2;
                        }
                        else {
                            board[i][j] = turn;
                            turn = 1;
                        }
                    }  
                }
            }
        }
        /*显示目前对局情况*/
        if (res == 0) {
            GUI.Box(new Rect(m - 50, 165, 100, 35), "PLAY!");
        }
        else if (res == 1) {
            GUI.Box(new Rect(m - 50, 165, 100, 35), "O WIN!");
        }
        else if (res == 2) {
            GUI.Box(new Rect(m - 50, 165, 100, 35), "X WIN!");
        }
        else if (res == 3) {
            GUI.Box(new Rect(m - 50, 165, 100, 35), "平局!");
        }
    }
}
