[System.Serializable]

//all the enums used in the game
public class EnumsScript
{
    public enum ItemType
    {
        NONE,
        Consumable,
        Equipable,
        Battle,
        Quest,
        Key,
        Collectible
    }

    public enum Condition
    {
        KO,
        Poison,
        Silence,
        Paralisys,
        Cursed,
        Sleep,
        All,
        Ok,
        NONE
    }

    public enum EquipedOn
    {
        HEAD,
        CHEST,
        ARMS,
        FOOT,
        MAIN_WEAPON,
        OFF_HAND,
        ACCESSORY1,
        ACCESSORY2,
        NONE
    }

    public enum Stats
    {
        HP,
        SP,
        PHYSICAL_ATTACK,
        PHYSICAL_DEFENCE,
        SPIRITUAL_ATTACK,
        SPIRITUAL_DEFENCE,
        CRITIC,
        NONE
    }

    public enum Element
    {
        NEUTRAL,
        WATER,
        FIRE,
        EARTH,
        WIND,
        DARK,
        LIGHT
    }

    public enum Skill_Type
    {
        PASIVE_STAT_SKILL,
        PASIVE_ABILITY,
        ACTIVE_ABILITY
    }

    public enum Quest_Type
    {
        MAIN_QUEST,
        SIDE_QUEST,
        SPECIAL_QUEST
    }

    public enum Quest_Objective_Type
    {
        NONE,
        HUNT_MONSTER,
        TALK_TO_NPC,
        COLLECT_ITEM,
        KILL_BOSS,
        REACH_PLACE
    }

    public enum Reward_Type
    {
        NONE,
        ITEM,
        GOLD,
        EXPERIENCE
    }

    public enum Enemy_State
    {
        INITIAL,
        IDLE,
        ATTACK,
        DEFEND,
        SEEK,
        FLEE,
        AVOID,
        ABILITY,
        DEATH,
        NO_ACTION,
        STRUGGLE,
        NONE
    }

    public enum Player_State
    {
        IDLE,
        ATTACK,
        DEFEND,
        APPROACH,
        FLEE,
        AVOID,
        ABILITY,
        DEATH,
        NONE
    }

    public enum NPC_State
    {
        IDLE,
        TALKING,
        WALKING,
        RUNNING,
        BUBBLE_TALK,
        NONE
    }

    public enum Allies_State
    {
        INITIAL,
        IDLE,
        ATTACK,
        DEFEND,
        SEEK,
        FLEE,
        AVOID,
        ABILITY,
        DEATH,
        NO_ACTION,
        STRUGGLE,
        HEAL,
        SUPPORT_PLAYER,
        STRATEGY1,
        STRATEGY2,
        STRATEGY3,
        NONE
    }
}