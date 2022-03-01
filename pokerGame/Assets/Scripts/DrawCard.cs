using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : MonoBehaviour
{
    int round = 0;
    public GameObject TwoH;
    public GameObject TwoD;
    public GameObject TwoC;
    public GameObject TwoS;
    public GameObject ThreeH;
    public GameObject ThreeD;
    public GameObject ThreeC;
    public GameObject ThreeS;
    public GameObject FourH;
    public GameObject FourD;
    public GameObject FourC;
    public GameObject FourS;
    public GameObject FiveH;
    public GameObject FiveD;
    public GameObject FiveC;
    public GameObject FiveS;
    public GameObject SixH;
    public GameObject SixD;
    public GameObject SixC;
    public GameObject SixS;
    public GameObject SevenH;
    public GameObject SevenD;
    public GameObject SevenC;
    public GameObject SevenS;
    public GameObject EightH;
    public GameObject EightD;
    public GameObject EightC;
    public GameObject EightS;
    public GameObject NineH;
    public GameObject NineD;
    public GameObject NineC;
    public GameObject NineS;
    public GameObject TenH;
    public GameObject TenD;
    public GameObject TenC;
    public GameObject TenS;
    public GameObject JH;
    public GameObject JD;
    public GameObject JC;
    public GameObject JS;
    public GameObject QH;
    public GameObject QD;
    public GameObject QC;
    public GameObject QS;
    public GameObject KH;
    public GameObject KD;
    public GameObject KC;
    public GameObject KS;
    public GameObject AH;
    public GameObject AD;
    public GameObject AC;
    public GameObject AS;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject MiddleArea;

    List <GameObject> cards = new List<GameObject>();

    void Start()
    {
         makeDeck();
    }

    public void makeDeck(){
        cards.Add(TwoH);
        cards.Add(TwoD);
        cards.Add(TwoC);
        cards.Add(TwoS);
        cards.Add(ThreeH);
        cards.Add(ThreeD);
        cards.Add(ThreeC);
        cards.Add(ThreeS);
        cards.Add(FourH);
        cards.Add(FourD);
        cards.Add(FourC);
        cards.Add(FourS);
        cards.Add(FiveH);
        cards.Add(FiveD);
        cards.Add(FiveC);
        cards.Add(FiveS);
        cards.Add(SixH);
        cards.Add(SixD);
        cards.Add(SixC);
        cards.Add(SixS);
        cards.Add(SevenH);
        cards.Add(SevenD);
        cards.Add(SevenC);
        cards.Add(SevenS);
        cards.Add(EightH);
        cards.Add(EightD);
        cards.Add(EightC);
        cards.Add(EightS);
        cards.Add(NineH);
        cards.Add(NineD);
        cards.Add(NineC);
        cards.Add(NineS);
        cards.Add(TenH);
        cards.Add(TenD);
        cards.Add(TenC);
        cards.Add(TenS);
        cards.Add(JH);
        cards.Add(JD);
        cards.Add(JC);
        cards.Add(JS);
        cards.Add(QH);
        cards.Add(QD);
        cards.Add(QC);
        cards.Add(QS);
        cards.Add(KH);
        cards.Add(KD);
        cards.Add(KC);
        cards.Add(KS);
        cards.Add(AH);
        cards.Add(AD);
        cards.Add(AC);
        cards.Add(AS);
    }

    public void nextPhase(){
        if(round == 0){
            dealCards();
        }else if(round == 1){
            flop();
        }else if(round == 2 || round == 3){
            riverAndTurn();
        }else{
            return;
        }
        round += 1;
    }

    public void dealCards(){
        for (var i = 0; i < 2; ++i){
            GameObject playerCard = Instantiate(getRandomCard(), new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerArea.transform, false);

            GameObject enemyCard = Instantiate(getRandomCard(), new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard.transform.SetParent(EnemyArea.transform, false);
        }
    }

    public void flop(){
        for (var i = 0; i < 3; ++i){
            GameObject middleCard = Instantiate(getRandomCard(), new Vector3(0, 0, 0), Quaternion.identity);
            middleCard.transform.SetParent(MiddleArea.transform, false);
        }
    }

    public void riverAndTurn(){
        GameObject middleCard = Instantiate(getRandomCard(), new Vector3(0, 0, 0), Quaternion.identity);
        middleCard.transform.SetParent(MiddleArea.transform, false);
    }

    public GameObject getRandomCard(){
        int randomNumber = Random.Range(0, cards.Count-1);
        GameObject randomCard = cards[randomNumber];
        cards.RemoveAt(randomNumber);

        return randomCard;
    }
}
