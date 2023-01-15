using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour {
    //handles attacks; executes critical chance calculation 

    //reference to local attack system
    public AttackSystem entityAttackSys;

    //reference to local entity stats
    public Entity entityMain;

    //reference to local entity inventory
    public Inventory entityInventory;

    //reference to global 3D model loader
    public Load3DMdlDataBase MDLloader;

    //reference to local main local entity animator
    public Animator entityMainAnim;

    //reference to this entity's death handler
    public DeathHandler entityDeathHandler;

    //instantiated projectiles
    public GameObject physicalProjectile;
    public GameObject staffProjectile;
    public GameObject staffConstantObject;

    //list of active projectiles
    public GameObject activeProjectile;
    public GameObject activeParticleSystem;

    //reference to local animator shell
    public AnimationControllerShell entityAnim;

    //projectile variables
    public float projectileCreateTimer = 0;
    public bool projectileSheathe = false;

    //max arrow velocity = not yet specified
    //max crossbow bolt velocity = not yet specified

    // Use this for initialization
    void Start () {
        Invoke("InitAttackHandler", 0.01f);

    }
	
    //initializer for attack system
    public void InitAttackHandler()
    {
        entityDeathHandler = gameObject.GetComponent<DeathHandler>();
        entityAttackSys = gameObject.GetComponent<AttackSystem>();
        entityMain = gameObject.GetComponent<Entity>();
        entityInventory = gameObject.GetComponent<Inventory>();
        entityMainAnim = gameObject.GetComponent<AnimationControllerShell>().animatorGroup[0];
        entityAnim = gameObject.GetComponent<AnimationControllerShell>();
        MDLloader = GameObject.Find("Databases").GetComponent<Load3DMdlDataBase>();
    }

	// Update is called once per frame
	void Update () {
       //Debug.DrawRay(transform.position + new Vector3(transform.forward.x, transform.forward.y + 0.75f, transform.forward.z* 0.5f), transform.Find("Main Camera").forward * 5, Color.red);      
    }

    // attack executor
    public void ExecuteMeleeAttack()
    {
        if (Time.timeScale == 1)
        {
            if (entityMainAnim.GetBool("IsShielding") == false && entityMainAnim.GetBool("IsParrying") == false)
            {
                RaycastHit[] targetEntities = Physics.BoxCastAll(transform.position, new Vector3(0.1f, 0.5f, 0.5f), transform.forward, Quaternion.identity, entityInventory.entityRightHandSlot.WeaponReach);
                if (targetEntities.Length > 0)
                {
                    //checks if targets are not the caster && have entity components
                    List<RaycastHit> validEntities = new List<RaycastHit>();
                    foreach (RaycastHit hit in targetEntities)
                    {
                        if (hit.transform.root.gameObject.GetInstanceID() != gameObject.GetInstanceID() &&
                            hit.transform.root.gameObject.GetComponent<Entity>()
                            )
                        {
                            validEntities.Add(hit);
                        }
                    }
                    if (validEntities.Count > 0)
                    {
                        //calcaulates damage
                        if (entityMainAnim.GetBool("IsPowerAttack") == true)
                        {
                            validEntities[0].transform.root.gameObject.GetComponent<Entity>().TakeDamage(CalculateCritical(Mathf.RoundToInt(entityMain.currentEntityPhysicalDamage * 1.35f)), "Physical", true, false);
                        }
                        else
                        {
                            validEntities[0].transform.root.gameObject.GetComponent<Entity>().TakeDamage(CalculateCritical(entityMain.currentEntityPhysicalDamage), "Physical", false, false);                           
                        }

                        //checks if effect can be applied
                        GameObject[] EnemyTargets = { validEntities[0].transform.root.gameObject };
                        WorldEffectSystem.BestowStatusEffect(entityInventory.entityRightHandSlot.EffectNameList,
                            entityInventory.entityRightHandSlot.EffectTypeList,
                            entityInventory.entityRightHandSlot.EffectChanceList,
                            entityInventory.entityRightHandSlot.EffectDurationList,
                            entityInventory.entityRightHandSlot.EffectTargetList,
                            gameObject,
                            EnemyTargets);
                    }

                }
            }
        }
    }

    //CROSSBOW ATTACKS
    //instantiates bolt at starting point; call when attack starts
    public void StartCrossbowAttackShell()
    {
        projectileCreateTimer = 0;
        projectileSheathe = false;
        Invoke("StartCrossbowAttack", 0.6666667f);
    }

    public void StartCrossbowAttack()
    {
        if (Time.timeScale == 1)
        {
            //instantiate where bolt should be at start of drawback
            activeProjectile = Instantiate(physicalProjectile, entityAnim.projectileWpnAnim.transform);
            //assign info here
            activeProjectile.GetComponent<Rigidbody>().isKinematic = true;
            activeProjectile.GetComponent<Collider>().enabled = false;
            activeProjectile.transform.localPosition = new Vector3(0, 0.15f, 0.3f);
            activeProjectile.GetComponent<UniversalProjectile>().AssignParentGameObj(gameObject);
            activeProjectile.GetComponent<UniversalProjectile>().projectileDamageType = "Physical";
            activeProjectile.GetComponent<UniversalProjectile>().projectileEffectTypeList = new List<string>(entityInventory.entityRightHandSlot.EffectTypeList);
            activeProjectile.GetComponent<UniversalProjectile>().projectileEffectNameList = new List<string>(entityInventory.entityRightHandSlot.EffectNameList);
            activeProjectile.GetComponent<UniversalProjectile>().projectileEffectTargets = new List<string>(entityInventory.entityRightHandSlot.EffectTargetList);
            activeProjectile.GetComponent<UniversalProjectile>().projectileEffectChanceList = new List<float>(entityInventory.entityRightHandSlot.EffectChanceList);
            activeProjectile.GetComponent<UniversalProjectile>().projectileEffectDurationList = new List<int>(entityInventory.entityRightHandSlot.EffectDurationList);

            if (projectileSheathe)
            {
                Destroy(activeProjectile);
            }
        }
    }

    //crossbow bolt pullback; call while charging
    public void PullbackCrossbowBolt()
    {
        if (Time.timeScale == 1)
        {
            if (projectileCreateTimer < (.6666667f * entityInventory.entityRightHandSlot.WeaponSpeed))
            {
                projectileCreateTimer += Time.deltaTime;
            }
            //change transform to match the pullback animation
            else if (activeProjectile.transform.localPosition.z > -0.2)
            {
                activeProjectile.transform.localPosition = new Vector3
                (activeProjectile.transform.localPosition.x, activeProjectile.transform.localPosition.y,
                (activeProjectile.transform.localPosition.z - 0.5f / 40) * entityInventory.entityRightHandSlot.WeaponSpeed);
            }
        }

    }

    //fire crossbow bolt; call when crossbow is fired
    public void FireCrossbow()
    {
        if (Time.timeScale == 1)
        {
            
            //assign damage
            activeProjectile.GetComponent<UniversalProjectile>().projectileDmg = CalculateCritical(entityMain.currentEntityPhysicalDamage + entityInventory.AmmoQuiver[0].PhysicalDamage);
            //assign forward velocity here
            activeProjectile.GetComponent<Rigidbody>().isKinematic = false;
            activeProjectile.GetComponent<Collider>().enabled = true;
            Physics.IgnoreCollision(entityDeathHandler.rElbowJoint.GetComponent<Collider>(),activeProjectile.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(entityDeathHandler.lElbowJoint.GetComponent<Collider>(), activeProjectile.GetComponent<Collider>(), true);
            activeProjectile.transform.parent = null;
            activeProjectile.GetComponent<Rigidbody>().velocity = activeProjectile.transform.forward * 50;
            entityInventory.AmmoQuiver.RemoveAt(0);
        }

    }

    //sheathes bolt
    public void SheatheCrossbowBolt()
    {
        if (Time.timeScale == 1)
        {
            Destroy(activeProjectile);
            projectileSheathe = true;
        }

    }

    //starts attack with bow; call when attack starts
    public void StartBowAttackShell()
    {
        projectileCreateTimer = 0;
        projectileSheathe = false;
        Invoke("StartBowAttack", 0.6666667f);
    }

    public void StartBowAttack()
    {
        if (Time.timeScale == 1)
        {
            activeProjectile = Instantiate(physicalProjectile, entityAnim.projectileWpnAnim.transform);
            //assign info here
            activeProjectile.GetComponent<Rigidbody>().isKinematic = true;
            activeProjectile.GetComponent<Collider>().enabled = false;
            activeProjectile.transform.localPosition = new Vector3(-0.03f, 0.03f, 0.175f);
            activeProjectile.GetComponent<UniversalProjectile>().AssignParentGameObj(gameObject);
            activeProjectile.GetComponent<UniversalProjectile>().projectileDamageType = "Physical";
            activeProjectile.GetComponent<UniversalProjectile>().projectileEffectTypeList = new List<string>(entityInventory.entityRightHandSlot.EffectTypeList);
            activeProjectile.GetComponent<UniversalProjectile>().projectileEffectNameList = new List<string>(entityInventory.entityRightHandSlot.EffectNameList);
            activeProjectile.GetComponent<UniversalProjectile>().projectileEffectTargets = new List<string>(entityInventory.entityRightHandSlot.EffectTargetList);
            activeProjectile.GetComponent<UniversalProjectile>().projectileEffectChanceList = new List<float>(entityInventory.entityRightHandSlot.EffectChanceList);
            activeProjectile.GetComponent<UniversalProjectile>().projectileEffectDurationList = new List<int>(entityInventory.entityRightHandSlot.EffectDurationList);
            if (projectileSheathe)
            {
                Destroy(activeProjectile);
            }
        }

    }

    //charges up bow; call while charging
    public void PullBackArrow()
    {
        if (Time.timeScale == 1)
        {
            //change tranform to match the pullback animation
            if (projectileCreateTimer < (.6666667f * entityInventory.entityRightHandSlot.WeaponSpeed))
            {
                projectileCreateTimer += Time.deltaTime;
            }
            //change transform to match the pullback animation
            else if (activeProjectile.transform.localPosition.z > -0.55)
            {
                activeProjectile.transform.localPosition = new Vector3
                (activeProjectile.transform.localPosition.x, activeProjectile.transform.localPosition.y, 
                (activeProjectile.transform.localPosition.z - 0.725f / 30) * entityInventory.entityRightHandSlot.WeaponSpeed);
            }
        }

    }

    //fires bow; call when bow is fired
    public void FireBow()
    {
        if (Time.timeScale == 1)
        {
            //assign damage; bases on charge
            activeProjectile.GetComponent<UniversalProjectile>().projectileDmg = Mathf.RoundToInt(CalculateCritical(entityMain.currentEntityPhysicalDamage + entityInventory.AmmoQuiver[0].PhysicalDamage) * entityAttackSys.weaponCharge);
            //assign forward velocity here
            activeProjectile.GetComponent<Rigidbody>().isKinematic = false;
            activeProjectile.GetComponent<Collider>().enabled = true;
            Physics.IgnoreCollision(entityDeathHandler.rElbowJoint.GetComponent<Collider>(), activeProjectile.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(entityDeathHandler.lElbowJoint.GetComponent<Collider>(), activeProjectile.GetComponent<Collider>(), true);
            activeProjectile.transform.parent = null;
            activeProjectile.GetComponent<Rigidbody>().velocity = activeProjectile.transform.forward * 35;
            entityInventory.AmmoQuiver.RemoveAt(0);
        }

    }

    //sheathes arrow
    public void SheatheArrow()
    {
        if (Time.timeScale == 1)
        {
            Destroy(activeProjectile);
            projectileSheathe = true;
        }

    }

    //exceutes charged attack with staff
    public void ExecuteChargedStaffAttack()
    {
        if (Time.timeScale == 1)
        {
            if (entityInventory.entityLeftHandSlot.WeaponType != "Tome")
            {
                //assign default magical projectile because there is no spell available
                staffProjectile = MDLloader.FetchPrefab(MDLloader.spawnedProjectilesDirectory, "Default Magical Projectile");
            }
            //assign info here
            activeProjectile = Instantiate(staffProjectile, entityAnim.projectileWpnAnim.transform.Find("Humanoid_Armature/lower_spine/middle_spine/upper_spine/" +
            "upper_arm_L_001/lower_arm_L_001/hand_L_001/"));
            activeProjectile.GetComponent<Rigidbody>().isKinematic = true;
            activeProjectile.GetComponent<Collider>().enabled = false;
            activeProjectile.GetComponent<UniversalProjectile>().AssignParentGameObj(gameObject);
            activeProjectile.GetComponent<UniversalProjectile>().parentGameObject = transform.root.gameObject;
            activeProjectile.GetComponent<UniversalProjectile>().projectileDmg = (CalculateCritical(entityMain.currentEntityMagicalDamage));
            activeProjectile.GetComponent<UniversalProjectile>().projectileDamageType = "Magical";
            if (entityInventory.entityLeftHandSlot.WeaponType == "Tome")
            {
                activeProjectile.GetComponent<UniversalProjectile>().projectileEffectTypeList = new List<string>(entityInventory.entityLeftHandSlot.EffectTypeList);
                activeProjectile.GetComponent<UniversalProjectile>().projectileEffectNameList = new List<string>(entityInventory.entityLeftHandSlot.EffectNameList);
                activeProjectile.GetComponent<UniversalProjectile>().projectileEffectTargets = new List<string>(entityInventory.entityLeftHandSlot.EffectTargetList);
                activeProjectile.GetComponent<UniversalProjectile>().projectileEffectChanceList = new List<float>(entityInventory.entityLeftHandSlot.EffectChanceList);
                activeProjectile.GetComponent<UniversalProjectile>().projectileEffectDurationList = new List<int>(entityInventory.entityLeftHandSlot.EffectDurationList);
            }
            else
            {
                activeProjectile.GetComponent<UniversalProjectile>().projectileEffectTypeList = new List<string>(entityInventory.entityRightHandSlot.EffectTypeList);
                activeProjectile.GetComponent<UniversalProjectile>().projectileEffectNameList = new List<string>(entityInventory.entityRightHandSlot.EffectNameList);
                activeProjectile.GetComponent<UniversalProjectile>().projectileEffectTargets = new List<string>(entityInventory.entityRightHandSlot.EffectTargetList);
                activeProjectile.GetComponent<UniversalProjectile>().projectileEffectChanceList = new List<float>(entityInventory.entityRightHandSlot.EffectChanceList);
                activeProjectile.GetComponent<UniversalProjectile>().projectileEffectDurationList = new List<int>(entityInventory.entityRightHandSlot.EffectDurationList);
            }

            //assign velocity and transform here
            activeProjectile.transform.localPosition = new Vector3(-0.75f, 0.75f, -0.5f);
            activeProjectile.GetComponent<Rigidbody>().isKinematic = false;
            activeProjectile.GetComponent<Collider>().enabled = true;
            activeProjectile.transform.parent = null;
            activeProjectile.GetComponent<Rigidbody>().velocity = transform.forward * 20;

        }

    }

    //executes constant attack with staff
    public void ExecuteConstantStaffAttack()
    {
        if (Time.timeScale == 1)
        {
            //cast box collider that listens for hit objects
            RaycastHit[] hit = Physics.BoxCastAll(entityAnim.projectileWpnAnim.transform.Find("Humanoid_Armature/lower_spine/middle_spine/upper_spine/" +
            "upper_arm_L_001/lower_arm_L_001/hand_L_001/").transform.position + transform.forward, new Vector3(1, 1, 1), transform.forward,
                Quaternion.identity, 4);
            if (hit.Length > 0)
            {
                List<GameObject> targetEnemyList = new List<GameObject>(); 
                foreach (RaycastHit M in hit)
                {
                    if (M.transform.root.gameObject.GetComponent<Entity>())
                    {
                        if (M.transform.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                        {
                            if (M.transform.root.gameObject.GetComponent<Entity>())
                            {
                                M.transform.root.gameObject.GetComponent<Entity>().AddToConstantDamage(entityMain.currentEntityMagicalDamage * Time.deltaTime);
                                targetEnemyList.Add(M.transform.root.gameObject);
                            }                           
                        }
                        
                    }
                }
                WorldEffectSystem.BestowStatusEffect(entityInventory.entityLeftHandSlot.EffectNameList,
                entityInventory.entityLeftHandSlot.EffectTypeList,
                entityInventory.entityLeftHandSlot.EffectChanceList,
                entityInventory.entityLeftHandSlot.EffectDurationList,
                entityInventory.entityLeftHandSlot.EffectTargetList,
                gameObject,
                targetEnemyList.ToArray());
            }

            //activate particle system here
            activeParticleSystem = Instantiate(staffConstantObject, entityAnim.projectileWpnAnim.transform.Find("Humanoid_Armature/lower_spine/middle_spine/upper_spine/" +
            "upper_arm_L_001/lower_arm_L_001/hand_L_001/"));
            activeParticleSystem.transform.localPosition = new Vector3(-0.175f, 0.75f, -0.5f);
            activeParticleSystem.transform.localEulerAngles = new Vector3(0, -90, 0);
        }

    }

    //continues attack
    public void ResumeConstantStaffAttck()
    {
        if (Time.timeScale == 1)
        {
            //cast box collider that listens for hit objects
            RaycastHit[] hit = Physics.BoxCastAll(entityAnim.projectileWpnAnim.transform.Find("Humanoid_Armature/lower_spine/middle_spine/upper_spine/" +
            "upper_arm_L_001/lower_arm_L_001/hand_L_001/").transform.position + transform.forward, new Vector3(1, 1, 1), transform.forward,
                Quaternion.identity, 4);
            if (hit.Length > 0)
            {
                List<GameObject> targetEnemyList = new List<GameObject>();
                foreach (RaycastHit M in hit)
                {
                    if (M.transform.root.gameObject.GetComponent<Entity>())
                    {
                        if (M.transform.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                        {
                            if (M.transform.root.gameObject.GetComponent<Entity>())
                            {
                                M.transform.root.gameObject.GetComponent<Entity>().AddToConstantDamage(entityMain.currentEntityMagicalDamage * Time.deltaTime);
                                targetEnemyList.Add(M.transform.root.gameObject);
                            }
                        }

                    }
                }
                WorldEffectSystem.BestowStatusEffect(entityInventory.entityLeftHandSlot.EffectNameList,
                entityInventory.entityLeftHandSlot.EffectTypeList,
                entityInventory.entityLeftHandSlot.EffectChanceList,
                entityInventory.entityLeftHandSlot.EffectDurationList,
                entityInventory.entityLeftHandSlot.EffectTargetList,
                gameObject,
                targetEnemyList.ToArray());
            }
        }

    }

    //ends attack
    public void EndConstantStaffAttack()
    {
        if (Time.timeScale == 1)
        {
            //cast box collider that listens for hit objects
            RaycastHit[] hit = Physics.BoxCastAll(entityAnim.projectileWpnAnim.transform.Find("Humanoid_Armature/lower_spine/middle_spine/upper_spine/" +
            "upper_arm_L_001/lower_arm_L_001/hand_L_001/").transform.position + transform.forward, new Vector3(1, 1, 1), transform.forward,
                Quaternion.identity, 4);
            if (hit.Length > 0)
            {
                List<GameObject> targetEnemyList = new List<GameObject>();
                foreach (RaycastHit M in hit)
                {
                    if (M.transform.root.gameObject.GetComponent<Entity>())
                    {
                        if (M.transform.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                        {
                            if (M.transform.root.gameObject.GetComponent<Entity>())
                            {
                                M.transform.root.gameObject.GetComponent<Entity>().AddToConstantDamage(entityMain.currentEntityMagicalDamage * Time.deltaTime);
                                targetEnemyList.Add(M.transform.root.gameObject);
                            }
                        }

                    }
                }
                WorldEffectSystem.BestowStatusEffect(entityInventory.entityLeftHandSlot.EffectNameList,
                entityInventory.entityLeftHandSlot.EffectTypeList,
                entityInventory.entityLeftHandSlot.EffectChanceList,
                entityInventory.entityLeftHandSlot.EffectDurationList,
                entityInventory.entityLeftHandSlot.EffectTargetList,
                gameObject,
                targetEnemyList.ToArray());
            }
            //destroy particle system here
            activeParticleSystem.GetComponent<ParticleSystem>().Stop();
            Destroy(activeParticleSystem, 1);
        }

    }

    //critical calculator
    public int CalculateCritical(int baseDamage)
    {
        int appliedDmg;
        float CalcChance = Random.Range(0.0f,100.1f);
        if (CalcChance <= entityMain.currentEntityCritChance)
        {
            //critical hit lands; x2 damage
            appliedDmg = (baseDamage * 2);
        }
        else
        {
            //no critical hit; regular damage
            appliedDmg = baseDamage;
        }
        return appliedDmg;
    }
}
