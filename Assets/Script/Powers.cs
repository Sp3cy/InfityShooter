using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Descrizione del potere per ogni livello
[System.Serializable]
public class PowersStruct
{
    public string[] descLevel;
    public int maxLevel = 3;
    public Sprite sprite;
    public bool isActive = true;

    private int id;
    private int level = 0;

    public int Level { get => level; set => level = value; }
    public int Id { get => id; set => id = value; }
}

public class Powers : MonoBehaviour
{
    [Header("- Powers")]
    public PowersStruct grenadePowerUp;
    public PowersStruct boltPowerUp;
    public PowersStruct kunaiPowerUp;
    public PowersStruct crazyCirclePowerUp;
    public PowersStruct rotatingBladePowerUp;

    [System.NonSerialized]
    public List<PowersStruct> powersData = new List<PowersStruct>();
    protected List<int> avaiblePowerIndexes = new List<int>();

    [Header("- Granata")]
    public AudioSource grenadeExplosionSound;
    public GameObject grenadePrefab;
    public GameObject grenadeParticleExplosion;
    public float grenadeForce = 10f;
    public float grenadeTimer = 1f;
    public float grenadeRechargeT = 10f;
    public float offsetGrenade = 1f;

    public static float grenadeDamage = 20f;
    protected int grenadeAmount = 0;

    [Header("- Bolt")]
    public AudioSource boltSoundfx;
    public GameObject boltPrefab;
    public GameObject boltHitZone;
    public GameObject particleBruciatura;
    public float boltRechargeT = 2f;
    public float boltMaxRange = 7f;

    public static float boltDamage = 20f;
    protected int boltAmount;

    [Header("- Kunai")]
    public GameObject kunaiPrefab;
    public int kunayDestroyTime = 3;

    public float kunayRechargeT = 3f;
    public float kunaiForce = 35f;

    public static float kunaiDamage = 5f;
    protected int kunaiAmount = 0;

    [Header("- Crazy Circle")]
    public GameObject crazyCirclePrefab;
    public float crazyCircleRechargeT = 3.5f;

    public static float crazyCircleDamage = 5f;
    protected int crazyCircleAmount = 0;


    [Header("- Rotating Blade")]
    public GameObject rotatingBladePrefab;
    public float rotatingBladeRechargeT = 1f;
    public float rotatingBladeOrbitRadius = 1.5f;
    public float rotatingBladeRotationSpeed = 180f;
    public float rotatingBladeOrbitSpeed = 90f;

    public static float rotatingBladeDamage = 10f;
    protected int rotatingBladeAmount = 0;





    protected GameObject player;

    // -- NON SI USA START IN QUESTO SCRIPT
    // -- NEMMENO UPDATE

    // -- Aggiungere powerup qua
    protected void SetupPowers()
    {
        powersData.Clear();

        if (grenadePowerUp.isActive) powersData.Add(grenadePowerUp);
        if (boltPowerUp.isActive) powersData.Add(boltPowerUp);
        if (kunaiPowerUp.isActive) powersData.Add(kunaiPowerUp);
        if (crazyCirclePowerUp.isActive) powersData.Add(crazyCirclePowerUp);
        if (rotatingBladePowerUp.isActive) powersData.Add(rotatingBladePowerUp);

        for (int i = 0; i < powersData.Count; i++) powersData[i].Id = i;
    }


    // Controlla se i poteri utilizzabili hanno raggiunto il livello massimo
    // If no, li assegna come disponibili
    public void SetupAvaiblePowers()
    {
        avaiblePowerIndexes.Clear();

        for (int i=0; i<powersData.Count; i++)
        {
            if (powersData[i].Level < powersData[i].maxLevel)
            {
                avaiblePowerIndexes.Add(i);
            }
        }
    }

    public PowersStruct GetRandPowerUp()
    {
        if (avaiblePowerIndexes.Count <= 0) return null;

        int index = avaiblePowerIndexes[Random.Range(0, avaiblePowerIndexes.Count)];

        PowersStruct pw = powersData[index];
        avaiblePowerIndexes.Remove(index);

        return pw;
    }

