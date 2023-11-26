using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MainMenuManager : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Instance.bgm.Play();
            AudioManager.Instance.endbgm.Stop();
        }
    }
}