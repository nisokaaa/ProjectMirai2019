//
// Selected U3D Japanese fonts  sample scene's script
//
// the script encoding is UTF-8 with BOM
//

using UnityEngine;

namespace FutureCartographer.U3DJapaneseFont.Demo
{
	public class SampleScene : MonoBehaviour
	{
		public GUISkin customSkin;

		private int buttonW = 200;
		private int buttonH = 48;
		private int buttonT = 60;

		// draw GUI text
		void OnGUI()
		{
			int x = (Screen.width / 2) - buttonW / 2;
			int y = (Screen.height - buttonT * 3) / 2;

			GUI.skin = customSkin;

			// draw words "start at first"
			if (GUI.Button(new Rect(x, y + buttonT * 0, buttonW, buttonH), "最初から始める"))
			{
			}

			// draw word "continue"
			if (GUI.Button(new Rect(x, y + buttonT * 1, buttonW, buttonH), "続きから"))
			{
			}

			// draw word "setting"
			if (GUI.Button(new Rect(x, y + buttonT * 2, buttonW, buttonH), "環境設定"))
			{
			}
		}

		// camera rotation
		void Update()
		{
			this.transform.Rotate(0.0f, 10.0f * Time.deltaTime, 0.0f);
		}
	}
}
