using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISightManager : MonoBehaviour
{
    #region Singleton
    public static AISightManager instance;
    public GameObject player;

    void Awake()
    {
        instance = this;
        player = GameObject.Find("Player");
    }
    #endregion

}