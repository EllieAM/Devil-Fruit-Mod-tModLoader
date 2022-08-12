using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilFruitMod.Items
{
    public class RayleighSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("This sword is currently unused.  It might be added if I ever get NPC melee to work.");  
        }

        public override void SetDefaults()
        {
            Item.damage = 50;           //The damage of your weapon
            Item.DamageType = DamageClass.MeleeNoSpeed/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;          //Is your weapon a melee weapon?
            Item.width = 44;            //Weapon's texture's width
            Item.height = 46;           //Weapon's texture's height
            Item.useTime = 20;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 20;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            Item.knockBack = 6;         //The force of knockback of the weapon. Maximum is 20
            Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
            Item.rare = 2;              //The rarity of the weapon, from -1 to 13
            Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
            Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
        }
    }
}