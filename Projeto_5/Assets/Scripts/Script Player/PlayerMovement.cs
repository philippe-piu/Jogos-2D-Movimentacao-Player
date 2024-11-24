using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refer�ncias Unity")]
    private Rigidbody2D rbd;
    private Animator anima;

    [Header("Movimento Horizontal")]
    public bool rightDirectionSprite = true;
    private float speedPlayerAtual;
    public float speedWalk;
    public float speedRun;
    private float horizontalMovement;

    [Header("Jump")]
    public bool isGround = true;
    public float jumpForce;
    public Transform ground;
    public LayerMask groundMask;

    public void Awake()
    {
        rbd = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        
    }
    
    void Start()
    {
        speedPlayerAtual = speedWalk;
    }
        
    public void Movement()
    {
        // A vari�vel horizontalMovement recebe o comando de Input do teclado: A (Esquerda) e D (Direita)
        horizontalMovement = Input.GetAxis("Horizontal");
        // O comando de velocidade do Rigidbody2D recebe a multiplica��o do movimento horizontal com a velocidade definida,
        // controlando a velocidade no eixo X, enquanto a velocidade no eixo Y se mant�m inalterada.
        rbd.linearVelocity = new Vector2(horizontalMovement * speedPlayerAtual, rbd.linearVelocity.y);
        // Chama o m�todo para espelhar o sprite do Player, se necess�rio
        MirrorSprite();
    }

    // M�todo para espelhar o sprite do player
    public void MirrorSprite()
    {
        // Se horizontalMovement for maior que 0, significa que o movimento � para a direita
        if (horizontalMovement > 0)
        {
            // O Transform do Player, na sua escala, recebe valores positivos para X, Y, Z,
            // indicando que o sprite est� voltado para a direita.
            transform.localScale = new Vector3(1f, 1f, 1f);
            // Define que o movimento para a direita � verdadeiro
            rightDirectionSprite = true;
            // Ativa a anima��o de andar
            anima.SetBool("walk", true);
        }
        else if (horizontalMovement < 0)
        {
            // Se horizontalMovement for menor que 0, significa que o movimento � para a esquerda
            /// O Transform do Player, na sua escala, recebe -1 no eixo X e 1 em Y e Z,
            // indicando que o sprite est� voltado para a esquerda.
            transform.localScale = new Vector3(-1f, 1f, 1f);
            // Define que o movimento para a direita � falso
            rightDirectionSprite = false;
            // Ativa a anima��o de andar
            anima.SetBool("walk", true);
        }else if (horizontalMovement == 0)
        {
            // Se horizontalMovement for igual a 0, significa que o Player est� parado
            // Desativa a anima��o de andar
            anima.SetBool("walk", false);
            // Desativa a anima��o de run
            anima.SetBool("run", false);
        }
    }

    public void Run()
    {
        // Verifica se o personagem est� no ch�o usando um OverlapCapsule
        // O capsule � definido pela posi��o do objeto 'ground', com um tamanho de 0.79f na largura e 0.15f na altura
        // 'groundMask' � a camada que representa o ch�o
        isGround = Physics2D.OverlapCapsule(ground.position, new Vector2(0.79f, 0.15f), CapsuleDirection2D.Horizontal, 0, groundMask);
        // Verifica se h� movimento horizontal, se a tecla de correr est� pressionada e se o personagem est� no ch�o
        if (horizontalMovement != 0 && Input.GetKey(KeyCode.F) && isGround) {
            // Define a velocidade atual para a velocidade de corrida
            speedPlayerAtual = speedRun;
            // Ativa a anima��o de correr
            anima.SetBool("run", true);
        }
        else
        {
            // Se n�o estiver correndo, define a velocidade atual para a velocidade de caminhada
            speedPlayerAtual = speedWalk;
            // Desativa a anima��o de correr
            anima.SetBool("run", false);
        }
    }

    //M�todo de Pulo do Jogador
    public void Jump()
    {
        // Verifica se o personagem est� no ch�o usando um OverlapCapsule
        // O capsule � definido pela posi��o do objeto 'ground', com um tamanho de 0.79f na largura e 0.15f na altura
        // 'groundMask' � a camada que representa o ch�o
        isGround = Physics2D.OverlapCapsule(ground.position, new Vector2(0.79f, 0.15f), CapsuleDirection2D.Horizontal, 0, groundMask);

        // Verifica se o personagem est� no ch�o
        if (isGround) {
            // Se o bot�o de pulo for pressionado
            if (Input.GetButtonDown("Jump"))
            {
                // Aplica uma for�a de impulso na dire��o vertical para o pulo
                rbd.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                // Define isGround como falso para evitar m�ltiplos pulos enquanto no ar
                isGround = false;
                // Ativa a anima��o de salto
                anima.SetBool("jump", true);
            }
        }

        // Se o Jogador estiver no ch�o e sua velocidade no eixo Y for menor ou igual a 0.1f
        // Isso indica que o jogador est� praticamente parado no eixo Y e, portanto, desativa a anima��o de salto
        if (isGround && rbd.linearVelocity.y <= 0.1f){
            anima.SetBool("jump", false);
        }
    }

}
