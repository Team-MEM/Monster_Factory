namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioManager : Singleton<AudioManager>
    {
        public AudioSource machinePlacement, click, music, cash, powerOn, powerOff, demolish, rotate, monsterCreated, error;
    }

}

