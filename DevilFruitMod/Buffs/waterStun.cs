using Terraria;
using Terraria.ModLoader;

namespace DevilFruitMod.Buffs
{
    public class waterStun : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Can't Swim");
            Description.SetDefault("Your devil fruit is preventing you from swimming!");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            DevilFruitUser user = player.GetModPlayer<DevilFruitUser>();

            if (user.eatenDevilFruit > 0 && player.wet && !(player.honeyWet || player.lavaWet))
            {
                // Some other effects:
                //player.lifeRegen++;
                //player.meleeCrit += 2;
                //player.meleeDamage += 0.051f;
                //player.meleeSpeed += 0.051f;
                //player.statDefense += 3;
                //player.moveSpeed += 0.05f;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}