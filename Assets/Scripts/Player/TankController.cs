using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{   
    [SerializeField] private TankMovement _tankMovement;
    [SerializeField] private TankTowerRotate _tankTowerRotate;
    [SerializeField] private TankShooting _tankShooting;


    private string _axisHorizontal = "Horizontal";
    private string _axisVertical = "Vertical";

    public static bool IsMovement = false;


    private void Update()
    {
       
    }


}
