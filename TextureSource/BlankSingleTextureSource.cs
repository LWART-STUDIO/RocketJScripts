using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DTerrain
{
    /// <summary>
    /// Blank MainTexture that will be filled with a color whenever you set any _mainTexture to it. (Remains only size of set _mainTexture, but not it's color data).
    /// </summary>
    public class BlankSingleTextureSource: SingleTextureSource
    {
        [SerializeField]
        private Color startingColor;

        private Texture2D _mainTexture;
        public Texture2D MainTexture
        {
            get => _mainTexture;
            set
            {
                base.Texture = value;

                MainTexture = new Texture2D(MainTexture.width, MainTexture.height);

                Color[] colors = new Color[MainTexture.width * MainTexture.height];
                colors = colors.Select(c => startingColor).ToArray();

                MainTexture.SetPixels(colors);
                MainTexture.Apply();

                Graphics.CopyTexture(MainTexture, OriginalTexture);
                OriginalTexture.Apply();
            }
        }

    }
}
