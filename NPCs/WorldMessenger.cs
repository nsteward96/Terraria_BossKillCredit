using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BossKillCredit.NPCs
{
    public class WorldMessenger : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (Main.expertMode && npc.boss)
            {
                try
                {
                    IList<string> ParticipatingPlayers = DetermineParticipatingPlayers(npc);
                    string ParticipatingPlayersString = BuildParticipatingPlayersString(npc, ParticipatingPlayers);
                    OutputParticipatingPlayersString(ParticipatingPlayersString);
                }
                catch(Exception e) {}
            }
            base.NPCLoot(npc);
        }

        private IList<string> DetermineParticipatingPlayers(NPC npc)
        {
            IList<string> ParticipatingPlayers = new List<string>();

            IList Players = Main.player;

            int ForeachCount = 0;
            foreach (Player player in Players)
            {
                if (player.name != "" && npc.playerInteraction[ForeachCount] == true)
                {
                    ParticipatingPlayers.Add(player.name);
                }
                ForeachCount++;
            }

            return ParticipatingPlayers;
        }

        private string BuildParticipatingPlayersString(NPC npc, IList<string> Players)
        {
            string ParticipatingPlayersString = "";
            int numberOfPlayers = Players.Count;

            if (numberOfPlayers > 2)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    if (i != numberOfPlayers-1)
                    {
                        ParticipatingPlayersString += (Players[i] + ", ");
                    }
                    else
                    {
                        ParticipatingPlayersString += "and " + Players[i] + " have claimed a bag of treasure from " + npc.FullName + "!";
                    }
                }
            }
            else if (numberOfPlayers == 2)
            {
                ParticipatingPlayersString = Players[0] + " and " + Players[1] + " have claimed a bag of treasure from " + npc.FullName + "!";
            }
            else
            {
                ParticipatingPlayersString = Players[0] + " has claimed a bag of treasure from " + npc.FullName + "";
            }

            return ParticipatingPlayersString;
        }

        private void OutputParticipatingPlayersString(string ParticipatingPlayersString)
        {
            if (Main.dedServ)
            {
                NetworkText text = NetworkText.FromLiteral(ParticipatingPlayersString);
                NetMessage.BroadcastChatMessage(text, Color.Violet);
            }
            else
            {
                Main.NewText(ParticipatingPlayersString);
            }
        }
    }
}