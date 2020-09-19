using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
         public static GameManager instance;
         public float respawnTime = 2f;
    public int currentLives =3;
    private void Awake() {
        instance = this;
    }
  public void KillPlayer()
  {
    currentLives --;
    if(currentLives >0)
    {
      StartCoroutine(RespawnCo());
    }else
    {

    }
  }
  public IEnumerator RespawnCo(){
    
    yield return new WaitForSeconds(respawnTime);
    HealthManager.instance.Respawn();
    WaveManager.instance.ContinueSpawning();
  }
}
