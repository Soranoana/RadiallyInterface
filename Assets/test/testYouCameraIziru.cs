/*
 * ======================================================================
 * Project Name    : RadiallyInterface
 * File Name       : testYouCameraIziru.cs
 * Creation Date   : 2020/01/30
 *  
 * Copyright © 2020 Soranoana(ソラノアナ). All rights reserved.
 *  
 * This source code or any portion thereof must not be  
 * reproduced or used in any manner whatsoever.
 * ======================================================================
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testYouCameraIziru : MonoBehaviour {

    public GameObject mainCamera;
    public GameObject leapCamera;
    public GameObject centralObject;
    public Vector3 mainPosition;
    public Vector3 leapPosition;
    public Vector3 leapRotation;
    public Vector3 centralPosition;
    private int flg = 0;
    void Start() {
        /*
        mainCamera.GetComponent<Camera>().targetDisplay = 0;
        leapCamera.GetComponent<Camera>().targetDisplay = 1;
        */
        leapCamera.GetComponent<AudioListener>().enabled = false;
        leapCamera.transform.eulerAngles = leapRotation;
    }

    void Update() {
        /*
        if (flg == 0) {
            mainCamera.GetComponent<Camera>().targetDisplay = 2;
            leapCamera.GetComponent<Camera>().targetDisplay = 0;
            flg = 1;
        } else if (flg == 1) {
            leapCamera.GetComponent<Camera>().targetDisplay = 1;
            mainCamera.GetComponent<Camera>().targetDisplay = 0;
            flg = 2;
        }
        */
        if (/*centralObject.transform.Find("0") || */centralObject.GetComponent<createTrapezoidPole>() == null) {
            mainCamera.transform.position = mainPosition;
            leapCamera.transform.position = leapPosition;
            centralObject.transform.position = centralPosition;
            Destroy(this);
        }
    }
}