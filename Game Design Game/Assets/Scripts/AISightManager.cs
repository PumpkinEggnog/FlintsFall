using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISightManager : MonoBehaviour
{
    #region Singleton
    public static AISightManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;
}