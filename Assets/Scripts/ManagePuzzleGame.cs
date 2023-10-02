using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ManagePuzzleGame : MonoBehaviour
{
    // Variáveis de embaralhamento
    float timer;
    float intervaloStart = 7;
    bool partesEmbaralhadas = false;

    // Variáveis de posição
    public Image parte;
    public Image localMarcado;
    float lmLargura, lmAltura;

    // Variáveis de vitória
    bool ganhou = false;
    int score = 0;
    float checkTimer;
    float intervaloCheck = 1;

    // Variáveis de derrota
    bool perdeu = false;
    public float gameOverTime;
    float gameOverTimer;

    public delegate void RunFunction();

    void chamaMenu() {
        SceneManager.LoadScene("MenuPrincipalCustom");
    }

    void criarLocaisMarcados() {
        lmLargura = 100;
        lmAltura = 100;

        float numLinhas = 5;
        float numColunas = 5;
        float linha, coluna;

        for (int i = 0; i < 25; i++) {
            Vector3 posicaoCentro = new Vector3();

            posicaoCentro = GameObject.Find("ladoDireito").transform.position;

            linha = i % 5;
            coluna = i / 5;

            Vector3 lmPosicao = 
                new Vector3(
                    posicaoCentro.x + lmLargura * (linha - numLinhas / 2),
                    posicaoCentro.y - lmAltura * (coluna - numColunas / 2),
                    posicaoCentro.z
                );

            Image lm = (Image)(Instantiate(localMarcado, lmPosicao, Quaternion.identity));

            lm.tag = "" + (i + 1);
            lm.name = "LM" + (i + 1);
            lm.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    public void criarPartes() {
        lmLargura = 100;
        lmAltura = 100;

        float numLinhas, numColunas;

        numLinhas = numColunas = 5;

        float linha, coluna;

        for (int i = 0; i < 25; i++) {
            Vector3 posicaoCentro = new Vector3();
            posicaoCentro = GameObject.Find("ladoEsquerdo").transform.position;

            linha = i % 5;
            coluna = i / 5;

            Image lm;

            Vector3 lmPosicao =
                new Vector3(
                    posicaoCentro.x + lmLargura * (linha - numLinhas / 2),
                    posicaoCentro.y - lmAltura * (coluna - numColunas / 2),
                    posicaoCentro.z
                );

            if (score == 0) {

                lm = (Image)(Instantiate(parte, lmPosicao, Quaternion.identity));

                lm.tag = "" + (i + 1);
                lm.name = "Parte" + (i + 1);
                lm.transform.SetParent(GameObject.Find("Canvas").transform);
            } else {
                lm = GameObject.Find("Parte" + (i + 1)).GetComponent<Image>();
                lm.transform.position = lmPosicao;
            }

            var source = "";

            if (score == 0) {
                source = "ring";
            } 
            
            if (score == 1) {
                source = "lion";
            }

            Sprite[] todasSprites = Resources.LoadAll<Sprite>(source);
            Sprite si = todasSprites[i];
            lm.GetComponent<Image>().sprite = si;
        }
    }

    void embaralhaPartes() {
        int[] novoArray = new int[25];

        for (int i = 0; i < 25; i++) {
            novoArray[i] = i;
        }

        int tmp;

        for (int i = 0; i < 25; i++) {
            tmp = novoArray[i];

            int r = Random.Range(i, 10);

            novoArray[i] = novoArray[r];
            novoArray[r] = tmp;
        }

        float linha, coluna, numLinhas, numColunas;

        numLinhas = numColunas = 5;

        for (int i = 0; i < 25; i++) {
            linha = (novoArray[i]) % 5;
            coluna = (novoArray[i]) / 5;

            Vector3 posicaoCentro = new Vector3();

            posicaoCentro = GameObject.Find("ladoEsquerdo").transform.position;

            var g = GameObject.Find("Parte" + (i + 1));

            Vector3 novaPosicao =
                new Vector3(
                    posicaoCentro.x + lmLargura * (linha - numLinhas / 2),
                    posicaoCentro.y - lmAltura * (coluna - numColunas / 2),
                    posicaoCentro.z
                );

            g.transform.position = novaPosicao;

            g.GetComponent<DragAndDrop>().posicaoInicialPartes();
        }
    }

    void checaPartes() {
            int contagem = 0;

            for (int i = 0; i < 25; i++) {
                var parte = GameObject.Find("Parte" + (i + 1));
                var lm = GameObject.Find("LM" + (i + 1));

                float distancia = Vector3.Distance(lm.transform.position, parte.transform.position);

                if (distancia == 0) {
                    contagem += 1;
                }
            }

            if (contagem == 25) {
                falaGanhou();
                score += 1;
                ganhou = true;

                if (score == 0) {
                    Invoke(nameof(setupJogo), 10);
                } else {
                    Invoke(nameof(chamaMenu), 10);
                }
            }
    }

    void falaInicial() {
        GameObject.Find("totemInicio").GetComponent<tocadorInicio>().playInicio();
    }

    void falaGanhou() {
        GameObject.Find("totemGanhou").GetComponent<tocadorGanhou>().playGanhou();
    }

    void falaPerdeu() {
        GameObject.Find("totemPerdeu").GetComponent<tocadorPerdeu>().playPerdeu();
    }

    void atualizarTimer() {
        var minutes = Mathf.FloorToInt(gameOverTimer / 60);
        var seconds = gameOverTimer % 60;

        var minutesString = minutes.ToString("00");
        var secondsString = seconds.ToString("00");
        var timerString = minutesString + ":" + secondsString;

        var textTimer = GameObject.Find("timer").GetComponent<TextMeshProUGUI>();
        textTimer.text = timerString;

        print(timerString);
    }

    void setupJogo() {
        partesEmbaralhadas = false;
        ganhou = false;
        perdeu = false;
        timer = 0;
        checkTimer = 0;
        gameOverTimer = gameOverTime;

        atualizarTimer();
        criarPartes();
        falaInicial();
    }

    // Start is called before the first frame update
    void Start()
    {
        criarLocaisMarcados();
        setupJogo();
    }

    // Update is called once per frame
    void Update()
    {
        if (perdeu) {
            return;
        }

        if (score == 2) {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= intervaloStart && !partesEmbaralhadas) {
            embaralhaPartes();
            partesEmbaralhadas = true;
        }


        if (partesEmbaralhadas) {
            checkTimer += Time.deltaTime;


            if (checkTimer >= intervaloCheck && !ganhou) {
                checaPartes();
                atualizarTimer();

                if (gameOverTimer == 0) {
                    falaPerdeu();
                    perdeu = true;
                    
                    Invoke(nameof(chamaMenu), 8);
                }
                
                checkTimer -= intervaloCheck;
                gameOverTimer -= 1;
            }
        }
    }
}
