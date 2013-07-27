using UnityEngine;
using System.Collections;

public class ViewerGUI : MonoBehaviour {

	public bool glidShowFlag = true;
	public bool measurerShowFlag = false;

	void Start () {
		if (measurerShowFlag == true){
			SendMessage("OnMeasure");
		} else {
			SendMessage("OffMeasure");
		}
		if (glidShowFlag == true){
			SendMessage("OnGrid");
		} else {
			SendMessage("OffGrid");
		}
		SendMessage("ChangeModel", "Enbuoh");	
	}

	void OnGUI () {
		// 3Dモデル選択メニュー
		if (GUI.Button(new Rect(0, 0, 100, 50), "ジャローダ")) {
			SendMessage("ChangeModel", "Jalorda");
		}
		if (GUI.Button(new Rect(0, 50,  100, 50), "エンブオー")) {
			SendMessage("ChangeModel", "Enbuoh");
		}
		if (GUI.Button(new Rect(0, 100, 100, 50), "ダイケンキ")) {
			SendMessage("ChangeModel", "Daikenki");
		}
		if (GUI.Button(new Rect(0, 150, 100, 50), "")) {}
		if (GUI.Button(new Rect(0, 200, 100, 50), "")) {}
		if (GUI.Button(new Rect(0, 250, 100, 50), "")) {}
		if (GUI.Button(new Rect(0, 300, 100, 50), "")) {}
		if (GUI.Button(new Rect(0, 350, 100, 50), "")) {}
		if (GUI.Button(new Rect(0, 400, 100, 50), "")) {}
		if (GUI.Button(new Rect(0, 450, 100, 50), "")) {}
		if (GUI.Button(new Rect(0, 500, 100, 50), "")) {}
		if (GUI.Button(new Rect(0, 550, 100, 50), "")) {}
		if (GUI.Button(new Rect(0, 600, 100, 50), "")) {}
		if (GUI.Button(new Rect(0, 650, 100, 50), "")) {}

		// 機能メニュー
		if (GUI.Button(new Rect((Screen.width - 100), 0, 100, 50), "リセット")) {
			SendMessage("ResetPosition");
		}
		if (GUI.Button(new Rect((Screen.width - 100), 100, 100, 50), "強ライト")) {
			SendMessage("ChangeLightHigh");
		}
		if (GUI.Button(new Rect((Screen.width - 100), 150, 100, 50), "弱ライト")) {
			SendMessage("ChangeLightLow");
		}
		if (measurerShowFlag == true){
			if (GUI.Button(new Rect((Screen.width - 100), 250, 100, 50), "メジャー\nOFF")) {
				SendMessage("OffMeasure");
				measurerShowFlag = false;
			}
		} else {
			if (GUI.Button(new Rect((Screen.width - 100), 250, 100, 50), "メジャー\nON")) {
				SendMessage("OnMeasure");
				measurerShowFlag = true;
			}
		}
		if (glidShowFlag == true){
			if (GUI.Button(new Rect((Screen.width - 100), 300, 100, 50), "グリッド\n0FF")) {
				SendMessage("OffGrid");
				glidShowFlag = false;
			}
		} else {
			if (GUI.Button(new Rect((Screen.width - 100), 300, 100, 50), "グリッド\n0N")) {
				SendMessage("OnGrid");
				glidShowFlag = true;
			}
		}
	}
}
