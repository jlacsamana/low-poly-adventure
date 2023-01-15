using System.Collections;
using System.Collections.Generic;


public class EntityStatusEffect{
    //this stores data for a status effect

    //effect duration in seconds
    public int EffectDuration { get; set; }

    //effect name
    public string EffectName { get; set; }

    //effect type
    public string EffectType { get; set; }

    //effect changes on health, stamina or mana per second
    public int EntityHealthChange { get; set; }
    public int EntityStaminaChange { get; set; }
    public int EntityManaChange { get; set; }


    //constant effects on stats while status effect is active
    public float EntityPhysDmgChange { get; set; }
    public float EntityMagDmgChange { get; set; }
    public float EntitySpeedChange { get; set; }
    public float EntityCriticalChanceChange { get; set; }
    public float EntityArmorChange { get; set; }
    public float EntityMagicalArmorChange { get; set; }

    //can effect stack with itself or just renews any previous instance of itself
    public bool IsStackable { get; set; }


}
