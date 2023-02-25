using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MagicConst
{
    public static readonly string LIFE = "life";
    public static readonly string FIRE = "fire";
    public static readonly string WATER = "water";
    public static readonly string EARTH = "earth";
    public static readonly string FREEZE = "freeze";
    public static readonly string RAZOR = "razor";
    public static readonly string DARK = "dark";
    public static readonly string STEAM = "steam";
    public static readonly string POISON = "poison";
    public static readonly string ICE = "ice";
    public static readonly string SHIELD = "shield";
    public static readonly string STUN = "stun";

    public static readonly Dictionary<string, string> MODIFICATOR_BY_KEY = new Dictionary<string, string>()
    {
        {LIFE,"LifeModificator" },
        {FIRE,"FireModificator" },
        {WATER,"WaterModificator" },
        {EARTH,"EarthModificator" },
        {FREEZE,"FrezeModificator" },
        {RAZOR,"RazorModificator" },
        {DARK,"DarkModificator" },
        {STEAM,"SteamModificator" },
        {POISON,"PoisonModificator" },
        {ICE,"IceModificator" },
        {SHIELD,"ShieldModificator" },
        {STUN,"StunModificator" },
    };

    public static readonly Dictionary<string, string> MAGICS_BY_KEY = new Dictionary<string, string>()
    {
        {LIFE,"Magic" },
        {FIRE,"Magic" },
        {WATER,"Magic" },
        {EARTH,"Magic" },
        {FREEZE,"Magic" },
        {RAZOR,"Magic" },
        {DARK,"Magic" },
        {STEAM,"Magic" },
        {POISON,"Magic" },
        {ICE,"Magic" },
        {SHIELD,"Magic" },
    };

    public static readonly Dictionary<string, List<string>> MELT_CAST_SEQUENCES = new Dictionary<string, List<string>>()
    {
        {STEAM, new List<string>() { WATER, FIRE } },
        {ICE, new List<string>() { WATER, FREEZE } },
        {POISON, new List<string>() { WATER, DARK } },
    };

    public static readonly Dictionary<int, MetaSphere> META_SPHERES = new Dictionary<int, MetaSphere>()
    {
        {0b_00001000001, new MetaSphere("LifeDark", MetaSphereType.cost)},
        {0b_00100000001, new MetaSphere(WATER, MetaSphereType.element)},
        {0b_00000000110, new MetaSphere(STEAM, MetaSphereType.element)},
        {0b_00000010010, new MetaSphere("FireFreeze", MetaSphereType.cost)},
        {0b_00001000010, new MetaSphere("Explosion", MetaSphereType.damage)},
        {0b_00100000010, new MetaSphere(DARK, MetaSphereType.element)},
        {0b_01000000010, new MetaSphere(WATER, MetaSphereType.element)},
        {0b_00000010100, new MetaSphere(ICE, MetaSphereType.element)},
        {0b_00000100100, new MetaSphere(RAZOR, MetaSphereType.damage)},//Electro
        {0b_00001000100, new MetaSphere(POISON, MetaSphereType.element)},
        {0b_00000101000, new MetaSphere("EarthRazor", MetaSphereType.cost)},
        {0b_00010010000, new MetaSphere(WATER, MetaSphereType.element)},
    };

}