    protected IEnumerator Grenade()
    {
        var grenade = Instantiate(grenadePrefab, player.transform.position, player.transform.rotation);
        Rigidbody2D grenadeRb = grenade.GetComponent<Rigidbody2D>();
        Collider2D grenadeCol = grenade.GetComponent<Collider2D>();

        grenadeCol.enabled = false;

        // Direzione in avanti rispetto alla rotazione del player
        Vector2 forwardDir = player.transform.up;

        // Aggiungi una variazione angolare casuale fino a �30 gradi
        float angleOffset = Random.Range(-30f, 30f);
        Vector2 randomizedDir = Quaternion.Euler(0, 0, angleOffset) * forwardDir;

        // Applica la forza
        grenadeRb.AddForce(randomizedDir.normalized * grenadeForce, ForceMode2D.Force);

        // Wait for grenade timer
        yield return new WaitForSeconds(grenadeTimer);

        // Set the collider active
        grenadeCol.enabled = true;

        // Wait fix for collider function
        yield return new WaitForFixedUpdate();
        grenadeCol.enabled = false;

        var grenadeExplodeEffect = Instantiate(grenadeParticleExplosion, grenade.transform.position, Quaternion.identity);

        grenadeExplosionSound.pitch = Random.Range(1f, 1.3f);
        grenadeExplosionSound.Play();
        Destroy(grenade);

        // Wait for particles anim
        yield return new WaitForSeconds(grenadeExplodeEffect.GetComponent<ParticleSystem>().main.duration);
        Destroy(grenadeExplodeEffect);

        yield return null;
    }


    protected IEnumerator Bolts(Transform enemyPos)
    {
        // Instantiate bolt and sound
        var bolt = Instantiate(boltPrefab, enemyPos.position - new Vector3(0, -11.6f, 0), Quaternion.Euler(55,180,0));
        boltSoundfx.pitch = Random.Range(0.65f, 1f);
        boltSoundfx.Play();

        // When bolt anim (only strikes) instantiate the hitzone
        yield return new WaitForSeconds(boltPrefab.GetComponent<ParticleSystem>().main.duration - 0.65f);
        var hitZone = Instantiate(boltHitZone, bolt.transform.position + new Vector3(0, -11.6f, 0), Quaternion.identity);

        // Wait 3 frames (Just for caution) and destroy hitzone -- Cringe but i wont write a for
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Destroy(hitZone);

        // Wait for bruciature anim to end
        yield return new WaitForSeconds(particleBruciatura.GetComponent<ParticleSystem>().main.duration);
        Destroy(bolt);

        yield return null;
    }

    public IEnumerator Kunai()
    {
        //instantiate kunai
        var kunai = Instantiate(kunaiPrefab, player.transform.position, player.transform.rotation);
        Rigidbody2D kunaiRb = kunai.GetComponent<Rigidbody2D>();
        Collider2D kunaiCol = kunai.GetComponent<Collider2D>();
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        kunaiRb.AddForce(randomDir * grenadeForce, ForceMode2D.Force);

        //wait destroy time
        yield return new WaitForSeconds(kunayDestroyTime);
        Destroy(kunai);

        yield return null;
    }

    public IEnumerator CrazyCircle()
    {
        //instantiate CrazyCircle
        var crazyCircle = Instantiate(crazyCirclePrefab, player.transform.position, player.transform.rotation);
        
        //wait anim duration
        yield return new WaitForSeconds(crazyCirclePrefab.GetComponent<ParticleSystem>().main.duration);
        Destroy(crazyCircle);

        yield return null;
    }

    public IEnumerator RotatingBlade(int index, int total)
    {
        GameObject blade = Instantiate(rotatingBladePrefab, player.transform.position, Quaternion.identity);

        float angleOffset = 360f / total * index;
        float currentAngle = angleOffset;

        float orbitRadius = rotatingBladeOrbitRadius;

        while (true)
        {
            if (player == null || blade == null) yield break;

            // Aggiorna l'angolo orbitale
            currentAngle += rotatingBladeOrbitSpeed * Time.deltaTime;
            float rad = currentAngle * Mathf.Deg2Rad;

            // Calcola posizione orbitale attorno al player
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * orbitRadius;
            blade.transform.position = player.transform.position + offset;

            // Ruota su s� stessa solo su Z
            blade.transform.Rotate(0f, 0f, rotatingBladeRotationSpeed * Time.deltaTime);

            // Forza la rotazione globale per evitare inclinazioni su altri assi
            Vector3 fixedEuler = blade.transform.rotation.eulerAngles;
            blade.transform.rotation = Quaternion.Euler(0f, 0f, fixedEuler.z);

            yield return null;
        }
    }

}
