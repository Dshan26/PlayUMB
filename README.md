## **Descripción del Proyecto**
Este proyecto es un juego 2D desarrollado en Unity que incluye mecánicas básicas como salto, generación de enemigos, manejo de puntajes y animaciones del jugador. Está diseñado para ser una introducción a los juegos 2D, utilizando sistemas como UI, colisiones, y prefabs.

---

## **Características Principales**

### 1. **Estados del Juego**
El juego tiene tres estados principales:
- **Ready:** El juego espera la entrada del jugador para comenzar.
- **Playing:** El jugador está en acción, con enemigos generándose y el puntaje aumentando.
- **Ended:** El juego termina al colisionar con un enemigo.

### 2. **Manejo del Jugador**
El jugador puede:
- Cambiar entre animaciones (por ejemplo, correr, saltar, morir).
- Saltar al presionar la barra espaciadora.
- Detectar colisiones con enemigos y puntos.

### 3. **Sistema de Enemigos**
- Los enemigos se generan dinámicamente desde un punto de spawn.
- Se detiene la generación de enemigos cuando el juego finaliza.

### 4. **Puntaje**
- El sistema de puntaje registra y muestra los puntos acumulados.
- Registra el puntaje máximo utilizando PlayerPrefs.

---

## **Scripts Principales**

### 1. **PlayerManager**
Maneja la lógica relacionada con el jugador:
- Cambia las animaciones del jugador utilizando un `Animator`.
- Detecta colisiones con etiquetas "Enemy" y "Points" para finalizar el juego o aumentar el puntaje.

**Código relevante:**
```csharp
void OnTriggerEnter2D(Collider2D collider)
{
    if (collider.tag == "Enemy") enemyCollision = true;
    else if (collider.tag == "Points") {
        ScoreManager.Instance.IncreasePoints();
    }
}
```

---

### 2. **ScoreManager**
Maneja el sistema de puntaje:
- Actualiza el puntaje actual y el puntaje máximo.
- Muestra el puntaje en la interfaz utilizando `TextMeshPro`.

**Código relevante:**
```csharp
public void IncreasePoints()
{
    points++;
    pointsText.text = points.ToString();
    UpdateMaxPoints();
}

public void UpdateMaxPoints()
{
    int maxPoints = PlayerPrefs.GetInt("Max", 0);

    if (points >= maxPoints)
    {
        maxPoints = points;
        PlayerPrefs.SetInt("Max", maxPoints);
    }

    maxPointsText.text = "BEST: " + maxPoints.ToString();
}
```

---

### 3. **SpawnManager**
Maneja la lógica de generación de enemigos:
- Genera enemigos repetidamente en un intervalo definido.
- Permite iniciar y detener la generación de enemigos.

**Código relevante:**
```csharp
public void StartSpawn()
{
    if (enemyPrefab == null)
    {
        Debug.LogError("SpawnManager: enemyPrefab no está asignado antes de comenzar el spawn.");
        return;
    }
    InvokeRepeating("SpawnEnemy", 0f, spawnTimer);
}

void SpawnEnemy()
{
    Instantiate(enemyPrefab, transform.position, Quaternion.identity);
}

public void StopSpawn()
{
    CancelInvoke("SpawnEnemy");
}
```

---

### 4. **GameManager**
Controla el flujo del juego:
- Administra el cambio entre los estados del juego.
- Maneja el desplazamiento del fondo (parallax) durante el estado "Playing".

**Código relevante:**
```csharp
void UpdateGameState(bool action1)
{
    if (gameState == GameState.Ready && action1 ){
        gameState = GameState.Playing;
        uiReady.SetActive(false);

        PlayerManager.Instance.setAnimation("PlayerRun");
        SpawnManager.Instance.StartSpawn();
    }
}

void UpdateParallax()
{
    if (gameState == GameState.Playing)
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
    }
}
```

---

## **Jerarquía de Objetos en Unity**
La jerarquía de objetos del proyecto incluye:
```
- Main Camera
- Game Canvas
    - Background
    - Platform
    - UI Ready
    - UI Score
        - Points
        - Info
- Player
- Enemy (Prefab)
- SpawnManager
- GameManager
- ScoreManager
```

---

## **Cómo Jugar**
1. **Inicio:** Al iniciar el juego, la interfaz muestra el estado "Ready".
2. **Comenzar:** Presiona la barra espaciadora, Enter o haz clic para empezar a jugar.
3. **Juego:**
   - Salta presionando la barra espaciadora.
   - Evita los enemigos.
   - Recoge puntos para aumentar el puntaje.
4. **Fin:** El juego termina al colisionar con un enemigo.
5. **Reinicio:** Reinicia la escena para jugar nuevamente.

---

## **Problemas Comunes y Soluciones**
1. **`NullReferenceException` para prefabs no asignados:**
   - Verifica que los prefabs estén asignados en el Inspector.

2. **Error en puntajes máximos:**
   - Asegúrate de que los campos `pointsText` y `maxPointsText` tengan asignados los objetos correctos de la UI.

3. **Objetos no generados:**
   - Comprueba que `enemyPrefab` está correctamente asignado en el `SpawnManager`.

---

## **Mejoras Futuras**
- Agregar efectos de sonido y música de fondo.
- Implementar niveles progresivos con dificultad creciente.
- Incluir una pantalla de inicio y un menú de pausa.
- Integrar más animaciones para el jugador y enemigos.

---

¡Gracias por jugar y explorar este proyecto de juego 2D en Unity! 🚀

