using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Dialopgue.Extensions {

    public static class StringExtensions {
        public static string InsertSpace(this string str, int spaceIndex) {
            int newSpaceIndex = str.Reverse().ToArray().Length - spaceIndex;
            return str.Insert(newSpaceIndex, " ");
        }
    }
}

