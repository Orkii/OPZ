using UnityEngine;

public static class Utility {
    public static Vector3 StringToVector3(string sVector) {
        Debug.Log(" =" + sVector);
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")")) {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        Vector3 result = new Vector3(
            float.Parse(sArray[0].Replace('.', ',')),
            float.Parse(sArray[1].Replace('.', ',')),
            float.Parse(sArray[2].Replace('.', ',')));

        return result;
    }
}