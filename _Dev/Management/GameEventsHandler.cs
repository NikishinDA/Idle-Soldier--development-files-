// The Game Events used across the Game.
// Anytime there is a need for a new event, it should be added here.

using System;
using UnityEngine;

public static class GameEventsHandler
{
    public static readonly GameStartEvent GameStartEvent = new GameStartEvent();
    public static readonly GameOverEvent GameOverEvent = new GameOverEvent();
    public static readonly PlayerProgressEvent PlayerProgressEvent = new PlayerProgressEvent();
    public static readonly MoneyCollectEvent MoneyCollectEvent = new MoneyCollectEvent();
    public static readonly ArmorCollectEvent ArmorCollectEvent = new ArmorCollectEvent();
    public static readonly BoosterSpeedCollectEvent BoosterSpeedCollectEvent = new BoosterSpeedCollectEvent();
    public static readonly BoosterWeaponCollectEvent BoosterWeaponCollectEvent = new BoosterWeaponCollectEvent();
    public static readonly BoosterMoneyCollectEvent BoosterMoneyCollectEvent = new BoosterMoneyCollectEvent();
    public static readonly SlowDownEvent SlowDownEvent = new SlowDownEvent();
    public static readonly MagnetActivationEvent MagnetActivationEvent = new MagnetActivationEvent();
    public static readonly LevelPreparedEvent LevelPreparedEvent = new LevelPreparedEvent();
    public static readonly BossSpawnEvent BossSpawnEvent = new BossSpawnEvent();
    public static readonly BossDeathEvent BossDeathEvent = new BossDeathEvent();
    public static readonly PlayerTakeDamageEvent PlayerTakeDamageEvent = new PlayerTakeDamageEvent();
    public static readonly PlayerDPSUpgradeEvent PlayerDPSUpgradeEvent = new PlayerDPSUpgradeEvent();
    public static readonly PlayerArmorUpgradeEvent PlayerArmorUpgradeEvent = new PlayerArmorUpgradeEvent();
    public static readonly PlayerMoneyUpgradeEvent PlayerMoneyUpgradeEvent = new PlayerMoneyUpgradeEvent();
    public static readonly PlayerTargetChangeEvent PlayerTargetChangeEvent = new PlayerTargetChangeEvent();
    public static readonly PlayerCheckpointCrossEvent PlayerCheckpointCrossEvent = new PlayerCheckpointCrossEvent();
    public static readonly LevelFinishedSpawningEvent LevelFinishedSpawningEvent = new LevelFinishedSpawningEvent();
    public static readonly EnemySpawnedEvent EnemySpawnedEvent = new EnemySpawnedEvent();
    public static readonly EnemyKilledEvent EnemyKilledEvent = new EnemyKilledEvent();
    public static readonly PlayerDamageChangeEvent PlayerDamageChangeEvent = new PlayerDamageChangeEvent();
    public static readonly PlayerMaxArmorChangeEvent PlayerMaxArmorChangeEvent = new PlayerMaxArmorChangeEvent();
    public static readonly LevelSpeedChangeEvent LevelSpeedChangeEvent = new LevelSpeedChangeEvent();
    public static readonly PlayerFinishLevelEvent PlayerFinishLevelEvent = new PlayerFinishLevelEvent();
    public static readonly TutorialShowEvent TutorialShowEvent = new TutorialShowEvent();
    public static readonly MoneyAmountChangeEvent MoneyAmountChangeEvent = new MoneyAmountChangeEvent();
    public static readonly TutorialToggleEvent TutorialToggleEvent = new TutorialToggleEvent();
    public static readonly PlayerWeaponChangeEvent PlayerWeaponChangeEvent = new PlayerWeaponChangeEvent();
    public static readonly AmbienceChangeEvent AmbienceChangeEvent = new AmbienceChangeEvent();
    public static readonly PlayerModelChangeEvent PlayerModelChangeEvent = new PlayerModelChangeEvent();
    public static readonly PlayerChangeModelRequestEvent PlayerChangeModelRequestEvent = new PlayerChangeModelRequestEvent();
}

public class GameEvent {}

public class GameStartEvent : GameEvent
{
}

public class GameOverEvent : GameEvent
{
    public bool IsWin;
}

public class PlayerProgressEvent : GameEvent
{
    
}

public class MoneyCollectEvent : GameEvent
{
    
}

public class ArmorCollectEvent : GameEvent
{
}
public class BoosterSpeedCollectEvent : BoosterEvent
{
}

public class BoosterEvent : GameEvent
{
    public bool Toggle;
}
public class BoosterWeaponCollectEvent : BoosterEvent
{
}
public class BoosterMoneyCollectEvent : BoosterEvent
{
}
public class SlowDownEvent : GameEvent
{
    public bool Toggle;
}
public class MagnetActivationEvent : GameEvent
{
    
}
public class LevelPreparedEvent : GameEvent
{
    
}
public class PlayerTargetChangeEvent : GameEvent
{
    public Transform Target;
}

public class PlayerTakeDamageEvent : GameEvent
{
    public int Damage;
}


public class PlayerDPSUpgradeEvent : GameEvent
{
    public int Level;
}
public class PlayerArmorUpgradeEvent : GameEvent
{
    public int Level;
}
public class PlayerMoneyUpgradeEvent : GameEvent
{
}
public class BossSpawnEvent : GameEvent
{
    
}

public class BossDeathEvent : GameEvent
{
}
public class PlayerCheckpointCrossEvent : GameEvent
{
    public int Section;
}

public class LevelFinishedSpawningEvent : GameEvent
{
}

public class EnemySpawnedEvent : GameEvent
{
    public EnemyController EnemyController;
}

public class EnemyKilledEvent : GameEvent
{
    public Transform Transform;
    public int Cost;
}

public class PlayerDamageChangeEvent : GameEvent
{
    public int Damage;
    public int UpgradeLevel;
}
public class PlayerMaxArmorChangeEvent : GameEvent
{
    public int MaxArmor;
}
public class LevelSpeedChangeEvent: GameEvent
{
    public float Speed;
}
public class  PlayerFinishLevelEvent : GameEvent{}

public class TutorialShowEvent : GameEvent
{
}

public class MoneyAmountChangeEvent : GameEvent
{
    public int Amount;
}

public class TutorialToggleEvent : GameEvent
{
    public bool Toggle;
}

public class PlayerWeaponChangeEvent : GameEvent
{
    public WeaponType WeaponType;
}

public class AmbienceChangeEvent : GameEvent
{
    public int Number;
}
public class PlayerModelChangeEvent : GameEvent
{
    public bool Bin;
}
public class PlayerChangeModelRequestEvent : GameEvent
{
    public bool Bin;
}


