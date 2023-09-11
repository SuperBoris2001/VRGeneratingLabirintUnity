using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty gripAnimationAction;
    public InputActionProperty menuAnimationAction;

    public InputActionProperty lightSpellAction;
    public InputActionProperty damageSpellAction;

    public Animator handAnimator;

    public AudioSource damageSound;
    public AudioSource lightSound;

    public GameObject damageSpell;
    public GameObject lightSpell;
    public GameObject damageStuff;
    public GameObject stuff;
    public GameObject magicStuff;
    public GameObject canvasMenu;

    private GameObject tmp = null;
    private GameObject currentDamageSpell = null;
    private bool menuActive = true;



    void Start()
    {

    }

    void Update()
    {
        float grip = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip",grip);
        Console.WriteLine(grip);

        float menu = menuAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", grip);

        bool a_click = lightSpellAction.action.WasPerformedThisFrame();
        bool b_click = damageSpellAction.action.WasPerformedThisFrame();
        bool menu_click = menuAnimationAction.action.WasPerformedThisFrame();

        Vector3 pos = stuff.transform.position;
        Vector3 damagePos = damageStuff.transform.position;

        if (menu_click)
        {
            if (menuActive)
            {
                canvasMenu.SetActive(false);
                menuActive = false;
            }
            else
            {
                canvasMenu.SetActive(true);
                menuActive = true;

            }
        }

        if (a_click) 
        {
            if (tmp == null)
            {
                tmp = Instantiate(lightSpell, pos, Quaternion.identity);
                tmp.transform.SetParent(stuff.transform);
                lightSound.Play();
            }
            else
            {
                Destroy(tmp);
                tmp = null;
                lightSound.Stop();
            }
            
        }

        if (b_click)
        {
            if (currentDamageSpell == null)
            {
                currentDamageSpell = Instantiate(damageSpell, damagePos, Quaternion.identity);
                currentDamageSpell.transform.SetParent(damageStuff.transform);
                currentDamageSpell.transform.rotation = damageStuff.transform.rotation;
                damageSound.Play();

            }
            else
            {
                Destroy(currentDamageSpell);
                currentDamageSpell = null;
                damageSound.Stop();
            }

        }
    }
}
