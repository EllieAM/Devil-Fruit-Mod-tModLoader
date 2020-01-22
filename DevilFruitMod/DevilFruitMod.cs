using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DevilFruitMod
{
    class DevilFruitMod : Mod
    {
        public static ModHotKey UsePowers1Hotkey;
        public static ModHotKey UsePowers2Hotkey;
        public static ModHotKey UsePowers3Hotkey;
        public static ModHotKey MobilityHotkey;
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
            MobilityHotkey = RegisterHotKey("Mobility Power", "Q");

            Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/ShockwaveEffect")); // The path to the compiled shader file.

            Filters.Scene["Shockwave1"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
            Filters.Scene["Shockwave1"].Load();
            Filters.Scene["Shockwave2"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
            Filters.Scene["Shockwave2"].Load();
        }

        public override void Unload()
        {
        }
    }
}
