using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameRuleManegenent : MonoBehaviour
{
    void AddPlatform(PlatformType type, int num, ref StageInfomation stageInfo, float moveSpeed = 0f, float beltSpeed = 0f, float gap = 0f)
    {
        for(int i = 0; i < num; i++)
        {
            var platform = new PlatformInfo
            {
                type = type,
                moveSpeed = moveSpeed,
                beltSpeed = beltSpeed,
                gap = gap
            };
            stageInfo.platforms.Add(platform);
        }
    }

    void AddItem(ItemType type, int num, ref StageInfomation stageInfo) 
    {
        for(var i = 0; i < num; ++i)
        {
            stageInfo.items.Add(type);
        }
    }

  void SetStageDatas()
    {
        const float normalSpeed = 2.5f;
        const float fastSpeed = 5f;

        const float normalGap = 2f;
        const float wideGap = 4f;


        //原作の1~20ステージを短縮

        //1
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 3;
            AddPlatform(PlatformType.Normal, 23, ref stageInfo);

            Stages[0] = stageInfo;
        }

        //2
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 3;
            AddPlatform(PlatformType.Normal,16, ref stageInfo);

            AddItem(ItemType.HighJump, 5, ref stageInfo);

            Stages[1] = stageInfo; 
        }

        //3
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 3;

            AddPlatform(PlatformType.Normal, 5, ref stageInfo);
            AddPlatform(PlatformType.Ice, 12, ref stageInfo);

            AddItem(ItemType.HighJump, 1, ref stageInfo);

            Stages[2] = stageInfo;
        }

        //4
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 3;

            AddPlatform(PlatformType.Normal, 1, ref stageInfo);
            AddPlatform(PlatformType.Move, 15, ref stageInfo, moveSpeed: normalSpeed);

            AddItem(ItemType.HighJump, 1, ref stageInfo);

            Stages[3] = stageInfo;
        }

        //5
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2.5f;

            AddPlatform(PlatformType.Normal, 6, ref stageInfo);
            AddPlatform(PlatformType.Ice, 3, ref stageInfo);
            AddPlatform(PlatformType.Move, 7, ref stageInfo, moveSpeed: normalSpeed);

            AddItem(ItemType.DoubleJump, 3, ref stageInfo);


            Stages[4] = stageInfo;
        }

        //6
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1.5f;

            AddPlatform(PlatformType.Normal, 3, ref stageInfo);
            AddPlatform(PlatformType.Ice, 3, ref stageInfo);
            AddPlatform(PlatformType.Move, 7, ref stageInfo, moveSpeed: normalSpeed);

            AddItem(ItemType.DoubleJump, 5, ref stageInfo);


            Stages[5] = stageInfo;
        }

        //7
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2;

            AddPlatform(PlatformType.Normal, 10, ref stageInfo);
            AddPlatform(PlatformType.Ice, 5, ref stageInfo);

            AddItem(ItemType.FastFall, 3, ref stageInfo);


            Stages[6] = stageInfo;
        }

        //8
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2.5f;

            AddPlatform(PlatformType.Normal, 2, ref stageInfo);
            AddPlatform(PlatformType.Belt, 14, ref stageInfo, beltSpeed: normalSpeed);

            AddItem(ItemType.HighJump, 2, ref stageInfo);

            Stages[7] = stageInfo;
        }

        //9
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2.5f;

            AddPlatform(PlatformType.Normal, 2, ref stageInfo);
            AddPlatform(PlatformType.Ice, 4, ref stageInfo);
            AddPlatform(PlatformType.Move, 5, ref stageInfo, moveSpeed: normalSpeed);
            AddPlatform(PlatformType.Belt, 7, ref stageInfo, beltSpeed: normalSpeed);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.FastFall, 1, ref stageInfo);

            Stages[8] = stageInfo;
        }

        //10
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2.5f;

            AddPlatform(PlatformType.Normal, 7, ref stageInfo);
            AddPlatform(PlatformType.Ice, 3, ref stageInfo);
            AddPlatform(PlatformType.Move, 3, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt, 3, ref stageInfo, beltSpeed: normalSpeed);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.DoubleJump, 5, ref stageInfo);
            AddItem(ItemType.FastFall, 1, ref stageInfo);

            Stages[9] = stageInfo;
        }

        //ここから新要素

        //11
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1f;

            AddPlatform(PlatformType.Normal, 7, ref stageInfo);
            AddPlatform(PlatformType.Ice, 3, ref stageInfo);
            AddPlatform(PlatformType.Move, 3, ref stageInfo, moveSpeed: normalSpeed);
            AddPlatform(PlatformType.Belt, 3, ref stageInfo, beltSpeed: normalSpeed);

            AddItem(ItemType.LongHorn, 3, ref stageInfo);

            Stages[10] = stageInfo;
        }

        //12
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2.5f;

            AddPlatform(PlatformType.Normal, 3, ref stageInfo);
            AddPlatform(PlatformType.Ice, 3, ref stageInfo);
            AddPlatform(PlatformType.Move, 10, ref stageInfo, moveSpeed: fastSpeed);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.WideSight, 3, ref stageInfo);

            Stages[11] = stageInfo;
        }

        //13
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1.5f;

            AddPlatform(PlatformType.Normal, 4, ref stageInfo);
            AddPlatform(PlatformType.Ice, 9, ref stageInfo);
            AddPlatform(PlatformType.Belt, 2, ref stageInfo, beltSpeed: normalSpeed);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.ConstSpeed, 3, ref stageInfo);

            Stages[12] = stageInfo;
        }

        //14
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2.5f;

            AddPlatform(PlatformType.Normal, 4, ref stageInfo);
            AddPlatform(PlatformType.Ice, 2, ref stageInfo);
            AddPlatform(PlatformType.Swamp, 9, ref stageInfo);

            AddItem(ItemType.DoubleJump, 2, ref stageInfo);
            AddItem(ItemType.FastFall, 2, ref stageInfo);

            Stages[13] = stageInfo;
        }

        //15
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1f;

            AddPlatform(PlatformType.Ice, 3, ref stageInfo);
            AddPlatform(PlatformType.Move, 3, ref stageInfo, moveSpeed: normalSpeed);
            AddPlatform(PlatformType.Gap, 10, ref stageInfo, gap: normalGap);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.LongHorn, 3, ref stageInfo);

            Stages[14] = stageInfo;
        }

        //16
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1.5f;

            AddPlatform(PlatformType.Normal, 16, ref stageInfo);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.LongHorn, 1, ref stageInfo);
            AddItem(ItemType.WideSight, 1, ref stageInfo);
            AddItem(ItemType.ConstSpeed, 1, ref stageInfo);

            Stages[15] = stageInfo;
        }

        //17
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2.5f;

            AddPlatform(PlatformType.Normal, 3, ref stageInfo);
            AddPlatform(PlatformType.Swamp, 7, ref stageInfo);
            AddPlatform(PlatformType.Gap, 7, ref stageInfo, gap: normalGap);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.FastFall, 1, ref stageInfo);

            Stages[16] = stageInfo;
        }

        //18
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2f;

            AddPlatform(PlatformType.Normal, 1, ref stageInfo);
            AddPlatform(PlatformType.Ice, 3 , ref stageInfo);
            AddPlatform(PlatformType.Move, 3, ref stageInfo, moveSpeed: normalSpeed);
            AddPlatform(PlatformType.Belt, 3, ref stageInfo, beltSpeed: normalSpeed);
            AddPlatform(PlatformType.Swamp, 3, ref stageInfo);
            AddPlatform(PlatformType.Gap, 3, ref stageInfo, gap: normalGap);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.FastFall, 1, ref stageInfo);
            AddItem(ItemType.LongHorn, 1, ref stageInfo);
            AddItem(ItemType.WideSight, 1, ref stageInfo);
            AddItem(ItemType.ConstSpeed, 1, ref stageInfo);

            Stages[17] = stageInfo;
        }

        //19
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2.5f;

            AddPlatform(PlatformType.Normal, 2, ref stageInfo);
            AddPlatform(PlatformType.Ice, 2, ref stageInfo);
            AddPlatform(PlatformType.Move, 4, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt,4, ref stageInfo, beltSpeed: fastSpeed);
            AddPlatform(PlatformType.Gap, 4, ref stageInfo, gap: wideGap);

            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.FastFall, 1, ref stageInfo);
            AddItem(ItemType.LongHorn, 1, ref stageInfo);

            Stages[18] = stageInfo;
        }

        //20
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1.5f;

            AddPlatform(PlatformType.Normal, 2, ref stageInfo);
            AddPlatform(PlatformType.Ice, 5, ref stageInfo);
            AddPlatform(PlatformType.Move, 10, ref stageInfo, moveSpeed: fastSpeed);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.WideSight, 1, ref stageInfo);

            Stages[19] = stageInfo;
        }

        //原作21~25若干改変

        //21
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1.75f;

            AddPlatform(PlatformType.Normal, 2, ref stageInfo);
            AddPlatform(PlatformType.Ice, 7, ref stageInfo);
            AddPlatform(PlatformType.Move, 5, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt, 9, ref stageInfo, beltSpeed: fastSpeed);

            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.FastFall, 5, ref stageInfo);

            Stages[20] = stageInfo;
        }

        //22
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2f;

            AddPlatform(PlatformType.Normal, 4, ref stageInfo);
            AddPlatform(PlatformType.Ice, 2, ref stageInfo);
            AddPlatform(PlatformType.Move, 9, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt, 1, ref stageInfo, beltSpeed: fastSpeed);

            Stages[21] = stageInfo;
        }

        //23
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1.25f;

            AddPlatform(PlatformType.Normal, 6, ref stageInfo);
            AddPlatform(PlatformType.Ice, 4, ref stageInfo);
            AddPlatform(PlatformType.Move, 2, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt, 3, ref stageInfo, beltSpeed: fastSpeed);

            AddItem(ItemType.HighJump, 3, ref stageInfo);
            AddItem(ItemType.DoubleJump, 3, ref stageInfo);

            Stages[22] = stageInfo;
        }

        //24
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 2f;

            AddPlatform(PlatformType.Move, 8, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt, 7, ref stageInfo, beltSpeed: fastSpeed);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.FastFall, 1, ref stageInfo);

            Stages[23] = stageInfo;
        }

        //25
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1.25f;

            AddPlatform(PlatformType.Normal, 1, ref stageInfo);
            AddPlatform(PlatformType.Ice, 3, ref stageInfo);
            AddPlatform(PlatformType.Move, 5, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt, 5, ref stageInfo, beltSpeed: fastSpeed);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.FastFall, 3, ref stageInfo);

            Stages[24] = stageInfo;
        }

        //新要素込みの高難度終盤ステージ

        //26
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1.5f;

            AddPlatform(PlatformType.Normal, 3, ref stageInfo);
            AddPlatform(PlatformType.Ice, 5, ref stageInfo);
            AddPlatform(PlatformType.Swamp, 5, ref stageInfo);
            AddPlatform(PlatformType.Gap, 7, ref stageInfo, gap: wideGap);

            AddItem(ItemType.LongHorn, 1, ref stageInfo);
            AddItem(ItemType.WideSight, 2, ref stageInfo);
            AddItem(ItemType.ConstSpeed, 3, ref stageInfo);

            Stages[25] = stageInfo;
        }

        //27
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1.5f;

            AddPlatform(PlatformType.Ice, 3, ref stageInfo);
            AddPlatform(PlatformType.Move, 5, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt, 4, ref stageInfo, beltSpeed: fastSpeed);
            AddPlatform(PlatformType.Swamp, 4, ref stageInfo);

            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.WideSight, 1, ref stageInfo);

            Stages[26] = stageInfo;
        }

        //28
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1f;

            AddPlatform(PlatformType.Normal, 1, ref stageInfo);
            AddPlatform(PlatformType.Ice, 5, ref stageInfo);
            AddPlatform(PlatformType.Move, 1, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt, 1, ref stageInfo, beltSpeed: fastSpeed);
            AddPlatform(PlatformType.Swamp, 1, ref stageInfo);
            AddPlatform(PlatformType.Gap, 5, ref stageInfo, gap: wideGap);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.FastFall, 1, ref stageInfo);
            AddItem(ItemType.LongHorn, 1, ref stageInfo);
            AddItem(ItemType.ConstSpeed, 1, ref stageInfo);

            Stages[27] = stageInfo;
        }

        //29
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1.5f;

            AddPlatform(PlatformType.Move, 6, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt, 3, ref stageInfo, beltSpeed: fastSpeed);
            AddPlatform(PlatformType.Swamp, 3, ref stageInfo);
            AddPlatform(PlatformType.Gap, 6, ref stageInfo, gap: wideGap);

            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.FastFall, 1, ref stageInfo);
            AddItem(ItemType.ConstSpeed, 1, ref stageInfo);

            Stages[28] = stageInfo;
        }

        //30
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 0.75f;

            AddPlatform(PlatformType.Normal, 1, ref stageInfo);
            AddPlatform(PlatformType.Ice, 2, ref stageInfo);
            AddPlatform(PlatformType.Move, 5, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt, 3, ref stageInfo, beltSpeed: fastSpeed);
            AddPlatform(PlatformType.Swamp, 1, ref stageInfo);
            AddPlatform(PlatformType.Gap, 2, ref stageInfo, gap: wideGap);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.FastFall, 1, ref stageInfo);
            AddItem(ItemType.LongHorn, 1, ref stageInfo);
            AddItem(ItemType.WideSight, 1, ref stageInfo);
            AddItem(ItemType.ConstSpeed, 1, ref stageInfo);

            Stages[29] = stageInfo;
        }
    }
}

/* template
//26
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 1f;

            AddPlatform(PlatformType.Normal, 1, ref stageInfo);
            AddPlatform(PlatformType.Ice, 3, ref stageInfo);
            AddPlatform(PlatformType.Move, 3, ref stageInfo, moveSpeed: fastSpeed);
            AddPlatform(PlatformType.Belt, 3, ref stageInfo, beltSpeed: fastSpeed);
            AddPlatform(PlatformType.Swamp, 3, ref stageInfo);
            AddPlatform(PlatformType.Gap, 3, ref stageInfo, gap: wideGap);

            AddItem(ItemType.HighJump, 1, ref stageInfo);
            AddItem(ItemType.DoubleJump, 1, ref stageInfo);
            AddItem(ItemType.FastFall, 1, ref stageInfo);
            AddItem(ItemType.LongHorn, 1, ref stageInfo);
            AddItem(ItemType.WideSight, 1, ref stageInfo);
            AddItem(ItemType.ConstSpeed, 1, ref stageInfo);

            Stages[25] = stageInfo;
        }
 */