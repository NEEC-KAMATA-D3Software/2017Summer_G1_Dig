﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLib.Device
{
    /// <summary>
    /// BGM読み込み
    /// </summary>
    public class BGMLoader : Loader
    {
        private Sound sound;

        public BGMLoader(Sound sound, string[,] resources) :
            base(resources)//親クラスで初期化
        {
            this.sound = sound;
            Initialize();
        }

        public override void Update()
        {
            //まず終了フラグを有効にして
            endFlag = true;

            //カウンタが最大に達してないか？
            if (counter < maxNum)
            {
                //BGM読み込み
                sound.LoadBGM(
                    resources[counter, 0], //アセット名
                    resources[counter, 1]);//ファイルパス
                //カウントアップ
                counter += 1;
                //まだ読み込むものがあったのでフラグを戻す
                endFlag = false;
            }
        }
    }
}
