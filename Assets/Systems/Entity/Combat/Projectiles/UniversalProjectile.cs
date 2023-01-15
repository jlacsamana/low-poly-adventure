using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class UniversalProjectile : MonoBehaviour {
    //attach to all projectile gameobjects

    //projectile base damage
    public int projectileDmg;

    //projectile damage type
    public string projectileDamageType;

    //ammo info
    public JsonData projectileInfo;

    //attached effect info & chance of effect
    public List<string> projectileEffectTypeList;
    public List<string> projectileEffectNameList;
    public List<string> projectileEffectTargets;
    public List<float> projectileEffectChanceList;
    public List<int> projectileEffectDurationList;

    //entity that fired this projectile
    public GameObject parentGameObject;



    //has collided?
    public bool hasHitObj = false;



	// Use this for initialization
	void Start () {
        projectileEffectTypeList = new List<string>();
        projectileEffectNameList = new List<string>();
        projectileEffectTargets = new List<string>();
        projectileEffectChanceList = new List<float>();
        projectileEffectDurationList = new List<int>();

    }
	
	// Update is called once per frame
	void Update () {
        //destroys object upon hitting
        if (hasHitObj)
        {
            if (projectileDamageType == "Physical")
            {
                DestroyProjectile();
            }
            else if (projectileDamageType == "Magical")
            {
                DestroyProjectile();
            }

        }
        //destroys object if too far from caster
        if (Vector3.Distance(transform.position, parentGameObject.transform.position) > 250)
        {
            DestroyProjectile();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject != parentGameObject && hasHitObj == false)
        {
            Quaternion savedRotation = gameObject.transform.rotation;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Collider>().enabled = false;
            //attaches this projectile to a proper movable collider, so its scale doesnt mess up
            gameObject.transform.parent = collision.transform.root;

            //calculates damage if hit object/entity has an "entity" component attached
            if (collision.transform.gameObject.GetComponent<Entity>())
            {
                //attaches this projectile to a proper movable collider, so it moves along with body parts
                gameObject.transform.parent = collision.transform;
                collision.transform.root.gameObject.GetComponent<Entity>().TakeDamage(projectileDmg, projectileDamageType, false, true);
                GameObject[] target = {collision.transform.root.gameObject};
                WorldEffectSystem.BestowStatusEffect(
                    projectileEffectNameList.ToArray(),
                    projectileEffectTypeList.ToArray(),
                    projectileEffectChanceList.ToArray(),
                    projectileEffectDurationList.ToArray(),
                    projectileEffectTargets.ToArray(),
                    parentGameObject,
                    target
                    );
            }
            hasHitObj = true;
        }
    }

    //destroy checker
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    //assign parent of gameobject
    public void AssignParentGameObj(GameObject parentToSet)
    {
        parentGameObject = parentToSet;
    }


}
