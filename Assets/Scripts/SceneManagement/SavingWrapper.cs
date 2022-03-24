using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}
