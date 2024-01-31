using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Data_manager
{
    public struct SceneName
    {
        public static string MainLevel{ get { return "Level Selection"; } }
        public static string StartingLevel { get { return "Starting Scene"; } }
        public static string CreateCharacterLevel { get { return "Create Character Scene"; } }
        public static string BattleScene { get { return "Battle scene"; } }

        public static string AddTowerScene { get { return "Add Dna Scene"; } }
    }
}