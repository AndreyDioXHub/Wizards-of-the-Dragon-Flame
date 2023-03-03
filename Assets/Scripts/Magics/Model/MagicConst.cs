using com.czeeep.spell.biom;
using com.czeeep.spell.magicmodel;
using com.czeeep.spell.modificator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MagicConst
{
    public static readonly string ERROR_KEY = "error key";
    public static readonly string TYPE_SPRAY = "spray";
    public static readonly string TYPE_LAZER = "lazer";
    public static readonly string TYPE_PROJECTILE = "projectile";
    public static readonly string TYPE_MINE = "mine";
    public static readonly string TYPE_BLAST = "blast";

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
    public static readonly string MINELIFE = "minelife";
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
    public static readonly string UNARMOR = "unarmor";

    //tir, key, time, power, maxPower, damageFull, multiplierHitPoint, multiplierShieldPoint, slowdown, unArmor,
    //List<string> additionalEffects, type, List<string> meltCastSequences
    public static readonly Dictionary<string, ModificatorInfo> MAGICS_MODIFICATOR_BY_KEY = new Dictionary<string, ModificatorInfo>()
    {
        {LIFE, new  ModificatorInfo(1, LIFE, 5f, 5, -50f, 1, 0, 0, new List<string>(), new List<string>(){DARK})},
        {FIRE, new  ModificatorInfo(1, FIRE, 5f, 5, 50f, 1, 1, 0, new List<string>(), new List<string>(){FREEZE, WATER})},
        {WATER, new  ModificatorInfo(1, WATER, 20f, 5, 0f, 0, 0, 1, new List<string>(), new List<string>(){FIRE, STEAM, RAZOR})},
        {EARTH, new  ModificatorInfo(1, EARTH, 1f, 5, 50f, 1, 1, 0, new List<string>(){STUN})},
        {FREEZE, new  ModificatorInfo(1, FREEZE, 10f, 5, 50f, 1, 0, 1, new List<string>(), new List<string>(){ FIRE, WATER})},
        {RAZOR, new  ModificatorInfo(1, RAZOR, 5f, 5, 50f, 1, 2, 0, new List<string>(), new List<string>(){WATER})},
        {DARK, new  ModificatorInfo(1, DARK, 10f, 5, 50f, 1, 0, 1, new List<string>(), new List<string>(){LIFE, WATER })},
        {SHIELD, new  ModificatorInfo(1, SHIELD, 5f, 5, -50f, 0, 1, 0, new List<string>())},
        {STEAM, new  ModificatorInfo(2, STEAM, 20f, 5, 0f, 0, 0, 2, new List<string>(), new List<string>(){FIRE, WATER, RAZOR})},
        {POISON, new  ModificatorInfo(2, POISON, 10f, 5, 60f, 1, 0, 2, new List<string>(), new List<string>(){LIFE, WATER})},
        {ICE, new  ModificatorInfo(2, ICE, 1f, 5, 60f, 2, 1, 0, new List<string>(){FREEZE, STUN})},
        {FIREBALL1, new  ModificatorInfo(2, FIREBALL1, 1f, 5, 60f, 1, 1, 0, new List<string>(){FIRE, STUN})},
        {DARKBALL, new  ModificatorInfo(2, DARKBALL, 1f, 5, 60f, 2, 1, 0, new List<string>(){DARK, STUN})},
        {SPEEDUP, new  ModificatorInfo(2, SPEEDUP, 20f, 5, -60f, 1, 0, -2, new List<string>())},
        {MINELIFE, new  ModificatorInfo(2, MINELIFE, 1f, 1, -60f, 1, 0, 0, new List<string>(){LIFE, FORCE1, STUN})},
        {MINEFIRE, new  ModificatorInfo(2, MINEFIRE, 1f, 1, 60f, 1, 1, 0, new List<string>(){FIRE, FORCE1, STUN})},
        {MINEWATER, new  ModificatorInfo(2, MINEWATER, 1f, 1, 60f, 1, 1, 1, new List<string>(){WATER, FORCE1, STUN})},
        {MINEFREEZE, new  ModificatorInfo(2, MINEFREEZE, 1f, 1, 60f, 2, 1, 0, new List<string>(){FREEZE, FORCE1, STUN})},
        {MINERAZOR, new  ModificatorInfo(2, MINERAZOR, 1f, 1, 60f, 1, 2, 0, new List<string>(){RAZOR, FORCE1, STUN, UNARMOR})},
        {MINEDARK, new  ModificatorInfo(2, MINEDARK, 1f, 1, 60f, 2, 1, 0, new List<string>(){DARK, FORCE1, STUN})},
        {FORCE1, new  ModificatorInfo(3, FORCE1, 1f, 25, 0f, 0, 0, 0, new List<string>())},
        {ICICLE1, new  ModificatorInfo(3, ICICLE1, 1f, 5, 72f, 2, 1, 0, new List<string>(){FREEZE, STUN})},
        {FIREBALL2, new  ModificatorInfo(3, FIREBALL2, 1f, 5, 72f, 1, 1, 0, new List<string>(){FIRE, STUN})},
        {DARKFIREBALL, new  ModificatorInfo(3, DARKFIREBALL, 1f, 5, 72f, 2, 1, 0, new List<string>(){DARK, FIRE, STUN})},
        {MINEPOISON, new  ModificatorInfo(3, MINEPOISON, 1f, 1, 72f, 2, 1, 0, new List<string>(){POISON, FORCE1, STUN})},
        {MINEICE, new  ModificatorInfo(3, MINEICE, 1f, 1, 72f, 2, 1, 0, new List<string>(){ICE, FORCE1, STUN})},
        {UNARMOR, new  ModificatorInfo(3, UNARMOR, 1f, 1, 0f, 0, 0, 0, new List<string>())},
        {FIREBALL3, new  ModificatorInfo(4, FIREBALL3, 1f, 5, 90f, 1, 1, 0, new List<string>(){FIRE, FIRE, STUN})},
        {TAUREL, new  ModificatorInfo(4, TAUREL, 1f, 5, 90f, 1, 2, 0, new List<string>(){RAZOR, RAZOR, UNARMOR})},
        {GHOSTING, new  ModificatorInfo(5, GHOSTING, 1f, 5, 0f, 0, 0, 1, new List<string>(){UNARMOR})},
        {UNMODIFICATOR, new  ModificatorInfo(5, UNMODIFICATOR, 1f, 1, -110f, 1, 1, -10, new List<string>(){})},
        {FIREBALL4, new  ModificatorInfo(5, FIREBALL4, 2f, 5, 110f, 1, 1, 0, new List<string>(){FIRE, FIRE,FIRE,FIRE, STUN, STUN})},
        {FORCE2, new  ModificatorInfo(5, FORCE2, 2f, 25, 0f, 0, 0, 0, new List<string>())},
        {ICICLE2, new  ModificatorInfo(5, ICICLE2, 1f, 5, 110f, 2, 1, 0, new List<string>(){FREEZE, FREEZE, FREEZE, STUN, STUN})},
        {ICICLE3, new  ModificatorInfo(7, ICICLE3, 2f, 5, 160f, 2, 1, 0, new List<string>(){FREEZE, FREEZE, FREEZE,  FREEZE, STUN, STUN, STUN})},
        {TORNADO, new  ModificatorInfo(8, TORNADO, 5f, 5, 190f, 1, 1, 0, new List<string>(){FORCE1, FORCE1, FORCE1, FORCE1, FORCE1})},
        {REVIVAL, new  ModificatorInfo(10, REVIVAL, 1f, 5, 0f, 0, 0, 0, new List<string>())},
        {METEOR, new  ModificatorInfo(10, METEOR, 5f, 1, 280f, 1, 1, 0, new List<string>(){FIRE, FIRE, FIRE, FIRE, FIRE, STUN, STUN, STUN})},
        {BLAST, new  ModificatorInfo(12, BLAST, 5f, 1, 336f, 1, 1, 0, new List<string>(){FORCE1, FORCE1, FORCE1, FORCE1, FORCE1, STUN, STUN, STUN})},
        {FIREBLAST, new  ModificatorInfo(13, FIREBLAST, 5f, 1, 400f, 1, 1, 0, new List<string>(){FIRE, FORCE1, FORCE1, FORCE1, FORCE1, FORCE1, STUN, STUN, STUN})},
        {ENERGYBLAST, new  ModificatorInfo(14, ENERGYBLAST, 5f, 1, 480f, 1, 2, 0, new List<string>(){RAZOR, FORCE1, FORCE1, FORCE1, FORCE1, FORCE1, STUN, STUN, STUN})},
        {ICEBLAST, new  ModificatorInfo(15, ICEBLAST, 5f, 1, 575f, 2, 1, 0, new List<string>(){FREEZE, FORCE1, FORCE1, FORCE1, FORCE1, FORCE1, STUN, STUN, STUN})},
        {STUN, new  ModificatorInfo(1, STUN, 1f, 5, 0f, 0, 0, 0, new List<string>())}
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
        {STUN,"StunModificator" },
        {FIREBALL1,"FireBall1Modificator" },
        {DARKBALL,"DarkBallModificator" },
        {SPEEDUP,"SpeedUpModificator" },
        {MINELIFE,"MineLifeModificator" },
        {MINEFIRE,"MineFireModificator" },
        {MINEWATER,"MineWaterModificator" },
        {MINEFREEZE,"MineFreezeModificator" },
        {MINERAZOR,"MineRazorModificator" },
        {MINEDARK,"MineDarkModificator" },
        {FORCE1,"Force1Modificator" },
        {ICICLE1,"Icicle1Modificator" },
        {FIREBALL2,"FireBall2Modificator" },
        {DARKFIREBALL,"DarkFireBallModificator" },
        {MINEPOISON,"MinePoisonModificator" },
        {MINEICE,"MineIceModificator" },
        {UNARMOR,"UnArmorModificator" },
        {FIREBALL3,"FireBall3Modificator" },
        {TAUREL,"TaurelModificator" },
        {GHOSTING,"GhostingModificator" },
        {UNMODIFICATOR,"UnModificatorModificator" },
        {FIREBALL4,"FireBall4Modificator" },
        {FORCE2,"Force2Modificator" },
        {ICICLE2,"Icicle2Modificator" },
        {ICICLE3,"Icicle3Modificator" },
        {TORNADO,"TornadoModificator" },
        {REVIVAL,"RevivalModificator" },
        {METEOR,"MeteorModificator" },
        {BLAST,"BlastModificator" },
        {FIREBLAST,"FireBlastModificator" },
        {ENERGYBLAST,"EnergyBlastModificator" },
        {ICEBLAST,"IceBlastModificator" }
    };

    /*
    public static readonly Dictionary<string, ProjectileInfo> TYPE_PROJECTILE_BY_KEY = new Dictionary<string, ProjectileInfo>()
    {
        {BLAST, new ProjectileInfo(20, 5, 0) },
        {FIREBLAST, new ProjectileInfo(20, 5, 0) },
        {ENERGYBLAST, new ProjectileInfo(20, 5, 0) },
        {ICEBLAST, new ProjectileInfo(20, 5, 0) }
    };
    */

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
        {SHIELD,TYPE_LAZER },
        {FIREBALL1,TYPE_PROJECTILE },
        {DARKBALL,TYPE_PROJECTILE },
        {SPEEDUP,TYPE_LAZER },
        {MINEFIRE,TYPE_MINE},
        {MINELIFE,TYPE_MINE},
        {MINEWATER,TYPE_MINE },
        {MINEFREEZE,TYPE_MINE },
        {MINERAZOR,TYPE_MINE },
        {MINEDARK,TYPE_MINE },
        {FORCE1,TYPE_LAZER },
        {ICICLE1,TYPE_PROJECTILE },
        {FIREBALL2,TYPE_PROJECTILE },
        {DARKFIREBALL,TYPE_PROJECTILE },
        {MINEPOISON,TYPE_MINE },
        {MINEICE,TYPE_MINE },
        {UNARMOR,TYPE_LAZER },
        {FIREBALL3,TYPE_PROJECTILE },
        {TAUREL,TYPE_PROJECTILE },
        {GHOSTING,TYPE_LAZER },
        {UNMODIFICATOR,TYPE_LAZER },
        {FIREBALL4,TYPE_PROJECTILE },
        {FORCE2,TYPE_LAZER },
        {ICICLE2,TYPE_PROJECTILE },
        {ICICLE3,TYPE_PROJECTILE },
        {TORNADO,TYPE_LAZER },
        {REVIVAL,TYPE_PROJECTILE },
        {METEOR,TYPE_PROJECTILE },
        {BLAST,TYPE_BLAST },
        {FIREBLAST,TYPE_BLAST },
        {ENERGYBLAST,TYPE_BLAST },
        {ICEBLAST,TYPE_BLAST }
    };

    public static readonly Dictionary<int, string> TYPE_MAGIC = new Dictionary<int, string>()
    {
        {0b_00001, TYPE_SPRAY },
        {0b_00011, TYPE_LAZER },
        {0b_00111, TYPE_PROJECTILE },
        {0b_01111, TYPE_MINE },
        {0b_11111, TYPE_BLAST }
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
        {FIREBALL1,"Magic" },
        {DARKBALL,"Magic" },
        {SPEEDUP,"Magic" },
        {MINELIFE,"Magic" },
        {MINEFIRE,"Magic" },
        {MINEWATER,"Magic" },
        {MINEFREEZE,"Magic" },
        {MINERAZOR,"Magic" },
        {MINEDARK,"Magic" },
        {FORCE1,"Magic" },
        {ICICLE1,"Magic" },
        {FIREBALL2,"Magic" },
        {DARKFIREBALL,"Magic" },
        {MINEPOISON,"Magic" },
        {MINEICE,"Magic" },
        {UNARMOR,"Magic" },
        {FIREBALL3,"Magic" },
        {TAUREL,"Magic" },
        {GHOSTING,"Magic" },
        {UNMODIFICATOR,"Magic" },
        {FIREBALL4,"Magic" },
        {FORCE2,"Magic" },
        {ICICLE2,"Magic" },
        {ICICLE3,"Magic" },
        {TORNADO,"Magic" },
        {REVIVAL,"Magic" },
        {METEOR,"Magic" },
        {BLAST,"Magic" },
        {FIREBLAST,"Magic" },
        {ENERGYBLAST,"Magic" },
        {ICEBLAST,"Magic" }
    };

    public static readonly Dictionary<string, List<string>> MELT_CAST_SEQUENCES = new Dictionary<string, List<string>>()
    {
        {LIFE, new List<string>(){LIFE}},
        {FIRE, new List<string>(){FIRE}},
        {WATER, new List<string>(){WATER}},
        {EARTH, new List<string>(){EARTH}},
        {FREEZE, new List<string>(){FREEZE}},
        {RAZOR, new List<string>(){RAZOR}},
        {DARK, new List<string>(){DARK}},
        {SHIELD, new List<string>(){SHIELD}},
        {STEAM, new List<string>(){WATER, FIRE}},
        {POISON, new List<string>(){ WATER, DARK}},
        {ICE, new List<string>(){WATER, FREEZE}},
        {FIREBALL1, new List<string>(){EARTH, FIRE}},
        {DARKBALL, new List<string>(){EARTH, DARK}},
        {SPEEDUP, new List<string>(){LIFE, RAZOR}},
        {MINELIFE, new List<string>(){SHIELD, LIFE}},
        {MINEFIRE, new List<string>(){SHIELD, FIRE}},
        {MINEWATER, new List<string>(){SHIELD, WATER}},
        {MINEFREEZE, new List<string>(){SHIELD, FREEZE}},
        {MINERAZOR, new List<string>(){SHIELD, RAZOR}},
        {MINEDARK, new List<string>(){SHIELD, DARK}},
        {FORCE1, new List<string>(){WATER, FIRE, RAZOR}},
        {ICICLE1, new List<string>(){WATER, WATER, FREEZE}},
        {FIREBALL2, new List<string>(){EARTH, FIRE, FIRE}},
        {DARKFIREBALL, new List<string>(){EARTH, DARK, FIRE}},
        {MINEPOISON, new List<string>(){SHIELD, WATER, DARK}},
        {MINEICE, new List<string>(){SHIELD, WATER, FREEZE}},
        {UNARMOR, new List<string>(){SHIELD, RAZOR, RAZOR}},
        {FIREBALL3, new List<string>(){EARTH, FIRE, FIRE, FIRE}},
        {TAUREL, new List<string>(){SHIELD, WATER, FREEZE, RAZOR}},
        {GHOSTING, new List<string>(){WATER, FIRE, RAZOR, DARK, SHIELD}},
        {UNMODIFICATOR, new List<string>(){WATER, FIRE, RAZOR, LIFE, RAZOR}},
        {FIREBALL4, new List<string>(){EARTH, FIRE, FIRE, FIRE, FIRE}},
        {FORCE2, new List<string>(){WATER, WATER, FIRE, FIRE, RAZOR}},
        {ICICLE2, new List<string>(){WATER, WATER, WATER, FREEZE, FREEZE}},
        {ICICLE3, new List<string>(){WATER, WATER, WATER, WATER, FREEZE, FREEZE, FREEZE}},
        {TORNADO, new List<string>(){WATER, WATER, FIRE, FIRE, RAZOR, WATER, FIRE, RAZOR}},
        {REVIVAL, new List<string>(){WATER, FIRE, RAZOR, LIFE, RAZOR, WATER, WATER, FIRE, FIRE, RAZOR}},
        {METEOR, new List<string>(){WATER, WATER, EARTH, FIRE, FIRE, FIRE, FIRE, FIRE, FIRE, RAZOR}},
        {BLAST, new List<string>(){WATER, WATER, FIRE, FIRE, RAZOR, WATER, WATER, WATER, WATER, FREEZE, FREEZE, FREEZE}},
        {FIREBLAST, new List<string>(){WATER, WATER, FIRE, FIRE, RAZOR, WATER, WATER, WATER, WATER, FREEZE, FREEZE, FREEZE, FIRE}},
        {ENERGYBLAST, new List<string>(){WATER, WATER, FIRE, FIRE, RAZOR, WATER, WATER, WATER, WATER, FREEZE, FREEZE, FREEZE, LIFE, RAZOR}},
        {ICEBLAST, new List<string>(){WATER, WATER, FIRE, FIRE, RAZOR, WATER, WATER, WATER, WATER, FREEZE, FREEZE, FREEZE, WATER, WATER, FREEZE}}
    };

    public static readonly Dictionary<int, MetaSphere> META_SPHERES = new Dictionary<int, MetaSphere>()
    {

        {6, new MetaSphere(STEAM, MetaSphereType.element)},
        {10, new MetaSphere(FIREBALL1, MetaSphereType.element)},
        {18, new MetaSphere(COSTFIREFREZE, MetaSphereType.cost)},
        {20, new MetaSphere(ICE, MetaSphereType.element)},
        {33, new MetaSphere(SPEEDUP, MetaSphereType.element)},
        {36, new MetaSphere(RAZOR, MetaSphereType.damage)},
        {40, new MetaSphere(COSTEARTHRAZOR, MetaSphereType.cost)},
        {65, new MetaSphere(COSTLIFEDARK, MetaSphereType.cost)},
        {68, new MetaSphere(POISON, MetaSphereType.element)},
        {72, new MetaSphere(DARKBALL, MetaSphereType.element)},
        {129, new MetaSphere(MINELIFE, MetaSphereType.element)},
        {130, new MetaSphere(MINEFIRE, MetaSphereType.element)},
        {132, new MetaSphere(MINEWATER, MetaSphereType.element)},
        {144, new MetaSphere(MINEFREEZE, MetaSphereType.element)},
        {160, new MetaSphere(MINERAZOR, MetaSphereType.element)},
        {192, new MetaSphere(MINEDARK, MetaSphereType.element)},
        {272, new MetaSphere(WATER, MetaSphereType.element)},
        {288, new MetaSphere(FORCE1, MetaSphereType.element)},
        {513, new MetaSphere(WATER, MetaSphereType.element)},
        {514, new MetaSphere(DARK, MetaSphereType.element)},
        {640, new MetaSphere(MINEPOISON, MetaSphereType.element)},
        {1026, new MetaSphere(WATER, MetaSphereType.element)},
        {1028, new MetaSphere(ICICLE1, MetaSphereType.element)},
        {1152, new MetaSphere(MINEICE, MetaSphereType.element)},
        {2050, new MetaSphere(FIREBALL2, MetaSphereType.element)},
        {2080, new MetaSphere(COSTEARTHRAZOR, MetaSphereType.cost)},
        {2112, new MetaSphere(DARKFIREBALL, MetaSphereType.element)},
        {4098, new MetaSphere(DARKFIREBALL, MetaSphereType.element)},
        {16416, new MetaSphere(UNARMOR, MetaSphereType.element)},
        {65792, new MetaSphere(FORCE2, MetaSphereType.element)},
        {73728, new MetaSphere(UNMODIFICATOR, MetaSphereType.element)},
        {98304, new MetaSphere(GHOSTING, MetaSphereType.element)},
        {132096, new MetaSphere(ICICLE2, MetaSphereType.element)},
        {262146, new MetaSphere(FIREBALL3, MetaSphereType.element)},
        {262176, new MetaSphere(COSTEARTHRAZOR, MetaSphereType.cost)},
        {524320, new MetaSphere(TAUREL, MetaSphereType.element)},
        {1048578, new MetaSphere(FIREBALL4, MetaSphereType.element)},
        {1048608, new MetaSphere(COSTEARTHRAZOR, MetaSphereType.cost)},
        {4194336, new MetaSphere(COSTEARTHRAZOR, MetaSphereType.cost)},
        {8454144, new MetaSphere(TORNADO, MetaSphereType.element)},
        {10485760, new MetaSphere(REVIVAL, MetaSphereType.element)},
        {12582912, new MetaSphere(METEOR, MetaSphereType.element)},
        {16778240, new MetaSphere(ICICLE3, MetaSphereType.element)},
        {41943040, new MetaSphere(BLAST, MetaSphereType.element)},
        {67108866, new MetaSphere(FIREBLAST, MetaSphereType.element)},
        {67117056, new MetaSphere(ENERGYBLAST, MetaSphereType.element)},
        {67239936, new MetaSphere(ICEBLAST, MetaSphereType.element)}
    };

    public static readonly Dictionary<Bioms, int> IncreaseSphereCount = new Dictionary<Bioms, int>() {
        {Bioms.dark, (int)SpheresElements.dark},
        {Bioms.life, (int)SpheresElements.life},
        {Bioms.fire, (int)SpheresElements.fire},
        {Bioms.water, (int)SpheresElements.water},
        {Bioms.freeze, (int)SpheresElements.freeze}
    };

    public static readonly Dictionary<Bioms, int> DecreaseSphereCount = new Dictionary<Bioms, int>() {
        {Bioms.dark, (int)SpheresElements.life},
        {Bioms.life, (int)SpheresElements.dark},
        {Bioms.fire, (int)(SpheresElements.freeze| SpheresElements.water)},
        {Bioms.water, (int)SpheresElements.fire},
        {Bioms.freeze, (int)SpheresElements.fire}
    };
    /*
    public enum Spheres
    {
        none = 0, //none
        life = 1, //life
        fire = 2, //fire
        water = 4, //water
        earth = 8, //earth
        freeze = 16, //freeze
        razor = 32, //razor
        dark = 64, //dark
        shield = 128, //shield
        steam = 256, //steam
        poison = 512, //poison
        ice = 1024, //ice
        fireball1 = 2048, //fireball1
        darkball = 4096, //darkball
        speedup = 8192, //speedup
        minerazor = 16384, //minerazor
        minedark = 32768, //minedark
        force1 = 65536, //force1
        icicle1 = 131072, //icicle1
        fireball2 = 262144, //fireball2
        mineice = 524288, //mineice
        fireball3 = 1048576, //fireball3
        unmodificator = 2097152, //unmodificator
        fireball4 = 4194304, //fireball4
        force2 = 8388608, //force2
        icicle2 = 16777216, //icicle2
        icicle3 = 33554432, //icicle3
        blast = 67108864 //blast
    }*/
}