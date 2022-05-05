using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{

    public float runSpeed;
    public float gotHayDestroyDelay; 
    private bool hitByHay;
    public bool canSpawn = true; // 1


    public float dropDestroyDelay;
    private Collider myCollider; 
    private Rigidbody myRigidbody;
    private SheepSpawner sheepSpawner;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Hay") && !hitByHay) 
        {
            Destroy(other.gameObject); 
            HitByHay();
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }

    private void HitByHay()
    {
        SoundManager.Instance.PlaySheepHitClip();

        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true; 
        runSpeed = 0; 

        Destroy(gameObject, gotHayDestroyDelay);
    }

    private void Drop()
    {
        SoundManager.Instance.PlaySheepDroppedClip();

        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        sheepSpawner.RemoveSheepFromList(gameObject);
        Destroy(gameObject, dropDestroyDelay); 
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }


}
