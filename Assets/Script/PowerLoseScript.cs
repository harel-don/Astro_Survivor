using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLoseScript : MonoBehaviour
{
    [SerializeField] private float timeToLosePower = 20;
    private float timer;

    public GameObject powerShoot;
    public GameObject powerArrow;
    public GameObject powerBomb;
    
    // private bool [] Powers;
    private Dictionary<GameObject, bool> powers;
    private List<GameObject> powersInt;
    [SerializeField] private ScriptForPlayer ScriptForPlayer;
    public Slide slide;
    // Start is called before the first frame update
    void Start()
    {
        // Powers = new bool[] {true, true, true};
        powers = new Dictionary<GameObject, bool>();
        powers.Add(powerArrow, true);
        powers.Add(powerShoot, true);
        powers.Add(powerBomb, true);
        powersInt = new List<GameObject>{powerArrow, powerShoot, powerBomb};
    }

    // Update is called once per frame
    void Update()
    {
        if(powersInt.Count == 0)return;
        slide.slider.value = 1 - (timer/ timeToLosePower);
        if (timer >= timeToLosePower)
        {
            var i = Random.Range((int) 0, powersInt.Count);
            if (powersInt[i].Equals(powerArrow))
            {
                RemovePowersHelper(i);
                powerArrow.SetActive(false);
                ScriptForPlayer.setArrowFalse();
            }

            else if (powersInt[i].Equals(powerBomb))
            {
                RemovePowersHelper(i);
                powerBomb.SetActive(false);
                ScriptForPlayer.setBombFalse();
            }
            
            else if (powersInt[i].Equals(powerShoot))
            {
                RemovePowersHelper(i);
                powerShoot.SetActive(false);
                ScriptForPlayer.setGunFalse();
            }

            timer = 0;
        }

        timer += Time.deltaTime;
    }

    private void RemovePowersHelper(int i)
    {
        powers[powersInt[i]] = false;
        powersInt.Remove(powersInt[i]);
    }
}
