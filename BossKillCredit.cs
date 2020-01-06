using Terraria.ModLoader;
using BossKillCredit.NPCs;

namespace BossKillCredit
{
    class BossKillCredit : Mod
    {
        private static WorldMessenger world_messenger;
        public BossKillCredit()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }
    }
}