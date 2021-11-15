using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace DevilFruitMod
{
    class DevilFruitMod : Mod
    {
        public static ModHotKey UsePowers1Hotkey;
        public static ModHotKey UsePowers2Hotkey;
        public static ModHotKey UsePowers3Hotkey;
        public static ModHotKey MiscHotkey;
        public static int hands;
        public static int hooks;
        public static bool npcShockwaveAvailable = true;
        //public static bool[] FruitTaken = { true };

        public DevilFruitMod()
        {
        }

        public override void Load()
        {
            UsePowers1Hotkey = RegisterHotKey("Ability 1", "Mouse1");
            UsePowers2Hotkey = RegisterHotKey("Ability 2", "Mouse2");
            UsePowers3Hotkey = RegisterHotKey("Ability 3", "Z");
            MiscHotkey = RegisterHotKey("Miscellaneous Power", "Q");

            //Big ol' thanks to Kazzymodus for helping me figure this shader business out
            //Without em', I wouldn't even have come close to understanding any of this
            if (Main.netMode != NetmodeID.Server)//My shader loading spot
            { 
                Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/ShockwaveEffect")); // The path to the compiled shader file.

                Ref<Effect> stoneRef = new Ref<Effect>(GetEffect("Effects/LoveStone"));
                GameShaders.Misc["LoveStone"] = new MiscShaderData(stoneRef, "StoneEffect");

                Filters.Scene["Shockwave1"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave1"].Load();
                Filters.Scene["Shockwave2"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave2"].Load();
            }
        }

        public override void Unload()
        {
        }
    }
}
