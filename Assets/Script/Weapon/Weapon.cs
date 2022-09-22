using System.Collections;
using UnityEngine;

public interface Weapon
{

    // FIRERATE CRINGE FIX --
    public void ResetFireRate();

    // DEVE lanciare la coro, viene chiamata ogni fiixed update
    public void Shoot();

    // DEVE eseguire la crezione di bullet e shoot
    public IEnumerator ShootCoro();

    // DEVE lanaciare la coro
    public void Reload();
    public IEnumerator ReloadCoro();

}
