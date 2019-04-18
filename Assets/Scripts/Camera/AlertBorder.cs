using System.Collections;
using UnityEngine;

public class AlertBorder : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Border border = default;

    // ---- INTERN ----
    private bool alarm = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!alarm)
        {
            Cube cube = other.GetComponent<Cube>();
            if(cube)
            {
                StartCoroutine(AlarmPulse()); ;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!alarm)
        {
            Cube cube = other.GetComponent<Cube>();
            if (cube)
            {
                StartCoroutine(AlarmPulse());
            }
        }
    }

    private IEnumerator AlarmPulse()
    {
        alarm = true;
        float time = 0f;
        Color color = border.GetMesh().material.color;
        // fade in
        while (time < 0.3f)
        {
            border.GetMesh().material.color = new Color(color.r, color.g, color.b, time / 1f);
            time += Time.deltaTime;
            yield return null;
        }
        // fade out
        while (time > 0f)
        {
            border.GetMesh().material.color = new Color(color.r, color.g, color.b, (time > 0 ? time : 0) / 1f);
            time -= Time.deltaTime;
            yield return null;
        }

        alarm = false;
    }
}
