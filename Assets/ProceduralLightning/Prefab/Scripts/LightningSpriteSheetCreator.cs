//
// Procedural Lightning for Unity
// (c) 2015 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using UnityEngine;
using UnityEngine.UI;

using System.Collections;

namespace DigitalRuby.ThunderAndLightning
{
    
#if UNITY_EDITOR && !UNITY_WEBPLAYER

    public class LightningSpriteSheetCreator : MonoBehaviour
    {
        [Tooltip("The width of the spritesheet in pixels")]
        public int Width = 512;

        [Tooltip("The height of the spritesheet in pixels")]
        public int Height = 512;

        [Tooltip("The number of rows in the spritesheet")]
        public int Rows = 5;

        [Tooltip("The number of columns in the spritesheet")]
        public int Columns = 5;

        [Tooltip("Delay in between frames to export. Gives time for animations and effects to happen.")]
        public float FrameDelay = 0.1f;

        [Tooltip("Background color for the sprite sheet. Usually black or transparent.")]
        public Color BackgroundColor = Color.black;

        [Tooltip("The full path and file name to save the saved sprite sheet to")]
        public string SaveFileName;

        [Tooltip("The label to notify that the export is working and then completed")]
        public Text ExportCompleteLabel;

        private bool exporting;
        private RenderTexture renderTexture;

        private void Start()
        {

        }

        private void Update()
        {

        }

        private IEnumerator ExportFrame(int row, int column, float delay)
        {
            yield return new WaitForSecondsLightning(delay);

            float cellWidth = (float)Width / (float)Columns;
            float cellHeight = (float)Height / (float)Rows;
            float x = ((float)column * cellWidth) / (float)Width;
            float y = ((float)row * cellHeight) / (float)Height;
            float w = cellWidth / (float)Width;
            float h = cellHeight / (float)Height;

            Camera.main.clearFlags = CameraClearFlags.Nothing;
            Camera.main.targetTexture = renderTexture;
            Rect existingViewportRect = Camera.main.rect;
            Camera.main.rect = new Rect(x, 1.0f - y - h, w, h);
            Camera.main.Render();
            Camera.main.rect = existingViewportRect;
            Camera.main.targetTexture = null;
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
        }

        private IEnumerator PerformExport(float delay)
        {
            yield return new WaitForSecondsLightning(delay);

            RenderTexture.active = renderTexture;
            Texture2D png = new Texture2D(Width, Height, TextureFormat.ARGB32, false, false);
            png.ReadPixels(new Rect(0, 0, Width, Height), 0, 0);
            RenderTexture.active = null;
            byte[] pngBytes = png.EncodeToPNG();
            if (string.IsNullOrEmpty(SaveFileName))
            {
                SaveFileName = System.IO.Path.Combine(Application.persistentDataPath, "LightningSpriteSheet.png");
            }
            System.IO.File.WriteAllBytes(SaveFileName, pngBytes);
            ExportCompleteLabel.text = "Done!";
            exporting = false;
            renderTexture = null;
        }

        public void ExportTapped()
        {
            if (exporting)
            {
                return;
            }

            exporting = true;
            ExportCompleteLabel.text = "Processing...";
            renderTexture = new RenderTexture(Width, Height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
            renderTexture.anisoLevel = 4;
            renderTexture.antiAliasing = 4;
            RenderTexture.active = renderTexture;
            GL.Clear(true, true, BackgroundColor, 0.0f);
            RenderTexture.active = null;

            float delay = FrameDelay * 0.5f;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    StartCoroutine(ExportFrame(i, j, delay));
                    delay += FrameDelay;
                }
            }
            StartCoroutine(PerformExport(delay));
        }
    }

#endif

}