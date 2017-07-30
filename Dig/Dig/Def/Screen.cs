using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dig.Def
{
    /// <summary>
    /// 画面サイズ用定数
    /// </summary>
    static class Screen
    {
        //16:9サイズ（1280x720, 1600x900, 1920x1080）
        //4:3サイズ（800x600, 1024x768, 1280x960）
        public static readonly int Width = 768;
        public static readonly int Height = 544;
        
        public static readonly int Block = 32;
        public static readonly int MaxRow = 16;
        public static readonly int MaxColumn = 24;
    }
}
