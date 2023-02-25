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
    public static readonly string FIREBALL1 = "fireball1";
    public static readonly string COSTFIREFREZE = "costFireFreze";
    public static readonly string SPEEDUP = "speedup";
    public static readonly string DAMAGEELECTRO = "damageElectro";
    public static readonly string COSTEARTHRAZOR = "costEarthRazor";
    public static readonly string COSTLIFEDARK = "costLifeDark";
    public static readonly string DAMAGEEXPLOSION = "damageExplosion";
    public static readonly string DARKBALL = "darkball";
    public static readonly string MINEFIRE = "minefire";
    public static readonly string MINEWATER = "minewater";
    public static readonly string MINEFREEZE = "minefreeze";
    public static readonly string MINERAZOR = "minerazor";
    public static readonly string FORCE1 = "force1";
    public static readonly string MINEPOISON = "minepoison";
    public static readonly string ICICLE1 = "icicle1";
    public static readonly string MINEICE = "mineice";
    public static readonly string FIREBALL2 = "fireball2";
    public static readonly string DARKFIREBALL = "darkfireball";
    public static readonly string FORCE2 = "force2";
    public static readonly string UNMODIFICATOR = "unmodificator";
    public static readonly string GHOSTING = "ghosting";
    public static readonly string ICICLE2 = "icicle2";
    public static readonly string FIREBALL3 = "fireball3";
    public static readonly string TAUREL = "taurel";
    public static readonly string FIREBALL4 = "fireball4";
    public static readonly string TORNADO = "tornado";
    public static readonly string REVIVAL = "revival";
    public static readonly string METEOR = "meteor";
    public static readonly string ICICLE3 = "icicle3";
    public static readonly string BLAST = "blast";
    public static readonly string FIREBLAST = "fireblast";
    public static readonly string ENERGYBLAST = "energyblast";
    public static readonly string ICEBLAST = "iceblast";

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

        {0b_110, new MetaSphere(STEAM, MetaSphereType.element)},
        {0b_1010, new MetaSphere(FIREBALL1, MetaSphereType.element)},
        {0b_10010, new MetaSphere(COSTFIREFREZE, MetaSphereType.cost)},
        {0b_10100, new MetaSphere(ICE, MetaSphereType.element)},
        {0b_100001, new MetaSphere(SPEEDUP, MetaSphereType.element)},
        {0b_100100, new MetaSphere(DAMAGEELECTRO, MetaSphereType.damage)},
        {0b_101000, new MetaSphere(COSTEARTHRAZOR, MetaSphereType.cost)},
        {0b_101000, new MetaSphere(COSTLIFEDARK, MetaSphereType.cost)},
        {0b_101000, new MetaSphere(DAMAGEEXPLOSION, MetaSphereType.damage)},
        {0b_1000100, new MetaSphere(POISON, MetaSphereType.element)},
        /*{0b_1001000, new MetaSphere(DARKBALL, MetaSphereType.element)},
        {0b_10000010, new MetaSphere(minefire, MetaSphereType.element)},
        {0b_10000100, new MetaSphere(minewater, MetaSphereType.element)},
        {0b_10010000, new MetaSphere(minefreeze, MetaSphereType.element)},
        {0b_10100000, new MetaSphere(minerazor, MetaSphereType.element)},
        {0b_11000000, new MetaSphere(minedark, MetaSphereType.element)},
        {0b_100100000, new MetaSphere(force1, MetaSphereType.element)},
        {0b_1010000000, new MetaSphere(minepoison, MetaSphereType.element)},
        {0b_10000000100, new MetaSphere(icicle1, MetaSphereType.element)},
        {0b_10010000000, new MetaSphere(mineice, MetaSphereType.element)},
        {0b_100000000010, new MetaSphere(fireball2, MetaSphereType.element)},
        {0b_1000000000010, new MetaSphere(darkfireball, MetaSphereType.element)},
        {0b_10000100000000, new MetaSphere(force2, MetaSphereType.element)},
        {0b_110000000000000, new MetaSphere(unmodificator, MetaSphereType.element)},
        {0b_1010000000000000, new MetaSphere(ghosting, MetaSphereType.element)},
        {0b_1010000000000000, new MetaSphere(ghosting, MetaSphereType.element)},*/
        /*{0b_00001000001, new MetaSphere("LifeDark", MetaSphereType.cost)},
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
        {0b_00010010000, new MetaSphere(WATER, MetaSphereType.element)},*/
    };

}