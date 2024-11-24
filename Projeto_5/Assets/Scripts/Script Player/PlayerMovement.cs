using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referências Unity")]
    private Rigidbody2D rbd;
    private Animator anima;

    [Header("Movimento Horizontal")]
    public bool rightDirectionSprite = true;
    private float speedPlayerAtual;
    public float speedWalk;
    public float speedRun;
    private float horizontalMovement;

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
        // A variável horizontalMovement recebe o comando de Input do teclado: A (Esquerda) e D (Direita)
        horizontalMovement = Input.GetAxis("Horizontal");
        // O comando de velocidade do Rigidbody2D recebe a multiplicação do movimento horizontal com a velocidade definida,
        // controlando a velocidade no eixo X, enquanto a velocidade no eixo Y se mantém inalterada.
        rbd.linearVelocity = new Vector2(horizontalMovement * speedPlayerAtual, rbd.linearVelocity.y);
        // Chama o método para espelhar o sprite do Player, se necessário
        MirrorSprite();
    }

    // Método para espelhar o sprite do player
    public void MirrorSprite()
    {
        // Se horizontalMovement for maior que 0, significa que o movimento é para a direita
        if (horizontalMovement > 0)
        {
            // O Transform do Player, na sua escala, recebe valores positivos para X, Y, Z,
            // indicando que o sprite está voltado para a direita.
            transform.localScale = new Vector3(1f, 1f, 1f);
            // Define que o movimento para a direita é verdadeiro
            rightDirectionSprite = true;
            // Ativa a animação de andar
            anima.SetBool("walk", true);
        }
        else if (horizontalMovement < 0)
        {
            // Se horizontalMovement for menor que 0, significa que o movimento é para a esquerda
            /// O Transform do Player, na sua escala, recebe -1 no eixo X e 1 em Y e Z,
            // indicando que o sprite está voltado para a esquerda.
            transform.localScale = new Vector3(-1f, 1f, 1f);
            // Define que o movimento para a direita é falso
            rightDirectionSprite = false;
            // Ativa a animação de andar
            anima.SetBool("walk", true);
        }else if (horizontalMovement == 0)
        {
            // Se horizontalMovement for igual a 0, significa que o Player está parado
            // Desativa a animação de andar
            anima.SetBool("walk", false);
            // Desativa a animação de run
            anima.SetBool("run", false);
        }
    }

}
