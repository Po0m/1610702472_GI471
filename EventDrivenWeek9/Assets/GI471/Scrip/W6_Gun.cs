using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class W6_Gun : MonoBehaviour
{
    public enum GunState
    {
        Idle,
        Fire,
        Reload,
    }

    public float damagePerShot;
    public float speedAnimMove;
    public float moveRange;

    public int maxAmmo;
    public int currentAmmo;
    public float delayPerShot;
    public float reloadTime;

    public Image reloadGuid;
    public Text textAmmo;

    private float countTimeShot;
    private float countReloadTime;

    private Vector3 defaultLocalPos;
    private Ray ray;
    private float countSin;
    public Animator animator;
    private GunState gunState;
    private GameObject owner;

    public delegate void DelegateHandleGunState (GunState gunState);
    public event DelegateHandleGunState OnGunStateChang;

    private void Start()
    {
        ray = new Ray();
        defaultLocalPos = this.transform.localPosition;

        reloadGuid.gameObject.SetActive(false);
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }

    public void Update()
    {
        UpdateTextAmmo();
        UpdateGunState();
    }

    public void FirePress()
    {
        ChangState(GunState.Fire);
    }

    public void FireRelease()
    {
        ChangState(GunState.Idle);
    }

    public void Reload()
    {
        if(currentAmmo < maxAmmo)
        {
            ChangState(GunState.Reload);
        }
    }

    private void UpdateTextAmmo()
    {
        textAmmo.text = currentAmmo + " / " + maxAmmo;
    }

    private void UpdateGunState()
    {
        switch (gunState)
        {
            case GunState.Idle:
                {
                    break;
                }
            case GunState.Fire:
                {
                    UpdateFire();
                    break;
                }
            case GunState.Reload:
                {
                    UpdateReload();
                    break;
                }
        }
    }

    public void UpdateFire()
    {
        if (countTimeShot >= delayPerShot)
        {
            if (currentAmmo > 0)
            {
                Fire(owner);
                currentAmmo--;
            }
            else
            {
                NotFire();
            }
            countTimeShot = 0.0f;
        }

        countTimeShot += Time.deltaTime;
    }

    public void UpdateReload()
    {
        if(countReloadTime >= reloadTime)
        {
            currentAmmo = maxAmmo;
            ChangState(GunState.Idle);
        }

        countReloadTime += Time.deltaTime;

        reloadGuid.fillAmount = countReloadTime / reloadTime;
    }

    public void Fire(GameObject owner)
    {
        animator.SetTrigger("Fire Trigger");
        

        RaycastHit hit;
        ray.origin = Camera.main.transform.position;
        ray.direction = Camera.main.transform.forward;
        bool isHit = Physics.Raycast(ray, out hit, 1000);

        if(isHit)
        {
            var receiveDamage = hit.collider.gameObject.GetComponent<W6_ReceiveDamage>();

            if(receiveDamage)
            {
                receiveDamage.TakeDamage(owner, damagePerShot);
            }
        }

    }

    public void NotFire()
    {
        //Notification not fire
    }

    public void UpdateAnimationMove(float speedRartio)
    {
        countSin += Time.deltaTime * (speedAnimMove * speedRartio);

        Vector3 curPos = this.transform.localPosition;
        curPos.y = defaultLocalPos.y + ((Mathf.Sin(countSin) * moveRange) * speedRartio);
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, curPos, Time.deltaTime * 10.0f);
    }

    public void UpdateAnimationFire()
    {

    }

    private void ChangState(GunState toChange)
    {
        if(gunState != toChange)
        {
            if(OnGunStateChang != null)
            {
                OnGunStateChang(toChange);
            }
            gunState = toChange;

            switch(gunState)
            {
                case GunState.Idle:
                    {
                        reloadGuid.gameObject.SetActive(false);
                        break;
                    }
                case GunState.Fire:
                    {
                        countTimeShot = 0.0f;
                        break;
                    }
                case GunState.Reload:
                    {
                        countReloadTime = 0.0f;
                        reloadGuid.gameObject.SetActive(true);
                        reloadGuid.fillAmount = 0.0f;
                        break;
                    }
            }
        }
    }

}
