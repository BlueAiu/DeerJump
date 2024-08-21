using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameRuleManegenent : MonoBehaviour
{
  void SetStageDatas()
    {
        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 3;
            for(int i = 0; i < 23; i++)
            {
                var platform = new PlatformInfo();
                platform.type = PlatformType.Normal;

                stageInfo.platforms.Add(platform);
            }

            Stages[0] = stageInfo;
        }

        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 3;
            for (int i = 0; i < 17; i++)
            {
                var platform = new PlatformInfo();
                platform.type = PlatformType.Normal;

                stageInfo.platforms.Add(platform);
            }

            Stages[1] = stageInfo; 
        }

        {
            var stageInfo = new StageInfomation();

            stageInfo.platformWidth = 3;
            for (int i = 0; i < 15; i++)
            {
                var platform = new PlatformInfo();
                platform.type = PlatformType.Normal;

                stageInfo.platforms.Add(platform);
            }

            for(int i = 0; i < 5; i++)
            {
                stageInfo.items.Add(ItemType.HighJump);
            }

            Stages[2] = stageInfo;
        }
    }
}
