using UnityEngine;
public abstract class ActivatedItem : Item
{
    // set effectDuration to -1 if no lingering effects
    // set cooldownDuration to -1 if item is one time use
    public float effectDuration, cooldownDuration; // make sure you set these up
    public float effectTimeElapsed, cooldownTimeElapsed;
    public bool isOnCooldown = false, isOnEffect = false;

    public float getCooldownDuration()
    {
        return cooldownDuration;
    }

    public float getEffectDuration()
    {
        return effectDuration;
    }
    public override void Update()
    {
        base.Update();
        // if the item is activated and is doing its effect
        if (isOnEffect)
        {
            processEffect();
            return;
        }

        // if the item is completely done with its activation
        // and the item is on cooldown
        if (isOnCooldown)
        {
            processCooldown();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        setItemDurations();
        if (effectDuration < -100 || cooldownDuration < -100)
        {
            Debug.Log("Set up the item durations please.");
        }
    }

    public override void process()
    {
        base.process();
        stats.setActivatedItem(this);
    }

    public void activateItem()
    {
        if (!isOnCooldown)
        {
            isOnEffect = true;
            isOnCooldown = true;
            // do the initial item activation effect, then set the effect and cooldown timer
            // to count down to zero
            setActivateItemBehavior();
            effectTimeElapsed = effectDuration;
            cooldownTimeElapsed = cooldownDuration;
        }
    }

    //returns true once effect ends, false if not
    private void processEffect()
    {
        // if the effect duration is -1, there is no effect, so
        // immediately move to cooldown
        if (effectDuration < -0.9) // basically if duration is -1
        {
            isOnEffect = false;
            return;
        }
        setItemEffectBehavior(); // do the effect
        effectTimeElapsed -= Time.deltaTime;
        if (effectTimeElapsed < 0)
        {
            isOnEffect = false;
            return; // effect is done, move to cooldown
        }
        else
        {
            return; // effect is still going
        }
    }

    // if the cooldown duration is -1, the item is a one time use
    private void processCooldown()
    {
        // if the cooldown duration is -1, the item is a one time use
        if (cooldownDuration < -0.9)
        {
            destroyItem();
            return;
        }
        cooldownTimeElapsed -= Time.deltaTime;
        if (cooldownTimeElapsed < 0)
        {
            //cooldown is completed, item is ready to use again
            isOnCooldown = false;
        }
        //invoke some item cooldown UI echo
    }

    private void destroyItem()
    {
        GameObject.Destroy(gameObject);
    }

    public bool isOneTimeUse()
    {
        // returns true if cooldownDuration is -1, false if not
        return cooldownDuration < -.09 ? true : false;
    }

    //define effect and cooldown durations
    protected abstract void setItemDurations();

    //define initial activation effects.
    protected virtual void setActivateItemBehavior()
    {
        return;
    }

    //define item effect after activation. Ignore if effectDuration is -1
    protected virtual void setItemEffectBehavior()
    {
        return;
    }
}
