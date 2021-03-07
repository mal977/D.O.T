using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesProperty:MonoBehaviour
{
    public GameObject line;
    public int lineNode = 0;

    public LinesProperty(GameObject line, int nodeID) {
        this.line = line;
        lineNode = nodeID;
    }
}
