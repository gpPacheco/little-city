# 🏙️ Little City

**Desenvolvido por:** Gabriel Fernandes Pacheco  
**Engine:** Unity 6 (Built-In Render Pipeline)  
**Disciplina:** Desenvolvimento de Jogos 3D — 2º Bimestre

---

## 📖 Descrição

Little City é um jogo 3D de exploração urbana onde você controla um fantasma que vaga livremente por uma cidade low poly. O objetivo é explorar o cenário urbano utilizando as habilidades de movimento do personagem, como correr, pular, voar e dar dash pela cidade.

---

## 🎮 Instruções de Jogabilidade

| Tecla | Ação |
|-------|------|
| W / S | Mover para frente / trás |
| A / D | Girar para esquerda / direita |
| Shift | Correr |
| Espaço | Pular |
| Espaço (segurar no ar) | Voar |
| Q | Dash (impulso rápido para frente) |

---

## 🎬 Gameplay

[![Gameplay Little City](https://img.youtube.com/vi/8_2fT6WQ13o/0.jpg)](https://youtu.be/8_2fT6WQ13o)

---

## 📸 Screenshots

### Menu Principal
![Menu Principal](prints/menu.png)

### Gameplay 1
![Gameplay 1](prints/gameplay1.png)

### Gameplay 2
![Gameplay 2](prints/gameplay2.png)

---

## ⚙️ Funcionalidades Desenvolvidas

### 1. 🚀 Dash — Impulso rápido para frente

Foi desenvolvido um sistema de dash que permite ao jogador dar um impulso rápido para frente ao pressionar a tecla **Q**. O sistema conta com um cooldown de 2 segundos para evitar uso excessivo, tornando o movimento mais estratégico durante a exploração da cidade.

```csharp
// Ativa o dash
if (Input.GetKeyDown(KeyCode.Q) && dashCooldownTimer <= 0 && !isDashing)
{
    isDashing = true;
    dashTimer = dashDuration;
    dashCooldownTimer = dashCooldown;
}

// Durante o dash
if (isDashing)
{
    dashTimer -= Time.deltaTime;
    Vector3 dashMove = transform.forward * dashSpeed * Time.deltaTime;
    controller.Move(dashMove);

    if (dashTimer <= 0)
        isDashing = false;

    return;
}
```

![Dash](prints/gameplay1.png)

---

### 2. ✈️ Voar — Segurar espaço no ar para voar

Foi desenvolvido um sistema de voo onde o jogador pode pressionar **Espaço** para pular e, enquanto estiver no ar, segurar **Espaço** para fazer o fantasma voar suavemente para cima. Ao soltar a tecla, o personagem cai normalmente com gravidade, trazendo maior liberdade de exploração ao jogador.

```csharp
// Pulo normal no chão
if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
{
    verticalVelocity = jumpForce;
    isFlying = false;
}

// Segurar espaço no ar = voar
if (Input.GetKey(KeyCode.Space) && !controller.isGrounded)
{
    isFlying = true;
}

// Soltar espaço = parar de voar
if (Input.GetKeyUp(KeyCode.Space))
{
    isFlying = false;
}

// Aplicar voo ou gravidade
if (isFlying)
{
    verticalVelocity = flySpeed;
}
else
{
    if (controller.isGrounded && verticalVelocity < 0)
        verticalVelocity = -1f;
    else
        verticalVelocity += gravity * Time.deltaTime;
}
```

![Voar](prints/gameplay2.png)

---

## 🛠️ Assets Utilizados

- [SimplePoly City - Low Poly Assets](https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899)
- [Ghost Character Free](https://assetstore.unity.com/packages/3d/characters/creatures/ghost-character-free-267003)