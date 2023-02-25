using com.czeeep.spell.magicmodel;
using com.czeeep.spell.modificator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MagicConst
{
    public static readonly string TYPE_SPRAY = "spray";
    public static readonly string TYPE_LAZER = "lazer";
    public static readonly string TYPE_PROJECTILE = "projectile";

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
    public static readonly string MINEDARK = "minedark";
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

    //tir, key, time, power, maxPower, damageFull, multiplierHitPoint, multiplierShieldPoint, slowdown, unArmor,
    //List<string> additionalEffects, type, List<string> meltCastSequences
    public static readonly Dictionary<string, ModificatorInfo> MAGICS_MODIFICATOR_BY_KEY = new Dictionary<string, ModificatorInfo>()
    {
        {LIFE, new  ModificatorInfo(1, TYPE_LAZER, LIFE, 5f, 5, -50f, 1, 0, 0, false, new List<string>(),  new List<string>(){ LIFE})},
        {FIRE, new  ModificatorInfo(1, TYPE_SPRAY, FIRE, 5f, 5, 50f, 1, 1, 0, false, new List<string>(), new List<string>(){ FIRE})},
        {WATER, new  ModificatorInfo(1, TYPE_SPRAY, WATER, 20f, 5, 0, 0, 0, 1, false, new List<string>(), new List<string>(){ WATER})},
        {EARTH, new  ModificatorInfo(1, TYPE_PROJECTILE, EARTH, 1f, 5, 50f, 1, 1, 0, false, new List<string>(){STUN}, new List<string>(){ EARTH})},
        {FREEZE, new  ModificatorInfo(1, TYPE_SPRAY, FREEZE, 10f, 5, 50f, 1, 1, 1, false, new List<string>(), new List<string>(){ FREEZE})},
        {RAZOR, new  ModificatorInfo(1, TYPE_LAZER, RAZOR, 5f, 5, 50f, 1, 2, 0, false, new List<string>(), new List<string>(){ RAZOR})},
        {DARK, new  ModificatorInfo(1, TYPE_LAZER, DARK, 10f, 5, 50f, 1, 0, 0, false, new List<string>(), new List<string>(){ DARK})},
        {SHIELD, new  ModificatorInfo(1, TYPE_LAZER, SHIELD, 5f, 5, -50f, 0, 1, 0, false, new List<string>(), new List<string>(){ SHIELD})},
        {STEAM, new  ModificatorInfo(2, TYPE_SPRAY, STEAM, 20f, 5, 0, 0, 0, 2, false, new List<string>(), new List<string>(){ WATER, FIRE})},
        {POISON, new  ModificatorInfo(2, TYPE_SPRAY, POISON, 10f, 5, 60, 1, 0, 2, false, new List<string>(), new List<string>(){ WATER, DARK})},
        {ICE, new  ModificatorInfo(2, TYPE_PROJECTILE, ICE, 1f, 5, 60, 2, 1, 0, false, new List<string>(){STUN, FREEZE}, new List<string>(){ WATER, FREEZE})},
        {FIREBALL1, new  ModificatorInfo(2, TYPE_PROJECTILE, FIREBALL1, 1f, 5, 60, 1, 1, 0, false, new List<string>(){STUN, FIRE}, new List<string>(){ EARTH, FIRE})},
        {DARKBALL, new  ModificatorInfo(2, TYPE_PROJECTILE, DARKBALL, 1f, 5, 60, 2, 1, 0, false, new List<string>(){STUN, DARK}, new List<string>(){ EARTH, DARK})},
        {SPEEDUP, new  ModificatorInfo(2, TYPE_LAZER, SPEEDUP, 20f, 5, -60, 1, 0, -1, false, new List<string>(), new List<string>(){ LIFE, RAZOR})},
        {MINEFIRE, new  ModificatorInfo(2, TYPE_PROJECTILE, MINEFIRE, 1f, 1, 60, 1, 1, 0, false, new List<string>(){STUN, FIRE, FORCE1}, new List<string>(){ SHIELD, FIRE})},
        {MINEWATER, new  ModificatorInfo(2, TYPE_PROJECTILE, MINEWATER, 1f, 1, 60, 1, 1, 0, false, new List<string>(){STUN, WATER, FORCE1}, new List<string>(){ SHIELD, WATER})},
        {MINEFREEZE, new  ModificatorInfo(2, TYPE_PROJECTILE, MINEFREEZE, 1f, 1, 60, 2, 1, 0, false, new List<string>(){STUN, FREEZE, FORCE1}, new List<string>(){ SHIELD, FREEZE})},
        {MINERAZOR, new  ModificatorInfo(2, TYPE_PROJECTILE, MINERAZOR, 1f, 1, 60, 1, 2, 0, true, new List<string>(){STUN, RAZOR, FORCE1}, new List<string>(){ SHIELD, RAZOR})},
        {MINEDARK, new  ModificatorInfo(2, TYPE_PROJECTILE, MINEDARK, 1f, 1, 60, 2, 1, 0, false, new List<string>(){STUN, DARK, FORCE1}, new List<string>(){ SHIELD, DARK})},
        {FORCE1, new  ModificatorInfo(3, TYPE_LAZER, FORCE1, 1f, 25, 0, 0, 0, 0, false, new List<string>(), new List<string>(){ WATER, FIRE, RAZOR})},
        {ICICLE1, new  ModificatorInfo(3, TYPE_PROJECTILE, ICICLE1, 1f, 5, 72, 2, 1, 0, false, new List<string>(){STUN, FREEZE}, new List<string>(){ WATER, WATER, FREEZE})},
        {FIREBALL2, new  ModificatorInfo(3, TYPE_PROJECTILE, FIREBALL2, 1f, 5, 72, 1, 1, 0, false, new List<string>(){STUN, FIRE}, new List<string>(){ EARTH, FIRE, FIRE})},
        {DARKFIREBALL, new  ModificatorInfo(3, TYPE_PROJECTILE, DARKFIREBALL, 1f, 5, 72, 2, 1, 0, false, new List<string>(){STUN, FIRE}, new List<string>(){ EARTH, DARK, FIRE})},
        {MINEPOISON, new  ModificatorInfo(3, TYPE_PROJECTILE, MINEPOISON, 1f, 1, 72, 2, 1, 0, false, new List<string>(){STUN, POISON, FORCE1}, new List<string>(){ SHIELD, WATER, DARK })},
        {MINEICE, new  ModificatorInfo(3, TYPE_PROJECTILE, MINEICE, 1f, 1, 72, 1, 1, 0, false, new List<string>(){STUN, ICE, FORCE1}, new List<string>(){ SHIELD, WATER, FREEZE })},
        {FIREBALL3, new  ModificatorInfo(4, TYPE_PROJECTILE, FIREBALL3, 1f, 5, 90, 1, 1, 0, false, new List<string>(){STUN, FIRE, FIRE}, new List<string>(){ EARTH, FIRE, FIRE, FIRE})},
    };

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
        {STUN,"StunModificator" }
    };

    public static readonly Dictionary<string, string> TYPE_MAGIC_BY_KEY = new Dictionary<string, string>()
    {
        {LIFE,TYPE_LAZER },
        {FIRE,TYPE_SPRAY },
        {WATER,TYPE_SPRAY },
        {EARTH,TYPE_PROJECTILE },
        {FREEZE,TYPE_SPRAY },
        {RAZOR,TYPE_LAZER },
        {DARK,TYPE_LAZER},
        {STEAM,TYPE_SPRAY },
        {POISON,TYPE_SPRAY },
        {ICE,TYPE_PROJECTILE },
        {SHIELD,TYPE_LAZER }
    };

    public static readonly Dictionary<int, string> TYPE_MAGIC = new Dictionary<int, string>()
    {
        {0b_0001, TYPE_SPRAY },
        {0b_0011, TYPE_LAZER },
        {0b_0111, TYPE_PROJECTILE }
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
        {SHIELD,"Magic" }
    };

    public static readonly Dictionary<string, List<string>> MELT_CAST_SEQUENCES = new Dictionary<string, List<string>>()
    {
        {STEAM, new List<string>() { WATER, FIRE } },
        {ICE, new List<string>() { WATER, FREEZE } },
        {POISON, new List<string>() { WATER, DARK } }
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
        {0b_1000001, new MetaSphere(COSTLIFEDARK, MetaSphereType.cost)},
        {0b_1000010, new MetaSphere(DAMAGEEXPLOSION, MetaSphereType.damage)},
        {0b_1000100, new MetaSphere(POISON, MetaSphereType.element)},
        {0b_1001000, new MetaSphere(DARKBALL, MetaSphereType.element)},
        {0b_10000010, new MetaSphere(MINEFIRE, MetaSphereType.element)},
        {0b_10000100, new MetaSphere(MINEWATER, MetaSphereType.element)},
        {0b_10010000, new MetaSphere(MINEFREEZE, MetaSphereType.element)},
        {0b_10100000, new MetaSphere(MINERAZOR, MetaSphereType.element)},
        {0b_11000000, new MetaSphere(MINEDARK, MetaSphereType.element)},
        {0b_100010000, new MetaSphere(WATER, MetaSphereType.element)},
        {0b_100100000, new MetaSphere(FORCE1, MetaSphereType.element)},
        {0b_1000000001, new MetaSphere(WATER, MetaSphereType.element)},
        {0b_1000000010, new MetaSphere(DARK, MetaSphereType.element)},
        {0b_1010000000, new MetaSphere(MINEPOISON, MetaSphereType.element)},
        {0b_10000000010, new MetaSphere(WATER, MetaSphereType.element)},
        {0b_10000000100, new MetaSphere(ICICLE1, MetaSphereType.element)},
        {0b_10010000000, new MetaSphere(MINEICE, MetaSphereType.element)},
        {0b_100000000010, new MetaSphere(FIREBALL2, MetaSphereType.element)},
        {0b_1000000000010, new MetaSphere(DARKFIREBALL, MetaSphereType.element)},
        {0b_10000100000000, new MetaSphere(FORCE2, MetaSphereType.element)},
        {0b_110000000000000, new MetaSphere(UNMODIFICATOR, MetaSphereType.element)},
        {0b_1010000000000000, new MetaSphere(GHOSTING, MetaSphereType.element)},
        {0b_10000010000000000, new MetaSphere(ICICLE2, MetaSphereType.element)},
        {0b_100000000000000010, new MetaSphere(FIREBALL3, MetaSphereType.element)},
        {0b_10000000000000100000, new MetaSphere(TAUREL, MetaSphereType.element)},
        {0b_100000000000000000010, new MetaSphere(FIREBALL4, MetaSphereType.element)},
        {0b_100000000010000000000000, new MetaSphere(TORNADO, MetaSphereType.element)},
        {0b_101000000000000000000000, new MetaSphere(REVIVAL, MetaSphereType.element)},
        {0b_110000000000000000000000, new MetaSphere(METEOR, MetaSphereType.element)},
        {0b_1000000000000010000000000, new MetaSphere(ICICLE3, MetaSphereType.element)},
        {0b_10100000000000000000000000, new MetaSphere(BLAST, MetaSphereType.element)},
        {0b_100000000000000000000000010, new MetaSphere(FIREBLAST, MetaSphereType.element)},
        {0b_100000000000100000000000000, new MetaSphere(ENERGYBLAST, MetaSphereType.element)},
        {0b_100000000010000000000000000, new MetaSphere(ICEBLAST, MetaSphereType.element)}
    };

}