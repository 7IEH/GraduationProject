using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { profession, coin, Heart};
    public Type type;
    public int value;
}
