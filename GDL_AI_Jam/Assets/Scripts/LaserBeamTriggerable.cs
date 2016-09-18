using UnityEngine;
using System.Collections;

public class LaserBeamTriggerable : TriggerableTrap 
{
    private Coroutine fireLaserCoroutine = null;

    void Start() 
    {
        _line = gameObject.GetComponent<LineRenderer>();
        _line.enabled = false;
    }

    override public void ToggleState(bool active)
    {
        if (active)
            fireLaserCoroutine = StartCoroutine(FireLaser());
        else
        {
            if (fireLaserCoroutine != null)
            StopCoroutine(fireLaserCoroutine);
            _line.enabled = false;
        }
    }

    IEnumerator FireLaser()
    {
        _line.enabled = true;

        while (true)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            _line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100))
            {
                _line.SetPosition(1, hit.point);
                Player player = hit.transform.gameObject.GetComponent<Player>();
                if (player != null)
                    player.Kill();
            }
            else
                _line.SetPosition(1, ray.GetPoint(100));

            yield return null;
        }
    }

    LineRenderer _line;
}
