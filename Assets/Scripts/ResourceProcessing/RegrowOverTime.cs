using System.Collections;
using UnityEngine;

/// <summary>
/// This class is responsible for regrowing trees and rocks over time
/// </summary>
public class RegrowOverTime : MonoBehaviour, IResettable
{
    [SerializeField] private float regrowDelay;

    private IEnumerator regrowCoroutine;

    public void Regrow()
    {
        regrowCoroutine = RegrowCoroutine();
        StartCoroutine(regrowCoroutine);
    }

    private IEnumerator RegrowCoroutine()
    {
        // Wait for regrow delay
        yield return new WaitForSeconds(regrowDelay);

        // TODO: Implement regrowing

    }

    public void ResetGameObject()
    {
        // Stop regrowing coroutine if it is regrowing
        if (regrowCoroutine != null)
            StopCoroutine(regrowCoroutine);

        Destroy(gameObject);
    }
}
