using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    // ════════════════════════════════════════
    //            FLAME THROWER
    // ════════════════════════════════════════

    public float flameThrowerRechargeT = 10f;
    public static float flameThrowerDamage = 3;

    public AudioSource flameThrowerAudiofx;
    public GameObject flameThrowerPrefab;
    public GameObject player;
    public Button FlameButton;
    public Text flameThrowerCountdown;

    private bool active = false;
    private bool shooting = false;


    public IEnumerator FlameThrower()
    {
        shooting = true;

        // Instantiate flame
        var flame = Instantiate(flameThrowerPrefab, player.transform.position, player.transform.rotation) as GameObject;
        flame.transform.parent = player.transform;
        flameThrowerAudiofx.Play();
        FlameButton.interactable = false;
        StartCoroutine(StartCooldownCoroutine(flameThrowerRechargeT, flameThrowerCountdown));  // Avvia il cooldown

        // Wait for flame time
        yield return new WaitForSeconds(flame.GetComponent<ParticleSystem>().main.duration);

        // Destroy flame and set shooting to false
        Destroy(flame);
        flameThrowerAudiofx.Stop();
        shooting = false;

        // Wait for skill recharge time
        yield return new WaitForSeconds(flameThrowerRechargeT - flame.GetComponent<ParticleSystem>().main.duration);

        FlameButton.interactable = true;

        active = false;
    }    
    public void FlameThrowerStart()
    {
        if (active == true) return;
        else
        {
            StartCoroutine(FlameThrower());
            active = true;
        }
    }

    public bool IsSkillShooting()
    {
        return shooting;
    }

    public void pauseFlameThrowerAudio()
    {
        if (shooting) flameThrowerAudiofx.Pause();
    }

    public void resumeFlameThrowerAudio()
    {
        if (shooting) flameThrowerAudiofx.Play();
    }


    // ════════════════════════════════════════
    //            METEOR SHOWER
    // ════════════════════════════════════════

    public static float meteorDamage = 500f;
    public float meteroRechargeT = 10f;
    public GameObject meteorShowerPrefab;
    public GameObject meteorImpactPrefab;
    public float speed = 1f;
    public float spawnDistance = 10.0f;
    public Transform playerTransform;
    private Vector3 randomTargetPosition;
    public float spawnInterval = 0.5f;  // Tempo tra ogni spawn di meteora (in secondi)
    public float rainDuration = 5f;  // Durata della pioggia di meteore (5 secondi)
    public AudioClip meteorSound;  // Aggiungi il clip audio per il suono della meteora
    public AudioClip impactSound;  // Aggiungi il clip audio per il suono dell'impatto
    public Button meteorButton;
    public Text cooldownTextMeteor;  // Text UI per visualizzare il countdown

    // Variazioni random per il pitch e il volume
    public float minPitch = 0.8f;  // Pitch minimo
    public float maxPitch = 1.2f;  // Pitch massimo
    public float minVolume = 0.8f; // Volume minimo
    public float maxVolume = 1.0f; // Volume massimo

    private bool isRaining = false;  // Flag per verificare se la pioggia è attiva

    // Metodo per iniziare la pioggia di meteore
    public void StartMeteorShower()
    {
        if (!isRaining)
        {
            meteorButton.interactable = false;
            StartCoroutine(StartCooldownCoroutine(meteroRechargeT, cooldownTextMeteor));  // Avvia il cooldown METEORA
            isRaining = true;
            StartCoroutine(MeteorShowerCoroutine());
        }
    }

    // Coroutine che gestisce la pioggia di meteore
    IEnumerator MeteorShowerCoroutine()
    {
        float endTime = Time.time + rainDuration;  // Calcola il tempo di fine pioggia

        while (Time.time < endTime)
        {
            // Spawna una meteora in una posizione casuale
            StartMeteorMovement();

            // Aspetta per l'intervallo specificato prima di spawnare la prossima meteora
            yield return new WaitForSeconds(spawnInterval);
        }

        // Fine pioggia di meteore
        yield return new WaitForSeconds(meteroRechargeT - rainDuration);
        meteorButton.interactable = true;
        isRaining = false;
    }

    // Funzione che inizia il movimento della meteora
    void StartMeteorMovement()
    {
        // Genera la posizione random attorno al player
        Vector3 randomTargetPosition = GenerateRandomPositionAroundPlayer(playerTransform);

        // Calcola posizione di spawn sopra il target
        Vector3 spawnPosition = new Vector3(
            randomTargetPosition.x,
            randomTargetPosition.y + spawnDistance,
            -1f
        );

        // Rotazione desiderata
        Quaternion spawnRotation = Quaternion.Euler(0f, 90f, -90f);

        // Instanzia il meteorite
        GameObject newMeteor = Instantiate(meteorShowerPrefab, spawnPosition, spawnRotation);

        // Aggiungi l'AudioSource alla meteora e riproduci il suono
        AudioSource audioSource = newMeteor.GetComponent<AudioSource>();
        if (audioSource != null && meteorSound != null)
        {
            audioSource.clip = meteorSound;  // Imposta il suono della meteora
            audioSource.pitch = Random.Range(minPitch, maxPitch);  // Aggiungi variazione al pitch
            audioSource.volume = Random.Range(minVolume, maxVolume);  // Aggiungi variazione al volume
            audioSource.Play();  // Riproduci il suono della meteora
        }

        // Avvia una nuova coroutine SOLO per questo meteorite
        StartCoroutine(MoveMeteorTowardsTarget(newMeteor, randomTargetPosition));
    }

    // Coroutine che sposta la meteora verso il target
    IEnumerator MoveMeteorTowardsTarget(GameObject meteor, Vector3 targetPosition)
    {
        Collider2D imageCollider = meteor.transform.Find("MeteorImage").GetComponent<Collider2D>();
        imageCollider.enabled = false;

        // Movimento della meteora fino al target
        while (Vector2.Distance(meteor.transform.position, targetPosition) > 0.1f)
        {
            Vector2 newPosition2D = Vector2.MoveTowards(
                meteor.transform.position,
                targetPosition,
                speed * Time.deltaTime
            );

            meteor.transform.position = new Vector3(
                newPosition2D.x,
                newPosition2D.y,
                -1f
            );

            yield return null;
        }

        // Posizione finale al target
        meteor.transform.position = new Vector3(
            targetPosition.x,
            targetPosition.y,
            -1f
        );

        // Abilita il collider della meteora
        imageCollider.enabled = true;

        // Crea l'effetto di impatto
        GameObject newImpact = Instantiate(meteorImpactPrefab, targetPosition, Quaternion.identity);

        // Aggiungi l'AudioSource all'impatto e riproduci il suono dell'impatto
        AudioSource impactAudioSource = newImpact.GetComponent<AudioSource>();
        if (impactAudioSource != null && impactSound != null)
        {
            impactAudioSource.clip = impactSound;  // Imposta il suono dell'impatto
            impactAudioSource.pitch = Random.Range(minPitch, maxPitch);  // Aggiungi variazione al pitch
            impactAudioSource.volume = Random.Range(minVolume, maxVolume);  // Aggiungi variazione al volume
            impactAudioSource.Play();  // Riproduci il suono dell'impatto
        }

        // Disabilita i child (immagine e particelle), lascia solo la scia
        foreach (Transform child in meteor.transform)
        {
            yield return new WaitForSeconds(0.1f);
            child.gameObject.SetActive(false);
        }

        // Distruggi la meteora e l'effetto di impatto dopo che il ParticleSystem finisce
        yield return new WaitForSeconds(meteor.GetComponent<ParticleSystem>().main.duration);
        Destroy(meteor);

        yield return new WaitForSeconds(meteorImpactPrefab.GetComponent<ParticleSystem>().main.duration);
        Destroy(newImpact);
    }

    // Funzione che genera una posizione random intorno al player
    Vector3 GenerateRandomPositionAroundPlayer(Transform playerTransform)
    {
        float randomOffsetX = Random.Range(-7f, 7f);
        float randomOffsetY = Random.Range(-16f, 16f);

        Vector3 randomPosition = new Vector3(
            playerTransform.position.x + randomOffsetX,
            playerTransform.position.y + randomOffsetY,
            -1f // Mantieni asse Z fisso a -1
        );

        return randomPosition;
    }


    // ════════════════════════════════════════
    //            METODI CONDIVISI
    // ════════════════════════════════════════


    // Funzione per gestire il cooldown del bottone
    IEnumerator StartCooldownCoroutine(float cooldownDuration, Text coolDownText)
    {
        float remainingTime = cooldownDuration;

        while (remainingTime > 0)
        {
            // Aggiorna il testo con il tempo rimanente
            coolDownText.text = Mathf.Ceil(remainingTime).ToString();
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        coolDownText.text = "";  // Puoi anche resettare il testo o aggiungere un messaggio
    }


}

